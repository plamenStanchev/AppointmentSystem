using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentSystem.Infrastructure.Data.Migrations
{
    public partial class IdentityPinTransef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PIN",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PIN",
                table: "Patients",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Patients",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PIN",
                table: "Doctors",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PIN",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PIN",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "PIN",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
