using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class RemoveDoctorFromRezervation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervations_Doctors_DoctorId",
                table: "Rezervations");

            migrationBuilder.DropIndex(
                name: "IX_Rezervations_DoctorId",
                table: "Rezervations");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Rezervations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Rezervations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervations_DoctorId",
                table: "Rezervations",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervations_Doctors_DoctorId",
                table: "Rezervations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
