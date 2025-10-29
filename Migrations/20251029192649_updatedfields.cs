using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class updatedfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Stocks_stockId",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "stockId",
                table: "Portfolios",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_stockId",
                table: "Portfolios",
                newName: "IX_Portfolios_StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Stocks_StockId",
                table: "Portfolios",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Stocks_StockId",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "Portfolios",
                newName: "stockId");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_StockId",
                table: "Portfolios",
                newName: "IX_Portfolios_stockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Stocks_stockId",
                table: "Portfolios",
                column: "stockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
