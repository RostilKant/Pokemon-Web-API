using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class dropDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09a567ac-dd71-4097-a721-9cfdbf1a3d6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a493e651-0216-4c42-8bee-b0d565aaf5e7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c56d53ae-5ae1-4a8d-aa64-ee5b46209c13", "9a4e807c-e7a5-4cd1-9e67-0b5be6518a2d", "Manager", "MANAGER" },
                    { "0c47696f-df60-4007-b1f4-c82cefc3ef2a", "d40c2d42-c2eb-49ed-a6b4-fad7f888bb97", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "a493e651-0216-4c42-8bee-b0d565aaf5e7", "7cc01a7e-4e41-4d4a-b527-29caf32e2294", "Manager", "MANAGER" },
                    { "09a567ac-dd71-4097-a721-9cfdbf1a3d6f", "a280fa31-baa3-412f-b6f4-e469b261d02c", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
