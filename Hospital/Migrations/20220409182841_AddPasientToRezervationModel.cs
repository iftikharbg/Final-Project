using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddPasientToRezervationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PasientId",
                table: "Rezervations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervations_PasientId",
                table: "Rezervations",
                column: "PasientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervations_Pasients_PasientId",
                table: "Rezervations",
                column: "PasientId",
                principalTable: "Pasients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervations_Pasients_PasientId",
                table: "Rezervations");

            migrationBuilder.DropIndex(
                name: "IX_Rezervations_PasientId",
                table: "Rezervations");

            migrationBuilder.DropColumn(
                name: "PasientId",
                table: "Rezervations");
        }
    }
}
