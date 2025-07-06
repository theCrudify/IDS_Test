using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedSeedDataLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5552), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5553) });

            migrationBuilder.UpdateData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5558), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5559) });

            migrationBuilder.UpdateData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5564), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5564) });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4877), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4878) });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4885), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4886) });

            migrationBuilder.UpdateData(
                table: "master_departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4892), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4893) });

            migrationBuilder.UpdateData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5132), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5133) });

            migrationBuilder.UpdateData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5138), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5139) });

            migrationBuilder.UpdateData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5143), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5143) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5336), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5337) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5346), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5347) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Model", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5354), "Mesran SAE 15W-40", new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5354) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5361), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5362) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4154), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4155) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4163), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4164) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4177), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4178) });

            migrationBuilder.UpdateData(
                table: "master_roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4184), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4185) });

            migrationBuilder.UpdateData(
                table: "master_taxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5065), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5066) });

            migrationBuilder.UpdateData(
                table: "master_taxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5073), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5074) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4981) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4987), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4988) });

            migrationBuilder.UpdateData(
                table: "master_uoms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4993), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(4994) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5448), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5448) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5457), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5458) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5466), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5467) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5473), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5474) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5481), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5482) });

            migrationBuilder.UpdateData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5231), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5232) });

            migrationBuilder.UpdateData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5241) });

            migrationBuilder.UpdateData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5246), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5247) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5905), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5906) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5915), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5916) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5927), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5928) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5935), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5936) });

            migrationBuilder.UpdateData(
                table: "t_po_headers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DeliveryDate", "PODate", "PostingDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5798), new DateTime(2025, 7, 13, 8, 29, 4, 363, DateTimeKind.Local).AddTicks(5778), new DateTime(2025, 7, 6, 8, 29, 4, 363, DateTimeKind.Local).AddTicks(5761), new DateTime(2025, 7, 6, 8, 29, 4, 363, DateTimeKind.Local).AddTicks(5765), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5799) });

            migrationBuilder.UpdateData(
                table: "t_po_headers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DeliveryDate", "PODate", "PostingDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5828), new DateTime(2025, 7, 20, 8, 29, 4, 363, DateTimeKind.Local).AddTicks(5821), new DateTime(2025, 7, 6, 8, 29, 4, 363, DateTimeKind.Local).AddTicks(5812), new DateTime(2025, 7, 6, 8, 29, 4, 363, DateTimeKind.Local).AddTicks(5815), new DateTime(2025, 7, 6, 1, 29, 4, 363, DateTimeKind.Utc).AddTicks(5829) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3909), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3909) });

            migrationBuilder.UpdateData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3913), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3913) });

            migrationBuilder.UpdateData(
                table: "master_approval_matrices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3916), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3917) });

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

            migrationBuilder.UpdateData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3694), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3695) });

            migrationBuilder.UpdateData(
                table: "master_divisions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3697), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3698) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3792), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3793) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3797), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3798) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Model", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3802), "Mesran Super SAE 15W-40", new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3802) });

            migrationBuilder.UpdateData(
                table: "master_items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3806), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3806) });

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

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3853), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3854) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3859), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3859) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3863), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3864) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3868), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3868) });

            migrationBuilder.UpdateData(
                table: "master_users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3872), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3873) });

            migrationBuilder.UpdateData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3739), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3744), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3745) });

            migrationBuilder.UpdateData(
                table: "master_vendors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3748), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(3748) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4115), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4116) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4121), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4122) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4126), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4127) });

            migrationBuilder.UpdateData(
                table: "t_po_details",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4131), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4132) });

            migrationBuilder.UpdateData(
                table: "t_po_headers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DeliveryDate", "PODate", "PostingDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4045), new DateTime(2025, 7, 13, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4033), new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4026), new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4028), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4045) });

            migrationBuilder.UpdateData(
                table: "t_po_headers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DeliveryDate", "PODate", "PostingDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4071), new DateTime(2025, 7, 20, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4066), new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4061), new DateTime(2025, 7, 6, 8, 25, 46, 523, DateTimeKind.Local).AddTicks(4062), new DateTime(2025, 7, 6, 1, 25, 46, 523, DateTimeKind.Utc).AddTicks(4072) });
        }
    }
}
