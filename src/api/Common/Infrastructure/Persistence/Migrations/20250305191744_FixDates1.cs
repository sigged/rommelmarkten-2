using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDates1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "MarketDates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MarketDates");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "MarketDates");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MarketDates");

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing_EntryPrice",
                table: "MarketRevisions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing_StandPrice",
                table: "MarketRevisions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MarketDates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pricing_EntryPrice",
                table: "MarketRevisions");

            migrationBuilder.DropColumn(
                name: "Pricing_StandPrice",
                table: "MarketRevisions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MarketDates");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "MarketDates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MarketDates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "MarketDates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MarketDates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
