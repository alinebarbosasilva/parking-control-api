using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingControl.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialTimeValue",
                table: "PricesTable",
                newName: "InitialHourValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialHourValue",
                table: "PricesTable",
                newName: "InitialTimeValue");
        }
    }
}
