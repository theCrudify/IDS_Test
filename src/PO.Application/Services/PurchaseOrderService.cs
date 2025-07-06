using AutoMapper;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.PurchaseOrder;
using PO.Domain.Enums;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.PurchaseOrder;

namespace PO.Application.Services;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IGenericRepository<POHeader> _poHeaderRepository;
    private readonly IGenericRepository<PODetail> _poDetailRepository;
    private readonly IGenericRepository<POApprovalHistory> _poApprovalHistoryRepository;
    private readonly IGenericRepository<ApprovalMatrix> _approvalMatrixRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public PurchaseOrderService(
        IGenericRepository<POHeader> poHeaderRepository,
        IGenericRepository<PODetail> poDetailRepository,
        IGenericRepository<POApprovalHistory> poApprovalHistoryRepository,
        IGenericRepository<ApprovalMatrix> approvalMatrixRepository,
        IGenericRepository<User> userRepository,
        IMapper mapper,
        INotificationService notificationService)
    {
        _poHeaderRepository = poHeaderRepository;
        _poDetailRepository = poDetailRepository;
        _poApprovalHistoryRepository = poApprovalHistoryRepository;
        _approvalMatrixRepository = approvalMatrixRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<ApiResult<POHeaderDto>> CreatePOAsync(CreatePOHeaderDto createDto)
    {
        var poHeader = _mapper.Map<POHeader>(createDto);
        poHeader.Status = POStatus.Draft; // Initial status

        await _poHeaderRepository.AddAsync(poHeader);
        await _poHeaderRepository.SaveChangesAsync();

        // Handle PO Details
        foreach (var detailDto in createDto.PODetails)
        {
            var poDetail = _mapper.Map<PODetail>(detailDto);
            poDetail.POId = poHeader.Id;
            await _poDetailRepository.AddAsync(poDetail);
        }
        await _poDetailRepository.SaveChangesAsync();

        // Notify PO creator
        await _notificationService.CreateNotificationAsync(
            poHeader.CreatedById,
            NotificationType.POSubmitted,
            $"Your Purchase Order {poHeader.PONumber} has been created.",
            poHeader.Id
        );

        var poHeaderDto = _mapper.Map<POHeaderDto>(poHeader);
        return ApiResult<POHeaderDto>.SuccessResult(poHeaderDto, "Purchase Order created successfully.");
    }

    public async Task<ApiResult<POHeaderDto>> GetPOByIdAsync(int id)
    {
        var poHeader = await _poHeaderRepository.GetByIdAsync(id);
        if (poHeader == null)
        {
            return ApiResult<POHeaderDto>.ErrorResult("Purchase Order not found.");
        }

        var poHeaderDto = _mapper.Map<POHeaderDto>(poHeader);
        return ApiResult<POHeaderDto>.SuccessResult(poHeaderDto);
    }

    public async Task<ApiResult<POHeaderDto>> UpdatePOAsync(int id, UpdatePOHeaderDto updateDto)
    {
        var poHeader = await _poHeaderRepository.GetByIdAsync(id);
        if (poHeader == null)
        {
            return ApiResult<POHeaderDto>.ErrorResult("Purchase Order not found.");
        }

        // Only allow updates if PO is in Draft or ReopenedForCorrection status
        if (poHeader.Status != POStatus.Draft && poHeader.Status != POStatus.ReopenedForCorrection)
        {
            return ApiResult<POHeaderDto>.ErrorResult("Cannot update Purchase Order in its current status.");
        }

        _mapper.Map(updateDto, poHeader);
        await _poHeaderRepository.UpdateAsync(poHeader);
        await _poHeaderRepository.SaveChangesAsync();

        // TODO: Handle PODetails update (add, remove, modify existing)
        // This will require more complex logic to compare existing details with new ones

        var poHeaderDto = _mapper.Map<POHeaderDto>(poHeader);
        return ApiResult<POHeaderDto>.SuccessResult(poHeaderDto, "Purchase Order updated successfully.");
    }

    public async Task<ApiResult<bool>> SubmitPOForApprovalAsync(int poId)
    {
        var poHeader = await _poHeaderRepository.GetByIdAsync(poId);
        if (poHeader == null)
        {
            return ApiResult<bool>.ErrorResult("Purchase Order not found.");
        }

        // Only allow submission if PO is in Draft or ReopenedForCorrection status
        if (poHeader.Status != POStatus.Draft && poHeader.Status != POStatus.ReopenedForCorrection)
        {
            return ApiResult<bool>.ErrorResult("Purchase Order cannot be submitted in its current status.");
        }

        poHeader.Status = POStatus.PendingApprovalLevel1; // Set to first approval level
        await _poHeaderRepository.UpdateAsync(poHeader);
        await _poHeaderRepository.SaveChangesAsync();

        // Create POApprovalHistory entry for submission
        var approvalHistory = new POApprovalHistory
        {
            POId = poId,
            ApprovalLevel = ApprovalLevel.None, // Or a specific 'Submitted' level if defined
            ApprovalStatus = ApprovalStatus.Pending,
            ApproverId = poHeader.CreatedById, // Creator is the one submitting
            ApprovalDate = DateTime.Now,
            Comments = "Purchase Order submitted for approval."
        };
        await _poApprovalHistoryRepository.AddAsync(approvalHistory);
        await _poApprovalHistoryRepository.SaveChangesAsync();

        // Find the first approver based on ApprovalMatrix
        var approvalMatrix = await _approvalMatrixRepository.GetAllAsync(
            am => am.DeptId == poHeader.DeptId &&
                  poHeader.TotalDue >= am.MinAmount &&
                  poHeader.TotalDue <= am.MaxAmount &&
                  am.IsActive
        );
        var applicableMatrix = approvalMatrix.FirstOrDefault();

        if (applicableMatrix != null)
        {
            // Get users with the Checker role (first approval level)
            var checkerUsers = await _userRepository.GetAllAsync(u => u.RoleId == applicableMatrix.CheckerRoleId);
            foreach (var checkerUser in checkerUsers)
            {
                await _notificationService.CreateNotificationAsync(
                    checkerUser.Id,
                    NotificationType.POReadyCheck,
                    $"Purchase Order {poHeader.PONumber} is pending your review.",
                    poHeader.Id,
                    NotificationPriority.High
                );
            }
        }
        else
        {
            // If no approval matrix found, notify creator that PO is submitted but no approver found
            await _notificationService.CreateNotificationAsync(
                poHeader.CreatedById,
                NotificationType.POSubmitted,
                $"Purchase Order {poHeader.PONumber} has been submitted, but no approver was found.",
                poHeader.Id,
                NotificationPriority.Medium
            );
        }

        return ApiResult<bool>.SuccessResult(true, "Purchase Order submitted for approval.");
    }
}
