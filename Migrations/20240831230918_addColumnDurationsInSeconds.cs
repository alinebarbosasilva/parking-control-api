using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingControl.Migrations
{
    /// <inheritdoc />
    public partial class addColumnDurationsInSeconds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationInSeconds",
                table: "ParkingRegistrations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ParkingRegistrations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInSeconds",
                table: "ParkingRegistrations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ParkingRegistrations");
        }
    }
}
