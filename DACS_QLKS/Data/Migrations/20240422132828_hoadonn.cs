using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_QLKS.Data.Migrations
{
    /// <inheritdoc />
    public partial class hoadonn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_bookings_PhongId",
                table: "HoaDons");

            migrationBuilder.DropIndex(
                name: "IX_HoaDons_PhongId",
                table: "HoaDons");

            migrationBuilder.DropColumn(
                name: "PhongId",
                table: "HoaDons");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_BookingId",
                table: "HoaDons",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_bookings_BookingId",
                table: "HoaDons",
                column: "BookingId",
                principalTable: "bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_bookings_BookingId",
                table: "HoaDons");

            migrationBuilder.DropIndex(
                name: "IX_HoaDons_BookingId",
                table: "HoaDons");

            migrationBuilder.AddColumn<int>(
                name: "PhongId",
                table: "HoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_PhongId",
                table: "HoaDons",
                column: "PhongId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_bookings_PhongId",
                table: "HoaDons",
                column: "PhongId",
                principalTable: "bookings",
                principalColumn: "Id");
        }
    }
}
