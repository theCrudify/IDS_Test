// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UpdateTaxDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class UpdateTaxDto
{
    [Required]
    [MaxLength(50)]
    public string TaxDescription { get; set; } = string.Empty;

    [MaxLength(20)]
    public string TaxType { get; set; } = "VAT";

    public decimal TaxRate { get; set; } = 0.0m;
    
    public bool IsActive { get; set; }
}
