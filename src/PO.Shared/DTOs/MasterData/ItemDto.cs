// FILE LOCATION: src/PO.Shared/DTOs/MasterData/ItemDto.cs

using PO.Domain.Enums;

namespace PO.Shared.DTOs.MasterData;

public class ItemDto
{
    public int Id { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ItemType ItemType { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Specification { get; set; }
    public int? DefaultUOMId { get; set; }
    public string? DefaultUOMCode { get; set; }
    public int? DefaultTaxId { get; set; }
    public string? DefaultTaxCode { get; set; }
    public decimal? StandardPrice { get; set; }
    public bool IsActive { get; set; }
}
