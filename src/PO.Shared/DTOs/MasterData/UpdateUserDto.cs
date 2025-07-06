// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UpdateUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class UpdateUserDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    public int DeptId { get; set; }

    public int? ManagerId { get; set; }
    
    public bool IsActive { get; set; }
}
