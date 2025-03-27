using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_QLKS.Data.Migrations
{
    /// <inheritdoc />
    public partial class up1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxOccupancy",
                table: "LoaiPhongs");

            migrationBuilder.AddColumn<int>(
                name: "MaxOccupancy",
                table: "phongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "phongs",
                type: "nvarchar(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxOccupancy",
                table: "phongs");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "phongs");

            migrationBuilder.AddColumn<int>(
                name: "MaxOccupancy",
                table: "LoaiPhongs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
