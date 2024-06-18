using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyInventorySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForSoldProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("2f895467-8127-4a1c-97d3-a7dd0fcf3e73"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("dd69899f-5ba2-4b67-9e09-23e3607e85cb"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("0c0c898b-0bd5-4e39-869b-f6d2633e77ec"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("e28b1324-1081-472f-905c-4ef3191ab0a0"));

            migrationBuilder.DeleteData(
                table: "SoldProperties",
                keyColumns: new[] { "ContactId", "PropertyId" },
                keyValues: new object[] { new Guid("bfef41f8-eb7a-45c4-893c-0029a222e1b6"), new Guid("831bb3bb-98db-4e49-bf6f-17bdd50a8726") });

            migrationBuilder.DeleteData(
                table: "SoldProperties",
                keyColumns: new[] { "ContactId", "PropertyId" },
                keyValues: new object[] { new Guid("c3ebb52b-8c96-4279-949c-2710e9130af0"), new Guid("a473a311-5e3b-4a53-947b-27c3534c198c") });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("2f895467-8127-4a1c-97d3-a7dd0fcf3e73"), "john.doe@example.com", "John", "Doe", "123456789" },
                    { new Guid("dd69899f-5ba2-4b67-9e09-23e3607e85cb"), "jane.smith@example.com", "Jane", "Smith", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "DateOfRegistration", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0c0c898b-0bd5-4e39-869b-f6d2633e77ec"), "123 Main St", new DateTime(2024, 6, 18, 12, 28, 42, 130, DateTimeKind.Local).AddTicks(5326), "Maisonette", 130000m },
                    { new Guid("e28b1324-1081-472f-905c-4ef3191ab0a0"), "456 Elm St", new DateTime(2024, 6, 18, 12, 28, 42, 130, DateTimeKind.Local).AddTicks(5436), "Penthouse", 430000m }
                });

            migrationBuilder.InsertData(
                table: "SoldProperties",
                columns: new[] { "ContactId", "PropertyId", "AcquisitionPrice", "DateOfRegistration", "EffectiveTill" },
                values: new object[,]
                {
                    { new Guid("bfef41f8-eb7a-45c4-893c-0029a222e1b6"), new Guid("831bb3bb-98db-4e49-bf6f-17bdd50a8726"), 120000m, new DateTime(2024, 5, 19, 12, 28, 42, 130, DateTimeKind.Local).AddTicks(5476), null },
                    { new Guid("c3ebb52b-8c96-4279-949c-2710e9130af0"), new Guid("a473a311-5e3b-4a53-947b-27c3534c198c"), 420000m, new DateTime(2024, 6, 3, 12, 28, 42, 130, DateTimeKind.Local).AddTicks(5483), null }
                });
        }
    }
}
