using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingControl.Migrations
{
    /// <inheritdoc />
    public partial class addColumnTimeChargedHour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeChargedHour",
                table: "ParkingRegistrations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeChargedHour",
                table: "ParkingRegistrations");
        }
    }
}
