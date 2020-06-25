using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pokemon_Web_API.Migrations
{
    public partial class Initial004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Pokemons_Type",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_Type",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Types");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Types",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Types",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "Types",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Pokemons_PokemonId",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_PokemonId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Types");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Types",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Types",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Types_Type",
                table: "Types",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Pokemons_Type",
                table: "Types",
                column: "Type",
                principalTable: "Pokemons",
                principalColumn: "PokemonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
