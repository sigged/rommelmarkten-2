using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Revisions0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Market_BannerTypes_BannerTypeId",
                table: "Market");

            migrationBuilder.DropForeignKey(
                name: "FK_Market_MarketConfigurations_ConfigurationId",
                table: "Market");

            migrationBuilder.DropForeignKey(
                name: "FK_Market_Province_ProvinceId",
                table: "Market");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketDate_Market_ParentMarketId",
                table: "MarketDate");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketPayment_Market_MarketId",
                table: "MarketPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_Market_MarketId",
                table: "MarketWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_Market_MarketsId",
                table: "MarketWithTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Market",
                table: "Market");

            migrationBuilder.RenameTable(
                name: "Market",
                newName: "Markets");

            migrationBuilder.RenameIndex(
                name: "IX_Market_ProvinceId",
                table: "Markets",
                newName: "IX_Markets_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Market_ConfigurationId",
                table: "Markets",
                newName: "IX_Markets_ConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_Market_BannerTypeId",
                table: "Markets",
                newName: "IX_Markets_BannerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Markets",
                table: "Markets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketDate_Markets_ParentMarketId",
                table: "MarketDate",
                column: "ParentMarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketPayment_Markets_MarketId",
                table: "MarketPayment",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_BannerTypes_BannerTypeId",
                table: "Markets",
                column: "BannerTypeId",
                principalTable: "BannerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_MarketConfigurations_ConfigurationId",
                table: "Markets",
                column: "ConfigurationId",
                principalTable: "MarketConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_Province_ProvinceId",
                table: "Markets",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithTheme_Markets_MarketId",
                table: "MarketWithTheme",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithTheme_Markets_MarketsId",
                table: "MarketWithTheme",
                column: "MarketsId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketDate_Markets_ParentMarketId",
                table: "MarketDate");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketPayment_Markets_MarketId",
                table: "MarketPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_Markets_BannerTypes_BannerTypeId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_Markets_MarketConfigurations_ConfigurationId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_Markets_Province_ProvinceId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_Markets_MarketId",
                table: "MarketWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_Markets_MarketsId",
                table: "MarketWithTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Markets",
                table: "Markets");

            migrationBuilder.RenameTable(
                name: "Markets",
                newName: "Market");

            migrationBuilder.RenameIndex(
                name: "IX_Markets_ProvinceId",
                table: "Market",
                newName: "IX_Market_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Markets_ConfigurationId",
                table: "Market",
                newName: "IX_Market_ConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_Markets_BannerTypeId",
                table: "Market",
                newName: "IX_Market_BannerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Market",
                table: "Market",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Market_BannerTypes_BannerTypeId",
                table: "Market",
                column: "BannerTypeId",
                principalTable: "BannerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Market_MarketConfigurations_ConfigurationId",
                table: "Market",
                column: "ConfigurationId",
                principalTable: "MarketConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Market_Province_ProvinceId",
                table: "Market",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketDate_Market_ParentMarketId",
                table: "MarketDate",
                column: "ParentMarketId",
                principalTable: "Market",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketPayment_Market_MarketId",
                table: "MarketPayment",
                column: "MarketId",
                principalTable: "Market",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithTheme_Market_MarketId",
                table: "MarketWithTheme",
                column: "MarketId",
                principalTable: "Market",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithTheme_Market_MarketsId",
                table: "MarketWithTheme",
                column: "MarketsId",
                principalTable: "Market",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
