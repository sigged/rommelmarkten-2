using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    public partial class AddUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Consented = table.Column<bool>(type: "bit", nullable: false),
                    Avatar_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar_Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Avatar_ContentHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar_Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
