using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCitybike.Migrations.MvcBiketripsMay2021
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiketripsMay2021",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Departure = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Return = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Departure_station_id = table.Column<int>(type: "INTEGER", nullable: false),
                    Departure_station_name = table.Column<string>(type: "TEXT", nullable: true),
                    Return_station_id = table.Column<int>(type: "INTEGER", nullable: false),
                    Return_station_name = table.Column<string>(type: "TEXT", nullable: true),
                    Covered_distance_m = table.Column<decimal>(type: "TEXT", nullable: false),
                    Duration_sec = table.Column<int>(type: "INTEGER", nullable: false)
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
