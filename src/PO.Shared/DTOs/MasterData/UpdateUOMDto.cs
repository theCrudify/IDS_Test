// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UpdateUOMDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class UpdateUOMDto
{
    [Required]
    [MaxLength(50)]
    public string UOMDescription { get; set; } = string.Empty;

    [MaxLength(10)]
    public string BaseUnit { get; set; } = string.Empty;

    public decimal ConversionFactor { get; set; } = 1.0m;
    
    public bool IsActive { get; set; }
}
