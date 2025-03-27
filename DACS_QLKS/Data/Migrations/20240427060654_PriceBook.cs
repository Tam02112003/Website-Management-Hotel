using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_QLKS.Data.Migrations
{
    /// <inheritdoc />
    public partial class PriceBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_serviceOrders_bookings_BookingId",
                table: "serviceOrders");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "serviceOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomPrice",
                table: "bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                table: "bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_serviceOrders_bookings_BookingId",
                table: "serviceOrders",
                column: "BookingId",
                principalTable: "bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_serviceOrders_bookings_BookingId",
                table: "serviceOrders");

            migrationBuilder.DropColumn(
                name: "RoomPrice",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "bookings");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "serviceOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_serviceOrders_bookings_BookingId",
                table: "serviceOrders",
                column: "BookingId",
                principalTable: "bookings",
                principalColumn: "Id");
        }
    }
}
