using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class Initial003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonType");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Types",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Pokemons_Type",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_Type",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Types");

            migrationBuilder.CreateTable(
                name: "PokemonType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonType", x => new { x.Id, x.Name });
                    table.ForeignKey(
                        name: "FK_PokemonType_Pokemons_Id",
                        column: x => x.Id,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonType_Types_Name",
                        column: x => x.Name,
                        principalTable: "Types",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonType_Name",
                table: "PokemonType",
                column: "Name");
        }
    }
}
