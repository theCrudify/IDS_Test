// FILE LOCATION: src/PO.Shared/DTOs/MasterData/CreateDepartmentDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class CreateDepartmentDto
{
    [Required]
    [MaxLength(10)]
    public string DeptCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string DeptName { get; set; } = string.Empty;
}
