using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpfRegistration.EntityFramework.Migrations
{
    public partial class day31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TracerDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TracerDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TracerDetails", x => x.Id);
                });
        }
    }
}
