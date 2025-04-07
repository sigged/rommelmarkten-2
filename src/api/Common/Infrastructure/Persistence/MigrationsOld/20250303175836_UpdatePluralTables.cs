using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePluralTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketDate_Markets_ParentMarketId",
                table: "MarketDate");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketInvoiceLine_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketInvoiceReminder_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceReminder");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketRevisions_MarketRevisionId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketRevisions_MarketRevisionsId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketThemes_ThemeId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketThemes_ThemesId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_Markets_Province_ProvinceId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_MarketThemes_ThemeId",
                table: "MarketWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_MarketThemes_ThemesId",
                table: "MarketWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_Markets_MarketId",
                table: "MarketWithTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithTheme_Markets_MarketsId",
                table: "MarketWithTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Province",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketWithTheme",
                table: "MarketWithTheme");

            migrationBuilder.DropIndex(
                name: "IX_MarketWithTheme_MarketsId",
                table: "MarketWithTheme");

            migrationBuilder.DropIndex(
                name: "IX_MarketWithTheme_ThemesId",
                table: "MarketWithTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketRevisionWithTheme",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropIndex(
                name: "IX_MarketRevisionWithTheme_MarketRevisionsId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropIndex(
                name: "IX_MarketRevisionWithTheme_ThemesId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketInvoiceReminder",
                table: "MarketInvoiceReminder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketInvoiceLine",
                table: "MarketInvoiceLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketDate",
                table: "MarketDate");

            migrationBuilder.DropColumn(
                name: "MarketsId",
                table: "MarketWithTheme");

            migrationBuilder.DropColumn(
                name: "ThemesId",
                table: "MarketWithTheme");

            migrationBuilder.DropColumn(
                name: "MarketRevisionsId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.DropColumn(
                name: "ThemesId",
                table: "MarketRevisionWithTheme");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "MarketWithTheme",
                newName: "MarketWithThemes");

            migrationBuilder.RenameTable(
                name: "MarketRevisionWithTheme",
                newName: "MarketRevisionsWithThemes");

            migrationBuilder.RenameTable(
                name: "MarketInvoiceReminder",
                newName: "MarketInvoiceReminders");

            migrationBuilder.RenameTable(
                name: "MarketInvoiceLine",
                newName: "MarketInvoiceLines");

            migrationBuilder.RenameTable(
                name: "MarketDate",
                newName: "MarketDates");

            migrationBuilder.RenameIndex(
                name: "IX_Province_Code",
                table: "Provinces",
                newName: "IX_Provinces_Code");

            migrationBuilder.RenameIndex(
                name: "IX_MarketWithTheme_ThemeId",
                table: "MarketWithThemes",
                newName: "IX_MarketWithThemes_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketRevisionWithTheme_ThemeId",
                table: "MarketRevisionsWithThemes",
                newName: "IX_MarketRevisionsWithThemes_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketInvoiceReminder_ParentInvoiceId",
                table: "MarketInvoiceReminders",
                newName: "IX_MarketInvoiceReminders_ParentInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketInvoiceLine_ParentInvoiceId",
                table: "MarketInvoiceLines",
                newName: "IX_MarketInvoiceLines_ParentInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketDate_ParentMarketId",
                table: "MarketDates",
                newName: "IX_MarketDates_ParentMarketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketWithThemes",
                table: "MarketWithThemes",
                columns: new[] { "MarketId", "ThemeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketRevisionsWithThemes",
                table: "MarketRevisionsWithThemes",
                columns: new[] { "MarketRevisionId", "ThemeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketInvoiceReminders",
                table: "MarketInvoiceReminders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketInvoiceLines",
                table: "MarketInvoiceLines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketDates",
                table: "MarketDates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketDates_Markets_ParentMarketId",
                table: "MarketDates",
                column: "ParentMarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketInvoiceLines_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceLines",
                column: "ParentInvoiceId",
                principalTable: "MarketInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketInvoiceReminders_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceReminders",
                column: "ParentInvoiceId",
                principalTable: "MarketInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketRevisionsWithThemes_MarketRevisions_MarketRevisionId",
                table: "MarketRevisionsWithThemes",
                column: "MarketRevisionId",
                principalTable: "MarketRevisions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketRevisionsWithThemes_MarketThemes_ThemeId",
                table: "MarketRevisionsWithThemes",
                column: "ThemeId",
                principalTable: "MarketThemes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_Provinces_ProvinceId",
                table: "Markets",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithThemes_MarketThemes_ThemeId",
                table: "MarketWithThemes",
                column: "ThemeId",
                principalTable: "MarketThemes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithThemes_Markets_MarketId",
                table: "MarketWithThemes",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketDates_Markets_ParentMarketId",
                table: "MarketDates");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketInvoiceLines_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketInvoiceReminders_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketRevisionsWithThemes_MarketRevisions_MarketRevisionId",
                table: "MarketRevisionsWithThemes");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketRevisionsWithThemes_MarketThemes_ThemeId",
                table: "MarketRevisionsWithThemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Markets_Provinces_ProvinceId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithThemes_MarketThemes_ThemeId",
                table: "MarketWithThemes");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketWithThemes_Markets_MarketId",
                table: "MarketWithThemes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketWithThemes",
                table: "MarketWithThemes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketRevisionsWithThemes",
                table: "MarketRevisionsWithThemes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketInvoiceReminders",
                table: "MarketInvoiceReminders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketInvoiceLines",
                table: "MarketInvoiceLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketDates",
                table: "MarketDates");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Province");

            migrationBuilder.RenameTable(
                name: "MarketWithThemes",
                newName: "MarketWithTheme");

            migrationBuilder.RenameTable(
                name: "MarketRevisionsWithThemes",
                newName: "MarketRevisionWithTheme");

            migrationBuilder.RenameTable(
                name: "MarketInvoiceReminders",
                newName: "MarketInvoiceReminder");

            migrationBuilder.RenameTable(
                name: "MarketInvoiceLines",
                newName: "MarketInvoiceLine");

            migrationBuilder.RenameTable(
                name: "MarketDates",
                newName: "MarketDate");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_Code",
                table: "Province",
                newName: "IX_Province_Code");

            migrationBuilder.RenameIndex(
                name: "IX_MarketWithThemes_ThemeId",
                table: "MarketWithTheme",
                newName: "IX_MarketWithTheme_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketRevisionsWithThemes_ThemeId",
                table: "MarketRevisionWithTheme",
                newName: "IX_MarketRevisionWithTheme_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketInvoiceReminders_ParentInvoiceId",
                table: "MarketInvoiceReminder",
                newName: "IX_MarketInvoiceReminder_ParentInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketInvoiceLines_ParentInvoiceId",
                table: "MarketInvoiceLine",
                newName: "IX_MarketInvoiceLine_ParentInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketDates_ParentMarketId",
                table: "MarketDate",
                newName: "IX_MarketDate_ParentMarketId");

            migrationBuilder.AddColumn<Guid>(
                name: "MarketsId",
                table: "MarketWithTheme",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ThemesId",
                table: "MarketWithTheme",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MarketRevisionsId",
                table: "MarketRevisionWithTheme",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ThemesId",
                table: "MarketRevisionWithTheme",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Province",
                table: "Province",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketWithTheme",
                table: "MarketWithTheme",
                columns: new[] { "MarketId", "ThemeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketRevisionWithTheme",
                table: "MarketRevisionWithTheme",
                columns: new[] { "MarketRevisionId", "ThemeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketInvoiceReminder",
                table: "MarketInvoiceReminder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketInvoiceLine",
                table: "MarketInvoiceLine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketDate",
                table: "MarketDate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MarketWithTheme_MarketsId",
                table: "MarketWithTheme",
                column: "MarketsId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketWithTheme_ThemesId",
                table: "MarketWithTheme",
                column: "ThemesId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRevisionWithTheme_MarketRevisionsId",
                table: "MarketRevisionWithTheme",
                column: "MarketRevisionsId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRevisionWithTheme_ThemesId",
                table: "MarketRevisionWithTheme",
                column: "ThemesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketDate_Markets_ParentMarketId",
                table: "MarketDate",
                column: "ParentMarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketInvoiceLine_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceLine",
                column: "ParentInvoiceId",
                principalTable: "MarketInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketInvoiceReminder_MarketInvoices_ParentInvoiceId",
                table: "MarketInvoiceReminder",
                column: "ParentInvoiceId",
                principalTable: "MarketInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketRevisions_MarketRevisionId",
                table: "MarketRevisionWithTheme",
                column: "MarketRevisionId",
                principalTable: "MarketRevisions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketRevisions_MarketRevisionsId",
                table: "MarketRevisionWithTheme",
                column: "MarketRevisionsId",
                principalTable: "MarketRevisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketThemes_ThemeId",
                table: "MarketRevisionWithTheme",
                column: "ThemeId",
                principalTable: "MarketThemes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketRevisionWithTheme_MarketThemes_ThemesId",
                table: "MarketRevisionWithTheme",
                column: "ThemesId",
                principalTable: "MarketThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_Province_ProvinceId",
                table: "Markets",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithTheme_MarketThemes_ThemeId",
                table: "MarketWithTheme",
                column: "ThemeId",
                principalTable: "MarketThemes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketWithTheme_MarketThemes_ThemesId",
                table: "MarketWithTheme",
                column: "ThemesId",
                principalTable: "MarketThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
