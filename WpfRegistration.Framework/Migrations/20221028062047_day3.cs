using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpfRegistration.EntityFramework.Migrations
{
    public partial class day3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberofShots",
                table: "TracerDetails");

            migrationBuilder.DropColumn(
                name: "QrCode",
                table: "TracerDetails");

            migrationBuilder.DropColumn(
                name: "VaccineName",
                table: "TracerDetails");

            migrationBuilder.DropColumn(
                name: "isBoosterShot",
                table: "TracerDetails");

            migrationBuilder.DropColumn(
                name: "isVaccinated",
                table: "TracerDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFirstDose",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberofShots",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QrCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VaccineName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBoosterShot",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVaccinated",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFirstDose",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberofShots",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "QrCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VaccineName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isBoosterShot",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isVaccinated",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberofShots",
                table: "TracerDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QrCode",
                table: "TracerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VaccineName",
                table: "TracerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBoosterShot",
                table: "TracerDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVaccinated",
                table: "TracerDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
