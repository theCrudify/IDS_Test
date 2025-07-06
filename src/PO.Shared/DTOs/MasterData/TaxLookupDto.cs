// FILE LOCATION: src/PO.Shared/DTOs/MasterData/TaxLookupDto.cs

namespace PO.Shared.DTOs.MasterData;

public class TaxLookupDto
{
    public int Id { get; set; }
    public string TaxCode { get; set; } = string.Empty;
    public string TaxDescription { get; set; } = string.Empty;
}
