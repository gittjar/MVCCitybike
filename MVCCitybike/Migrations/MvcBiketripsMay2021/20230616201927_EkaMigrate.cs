using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCitybike.Migrations.MvcBiketripsMay2021
{
    /// <inheritdoc />
    public partial class EkaMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiketripsMay2021",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Departure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Return = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Departure_station_id = table.Column<int>(type: "int", nullable: false),
                    Departure_station_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Return_station_id = table.Column<int>(type: "int", nullable: false),
                    Return_station_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Covered_distance_m = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration_sec = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiketripsMay2021", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiketripsMay2021");
        }
    }
}
