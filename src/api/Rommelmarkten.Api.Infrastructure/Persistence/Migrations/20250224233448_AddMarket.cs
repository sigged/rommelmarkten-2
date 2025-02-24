using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMarket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrlSlug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Market",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_V1 = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BannerTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Pricing_EntryPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Pricing_StandPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Location_Hall = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_CoordLatitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_CoordLongitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_ImageThumbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organizer_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organizer_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organizer_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organizer_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organizer_ContactNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organizer_ShowName = table.Column<bool>(type: "bit", nullable: false),
                    Organizer_ShowPhone = table.Column<bool>(type: "bit", nullable: false),
                    Organizer_ShowEmail = table.Column<bool>(type: "bit", nullable: false),
                    DateLastAdminUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastUserUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Market", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Market_BannerTypes_BannerTypeId",
                        column: x => x.BannerTypeId,
                        principalTable: "BannerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Market_MarketConfigurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "MarketConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Market_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketDate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentMarketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartHour = table.Column<short>(type: "smallint", nullable: false),
                    StartMinutes = table.Column<short>(type: "smallint", nullable: false),
                    StopHour = table.Column<short>(type: "smallint", nullable: false),
                    StopMinutes = table.Column<short>(type: "smallint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketDate_Market_ParentMarketId",
                        column: x => x.ParentMarketId,
                        principalTable: "Market",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketWithTheme",
                columns: table => new
                {
                    MarketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    MarketsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketWithTheme", x => new { x.MarketId, x.ThemeId });
                    table.ForeignKey(
                        name: "FK_MarketWithTheme_MarketThemes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "MarketThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketWithTheme_MarketThemes_ThemesId",
                        column: x => x.ThemesId,
                        principalTable: "MarketThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketWithTheme_Market_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Market",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketWithTheme_Market_MarketsId",
                        column: x => x.MarketsId,
                        principalTable: "Market",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Market_BannerTypeId",
                table: "Market",
                column: "BannerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Market_ConfigurationId",
                table: "Market",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Market_ProvinceId",
                table: "Market",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketDate_ParentMarketId",
                table: "MarketDate",
                column: "ParentMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketWithTheme_MarketsId",
                table: "MarketWithTheme",
                column: "MarketsId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketWithTheme_ThemeId",
                table: "MarketWithTheme",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketWithTheme_ThemesId",
                table: "MarketWithTheme",
                column: "ThemesId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_Code",
                table: "Province",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketDate");

            migrationBuilder.DropTable(
                name: "MarketWithTheme");

            migrationBuilder.DropTable(
                name: "Market");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
