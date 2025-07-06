using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_divisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DivisionCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DivisionName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_divisions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CanCreatePO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanCheckPO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanAcknowledgePO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanApprovePO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ApprovalLevel = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_taxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaxCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxRate = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_taxes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_uoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UOMCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UOMDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseUnit = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConversionFactor = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_uoms", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VendorId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendorName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactPerson = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_vendors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Specification = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultUOMId = table.Column<int>(type: "int", nullable: true),
                    DefaultTaxId = table.Column<int>(type: "int", nullable: true),
                    StandardPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_master_items_master_taxes_DefaultTaxId",
                        column: x => x.DefaultTaxId,
                        principalTable: "master_taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_master_items_master_uoms_DefaultUOMId",
                        column: x => x.DefaultUOMId,
                        principalTable: "master_uoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_approval_matrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    MinAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CheckerRoleId = table.Column<int>(type: "int", nullable: false),
                    AcknowledgeRoleId = table.Column<int>(type: "int", nullable: false),
                    ApproverRoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_approval_matrices", x => x.Id);
                    table.CheckConstraint("CK_ApprovalMatrix_Amount", "MinAmount <= MaxAmount");
                    table.ForeignKey(
                        name: "FK_master_approval_matrices_master_roles_AcknowledgeRoleId",
                        column: x => x.AcknowledgeRoleId,
                        principalTable: "master_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_master_approval_matrices_master_roles_ApproverRoleId",
                        column: x => x.ApproverRoleId,
                        principalTable: "master_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_master_approval_matrices_master_roles_CheckerRoleId",
                        column: x => x.CheckerRoleId,
                        principalTable: "master_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeptCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeptName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeptHeadId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_departments", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "master_users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_master_users_master_departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "master_departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_master_users_master_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "master_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_master_users_master_users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "master_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_po_headers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PONumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PODate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    POType = table.Column<int>(type: "int", nullable: false),
                    VendorIdInt = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExchangeRate = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalDue = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ApprovalNotes = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApprovedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RejectedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RejectionReason = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_po_headers", x => x.Id);
                    table.CheckConstraint("CK_POHeader_Status", "Status IN (0, 1, 2, 3, 4, 5, 6)");
                    table.CheckConstraint("CK_POHeader_TotalDue", "TotalDue >= 0");
                    table.ForeignKey(
                        name: "FK_t_po_headers_master_departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "master_departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_po_headers_master_users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "master_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_po_headers_master_vendors_VendorIdInt",
                        column: x => x.VendorIdInt,
                        principalTable: "master_vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RelatedPOId = table.Column<int>(type: "int", nullable: true),
                    ActionUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_master_users_UserId",
                        column: x => x.UserId,
                        principalTable: "master_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notifications_t_po_headers_RelatedPOId",
                        column: x => x.RelatedPOId,
                        principalTable: "t_po_headers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_po_approval_histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    POId = table.Column<int>(type: "int", nullable: false),
                    ApprovalLevel = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RejectionReason = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_po_approval_histories", x => x.Id);
                    table.CheckConstraint("CK_ApprovalHistory_Level", "ApprovalLevel IN (1, 2, 3)");
                    table.CheckConstraint("CK_ApprovalHistory_Status", "ApprovalStatus IN (0, 1, 2)");
                    table.ForeignKey(
                        name: "FK_t_po_approval_histories_master_users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "master_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_po_approval_histories_t_po_headers_POId",
                        column: x => x.POId,
                        principalTable: "t_po_headers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_po_attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    POId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadedBy = table.Column<int>(type: "int", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_po_attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_po_attachments_master_users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "master_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_po_attachments_t_po_headers_POId",
                        column: x => x.POId,
                        principalTable: "t_po_headers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_po_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    POId = table.Column<int>(type: "int", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemDescription = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UOMId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LineTotalIncludingTax = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DivisionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_po_details", x => x.Id);
                    table.CheckConstraint("CK_PODetail_Quantity", "Quantity > 0");
                    table.CheckConstraint("CK_PODetail_UnitPrice", "UnitPrice >= 0");
                    table.ForeignKey(
                        name: "FK_t_po_details_master_divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "master_divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_t_po_details_master_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "master_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_po_details_master_taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "master_taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_po_details_master_uoms_UOMId",
                        column: x => x.UOMId,
                        principalTable: "master_uoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_po_details_t_po_headers_POId",
                        column: x => x.POId,
                        principalTable: "t_po_headers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "master_departments",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeptCode", "DeptHeadId", "DeptName", "IsActive", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1647), null, null, null, "TECH", null, "Technical Department", true, false, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1647), null },
                    { 2, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1652), null, null, null, "PROD", null, "Production Department", true, false, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1652), null },
                    { 3, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1655), null, null, null, "PURCH", null, "Purchasing Department", true, false, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1655), null }
                });

            migrationBuilder.InsertData(
                table: "master_divisions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DivisionCode", "DivisionName", "IsActive", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1804), null, null, null, "TECH", "Technical Division", true, false, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1804), null });

            migrationBuilder.InsertData(
                table: "master_roles",
                columns: new[] { "Id", "ApprovalLevel", "CanAcknowledgePO", "CanApprovePO", "CanCheckPO", "CanCreatePO", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsActive", "IsDeleted", "RoleCode", "RoleName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 0, false, false, false, true, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1314), null, null, null, true, false, "USR", "User Purchasing", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1314), null },
                    { 2, 1, false, false, true, true, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1320), null, null, null, true, false, "STF", "Staff Purchasing", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1321), null },
                    { 3, 2, true, false, true, false, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1324), null, null, null, true, false, "AMG", "Assistant Manager", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1325), null },
                    { 4, 2, true, true, false, false, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1328), null, null, null, true, false, "MGR", "Manager", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1329), null },
                    { 5, 3, false, true, false, false, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1332), null, null, null, true, false, "GM", "General Manager", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1333), null }
                });

            migrationBuilder.InsertData(
                table: "master_taxes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsActive", "IsDeleted", "TaxCode", "TaxDescription", "TaxRate", "TaxType", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1764), null, null, null, true, false, "M11", "VAT 11%", 11.00m, "VAT", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1765), null },
                    { 2, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1768), null, null, null, true, false, "M12", "VAT 12%", 12.00m, "VAT", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1769), null }
                });

            migrationBuilder.InsertData(
                table: "master_uoms",
                columns: new[] { "Id", "BaseUnit", "ConversionFactor", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsActive", "IsDeleted", "UOMCode", "UOMDescription", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "PCS", 1.000000m, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1701), null, null, null, true, false, "PCS", "Pieces", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1702), null },
                    { 2, "CAN", 1.000000m, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1705), null, null, null, true, false, "CAN3.6", "3.6 Liter Can", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1705), null },
                    { 3, "CAN", 1.000000m, new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1708), null, null, null, true, false, "CAN50", "50 Liter Can", new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1709), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_master_approval_matrices_AcknowledgeRoleId",
                table: "master_approval_matrices",
                column: "AcknowledgeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_master_approval_matrices_ApproverRoleId",
                table: "master_approval_matrices",
                column: "ApproverRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_master_approval_matrices_CheckerRoleId",
                table: "master_approval_matrices",
                column: "CheckerRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_master_approval_matrices_DeptId",
                table: "master_approval_matrices",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_master_departments_DeptCode",
                table: "master_departments",
                column: "DeptCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_departments_DeptHeadId",
                table: "master_departments",
                column: "DeptHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_master_divisions_DivisionCode",
                table: "master_divisions",
                column: "DivisionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_items_DefaultTaxId",
                table: "master_items",
                column: "DefaultTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_master_items_DefaultUOMId",
                table: "master_items",
                column: "DefaultUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_master_items_ItemCode",
                table: "master_items",
                column: "ItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_roles_RoleCode",
                table: "master_roles",
                column: "RoleCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_taxes_TaxCode",
                table: "master_taxes",
                column: "TaxCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_uoms_UOMCode",
                table: "master_uoms",
                column: "UOMCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_users_DeptId",
                table: "master_users",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_master_users_Email",
                table: "master_users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_users_EmployeeCode",
                table: "master_users",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_users_ManagerId",
                table: "master_users",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_master_users_RoleId",
                table: "master_users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_master_vendors_VendorId",
                table: "master_vendors",
                column: "VendorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_RelatedPOId",
                table: "notifications",
                column: "RelatedPOId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_UserId_IsRead_CreatedAt",
                table: "notifications",
                columns: new[] { "UserId", "IsRead", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_t_po_approval_histories_ApproverId",
                table: "t_po_approval_histories",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_approval_histories_POId_ApprovalLevel",
                table: "t_po_approval_histories",
                columns: new[] { "POId", "ApprovalLevel" });

            migrationBuilder.CreateIndex(
                name: "IX_t_po_attachments_POId",
                table: "t_po_attachments",
                column: "POId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_attachments_UploadedBy",
                table: "t_po_attachments",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_details_DivisionId",
                table: "t_po_details",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_details_ItemId",
                table: "t_po_details",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_details_POId",
                table: "t_po_details",
                column: "POId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_details_TaxId",
                table: "t_po_details",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_details_UOMId",
                table: "t_po_details",
                column: "UOMId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_headers_CreatedById",
                table: "t_po_headers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_headers_DeptId",
                table: "t_po_headers",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_t_po_headers_PONumber",
                table: "t_po_headers",
                column: "PONumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_po_headers_Status_CreatedAt",
                table: "t_po_headers",
                columns: new[] { "Status", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_t_po_headers_VendorId_PostingDate",
                table: "t_po_headers",
                columns: new[] { "VendorId", "PostingDate" });

            migrationBuilder.CreateIndex(
                name: "IX_t_po_headers_VendorIdInt",
                table: "t_po_headers",
                column: "VendorIdInt");

            migrationBuilder.AddForeignKey(
                name: "FK_master_approval_matrices_master_departments_DeptId",
                table: "master_approval_matrices",
                column: "DeptId",
                principalTable: "master_departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_master_departments_master_users_DeptHeadId",
                table: "master_departments",
                column: "DeptHeadId",
                principalTable: "master_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_master_users_master_departments_DeptId",
                table: "master_users");

            migrationBuilder.DropTable(
                name: "master_approval_matrices");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "t_po_approval_histories");

            migrationBuilder.DropTable(
                name: "t_po_attachments");

            migrationBuilder.DropTable(
                name: "t_po_details");

            migrationBuilder.DropTable(
                name: "master_divisions");

            migrationBuilder.DropTable(
                name: "master_items");

            migrationBuilder.DropTable(
                name: "t_po_headers");

            migrationBuilder.DropTable(
                name: "master_taxes");

            migrationBuilder.DropTable(
                name: "master_uoms");

            migrationBuilder.DropTable(
                name: "master_vendors");

            migrationBuilder.DropTable(
                name: "master_departments");

            migrationBuilder.DropTable(
                name: "master_users");

            migrationBuilder.DropTable(
                name: "master_roles");
        }
    }
}
