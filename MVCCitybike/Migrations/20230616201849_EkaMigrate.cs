using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCitybike.Migrations
{
    /// <inheritdoc />
    public partial class EkaMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FID = table.Column<int>(type: "int", nullable: false),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Citybike aseman nimi"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Osoite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kaupunki = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operaattor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kapasiteet = table.Column<int>(type: "int", nullable: false),
                    x = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    y = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Kuva = table.Column<byte>(type: "nvarchar(max)", nullable: false)
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
