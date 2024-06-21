using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyInventorySystem.Migrations
{
    /// <inheritdoc />
    public partial class DbInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SoldProperties",
                keyColumns: new[] { "ContactId", "PropertyId" },
                keyValues: new object[] { new Guid("74919c93-0a43-4a7d-a85e-04712af77d47"), new Guid("0c8beea9-4db7-41bb-9e39-f5a30abc9f32") });

            migrationBuilder.DeleteData(
                table: "SoldProperties",
                keyColumns: new[] { "ContactId", "PropertyId" },
                keyValues: new object[] { new Guid("2ec8ed47-4281-422e-9173-6c334239c9ae"), new Guid("4cec5cdc-830f-42f6-8a13-cd761b76b6e2") });

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("2ec8ed47-4281-422e-9173-6c334239c9ae"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("74919c93-0a43-4a7d-a85e-04712af77d47"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("0c8beea9-4db7-41bb-9e39-f5a30abc9f32"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("4cec5cdc-830f-42f6-8a13-cd761b76b6e2"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "SoldProperties",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Properties",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("033367b4-2a20-4c05-bec1-daf35cbadd88"), "jane.smith@example.com", "Jane", "Smith", "987654321" },
                    { new Guid("a1f55e64-d1a1-4fe7-80b2-bac0c18a109a"), "john.doe@example.com", "John", "Doe", "123456789" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "DateOfRegistration", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("9ead71af-ee38-415c-a160-6f27bf7cfe38"), "123 Main St", new DateTime(2024, 6, 21, 10, 18, 23, 725, DateTimeKind.Local).AddTicks(6937), "Maisonette", 130000m },
                    { new Guid("ad46dc38-d03b-455d-b91f-5e494d3c03ee"), "456 Elm St", new DateTime(2024, 6, 21, 10, 18, 23, 725, DateTimeKind.Local).AddTicks(6998), "Penthouse", 430000m }
                });

            migrationBuilder.InsertData(
                table: "SoldProperties",
                columns: new[] { "ContactId", "PropertyId", "AcquisitionPrice", "DateOfRegistration", "EffectiveTill" },
                values: new object[,]
                {
                    { new Guid("a1f55e64-d1a1-4fe7-80b2-bac0c18a109a"), new Guid("9ead71af-ee38-415c-a160-6f27bf7cfe38"), 120000m, new DateTime(2024, 5, 22, 10, 18, 23, 725, DateTimeKind.Local).AddTicks(7033), null },
                    { new Guid("033367b4-2a20-4c05-bec1-daf35cbadd88"), new Guid("ad46dc38-d03b-455d-b91f-5e494d3c03ee"), 420000m, new DateTime(2024, 6, 6, 10, 18, 23, 725, DateTimeKind.Local).AddTicks(7039), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SoldProperties",
                keyColumns: new[] { "ContactId", "PropertyId" },
                keyValues: new object[] { new Guid("a1f55e64-d1a1-4fe7-80b2-bac0c18a109a"), new Guid("9ead71af-ee38-415c-a160-6f27bf7cfe38") });

            migrationBuilder.DeleteData(
                table: "SoldProperties",
                keyColumns: new[] { "ContactId", "PropertyId" },
                keyValues: new object[] { new Guid("033367b4-2a20-4c05-bec1-daf35cbadd88"), new Guid("ad46dc38-d03b-455d-b91f-5e494d3c03ee") });

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("033367b4-2a20-4c05-bec1-daf35cbadd88"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("a1f55e64-d1a1-4fe7-80b2-bac0c18a109a"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("9ead71af-ee38-415c-a160-6f27bf7cfe38"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("ad46dc38-d03b-455d-b91f-5e494d3c03ee"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "SoldProperties",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("2ec8ed47-4281-422e-9173-6c334239c9ae"), "jane.smith@example.com", "Jane", "Smith", "987654321" },
                    { new Guid("74919c93-0a43-4a7d-a85e-04712af77d47"), "john.doe@example.com", "John", "Doe", "123456789" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "DateOfRegistration", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0c8beea9-4db7-41bb-9e39-f5a30abc9f32"), "123 Main St", new DateTime(2024, 6, 18, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4574), "Maisonette", 130000m },
                    { new Guid("4cec5cdc-830f-42f6-8a13-cd761b76b6e2"), "456 Elm St", new DateTime(2024, 6, 18, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4635), "Penthouse", 430000m }
                });

            migrationBuilder.InsertData(
                table: "SoldProperties",
                columns: new[] { "ContactId", "PropertyId", "AcquisitionPrice", "DateOfRegistration", "EffectiveTill" },
                values: new object[,]
                {
                    { new Guid("74919c93-0a43-4a7d-a85e-04712af77d47"), new Guid("0c8beea9-4db7-41bb-9e39-f5a30abc9f32"), 120000m, new DateTime(2024, 5, 19, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4678), null },
                    { new Guid("2ec8ed47-4281-422e-9173-6c334239c9ae"), new Guid("4cec5cdc-830f-42f6-8a13-cd761b76b6e2"), 420000m, new DateTime(2024, 6, 3, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4684), null }
                });
        }
    }
}
