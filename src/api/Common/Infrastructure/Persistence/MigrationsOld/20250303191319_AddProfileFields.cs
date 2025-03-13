using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar_Content",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Avatar_ContentHash",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Avatar_Type",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "Avatar_Name",
                table: "UserProfiles",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ActivationDate",
                table: "UserProfiles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivationRemindersSent",
                table: "UserProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "UserProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastActivationMailSendDate",
                table: "UserProfiles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastActivityDate",
                table: "UserProfiles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastPasswordResetDate",
                table: "UserProfiles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "UserProfiles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VAT",
                table: "UserProfiles",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ActivationRemindersSent",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LastActivationMailSendDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LastActivityDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LastPasswordResetDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserProfiles",
                newName: "Avatar_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar_Name",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar_Content",
                table: "UserProfiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Avatar_ContentHash",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Avatar_Type",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
