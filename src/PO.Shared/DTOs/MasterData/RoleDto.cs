using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

/// <summary>
/// Role Data Transfer Object
/// </summary>
public class RoleDto
{
    public int Id { get; set; }
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Create Role Data Transfer Object
/// </summary>
public class CreateRoleDto
{
    [Required]
    [StringLength(20)]
    public string RoleCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string RoleName { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Update Role Data Transfer Object
/// </summary>
public class UpdateRoleDto
{
    [Required]
    [StringLength(100)]
    public string RoleName { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }
}

/// <summary>
/// Role Lookup Data Transfer Object for dropdowns
/// </summary>
public class RoleLookupDto
{
    public int Id { get; set; }
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}