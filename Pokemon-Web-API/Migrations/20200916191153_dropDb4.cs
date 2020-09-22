using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class dropDb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "a493e651-0216-4c42-8bee-b0d565aaf5e7", "7cc01a7e-4e41-4d4a-b527-29caf32e2294", "Manager", "MANAGER" },
                    { "09a567ac-dd71-4097-a721-9cfdbf1a3d6f", "a280fa31-baa3-412f-b6f4-e469b261d02c", "Administrator", "ADMINISTRATOR" }
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09a567ac-dd71-4097-a721-9cfdbf1a3d6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a493e651-0216-4c42-8bee-b0d565aaf5e7");

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
    }
}
