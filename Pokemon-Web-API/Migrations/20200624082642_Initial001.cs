using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class Initial001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Pokemons_PokemonId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_PokemonId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Types");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Types",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Types_Id",
                table: "Types",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Pokemons_Id",
                table: "Types",
                column: "Id",
                principalTable: "Pokemons",
                principalColumn: "PokemonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Pokemons_Id",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_Id",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Types");

            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "Types",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Types_PokemonId",
                table: "Types",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Pokemons_PokemonId",
                table: "Types",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "PokemonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
