using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_QLKS.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DichVus");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "DichVus",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "serviceOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DichVuId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_serviceOrders_DichVus_DichVuId",
                        column: x => x.DichVuId,
                        principalTable: "DichVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_serviceOrders_DichVuId",
                table: "serviceOrders",
                column: "DichVuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceOrders");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "DichVus");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "DichVus",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
