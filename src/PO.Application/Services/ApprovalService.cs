using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.PurchaseOrder;
using PO.Domain.Enums;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.PurchaseOrder;

namespace PO.Application.Services;

public class ApprovalService : IApprovalService
{
    private readonly IGenericRepository<POHeader> _poHeaderRepository;
    private readonly IGenericRepository<POApprovalHistory> _poApprovalHistoryRepository;
    private readonly IGenericRepository<ApprovalMatrix> _approvalMatrixRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public ApprovalService(
        IGenericRepository<POHeader> poHeaderRepository,
        IGenericRepository<POApprovalHistory> poApprovalHistoryRepository,
        IGenericRepository<ApprovalMatrix> approvalMatrixRepository,
        IGenericRepository<User> userRepository,
        IMapper mapper,
        INotificationService notificationService)
    {
        _poHeaderRepository = poHeaderRepository;
        _poApprovalHistoryRepository = poApprovalHistoryRepository;
        _approvalMatrixRepository = approvalMatrixRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<ApiResult<bool>> ApprovePOAsync(int poId, int userId, string? notes)
    {
        var poHeader = await _poHeaderRepository.GetByIdAsync(poId);
        if (poHeader == null)
        {
            return ApiResult<bool>.ErrorResult("Purchase Order not found.");
        }

        var currentUser = await _userRepository.GetByIdAsync(userId);
        if (currentUser == null)
        {
            return ApiResult<bool>.ErrorResult("Approver user not found.");
        }

        // Determine current approval level based on PO status
        ApprovalLevel currentLevel;
        if (poHeader.Status == POStatus.PendingApprovalLevel1)
            currentLevel = ApprovalLevel.Checker;
        else if (poHeader.Status == POStatus.PendingApprovalLevel2)
            currentLevel = ApprovalLevel.Acknowledge;
        else if (poHeader.Status == POStatus.PendingApprovalLevel3)
            currentLevel = ApprovalLevel.Approver;
        else
            return ApiResult<bool>.ErrorResult("Purchase Order is not in a pending approval status.");

        // Check if the current user is authorized to approve at this level
        var approvalMatrix = await _approvalMatrixRepository.GetAllAsync(
            am => am.DeptId == poHeader.DeptId &&
                  poHeader.TotalDue >= am.MinAmount &&
                  poHeader.TotalDue <= am.MaxAmount &&
                  am.IsActive,
            include: source => source.Include(am => am.CheckerRole).Include(am => am.AcknowledgeRole).Include(am => am.ApproverRole)
        );

        var applicableMatrix = approvalMatrix.FirstOrDefault();

        if (applicableMatrix == null)
        {
            return ApiResult<bool>.ErrorResult("No applicable approval matrix found for this Purchase Order.");
        }

        bool authorized = false;
        if (currentLevel == ApprovalLevel.Checker && currentUser.RoleId == applicableMatrix.CheckerRoleId)
            authorized = true;
        else if (currentLevel == ApprovalLevel.Acknowledge && currentUser.RoleId == applicableMatrix.AcknowledgeRoleId)
            authorized = true;
        else if (currentLevel == ApprovalLevel.Approver && currentUser.RoleId == applicableMatrix.ApproverRoleId)
            authorized = true;

        if (!authorized)
        {
            return ApiResult<bool>.ErrorResult("User is not authorized to approve at this level.");
        }

        // Record approval history
        var approvalHistory = new POApprovalHistory
        {
            POId = poId,
            ApprovalLevel = currentLevel,
            ApprovalStatus = ApprovalStatus.Approved,
            ApproverId = userId,
            ApprovalDate = DateTime.Now,
            Comments = notes
        };
        await _poApprovalHistoryRepository.AddAsync(approvalHistory);

        // Determine next status
        POStatus nextStatus;
        if (currentLevel == ApprovalLevel.Checker && applicableMatrix.AcknowledgeRoleId != 0) // Assuming 0 means no next level
        {
            nextStatus = POStatus.PendingApprovalLevel2;
        }
        else if (currentLevel == ApprovalLevel.Acknowledge && applicableMatrix.ApproverRoleId != 0)
        {
            nextStatus = POStatus.PendingApprovalLevel3;
        }
        else
        {
            nextStatus = POStatus.Approved; // Final approval
        }

        poHeader.Status = nextStatus;
        poHeader.ApprovalNotes = notes;
        if (nextStatus == POStatus.Approved)
        {
            poHeader.ApprovedDate = DateTime.Now;
        }
        await _poHeaderRepository.UpdateAsync(poHeader);

        await _poHeaderRepository.SaveChangesAsync();
        await _poApprovalHistoryRepository.SaveChangesAsync();

        // Trigger notification
        if (nextStatus == POStatus.Approved)
        {
            // Notify PO creator that it's fully approved
            var poCreator = await _userRepository.GetByIdAsync(poHeader.CreatedById);
            if (poCreator != null)
            {
                await _notificationService.CreateNotificationAsync(
                    poCreator.Id,
                    NotificationType.POApproved,
                    $"Your Purchase Order {poHeader.PONumber} has been fully approved.",
                    poHeader.Id,
                    NotificationPriority.High
                );
            }
        }
        else
        {
            // Notify next approver
            int nextApproverRoleId = 0;
            NotificationType notificationType = NotificationType.POReadyCheck;

            if (nextStatus == POStatus.PendingApprovalLevel2)
            {
                nextApproverRoleId = applicableMatrix.AcknowledgeRoleId;
                notificationType = NotificationType.POReadyAcknowledge;
            }
            else if (nextStatus == POStatus.PendingApprovalLevel3)
            {
                nextApproverRoleId = applicableMatrix.ApproverRoleId;
                notificationType = NotificationType.POReadyApprove;
            }

            if (nextApproverRoleId != 0)
            {
                var nextApprovers = await _userRepository.GetAllAsync(u => u.RoleId == nextApproverRoleId);
                foreach (var approver in nextApprovers)
                {
                    await _notificationService.CreateNotificationAsync(
                        approver.Id,
                        notificationType,
                        $"Purchase Order {poHeader.PONumber} is pending your review.",
                        poHeader.Id,
                        NotificationPriority.High
                    );
                }
            }
        }

        return ApiResult<bool>.SuccessResult(true, $"Purchase Order approved at {currentLevel} level. Next status: {nextStatus}.");
    }

    public async Task<ApiResult<bool>> RejectPOAsync(int poId, int userId, string reason)
    {
        var poHeader = await _poHeaderRepository.GetByIdAsync(poId);
        if (poHeader == null)
        {
            return ApiResult<bool>.ErrorResult("Purchase Order not found.");
        }

        var currentUser = await _userRepository.GetByIdAsync(userId);
        if (currentUser == null)
        {
            return ApiResult<bool>.ErrorResult("Approver user not found.");
        }

        // Determine current approval level based on PO status
        ApprovalLevel currentLevel;
        if (poHeader.Status == POStatus.PendingApprovalLevel1)
            currentLevel = ApprovalLevel.Checker;
        else if (poHeader.Status == POStatus.PendingApprovalLevel2)
            currentLevel = ApprovalLevel.Acknowledge;
        else if (poHeader.Status == POStatus.PendingApprovalLevel3)
            currentLevel = ApprovalLevel.Approver;
        else
            return ApiResult<bool>.ErrorResult("Purchase Order is not in a pending approval status.");

        // Check if the current user is authorized to reject at this level (similar to approve logic)
        var approvalMatrix = await _approvalMatrixRepository.GetAllAsync(
            am => am.DeptId == poHeader.DeptId &&
                  poHeader.TotalDue >= am.MinAmount &&
                  poHeader.TotalDue <= am.MaxAmount &&
                  am.IsActive,
            include: source => source.Include(am => am.CheckerRole).Include(am => am.AcknowledgeRole).Include(am => am.ApproverRole)
        );

        var applicableMatrix = approvalMatrix.FirstOrDefault();

        if (applicableMatrix == null)
        {
            return ApiResult<bool>.ErrorResult("No applicable approval matrix found for this Purchase Order.");
        }

        bool authorized = false;
        if (currentLevel == ApprovalLevel.Checker && currentUser.RoleId == applicableMatrix.CheckerRoleId)
            authorized = true;
        else if (currentLevel == ApprovalLevel.Acknowledge && currentUser.RoleId == applicableMatrix.AcknowledgeRoleId)
            authorized = true;
        else if (currentLevel == ApprovalLevel.Approver && currentUser.RoleId == applicableMatrix.ApproverRoleId)
            authorized = true;

        if (!authorized)
        {
            return ApiResult<bool>.ErrorResult("User is not authorized to reject at this level.");
        }

        // Record rejection history
        var approvalHistory = new POApprovalHistory
        {
            POId = poId,
            ApprovalLevel = currentLevel,
            ApprovalStatus = ApprovalStatus.Rejected,
            ApproverId = userId,
            ApprovalDate = DateTime.Now,
            Comments = reason,
            RejectionReason = reason
        };
        await _poApprovalHistoryRepository.AddAsync(approvalHistory);

        // Set PO status to Rejected and then ReopenedForCorrection
        poHeader.Status = POStatus.Rejected;
        poHeader.RejectedDate = DateTime.Now;
        poHeader.RejectionReason = reason;
        await _poHeaderRepository.UpdateAsync(poHeader);

        // Save changes before setting to ReopenedForCorrection to ensure history is recorded
        await _poHeaderRepository.SaveChangesAsync();
        await _poApprovalHistoryRepository.SaveChangesAsync();

        // Set status to ReopenedForCorrection to allow creator to edit and resubmit
        poHeader.Status = POStatus.ReopenedForCorrection;
        await _poHeaderRepository.UpdateAsync(poHeader);
        await _poHeaderRepository.SaveChangesAsync();

        // TODO: Trigger notification to PO creator about rejection
        var poCreator = await _userRepository.GetByIdAsync(poHeader.CreatedById);
        if (poCreator != null)
        {
            await _notificationService.CreateNotificationAsync(
                poCreator.Id,
                NotificationType.PORejected,
                $"Your Purchase Order {poHeader.PONumber} has been rejected. Reason: {reason}. It is now reopened for correction.",
                poHeader.Id,
                NotificationPriority.High
            );
        }

        return ApiResult<bool>.SuccessResult(true, "Purchase Order rejected and set for correction.");
    }

    public async Task<ApiResult<List<POHeaderDto>>> GetPendingApprovalsForUserAsync(int userId)
    {
        var currentUser = await _userRepository.GetByIdAsync(userId);
        if (currentUser == null)
        {
            return ApiResult<List<POHeaderDto>>.ErrorResult("User not found.");
        }

        // Find POs where the current user's role matches the required role for the current approval level
        var pendingPOs = await _poHeaderRepository.GetAllAsync(
            po => (po.Status == POStatus.PendingApprovalLevel1 ||
                   po.Status == POStatus.PendingApprovalLevel2 ||
                   po.Status == POStatus.PendingApprovalLevel3),
            include: source => source.Include(po => po.PODetails)
                                      .Include(po => po.Vendor)
                                      .Include(po => po.Department)
        );

        var userPendingPOs = new List<POHeader>();

        foreach (var po in pendingPOs)
        {
            var approvalMatrix = await _approvalMatrixRepository.GetAllAsync(
                am => am.DeptId == po.DeptId &&
                      po.TotalDue >= am.MinAmount &&
                      po.TotalDue <= am.MaxAmount &&
                      am.IsActive,
                include: source => source.Include(am => am.CheckerRole).Include(am => am.AcknowledgeRole).Include(am => am.ApproverRole)
            );
            var applicableMatrix = approvalMatrix.FirstOrDefault();

            if (applicableMatrix == null) continue; // No matrix, skip

            bool isApproverForCurrentLevel = false;
            if (po.Status == POStatus.PendingApprovalLevel1 && currentUser.RoleId == applicableMatrix.CheckerRoleId)
                isApproverForCurrentLevel = true;
            else if (po.Status == POStatus.PendingApprovalLevel2 && currentUser.RoleId == applicableMatrix.AcknowledgeRoleId)
                isApproverForCurrentLevel = true;
            else if (po.Status == POStatus.PendingApprovalLevel3 && currentUser.RoleId == applicableMatrix.ApproverRoleId)
                isApproverForCurrentLevel = true;

            if (isApproverForCurrentLevel)
            {
                userPendingPOs.Add(po);
            }
        }

        var pendingPODtos = _mapper.Map<List<POHeaderDto>>(userPendingPOs);
        return ApiResult<List<POHeaderDto>>.SuccessResult(pendingPODtos);
    }
}
