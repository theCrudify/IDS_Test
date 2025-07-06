// FILE LOCATION: src/PO.Shared/DTOs/MasterData/ItemLookupDto.cs

namespace PO.Shared.DTOs.MasterData;

public class ItemLookupDto
{
    public int Id { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
}
