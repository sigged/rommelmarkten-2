using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MarketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusChanged = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketInvoice_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketInvoiceLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketInvoiceLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketInvoiceLine_MarketInvoice_ParentInvoiceId",
                        column: x => x.ParentInvoiceId,
                        principalTable: "MarketInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketInvoiceReminder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketInvoiceReminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketInvoiceReminder_MarketInvoice_ParentInvoiceId",
                        column: x => x.ParentInvoiceId,
                        principalTable: "MarketInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoice_InvoiceNumber",
                table: "MarketInvoice",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoice_MarketId",
                table: "MarketInvoice",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoiceLine_ParentInvoiceId",
                table: "MarketInvoiceLine",
                column: "ParentInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoiceReminder_ParentInvoiceId",
                table: "MarketInvoiceReminder",
                column: "ParentInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketInvoiceLine");

            migrationBuilder.DropTable(
                name: "MarketInvoiceReminder");

            migrationBuilder.DropTable(
                name: "MarketInvoice");
        }
    }
}
