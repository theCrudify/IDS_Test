// FILE LOCATION: src/PO.Shared/DTOs/MasterData/DepartmentDto.cs
// DESCRIPTION: Data Transfer Objects for Department entity

using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

/// <summary>
/// DTO for reading Department data
/// </summary>
public class DepartmentDto
{
    public int Id { get; set; }
    public string DeptCode { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public int? DeptHeadId { get; set; }
    public string? DeptHeadName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// DTO for creating a new Department
/// </summary>
public class CreateDepartmentDto
{
    [Required(ErrorMessage = "Department code is required")]
    [StringLength(20, ErrorMessage = "Department code cannot exceed 20 characters")]
    public string DeptCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department name is required")]
    [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
    public string DeptName { get; set; } = string.Empty;

    public int? DeptHeadId { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// DTO for updating an existing Department
/// </summary>
public class UpdateDepartmentDto
{
    [Required(ErrorMessage = "Department code is required")]
    [StringLength(20, ErrorMessage = "Department code cannot exceed 20 characters")]
    public string DeptCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department name is required")]
    [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
    public string DeptName { get; set; } = string.Empty;

    public int? DeptHeadId { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// Lightweight DTO for dropdown/selection purposes
/// </summary>
public class DepartmentLookupDto
{
    public int Id { get; set; }
    public string DeptCode { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
}