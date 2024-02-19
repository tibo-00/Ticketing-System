using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing_System.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildPrice = table.Column<double>(type: "float", nullable: false),
                    AdultPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConcertHall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertHall", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcertHallId = table.Column<int>(type: "int", nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: false),
                    NumberOfAdults = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    PerformanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ConcertHall",
                columns: new[] { "Id", "Name", "NumberOfSeats" },
                values: new object[,]
                {
                    { 1, "Mozart", 200 },
                    { 2, "Beethoven", 500 },
                    { 3, "Vivaldi", 750 },
                    { 4, "Bach", 400 },
                    { 5, "Chopin", 50 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concert");

            migrationBuilder.DropTable(
                name: "ConcertHall");

            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
