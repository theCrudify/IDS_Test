using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

/// <summary>
/// User Data Transfer Object
/// </summary>
public class UserDto
{
    public int Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public decimal ApprovalLimit { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Create User Data Transfer Object
/// </summary>
public class CreateUserDto
{
    [Required]
    [StringLength(20)]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ApprovalLimit { get; set; } = 0;

    [Required]
    public int RoleId { get; set; }

    public int? DepartmentId { get; set; }

    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Update User Data Transfer Object
/// </summary>
public class UpdateUserDto
{
    [Required]
    [StringLength(255)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ApprovalLimit { get; set; }

    [Required]
    public int RoleId { get; set; }

    public int? DepartmentId { get; set; }

    public bool IsActive { get; set; }
}

/// <summary>
/// User Lookup Data Transfer Object for dropdowns
/// </summary>
public class UserLookupDto
{
    public int Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}