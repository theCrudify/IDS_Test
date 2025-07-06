// FILE LOCATION: src/PO.Domain/Enums/PurchaseOrderEnums.cs
// DESCRIPTION: All enums related to Purchase Order domain - status, types, approval levels, etc.

namespace PO.Domain.Enums;

/// <summary>
/// Purchase Order status enum - represents the current state of a PO in the approval workflow
/// </summary>
public enum POStatus
{
    Draft = 0,
    PendingSubmission = 1, // PO is ready to be submitted for approval
    PendingApprovalLevel1 = 2, // Waiting for approval from the first level (e.g., Checker)
    PendingApprovalLevel2 = 3, // Waiting for approval from the second level (e.g., Acknowledge)
    PendingApprovalLevel3 = 4, // Waiting for approval from the third level (e.g., Approver)
    Approved = 5,
    Rejected = 6,
    ReopenedForCorrection = 7, // PO was rejected and is now open for correction by the creator
    Closed = 8
}

/// <summary>
/// Purchase Order type enum - defines the category of purchase order
/// </summary>
public enum POType
{
    Import,
    Miscellaneous,
    Local,
    Service,
    Inventory
}

/// <summary>
/// Approval level enum - defines the three-level approval hierarchy
/// </summary>
public enum ApprovalLevel
{
    None = 0,
    Checker = 1,
    Acknowledge = 2,
    Approver = 3
}

/// <summary>
/// Approval status for each approval level
/// </summary>
public enum ApprovalStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}

/// <summary>
/// Item type enum - distinguishes between goods and services
/// </summary>
public enum ItemType
{
    Barang,  // Goods
    Jasa     // Services
}

/// <summary>
/// Notification type enum - different types of notifications in the system
/// </summary>
public enum NotificationType
{
    POSubmitted,
    POApproved,
    PORejected,
    POReadyCheck,
    POReadyAcknowledge,
    POReadyApprove,
    POClosed
}

/// <summary>
/// Notification priority levels
/// </summary>
public enum NotificationPriority
{
    Low = 0,
    Medium = 1,
    High = 2
}

/// <summary>
/// User role enum - defines different roles in the system
/// </summary>
public enum UserRole
{
    User = 0,
    Staff = 1,
    AssistantManager = 2,
    Manager = 3,
    GeneralManager = 4,
    Finance = 5
}

/// <summary>
/// Currency codes supported by the system
/// </summary>
public enum CurrencyCode
{
    IDR,
    USD,
    EUR,
    JPY,
    SGD
}