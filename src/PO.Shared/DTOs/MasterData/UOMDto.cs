// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UOMDto.cs

namespace PO.Shared.DTOs.MasterData;

public class UOMDto
{
    public int Id { get; set; }
    public string UOMCode { get; set; } = string.Empty;
    public string UOMDescription { get; set; } = string.Empty;
    public string BaseUnit { get; set; } = string.Empty;
    public decimal ConversionFactor { get; set; }
    public bool IsActive { get; set; }
}
