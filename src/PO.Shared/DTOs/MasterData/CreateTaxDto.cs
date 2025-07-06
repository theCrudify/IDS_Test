// FILE LOCATION: src/PO.Shared/DTOs/MasterData/CreateTaxDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class CreateTaxDto
{
    [Required]
    [MaxLength(10)]
    public string TaxCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string TaxDescription { get; set; } = string.Empty;

    [MaxLength(20)]
    public string TaxType { get; set; } = "VAT";

    public decimal TaxRate { get; set; } = 0.0m;
}
