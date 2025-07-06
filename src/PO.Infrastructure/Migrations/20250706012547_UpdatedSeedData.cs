using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "master_approval_matrices",
                columns: new[] { "Id", "AcknowledgeRoleId", "ApproverRoleId", "CheckerRoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeptId", "IsActive", "IsDeleted", "MaxAmount", "MinAmount", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 3, 4, 2, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3909), null, null, null, 3, true, false, 10000000m, 0m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3909), null },
                    { 2, 4, 5, 2, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3913), null, null, null, 3, true, false, 50000000m, 10000001m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3913), null },
                    { 3, 3, 4, 2, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3916), null, null, null, 2, true, false, 25000000m, 0m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3917), null }
                });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3538), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3539) });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3543), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3543) });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3546), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3547) });

            migrationBuilder.UpdateData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3691), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3692) });

            migrationBuilder.InsertData(
                table: "master_divisions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DivisionCode", "DivisionName", "IsActive", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3694), null, null, null, "PROD", "Production Division", true, false, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3695), null },
                    { 3, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3697), null, null, null, "QC", "Quality Control Division", true, false, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3698), null }
                });

            migrationBuilder.InsertData(
                table: "master_items",
                columns: new[] { "Id", "Brand", "CreatedAt", "CreatedBy", "DefaultTaxId", "DefaultUOMId", "DeletedAt", "DeletedBy", "Description", "IsActive", "IsDeleted", "ItemCode", "ItemName", "ItemType", "Model", "Specification", "StandardPrice", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Fastenal", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3792), null, 1, 1, null, null, "Stainless Steel Screw M6x20mm", true, false, "ITM001", "Screw M6x20", 0, "M6x20", "Stainless Steel 316", 500m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3793), null },
                    { 2, "Shell", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3797), null, 1, 2, null, null, "High Performance Hydraulic Oil", true, false, "ITM002", "Hydraulic Oil", 0, "Tellus S2 M46", "ISO VG 46", 85000m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3798), null },
                    { 3, "Pertamina", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3802), null, 1, 3, null, null, "Engine Motor Oil SAE 15W-40", true, false, "ITM003", "Motor Oil", 0, "Mesran SAE 15W-40", "API CI-4/SL", 1250000m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3802), null },
                    { 4, null, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3806), null, 1, 1, null, null, "Preventive maintenance service", true, false, "SRV001", "Maintenance Service", 1, null, null, 500000m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3806), null }
                });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3212), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3214) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3219), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3224), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3224) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3228), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3228) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3232), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3232) });

            migrationBuilder.UpdateData(
                table: "master_taxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3655), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3655) });

            migrationBuilder.UpdateData(
                table: "master_taxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3659), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3660) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3597), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3598) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3613), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3613) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3616), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3617) });

            migrationBuilder.InsertData(
                table: "master_users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeptId", "Email", "EmployeeCode", "FullName", "IsActive", "IsDeleted", "ManagerId", "Phone", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3853), null, null, null, 3, "john.doe@company.com", "EMP001", "John Doe", true, false, null, "081234567890", 1, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3854), null },
                    { 4, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3868), null, null, null, 3, "sarah.wilson@company.com", "EMP004", "Sarah Wilson", true, false, null, "081234567893", 4, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3868), null },
                    { 5, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3872), null, null, null, 1, "david.brown@company.com", "EMP005", "David Brown", true, false, null, "081234567894", 5, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3873), null }
                });

            migrationBuilder.InsertData(
                table: "master_vendors",
                columns: new[] { "Id", "Address", "ContactPerson", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "IsActive", "IsDeleted", "Phone", "TaxId", "UpdatedAt", "UpdatedBy", "VendorId", "VendorName" },
                values: new object[,]
                {
                    { 1, "Jl. Industri No. 123, Jakarta", "Budi Santoso", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3739), null, null, null, "sales@sumberteknik.com", true, false, "021-1234567", "01.234.567.8-901.000", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3740), null, "V001", "PT. Sumber Teknik" },
                    { 2, "Jl. Raya Bogor KM 15, Depok", "Sari Dewi", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3744), null, null, null, "info@majujaya.co.id", true, false, "021-7654321", "01.234.567.8-902.000", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3745), null, "V002", "CV. Maju Jaya" },
                    { 3, "Jl. Sudirman No. 45, Jakarta Pusat", "Ahmad Rahman", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3748), null, null, null, "procurement@globalsupplier.com", true, false, "021-9876543", "01.234.567.8-903.000", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3748), null, "V003", "PT. Global Supplier" }
                });

            migrationBuilder.InsertData(
                table: "master_users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeptId", "Email", "EmployeeCode", "FullName", "IsActive", "IsDeleted", "ManagerId", "Phone", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3859), null, null, null, 3, "jane.smith@company.com", "EMP002", "Jane Smith", true, false, 4, "081234567891", 2, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3859), null },
                    { 3, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3863), null, null, null, 2, "mike.johnson@company.com", "EMP003", "Mike Johnson", true, false, 5, "081234567892", 3, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3864), null }
                });

            migrationBuilder.InsertData(
                table: "t_po_headers",
                columns: new[] { "Id", "ApprovalNotes", "ApprovedDate", "CreatedAt", "CreatedBy", "CreatedById", "Currency", "DeletedAt", "DeletedBy", "DeliveryAddress", "DeliveryDate", "DeptId", "ExchangeRate", "IsDeleted", "Notes", "PODate", "PONumber", "POType", "PostingDate", "RejectedDate", "RejectionReason", "Status", "SubTotal", "TaxAmount", "TotalDue", "UpdatedAt", "UpdatedBy", "VendorId", "VendorIdInt" },
                values: new object[] { 1, null, null, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4045), null, 1, "IDR", null, null, "Warehouse A, Jl. Gudang No. 10", new DateTime(2025, 7, 13, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4033), 3, 1.0m, false, "Purchase order for maintenance items", new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4026), "PO-2025-001", 2, new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4028), null, null, 0, 635000m, 69850m, 704850m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4045), null, "V001", 1 });

            migrationBuilder.InsertData(
                table: "t_po_details",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeliveryDate", "DivisionId", "IsDeleted", "ItemCode", "ItemDescription", "ItemId", "LineNumber", "LineTotal", "LineTotalIncludingTax", "Notes", "POId", "Quantity", "TaxAmount", "TaxId", "UOMId", "UnitPrice", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4115), null, null, null, null, 1, false, "ITM001", "Stainless Steel Screw M6x20mm", 1, 1, 50000m, 55500m, null, 1, 100m, 5500m, 1, 1, 500m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4116), null },
                    { 2, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4121), null, null, null, null, 1, false, "ITM002", "High Performance Hydraulic Oil", 2, 2, 85000m, 94350m, null, 1, 1m, 9350m, 1, 2, 85000m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4122), null },
                    { 3, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4126), null, null, null, null, 1, false, "SRV001", "Preventive maintenance service", 4, 3, 500000m, 555000m, null, 1, 1m, 55000m, 1, 1, 500000m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4127), null }
                });

            migrationBuilder.InsertData(
                table: "t_po_headers",
                columns: new[] { "Id", "ApprovalNotes", "ApprovedDate", "CreatedAt", "CreatedBy", "CreatedById", "Currency", "DeletedAt", "DeletedBy", "DeliveryAddress", "DeliveryDate", "DeptId", "ExchangeRate", "IsDeleted", "Notes", "PODate", "PONumber", "POType", "PostingDate", "RejectedDate", "RejectionReason", "Status", "SubTotal", "TaxAmount", "TotalDue", "UpdatedAt", "UpdatedBy", "VendorId", "VendorIdInt" },
                values: new object[] { 2, null, null, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4071), null, 2, "IDR", null, null, "Production Floor B", new DateTime(2025, 7, 20, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4066), 2, 1.0m, false, "Urgent purchase for production line", new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4061), "PO-2025-002", 0, new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4062), null, null, 1, 1250000m, 137500m, 1387500m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4072), null, "V002", 2 });

            migrationBuilder.InsertData(
                table: "t_po_details",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeliveryDate", "DivisionId", "IsDeleted", "ItemCode", "ItemDescription", "ItemId", "LineNumber", "LineTotal", "LineTotalIncludingTax", "Notes", "POId", "Quantity", "TaxAmount", "TaxId", "UOMId", "UnitPrice", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 4, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4131), null, null, null, null, 2, false, "ITM003", "Engine Motor Oil SAE 15W-40", 3, 1, 1250000m, 1387500m, null, 2, 1m, 137500m, 1, 3, 1250000m, new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4132), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "t_po_headers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "t_po_headers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1647), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1647) });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1652), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1652) });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1655) });

            migrationBuilder.UpdateData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1804), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1804) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1314), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1314) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1320), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1321) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1324), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1325) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1328), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1329) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1332), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1333) });

            migrationBuilder.UpdateData(
                table: "master_taxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1764), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1765) });

            migrationBuilder.UpdateData(
                table: "master_taxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1768), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1769) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1701), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1702) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1705), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1705) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1708), new DateTime(2025, 7, 6, 1, 23, 45, 368, DateTimeKind.Utc).AddTicks(1709) });
        }
    }
}
