using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class Init20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    actorId = table.Column<int>(nullable: false),
                    movieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => x.id);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_actorId",
                        column: x => x.actorId,
                        principalTable: "Actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_actorId",
                table: "ActorMovie",
                column: "actorId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_movieId",
                table: "ActorMovie",
                column: "movieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.AddColumn<int>(
                name: "Movieid",
                table: "Actors",
                type: "int",
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
    }
}
