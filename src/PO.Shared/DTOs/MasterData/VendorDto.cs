// FILE LOCATION: src/PO.Shared/DTOs/MasterData/VendorDto.cs

namespace PO.Shared.DTOs.MasterData;

public class VendorDto
{
    public int Id { get; set; }
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ContactPerson { get; set; }
    public string? TaxId { get; set; }
    public bool IsActive { get; set; }
}
