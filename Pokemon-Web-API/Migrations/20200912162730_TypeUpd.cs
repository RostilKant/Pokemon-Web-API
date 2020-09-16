using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class TypeUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da6df2b4-009d-4596-b666-cdcd7477a4ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f106ad3f-7631-4f52-8ebc-7886320c5a8d");

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
                    { "3d4bc948-0717-4dd8-96eb-e0aa6af025d8", "20832037-3207-4ee3-ac38-dc23ebbf549e", "Manager", "MANAGER" },
                    { "56b2ec0d-6655-4c8e-bf9b-64964eee35b0", "7adfcd01-aef1-444f-8c6d-619a5db871fb", "Administrator", "ADMINISTRATOR" }
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
                keyValue: "3d4bc948-0717-4dd8-96eb-e0aa6af025d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56b2ec0d-6655-4c8e-bf9b-64964eee35b0");

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
                    { "da6df2b4-009d-4596-b666-cdcd7477a4ba", "c736479d-472f-48e8-93e1-5212662a337c", "Manager", "MANAGER" },
                    { "f106ad3f-7631-4f52-8ebc-7886320c5a8d", "6ab2444d-6e62-4855-b0dd-1155db755b52", "Administrator", "ADMINISTRATOR" }
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
