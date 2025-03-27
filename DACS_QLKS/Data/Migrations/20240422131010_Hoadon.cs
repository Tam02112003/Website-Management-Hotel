using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_QLKS.Data.Migrations
{
    /// <inheritdoc />
    public partial class Hoadon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "DichVus",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DichVus");
        }
    }
}
