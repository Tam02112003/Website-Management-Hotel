using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_QLKS.Data.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DichVus_HoaDons_HoaDonId",
                table: "DichVus");

            migrationBuilder.DropIndex(
                name: "IX_DichVus_HoaDonId",
                table: "DichVus");

            migrationBuilder.DropColumn(
                name: "HoaDonId",
                table: "DichVus");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "serviceOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_serviceOrders_BookingId",
                table: "serviceOrders",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_serviceOrders_bookings_BookingId",
                table: "serviceOrders",
                column: "BookingId",
                principalTable: "bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_serviceOrders_bookings_BookingId",
                table: "serviceOrders");

            migrationBuilder.DropIndex(
                name: "IX_serviceOrders_BookingId",
                table: "serviceOrders");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "serviceOrders");

            migrationBuilder.AddColumn<int>(
                name: "HoaDonId",
                table: "DichVus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DichVus_HoaDonId",
                table: "DichVus",
                column: "HoaDonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DichVus_HoaDons_HoaDonId",
                table: "DichVus",
                column: "HoaDonId",
                principalTable: "HoaDons",
                principalColumn: "Id");
        }
    }
}
