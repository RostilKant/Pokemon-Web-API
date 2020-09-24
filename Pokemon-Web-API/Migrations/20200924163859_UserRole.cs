using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class UserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c47696f-df60-4007-b1f4-c82cefc3ef2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c56d53ae-5ae1-4a8d-aa64-ee5b46209c13");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "c56d53ae-5ae1-4a8d-aa64-ee5b46209c13", "9a4e807c-e7a5-4cd1-9e67-0b5be6518a2d", "Manager", "MANAGER" },
                    { "0c47696f-df60-4007-b1f4-c82cefc3ef2a", "d40c2d42-c2eb-49ed-a6b4-fad7f888bb97", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
