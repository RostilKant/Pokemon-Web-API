using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class InitialCreateDeepin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "472239f9-e140-4aa8-9a39-2639329a9159");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc433d91-1cc5-4aed-b23c-7d32c0118070");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0d0378d-f683-4fcd-9d51-af2b7cef2203");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ccf028e-66d7-4f70-9284-a5657acba08e", "ad5d8f54-be24-413d-9a48-84361d2bd8ee", "Manager", "MANAGER" },
                    { "829e5a44-592e-463a-9d23-665fde5820ad", "d1edb0b1-d4b1-4c75-bf4c-ff51e1c9d76f", "Administrator", "ADMINISTRATOR" },
                    { "f66345cb-2a6c-4c1d-88bf-aae2909f97fc", "1aa8be76-9e83-4e21-aed6-47adc19825d6", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "829e5a44-592e-463a-9d23-665fde5820ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ccf028e-66d7-4f70-9284-a5657acba08e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f66345cb-2a6c-4c1d-88bf-aae2909f97fc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e0d0378d-f683-4fcd-9d51-af2b7cef2203", "75e0f104-acca-48ee-87ba-60e9c2fced7a", "Manager", "MANAGER" },
                    { "dc433d91-1cc5-4aed-b23c-7d32c0118070", "1338dda4-a5c8-4760-98a2-6b5fd1eb20f9", "Administrator", "ADMINISTRATOR" },
                    { "472239f9-e140-4aa8-9a39-2639329a9159", "98a0f52e-4fcf-4596-a4ae-58487414558b", "User", "USER" }
                });
        }
    }
}
