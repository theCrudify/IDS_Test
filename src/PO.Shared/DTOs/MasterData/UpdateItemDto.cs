// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UpdateItemDto.cs
using System.ComponentModel.DataAnnotations;
using PO.Domain.Enums;

namespace PO.Shared.DTOs.MasterData;

public class UpdateItemDto
{
    [Required]
    [MaxLength(100)]
    public string ItemName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    public ItemType ItemType { get; set; }

    [MaxLength(20)]
    public string? Brand { get; set; }

    [MaxLength(20)]
    public string? Model { get; set; }

    [MaxLength(50)]
    public string? Specification { get; set; }

    public int? DefaultUOMId { get; set; }
    public int? DefaultTaxId { get; set; }
    public decimal? StandardPrice { get; set; }
    
    public bool IsActive { get; set; }
}
