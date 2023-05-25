using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCitybike.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FID = table.Column<int>(type: "INTEGER", nullable: false),
                    Nimi = table.Column<string>(type: "TEXT", nullable: true),
                    Namn = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Osoite = table.Column<string>(type: "TEXT", nullable: true),
                    Adress = table.Column<string>(type: "TEXT", nullable: true),
                    Kaupunki = table.Column<string>(type: "TEXT", nullable: true),
                    Stad = table.Column<string>(type: "TEXT", nullable: true),
                    Operaattor = table.Column<string>(type: "TEXT", nullable: true),
                    Kapasiteet = table.Column<int>(type: "INTEGER", nullable: false),
                    x = table.Column<decimal>(type: "TEXT", nullable: false),
                    y = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Station");
        }
    }
}
