// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UOMLookupDto.cs

namespace PO.Shared.DTOs.MasterData;

public class UOMLookupDto
{
    public int Id { get; set; }
    public string UOMCode { get; set; } = string.Empty;
    public string UOMDescription { get; set; } = string.Empty;
}
