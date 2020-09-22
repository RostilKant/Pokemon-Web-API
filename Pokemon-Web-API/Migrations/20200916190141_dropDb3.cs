using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class dropDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c03cc9e6-05d6-4062-bc37-b6f31a82568f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd928205-83fe-4438-9916-7ade7e7dea03");

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c0c818a-18cc-430a-a8e6-0197209ec407", "d8686d16-2067-42dd-918f-fc3e35ec79b0", "Manager", "MANAGER" },
                    { "06b41a55-08d0-4b77-8a52-77c08ff3b871", "8be5d9a4-5524-461f-a222-2e29457be47a", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06b41a55-08d0-4b77-8a52-77c08ff3b871");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c0c818a-18cc-430a-a8e6-0197209ec407");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c03cc9e6-05d6-4062-bc37-b6f31a82568f", "4d9979be-c8e3-4b8a-a3cb-9e4d9115b480", "Manager", "MANAGER" },
                    { "cd928205-83fe-4438-9916-7ade7e7dea03", "d0927450-3c2f-4ed6-a20a-001fcf633a51", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "PokemonId", "Height", "Name", "Weight" },
                values: new object[] { 1, 64, "bulbasaur", 22 });

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
