// FILE LOCATION: src/PO.Shared/DTOs/MasterData/CreateDivisionDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class CreateDivisionDto
{
    [Required]
    [MaxLength(10)]
    public string DivisionCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string DivisionName { get; set; } = string.Empty;
}
