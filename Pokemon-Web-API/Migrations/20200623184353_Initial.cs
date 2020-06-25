using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pokemon_Web_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_Pokemons_PokemonId",
                table: "Type");

            migrationBuilder.DropTable(
                name: "EncountersArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Type_PokemonId",
                table: "Type");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Type");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pokemons",
                newName: "PokemonId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Types",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Types",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Name");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_Id",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: "Pokemons",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Type",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Type",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "Type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EncountersArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PokemonId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncountersArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncountersArea_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Type_PokemonId",
                table: "Type",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_EncountersArea_PokemonId",
                table: "EncountersArea",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Pokemons_PokemonId",
                table: "Type",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
