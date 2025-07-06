// FILE LOCATION: src/PO.Shared/DTOs/MasterData/DivisionDto.cs

namespace PO.Shared.DTOs.MasterData;

public class DivisionDto
{
    public int Id { get; set; }
    public string DivisionCode { get; set; } = string.Empty;
    public string DivisionName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
