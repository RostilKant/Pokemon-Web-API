using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class AddingRolestoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9066ddce-9dde-4c69-8482-a520a5412893", "187c3d8a-b7b2-4aa5-8ec1-a99919650b1a", "Manager", "MANAGER" },
                    { "cca8f961-d7c6-4add-adef-3b496eb31768", "78d380ea-c146-401e-90ea-c17636c2d438", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9066ddce-9dde-4c69-8482-a520a5412893");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cca8f961-d7c6-4add-adef-3b496eb31768");
        }
    }
}
