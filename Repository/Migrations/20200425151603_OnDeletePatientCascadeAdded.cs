using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class OnDeletePatientCascadeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_AspNetUsers_PatientId",
                table: "Consultation");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_AspNetUsers_PatientId",
                table: "Consultation",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_AspNetUsers_PatientId",
                table: "Consultation");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_AspNetUsers_PatientId",
                table: "Consultation",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
