using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon_Web_API.Migrations
{
    public partial class InitialM2M001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PokemonType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonType");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Types",
                type: "integer",
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
    }
}
