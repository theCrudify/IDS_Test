// FILE LOCATION: src/PO.Domain/Common/BaseEntity.cs
// DESCRIPTION: Base entity class that contains common properties for all entities (audit fields, etc.)

using System.ComponentModel.DataAnnotations;

namespace PO.Domain.Common;

/// <summary>
/// Base entity class containing common properties for all entities
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primary key identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Date when the entity was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the entity was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// ID of the user who created this entity
    /// </summary>
    public int? CreatedBy { get; set; }

    /// <summary>
    /// ID of the user who last updated this entity
    /// </summary>
    public int? UpdatedBy { get; set; }

    /// <summary>
    /// Soft delete flag - true if entity is deleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Date when the entity was soft deleted
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// ID of the user who deleted this entity
    /// </summary>
    public int? DeletedBy { get; set; }
}