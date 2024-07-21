using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF8Complex.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    MoveId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Names_Chinese = table.Column<string>(type: "TEXT", nullable: false),
                    Names_English = table.Column<string>(type: "TEXT", nullable: false),
                    Names_Japanese = table.Column<string>(type: "TEXT", nullable: false),
                    Names_NameDetail_NameDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Names_NameDetail_NameType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.MoveId);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Names_Chinese = table.Column<string>(type: "TEXT", nullable: false),
                    Names_English = table.Column<string>(type: "TEXT", nullable: false),
                    Names_Japanese = table.Column<string>(type: "TEXT", nullable: false),
                    Names_NameDetail_NameDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Names_NameDetail_NameType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.PokemonId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
