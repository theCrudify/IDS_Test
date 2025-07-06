// FILE LOCATION: src/PO.Application/Services/UserService.cs

using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;
using System.Linq.Expressions;

namespace PO.Application.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IGenericRepository<User> userRepository,
        IMapper mapper,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<UserDto>>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetWithIncludesAsync(u => u.Role, u => u.Department, u => u.Manager);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return ApiResult<IEnumerable<UserDto>>.SuccessResult(userDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all users");
            return ApiResult<IEnumerable<UserDto>>.ErrorResult("Failed to retrieve users", 500);
        }
    }

    public async Task<ApiResult<PagedResult<UserDto>>> GetPagedUsersAsync(PagedRequest request)
    {
        try
        {
            Expression<Func<User, bool>> predicate = u => 
                string.IsNullOrEmpty(request.SearchTerm) || 
                u.FullName.Contains(request.SearchTerm) || 
                u.EmployeeCode.Contains(request.SearchTerm) ||
                u.Email.Contains(request.SearchTerm);

            var pagedUsers = await _userRepository.GetPagedWithIncludesAsync(
                request,
                u => u.Role, u => u.Department, u => u.Manager
            );

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(pagedUsers.Items);

            var result = new PagedResult<UserDto>
            {
                Items = userDtos,
                TotalCount = pagedUsers.TotalCount,
                PageNumber = pagedUsers.PageNumber,
                PageSize = pagedUsers.PageSize
            };

            return ApiResult<PagedResult<UserDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged users");
            return ApiResult<PagedResult<UserDto>>.ErrorResult("Failed to retrieve paged users", 500);
        }
    }

    public async Task<ApiResult<UserDto>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = (await _userRepository.GetWithIncludesAsync(u => u.Role, u => u.Department, u => u.Manager)).FirstOrDefault(u => u.Id == id);
            if (user == null)
                return ApiResult<UserDto>.NotFoundResult("User not found");

            var userDto = _mapper.Map<UserDto>(user);
            return ApiResult<UserDto>.SuccessResult(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user by id {UserId}", id);
            return ApiResult<UserDto>.ErrorResult("Failed to retrieve user", 500);
        }
    }

    public async Task<ApiResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        try
        {
            var existingUser = await _userRepository.FindAsync(u => u.EmployeeCode == createUserDto.EmployeeCode || u.Email == createUserDto.Email);
            if (existingUser.Any())
                return ApiResult<UserDto>.ErrorResult("User with the same Employee Code or Email already exists", 409);

            var user = _mapper.Map<User>(createUserDto);
            user.CreatedAt = DateTime.UtcNow;

            var createdUser = await _userRepository.AddAsync(user);
            var userDto = _mapper.Map<UserDto>(createdUser);

            return ApiResult<UserDto>.SuccessResult(userDto, "User created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return ApiResult<UserDto>.ErrorResult("Failed to create user", 500);
        }
    }

    public async Task<ApiResult<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return ApiResult<UserDto>.NotFoundResult("User not found");

            _mapper.Map(updateUserDto, user);
            user.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateAsync(user);
            var userDto = _mapper.Map<UserDto>(updatedUser);

            return ApiResult<UserDto>.SuccessResult(userDto, "User updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {UserId}", id);
            return ApiResult<UserDto>.ErrorResult("Failed to update user", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return ApiResult<bool>.NotFoundResult("User not found");

            await _userRepository.DeleteAsync(user);
            return ApiResult<bool>.SuccessResult(true, "User deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {UserId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete user", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<UserLookupDto>>> GetUsersForLookupAsync()
    {
        try
        {
            var users = await _userRepository.FindAsync(u => u.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<UserLookupDto>>(users);
            return ApiResult<IEnumerable<UserLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting users for lookup");
            return ApiResult<IEnumerable<UserLookupDto>>.ErrorResult("Failed to retrieve users for lookup", 500);
        }
    }
}
