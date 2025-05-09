﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommelmarkten.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Genesis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffiliateAds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    AffiliateName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AffiliateURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    AdContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliateAds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannerTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MaximumThemes = table.Column<int>(type: "int", nullable: false),
                    MaximumCharacters = table.Column<int>(type: "int", nullable: false),
                    AllowBanners = table.Column<bool>(type: "bit", nullable: false),
                    AllowPoster = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketThemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UrlSlug = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Consented = table.Column<bool>(type: "bit", nullable: false),
                    Avatar_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar_Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Avatar_ContentHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar_Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAQItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQItems_FAQCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FAQCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_V1 = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BannerTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pricing_EntryPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Pricing_StandPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                    table.PrimaryKey("PK_Markets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markets_BannerTypes_BannerTypeId",
                        column: x => x.BannerTypeId,
                        principalTable: "BannerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Markets_MarketConfigurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "MarketConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Markets_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ListAssociates",
                columns: table => new
                {
                    AssociateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    AssociatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListAssociates", x => new { x.ListId, x.AssociateId });
                    table.ForeignKey(
                        name: "FK_ListAssociates_ShoppingLists_ListId",
                        column: x => x.ListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListItems_ShoppingLists_ListId",
                        column: x => x.ListId,
                        principalTable: "ShoppingLists",
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
                        name: "FK_MarketDate_Markets_ParentMarketId",
                        column: x => x.ParentMarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MarketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusChanged = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketInvoices_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketRevisions",
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
                    table.PrimaryKey("PK_MarketRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketRevisions_Markets_RevisedMarketId",
                        column: x => x.RevisedMarketId,
                        principalTable: "Markets",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketWithTheme_MarketThemes_ThemesId",
                        column: x => x.ThemesId,
                        principalTable: "MarketThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketWithTheme_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketWithTheme_Markets_MarketsId",
                        column: x => x.MarketsId,
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
                        name: "FK_MarketInvoiceLine_MarketInvoices_ParentInvoiceId",
                        column: x => x.ParentInvoiceId,
                        principalTable: "MarketInvoices",
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
                        name: "FK_MarketInvoiceReminder_MarketInvoices_ParentInvoiceId",
                        column: x => x.ParentInvoiceId,
                        principalTable: "MarketInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_MarketRevisionWithTheme_MarketRevisions_MarketRevisionId",
                        column: x => x.MarketRevisionId,
                        principalTable: "MarketRevisions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketRevisionWithTheme_MarketRevisions_MarketRevisionsId",
                        column: x => x.MarketRevisionsId,
                        principalTable: "MarketRevisions",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FAQItems_CategoryId",
                table: "FAQItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_CategoryId",
                table: "ListItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_ListId",
                table: "ListItems",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketDate_ParentMarketId",
                table: "MarketDate",
                column: "ParentMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoiceLine_ParentInvoiceId",
                table: "MarketInvoiceLine",
                column: "ParentInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoiceReminder_ParentInvoiceId",
                table: "MarketInvoiceReminder",
                column: "ParentInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoices_InvoiceNumber",
                table: "MarketInvoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketInvoices_MarketId",
                table: "MarketInvoices",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketRevisions_RevisedMarketId",
                table: "MarketRevisions",
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

            migrationBuilder.CreateIndex(
                name: "IX_Markets_BannerTypeId",
                table: "Markets",
                column: "BannerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_ConfigurationId",
                table: "Markets",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_ProvinceId",
                table: "Markets",
                column: "ProvinceId");

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
                name: "AffiliateAds");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FAQItems");

            migrationBuilder.DropTable(
                name: "ListAssociates");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "MarketDate");

            migrationBuilder.DropTable(
                name: "MarketInvoiceLine");

            migrationBuilder.DropTable(
                name: "MarketInvoiceReminder");

            migrationBuilder.DropTable(
                name: "MarketRevisionWithTheme");

            migrationBuilder.DropTable(
                name: "MarketWithTheme");

            migrationBuilder.DropTable(
                name: "NewsArticles");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FAQCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "MarketInvoices");

            migrationBuilder.DropTable(
                name: "MarketRevisions");

            migrationBuilder.DropTable(
                name: "MarketThemes");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropTable(
                name: "BannerTypes");

            migrationBuilder.DropTable(
                name: "MarketConfigurations");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
