using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentSystem.Infrastructure.Data.Migrations
{
    public partial class SpellingMistakeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FistName",
                table: "Patients",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "FistName",
                table: "Doctors",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Patients",
                newName: "FistName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Doctors",
                newName: "FistName");
        }
    }
}
