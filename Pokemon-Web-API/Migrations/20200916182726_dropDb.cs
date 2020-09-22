using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class dropDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d4bc948-0717-4dd8-96eb-e0aa6af025d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56b2ec0d-6655-4c8e-bf9b-64964eee35b0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "af442955-d0dc-4076-9107-ef055c1ca1df", "31734519-eb40-41fa-9305-b20ae42a7fe7", "Manager", "MANAGER" },
                    { "9d8f3faf-6e6d-42cb-aab0-8eec6b468576", "d04d58f4-64a0-4533-81c9-bcecd4a86024", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d8f3faf-6e6d-42cb-aab0-8eec6b468576");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af442955-d0dc-4076-9107-ef055c1ca1df");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d4bc948-0717-4dd8-96eb-e0aa6af025d8", "20832037-3207-4ee3-ac38-dc23ebbf549e", "Manager", "MANAGER" },
                    { "56b2ec0d-6655-4c8e-bf9b-64964eee35b0", "7adfcd01-aef1-444f-8c6d-619a5db871fb", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
