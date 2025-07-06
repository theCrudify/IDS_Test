// FILE LOCATION: src/PO.Domain/Entities/MasterData/Vendor.cs
// DESCRIPTION: Vendor entity for managing supplier information with complete contact details

using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// Vendor entity - represents suppliers/vendors with complete contact and payment information
/// </summary>
public class Vendor : BaseEntity
{
    /// <summary>
    /// Unique vendor ID from SAP (e.g., "S3008", "S00128")
    /// </summary>
    [Required]
    [StringLength(10)]
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Full vendor/company name
    /// </summary>
    [Required]
    [StringLength(255)]
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Primary contact person name
    /// </summary>
    [StringLength(100)]
    public string? ContactPerson { get; set; }

    /// <summary>
    /// Complete office address
    /// </summary>
    public string? OfficeAddress { get; set; }

    /// <summary>
    /// City where vendor is located
    /// </summary>
    [StringLength(100)]
    public string? OfficeCity { get; set; }

    /// <summary>
    /// Primary phone number
    /// </summary>
    [StringLength(20)]
    public string? OfficePhone1 { get; set; }

    /// <summary>
    /// Secondary phone number
    /// </summary>
    [StringLength(20)]
    public string? OfficePhone2 { get; set; }

    /// <summary>
    /// Fax number
    /// </summary>
    [StringLength(20)]
    public string? OfficeFax { get; set; }

    /// <summary>
    /// Email address for official communication
    /// </summary>
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Payment terms (e.g., "DD30", "COD")
    /// </summary>
    [StringLength(100)]
    public string? PaymentTerms { get; set; }

    /// <summary>
    /// Detailed payment terms description
    /// </summary>
    public string? PaymentDetails { get; set; }

    /// <summary>
    /// Whether this vendor is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    /// <summary>
    /// Purchase orders from this vendor
    /// </summary>
    public virtual ICollection<POHeader> PurchaseOrders { get; set; } = new List<POHeader>();
}
