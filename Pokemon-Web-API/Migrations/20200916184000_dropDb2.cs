using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class dropDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00462904-40ff-4ebc-ae57-6af31642a18a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "780de9e3-ffdf-40a6-bffa-49447fd37d59");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c03cc9e6-05d6-4062-bc37-b6f31a82568f", "4d9979be-c8e3-4b8a-a3cb-9e4d9115b480", "Manager", "MANAGER" },
                    { "cd928205-83fe-4438-9916-7ade7e7dea03", "d0927450-3c2f-4ed6-a20a-001fcf633a51", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c03cc9e6-05d6-4062-bc37-b6f31a82568f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd928205-83fe-4438-9916-7ade7e7dea03");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "780de9e3-ffdf-40a6-bffa-49447fd37d59", "18f194ed-dea3-48b5-98a9-883dfa8338eb", "Manager", "MANAGER" },
                    { "00462904-40ff-4ebc-ae57-6af31642a18a", "1d916d45-a598-4304-b738-613230dacb3c", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
