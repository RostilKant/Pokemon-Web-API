using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class dropDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d8f3faf-6e6d-42cb-aab0-8eec6b468576");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af442955-d0dc-4076-9107-ef055c1ca1df");

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "780de9e3-ffdf-40a6-bffa-49447fd37d59", "18f194ed-dea3-48b5-98a9-883dfa8338eb", "Manager", "MANAGER" },
                    { "00462904-40ff-4ebc-ae57-6af31642a18a", "1d916d45-a598-4304-b738-613230dacb3c", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name", "PokemonId" },
                values: new object[,]
                {
                    { 1, "grass", 1 },
                    { 2, "poison", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00462904-40ff-4ebc-ae57-6af31642a18a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "780de9e3-ffdf-40a6-bffa-49447fd37d59");

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "af442955-d0dc-4076-9107-ef055c1ca1df", "31734519-eb40-41fa-9305-b20ae42a7fe7", "Manager", "MANAGER" },
                    { "9d8f3faf-6e6d-42cb-aab0-8eec6b468576", "d04d58f4-64a0-4533-81c9-bcecd4a86024", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name", "PokemonId" },
                values: new object[,]
                {
                    { 1, "grass", 1 },
                    { 2, "poison", 1 }
                });
        }
    }
}
