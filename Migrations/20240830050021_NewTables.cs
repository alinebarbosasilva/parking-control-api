using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingControl.Migrations
{
    /// <inheritdoc />
    public partial class NewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutDate",
                table: "ParkingRegistrations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "PricesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ValidityStartPeriod = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidityFinalPeriod = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InitialTimeValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    AdditionalHourlyValue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricesTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricesTable");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutDate",
                table: "ParkingRegistrations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
