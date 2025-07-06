// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UpdateDivisionDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class UpdateDivisionDto
{
    [Required]
    [MaxLength(100)]
    public string DivisionName { get; set; } = string.Empty;
    
    public bool IsActive { get; set; }
}
