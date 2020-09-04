using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class InitialAzureCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "da6df2b4-009d-4596-b666-cdcd7477a4ba", "c736479d-472f-48e8-93e1-5212662a337c", "Manager", "MANAGER" },
                    { "f106ad3f-7631-4f52-8ebc-7886320c5a8d", "6ab2444d-6e62-4855-b0dd-1155db755b52", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da6df2b4-009d-4596-b666-cdcd7477a4ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f106ad3f-7631-4f52-8ebc-7886320c5a8d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5941d792-cb65-4566-aa40-266dff91ddda", "872119b4-9b5b-4ea4-8e19-5e5ff4bce31b", "Manager", "MANAGER" },
                    { "d4a5d29f-3b8b-4310-96e1-93e42d187637", "a2e9023b-012e-4b11-9c93-5206a792bd1d", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
