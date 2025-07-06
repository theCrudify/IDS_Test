// FILE LOCATION: src/PO.Infrastructure/Data/PODbContext.cs
// DESCRIPTION: Entity Framework DbContext for Purchase Order system with MySQL configuration

using Microsoft.EntityFrameworkCore;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.PurchaseOrder;
using PO.Domain.Entities.System;

namespace PO.Infrastructure.Data;

/// <summary>
/// Entity Framework DbContext for the Purchase Order Approval System
/// </summary>
public class PODbContext : DbContext
{
    public PODbContext(DbContextOptions<PODbContext> options) : base(options)
    {
    }

    // Master Data DbSets
    public DbSet<Role> Roles { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<UOM> UOMs { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<Division> Divisions { get; set; }
    public DbSet<ApprovalMatrix> ApprovalMatrices { get; set; }

    // Purchase Order DbSets
    public DbSet<POHeader> POHeaders { get; set; }
    public DbSet<PODetail> PODetails { get; set; }
    public DbSet<POApprovalHistory> POApprovalHistories { get; set; }
    public DbSet<POAttachment> POAttachments { get; set; }

    // System DbSets
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure table names (following MySQL naming conventions)
        ConfigureTableNames(modelBuilder);

        // Configure relationships
        ConfigureRelationships(modelBuilder);

        // Configure indexes
        ConfigureIndexes(modelBuilder);

        // Configure constraints
        ConfigureConstraints(modelBuilder);

        // Seed initial data
        SeedInitialData(modelBuilder);
    }

    private void ConfigureTableNames(ModelBuilder modelBuilder)
    {
        // Master Data Tables
        modelBuilder.Entity<Role>().ToTable("master_roles");
        modelBuilder.Entity<Department>().ToTable("master_departments");
        modelBuilder.Entity<User>().ToTable("master_users");
        modelBuilder.Entity<Vendor>().ToTable("master_vendors");
        modelBuilder.Entity<Item>().ToTable("master_items");
        modelBuilder.Entity<UOM>().ToTable("master_uoms");
        modelBuilder.Entity<Tax>().ToTable("master_taxes");
        modelBuilder.Entity<Division>().ToTable("master_divisions");
        modelBuilder.Entity<ApprovalMatrix>().ToTable("master_approval_matrices");

        // Transaction Tables
        modelBuilder.Entity<POHeader>().ToTable("t_po_headers");
        modelBuilder.Entity<PODetail>().ToTable("t_po_details");
        modelBuilder.Entity<POApprovalHistory>().ToTable("t_po_approval_histories");
        modelBuilder.Entity<POAttachment>().ToTable("t_po_attachments");

        // System Tables
        modelBuilder.Entity<Notification>().ToTable("notifications");
    }

    private void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        // User -> Role relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // User -> Department relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Department)
            .WithMany(d => d.Users)
            .HasForeignKey(u => u.DeptId)
            .OnDelete(DeleteBehavior.Restrict);

        // User -> Manager (self-reference)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Manager)
            .WithMany(u => u.Subordinates)
            .HasForeignKey(u => u.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Department -> Department Head
        modelBuilder.Entity<Department>()
            .HasOne(d => d.DeptHead)
            .WithMany(u => u.ManagedDepartments)
            .HasForeignKey(d => d.DeptHeadId)
            .OnDelete(DeleteBehavior.SetNull);

        // POHeader relationships
        modelBuilder.Entity<POHeader>()
            .HasOne(p => p.Vendor)
            .WithMany(v => v.PurchaseOrders)
            .HasForeignKey(p => p.VendorId)
            .HasPrincipalKey(v => v.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<POHeader>()
            .HasOne(p => p.CreatedByUser)
            .WithMany(u => u.CreatedPurchaseOrders)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<POHeader>()
            .HasOne(p => p.Department)
            .WithMany(d => d.PurchaseOrders)
            .HasForeignKey(p => p.DeptId)
            .OnDelete(DeleteBehavior.Restrict);

        // PODetail relationships
        modelBuilder.Entity<PODetail>()
            .HasOne(pd => pd.POHeader)
            .WithMany(p => p.PODetails)
            .HasForeignKey(pd => pd.POId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PODetail>()
            .HasOne(pd => pd.Item)
            .WithMany(i => i.PODetails)
            .HasForeignKey(pd => pd.ItemCode)
            .HasPrincipalKey(i => i.ItemCode)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PODetail>()
            .HasOne(pd => pd.UOM)
            .WithMany(u => u.PODetails)
            .HasForeignKey(pd => pd.UOMId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PODetail>()
            .HasOne(pd => pd.Tax)
            .WithMany(t => t.PODetails)
            .HasForeignKey(pd => pd.TaxId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PODetail>()
            .HasOne(pd => pd.Division)
            .WithMany(d => d.PODetails)
            .HasForeignKey(pd => pd.DivisionId)
            .OnDelete(DeleteBehavior.SetNull);

        // POApprovalHistory relationships
        modelBuilder.Entity<POApprovalHistory>()
            .HasOne(ah => ah.POHeader)
            .WithMany(p => p.ApprovalHistory)
            .HasForeignKey(ah => ah.POId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<POApprovalHistory>()
            .HasOne(ah => ah.Approver)
            .WithMany(u => u.ApprovalHistory)
            .HasForeignKey(ah => ah.ApproverId)
            .OnDelete(DeleteBehavior.Restrict);

        // POAttachment relationships
        modelBuilder.Entity<POAttachment>()
            .HasOne(a => a.POHeader)
            .WithMany(p => p.Attachments)
            .HasForeignKey(a => a.POId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<POAttachment>()
            .HasOne(a => a.UploadedByUser)
            .WithMany(u => u.UploadedAttachments)
            .HasForeignKey(a => a.UploadedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // Notification relationships
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.RelatedPO)
            .WithMany(p => p.Notifications)
            .HasForeignKey(n => n.RelatedPOId)
            .OnDelete(DeleteBehavior.SetNull);

        // ApprovalMatrix relationships
        modelBuilder.Entity<ApprovalMatrix>()
            .HasOne(am => am.Department)
            .WithMany(d => d.ApprovalMatrices)
            .HasForeignKey(am => am.DeptId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ApprovalMatrix>()
            .HasOne(am => am.CheckerRole)
            .WithMany(r => r.CheckerMatrices)
            .HasForeignKey(am => am.CheckerRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApprovalMatrix>()
            .HasOne(am => am.AcknowledgeRole)
            .WithMany(r => r.AcknowledgeMatrices)
            .HasForeignKey(am => am.AcknowledgeRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApprovalMatrix>()
            .HasOne(am => am.ApproverRole)
            .WithMany(r => r.ApproverMatrices)
            .HasForeignKey(am => am.ApproverRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Item relationships
        modelBuilder.Entity<Item>()
            .HasOne(i => i.DefaultUOM)
            .WithMany(u => u.DefaultItems)
            .HasForeignKey(i => i.DefaultUOMId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Item>()
            .HasOne(i => i.DefaultTax)
            .WithMany(t => t.DefaultItems)
            .HasForeignKey(i => i.DefaultTaxId)
            .OnDelete(DeleteBehavior.SetNull);
    }

    private void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        // Unique indexes
        modelBuilder.Entity<Role>()
            .HasIndex(r => r.RoleCode)
            .IsUnique();

        modelBuilder.Entity<Department>()
            .HasIndex(d => d.DeptCode)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.EmployeeCode)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Vendor>()
            .HasIndex(v => v.VendorId)
            .IsUnique();

        modelBuilder.Entity<Vendor>()
            .HasAlternateKey(v => v.VendorId);

        modelBuilder.Entity<Item>()
            .HasIndex(i => i.ItemCode)
            .IsUnique();

        modelBuilder.Entity<Item>()
            .HasAlternateKey(i => i.ItemCode);

        modelBuilder.Entity<UOM>()
            .HasIndex(u => u.UOMCode)
            .IsUnique();

        modelBuilder.Entity<Tax>()
            .HasIndex(t => t.TaxCode)
            .IsUnique();

        modelBuilder.Entity<Division>()
            .HasIndex(d => d.DivisionCode)
            .IsUnique();

        modelBuilder.Entity<POHeader>()
            .HasIndex(p => p.PONumber)
            .IsUnique();

        // Performance indexes
        modelBuilder.Entity<POHeader>()
            .HasIndex(p => new { p.Status, p.CreatedAt });

        modelBuilder.Entity<POHeader>()
            .HasIndex(p => new { p.VendorId, p.PostingDate });

        modelBuilder.Entity<Notification>()
            .HasIndex(n => new { n.UserId, n.IsRead, n.CreatedAt });

        modelBuilder.Entity<POApprovalHistory>()
            .HasIndex(ah => new { ah.POId, ah.ApprovalLevel });
    }

    private void ConfigureConstraints(ModelBuilder modelBuilder)
    {
        // Check constraints for enum values
        modelBuilder.Entity<POHeader>()
            .HasCheckConstraint("CK_POHeader_Status", 
                "Status IN (0, 1, 2, 3, 4, 5, 6)"); // POStatus enum values

        modelBuilder.Entity<POApprovalHistory>()
            .HasCheckConstraint("CK_ApprovalHistory_Level", 
                "ApprovalLevel IN (1, 2, 3)"); // ApprovalLevel enum values

        modelBuilder.Entity<POApprovalHistory>()
            .HasCheckConstraint("CK_ApprovalHistory_Status", 
                "ApprovalStatus IN (0, 1, 2)"); // ApprovalStatus enum values

        // Financial constraints
        modelBuilder.Entity<POHeader>()
            .HasCheckConstraint("CK_POHeader_TotalDue", 
                "TotalDue >= 0");

        modelBuilder.Entity<PODetail>()
            .HasCheckConstraint("CK_PODetail_Quantity", 
                "Quantity > 0");

        modelBuilder.Entity<PODetail>()
            .HasCheckConstraint("CK_PODetail_UnitPrice", 
                "UnitPrice >= 0");

        // ApprovalMatrix constraints
        modelBuilder.Entity<ApprovalMatrix>()
            .HasCheckConstraint("CK_ApprovalMatrix_Amount", 
                "MinAmount <= MaxAmount");
    }

    private void SeedInitialData(ModelBuilder modelBuilder)
    {
        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                RoleName = "User Purchasing",
                RoleCode = "USR",
                CanCreatePO = true,
                CanCheckPO = false,
                CanAcknowledgePO = false,
                CanApprovePO = false,
                ApprovalLevel = Domain.Enums.ApprovalLevel.None,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = 2,
                RoleName = "Staff Purchasing",
                RoleCode = "STF",
                CanCreatePO = true,
                CanCheckPO = true,
                CanAcknowledgePO = false,
                CanApprovePO = false,
                ApprovalLevel = Domain.Enums.ApprovalLevel.Checker,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = 3,
                RoleName = "Assistant Manager",
                RoleCode = "AMG",
                CanCreatePO = false,
                CanCheckPO = true,
                CanAcknowledgePO = true,
                CanApprovePO = false,
                ApprovalLevel = Domain.Enums.ApprovalLevel.Acknowledge,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = 4,
                RoleName = "Manager",
                RoleCode = "MGR",
                CanCreatePO = false,
                CanCheckPO = false,
                CanAcknowledgePO = true,
                CanApprovePO = true,
                ApprovalLevel = Domain.Enums.ApprovalLevel.Acknowledge,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = 5,
                RoleName = "General Manager",
                RoleCode = "GM",
                CanCreatePO = false,
                CanCheckPO = false,
                CanAcknowledgePO = false,
                CanApprovePO = true,
                ApprovalLevel = Domain.Enums.ApprovalLevel.Approver,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Departments
        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1,
                DeptCode = "TECH",
                DeptName = "Technical Department",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = 2,
                DeptCode = "PROD",
                DeptName = "Production Department",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = 3,
                DeptCode = "PURCH",
                DeptName = "Purchasing Department",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed UOMs
        modelBuilder.Entity<UOM>().HasData(
            new UOM
            {
                Id = 1,
                UOMCode = "PCS",
                UOMDescription = "Pieces",
                BaseUnit = "PCS",
                ConversionFactor = 1.000000m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new UOM
            {
                Id = 2,
                UOMCode = "CAN3.6",
                UOMDescription = "3.6 Liter Can",
                BaseUnit = "CAN",
                ConversionFactor = 1.000000m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new UOM
            {
                Id = 3,
                UOMCode = "CAN50",
                UOMDescription = "50 Liter Can",
                BaseUnit = "CAN",
                ConversionFactor = 1.000000m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Taxes
        modelBuilder.Entity<Tax>().HasData(
            new Tax
            {
                Id = 1,
                TaxCode = "M11",
                TaxRate = 11.00m,
                TaxDescription = "VAT 11%",
                TaxType = "VAT",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Tax
            {
                Id = 2,
                TaxCode = "M12",
                TaxRate = 12.00m,
                TaxDescription = "VAT 12%",
                TaxType = "VAT",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Divisions
        modelBuilder.Entity<Division>().HasData(
            new Division
            {
                Id = 1,
                DivisionCode = "TECH",
                DivisionName = "Technical Division",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Domain.Common.BaseEntity && 
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (Domain.Common.BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}