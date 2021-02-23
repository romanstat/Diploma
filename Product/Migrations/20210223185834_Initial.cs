using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Product.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassedTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Balls = table.Column<string>(nullable: true),
                    Assessment = table.Column<string>(nullable: true),
                    PassedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassedTests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassedTests");
        }
    }
}
