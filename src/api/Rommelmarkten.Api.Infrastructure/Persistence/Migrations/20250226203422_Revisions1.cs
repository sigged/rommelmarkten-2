using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Revisions1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketRevision",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RevisedMarketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location_Hall = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_CoordLatitude = table.Column<double>(type: "float", maxLength: 100, nullable: true),
                    Location_CoordLongitude = table.Column<double>(type: "float", nullable: true),
                    Image_ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Image_ImageThumbUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Organizer_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Organizer_Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Organizer_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Organizer_URL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Organizer_ContactNotes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Organizer_ShowName = table.Column<bool>(type: "bit", nullable: false),
                    Organizer_ShowPhone = table.Column<bool>(type: "bit", nullable: false),
                    Organizer_ShowEmail = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketRevision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketRevision_Markets_RevisedMarketId",
                        column: x => x.RevisedMarketId,
                        principalTable: "Markets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MarketRevisionWithTheme",
                columns: table => new
                {
                    MarketRevisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    MarketRevisionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketRevisionWithTheme", x => new { x.MarketRevisionId, x.ThemeId });
                    table.ForeignKey(
                        name: "FK_MarketRevisionWithTheme_MarketRevision_MarketRevisionId",
                        column: x => x.MarketRevisionId,
                        principalTable: "MarketRevision",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketRevisionWithTheme_MarketRevision_MarketRevisionsId",
                        column: x => x.MarketRevisionsId,
                        principalTable: "MarketRevision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketRevisionWithTheme_MarketThemes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "MarketThemes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketRevisionWithTheme_MarketThemes_ThemesId",
                        column: x => x.ThemesId,
                        principalTable: "MarketThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketRevision_RevisedMarketId",
                table: "MarketRevision",
                column: "RevisedMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRevisionWithTheme_MarketRevisionsId",
                table: "MarketRevisionWithTheme",
                column: "MarketRevisionsId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRevisionWithTheme_ThemeId",
                table: "MarketRevisionWithTheme",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRevisionWithTheme_ThemesId",
                table: "MarketRevisionWithTheme",
                column: "ThemesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketRevisionWithTheme");

            migrationBuilder.DropTable(
                name: "MarketRevision");
        }
    }
}
