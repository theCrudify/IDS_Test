// FILE LOCATION: src/PO.Shared/DTOs/MasterData/VendorLookupDto.cs

namespace PO.Shared.DTOs.MasterData;

public class VendorLookupDto
{
    public int Id { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
}
