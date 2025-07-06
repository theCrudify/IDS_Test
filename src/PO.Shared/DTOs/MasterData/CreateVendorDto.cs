// FILE LOCATION: src/PO.Shared/DTOs/MasterData/CreateVendorDto.cs
using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

public class CreateVendorDto
{
    [Required]
    [MaxLength(20)]
    public string VendorId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string VendorName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? ContactPerson { get; set; }

    [MaxLength(30)]
    public string? TaxId { get; set; }
}
