using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddRelationToPacientModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcedureId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ProcedureId",
                table: "Doctors",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Procedures_ProcedureId",
                table: "Doctors",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Procedures_ProcedureId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_ProcedureId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "Doctors");
        }
    }
}
