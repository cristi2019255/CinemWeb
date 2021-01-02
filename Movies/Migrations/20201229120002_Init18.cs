using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class Init18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Movieid",
                table: "Actors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Movieid",
                table: "Actors",
                column: "Movieid");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_Movieid",
                table: "Actors",
                column: "Movieid",
                principalTable: "Movies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_Movieid",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_Movieid",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Movieid",
                table: "Actors");
        }
    }
}
