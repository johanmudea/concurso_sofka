using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcursoSofka.Migrations
{
    public partial class fixhistorical : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historicals_Questions_IdQuestion",
                table: "Historicals");

            migrationBuilder.RenameColumn(
                name: "IdQuestion",
                table: "Historicals",
                newName: "IdAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_Historicals_IdQuestion",
                table: "Historicals",
                newName: "IX_Historicals_IdAnswer");

            migrationBuilder.AddForeignKey(
                name: "FK_Historicals_Answers_IdAnswer",
                table: "Historicals",
                column: "IdAnswer",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historicals_Answers_IdAnswer",
                table: "Historicals");

            migrationBuilder.RenameColumn(
                name: "IdAnswer",
                table: "Historicals",
                newName: "IdQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_Historicals_IdAnswer",
                table: "Historicals",
                newName: "IX_Historicals_IdQuestion");

            migrationBuilder.AddForeignKey(
                name: "FK_Historicals_Questions_IdQuestion",
                table: "Historicals",
                column: "IdQuestion",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
