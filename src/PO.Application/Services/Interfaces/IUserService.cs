// FILE LOCATION: src/PO.Application/Services/Interfaces/IUserService.cs

using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

public interface IUserService
{
    Task<ApiResult<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<ApiResult<PagedResult<UserDto>>> GetPagedUsersAsync(PagedRequest request);
    Task<ApiResult<UserDto>> GetUserByIdAsync(int id);
    Task<ApiResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
    Task<ApiResult<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task<ApiResult<bool>> DeleteUserAsync(int id);
    Task<ApiResult<IEnumerable<UserLookupDto>>> GetUsersForLookupAsync();
}
