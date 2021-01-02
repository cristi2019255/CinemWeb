using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class Init31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "Movies",
                newName: "Image");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_cinemaId",
                table: "Adresses",
                column: "cinemaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Cinemas_cinemaId",
                table: "Adresses",
                column: "cinemaId",
                principalTable: "Cinemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Cinemas_cinemaId",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_cinemaId",
                table: "Adresses");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Movies",
                newName: "image");
        }
    }
}
