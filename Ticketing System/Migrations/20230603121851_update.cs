using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing_System.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PerformanceId",
                table: "Reservation",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_ConcertHallId",
                table: "Performance",
                column: "ConcertHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_ConcertId",
                table: "Performance",
                column: "ConcertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Performance_Concert_ConcertId",
                table: "Performance",
                column: "ConcertId",
                principalTable: "Concert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Performance_ConcertHall_ConcertHallId",
                table: "Performance",
                column: "ConcertHallId",
                principalTable: "ConcertHall",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Performance_PerformanceId",
                table: "Reservation",
                column: "PerformanceId",
                principalTable: "Performance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Performance_Concert_ConcertId",
                table: "Performance");

            migrationBuilder.DropForeignKey(
                name: "FK_Performance_ConcertHall_ConcertHallId",
                table: "Performance");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Performance_PerformanceId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_PerformanceId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Performance_ConcertHallId",
                table: "Performance");

            migrationBuilder.DropIndex(
                name: "IX_Performance_ConcertId",
                table: "Performance");
        }
    }
}
