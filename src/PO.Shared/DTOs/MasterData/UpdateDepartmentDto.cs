// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UpdateDepartmentDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class UpdateDepartmentDto
{
    [Required]
    [MaxLength(100)]
    public string DeptName { get; set; } = string.Empty;

    public int? DeptHeadId { get; set; }
    
    public bool IsActive { get; set; }
}
