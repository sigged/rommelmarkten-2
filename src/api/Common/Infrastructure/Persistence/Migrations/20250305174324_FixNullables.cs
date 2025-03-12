using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markets_BannerTypes_BannerTypeId",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "Id_V1",
                table: "Markets");

            migrationBuilder.AlterColumn<Guid>(
                name: "BannerTypeId",
                table: "Markets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_BannerTypes_BannerTypeId",
                table: "Markets",
                column: "BannerTypeId",
                principalTable: "BannerTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markets_BannerTypes_BannerTypeId",
                table: "Markets");

            migrationBuilder.AlterColumn<Guid>(
                name: "BannerTypeId",
                table: "Markets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id_V1",
                table: "Markets",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_BannerTypes_BannerTypeId",
                table: "Markets",
                column: "BannerTypeId",
                principalTable: "BannerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
