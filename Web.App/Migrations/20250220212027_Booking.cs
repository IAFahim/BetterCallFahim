using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.App.Migrations
{
    /// <inheritdoc />
    public partial class Booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepeatOption = table.Column<int>(type: "int", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaysToRepeatOn = table.Column<int>(type: "int", nullable: true),
                    RequestedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
