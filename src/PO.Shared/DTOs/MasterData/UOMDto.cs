using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.MasterData;

/// <summary>
/// UOM (Unit of Measure) Data Transfer Object
/// </summary>
public class UOMDto
{
    public int Id { get; set; }
    public string UOMCode { get; set; } = string.Empty;
    public string UOMName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Create UOM Data Transfer Object
/// </summary>
public class CreateUOMDto
{
    [Required]
    [StringLength(10)]
    public string UOMCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string UOMName { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Update UOM Data Transfer Object
/// </summary>
public class UpdateUOMDto
{
    [Required]
    [StringLength(100)]
    public string UOMName { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }
}

/// <summary>
/// UOM Lookup Data Transfer Object for dropdowns
/// </summary>
public class UOMLookupDto
{
    public int Id { get; set; }
    public string UOMCode { get; set; } = string.Empty;
    public string UOMName { get; set; } = string.Empty;
}