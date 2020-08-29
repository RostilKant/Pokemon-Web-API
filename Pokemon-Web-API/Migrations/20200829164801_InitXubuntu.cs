using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class InitXubuntu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9066ddce-9dde-4c69-8482-a520a5412893");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cca8f961-d7c6-4add-adef-3b496eb31768");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5941d792-cb65-4566-aa40-266dff91ddda", "872119b4-9b5b-4ea4-8e19-5e5ff4bce31b", "Manager", "MANAGER" },
                    { "d4a5d29f-3b8b-4310-96e1-93e42d187637", "a2e9023b-012e-4b11-9c93-5206a792bd1d", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5941d792-cb65-4566-aa40-266dff91ddda");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4a5d29f-3b8b-4310-96e1-93e42d187637");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9066ddce-9dde-4c69-8482-a520a5412893", "187c3d8a-b7b2-4aa5-8ec1-a99919650b1a", "Manager", "MANAGER" },
                    { "cca8f961-d7c6-4add-adef-3b496eb31768", "78d380ea-c146-401e-90ea-c17636c2d438", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
