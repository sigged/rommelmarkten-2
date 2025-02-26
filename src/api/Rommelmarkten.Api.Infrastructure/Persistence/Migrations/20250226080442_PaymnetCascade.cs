using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymnetCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Market_Province_ProvinceId",
                table: "Market");

            migrationBuilder.AddForeignKey(
                name: "FK_Market_Province_ProvinceId",
                table: "Market",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Market_Province_ProvinceId",
                table: "Market");

            migrationBuilder.AddForeignKey(
                name: "FK_Market_Province_ProvinceId",
                table: "Market",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
