using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class Init35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dateIn",
                table: "CinemaMovie",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateOut",
                table: "CinemaMovie",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "CinemaMovie",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SeatDate",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seatId = table.Column<int>(nullable: false),
                    DateIn = table.Column<DateTime>(nullable: false),
                    DateOut = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatDate", x => x.id);
                    table.ForeignKey(
                        name: "FK_SeatDate_Seats_seatId",
                        column: x => x.seatId,
                        principalTable: "Seats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatDate_seatId",
                table: "SeatDate",
                column: "seatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatDate");

            migrationBuilder.DropColumn(
                name: "dateIn",
                table: "CinemaMovie");

            migrationBuilder.DropColumn(
                name: "dateOut",
                table: "CinemaMovie");

            migrationBuilder.DropColumn(
                name: "id",
                table: "CinemaMovie");
        }
    }
}
