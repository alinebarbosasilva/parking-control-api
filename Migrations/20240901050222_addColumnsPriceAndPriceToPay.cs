using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingControl.Migrations
{
    /// <inheritdoc />
    public partial class addColumnsPriceAndPriceToPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceToPay",
                table: "ParkingRegistrations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceToPay",
                table: "ParkingRegistrations");
        }
    }
}
