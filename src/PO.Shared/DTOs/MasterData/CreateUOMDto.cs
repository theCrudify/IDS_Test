// FILE LOCATION: src/PO.Shared/DTOs/MasterData/CreateUOMDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class CreateUOMDto
{
    [Required]
    [MaxLength(10)]
    public string UOMCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string UOMDescription { get; set; } = string.Empty;

    [MaxLength(10)]
    public string BaseUnit { get; set; } = string.Empty;

    public decimal ConversionFactor { get; set; } = 1.0m;
}
