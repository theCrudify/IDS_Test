// FILE LOCATION: src/PO.Shared/DTOs/MasterData/TaxDto.cs

namespace PO.Shared.DTOs.MasterData;

public class TaxDto
{
    public int Id { get; set; }
    public string TaxCode { get; set; } = string.Empty;
    public string TaxDescription { get; set; } = string.Empty;
    public string TaxType { get; set; } = string.Empty;
    public decimal TaxRate { get; set; }
    public bool IsActive { get; set; }
}
