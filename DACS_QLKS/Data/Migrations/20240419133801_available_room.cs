using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_QLKS.Data.Migrations
{
    /// <inheritdoc />
    public partial class available_room : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "phongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "phongs");
        }
    }
}
