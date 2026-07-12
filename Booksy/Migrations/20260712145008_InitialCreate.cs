using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PreferredLanguage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeZone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ReceiveNewsletter = table.Column<bool>(type: "boolean", nullable: false),
                    ThemePreference = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TransactionStatus = table.Column<int>(type: "integer", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    OTPNumber = table.Column<string>(type: "text", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOTPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOTPs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "text", nullable: true),
                    Discount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false, defaultValue: 0m),
                    Traffic = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ISBN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupon_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarrierName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CarrierTrackingId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipment_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTag",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTag", x => new { x.BookId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BookTag_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    CartId1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Carts_CartId1",
                        column: x => x.CartId1,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: true),
                    PromotionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discount_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discount_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ReviewerName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "LastLoginDate", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredLanguage", "ProfilePictureUrl", "ReceiveNewsletter", "RegisteredDate", "SecurityStamp", "State", "Street", "ThemePreference", "TimeZone", "TwoFactorEnabled", "UserName", "ZipCode" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", 0, null, "90f186ef-c477-4e95-b023-317b04cd3edf", null, null, "admin@booksy.com", true, null, null, true, null, null, false, null, "System Admin", "ADMIN@BOOKSY.COM", "ADMIN@BOOKSY.COM", "AQAAAAIAAYagAAAAEEVgao+rr+oOcRFsCn55hmn7j8Fa2FFb9r3DTfud1rrar1BHmYQon9fFxUxJxhq3OQ==", null, false, "en", null, true, new DateTime(2026, 7, 12, 14, 50, 7, 500, DateTimeKind.Utc).AddTicks(3385), "275b7daa-8c8b-43a9-94d9-043f1aace741", null, null, null, null, false, "admin@booksy.com", null },
                    { "00000000-0000-0000-0000-000000000002", 0, null, "49d646bf-2275-4d15-9d5c-03d8bcb0e7eb", null, null, "customer1@booksy.com", true, null, null, true, null, null, false, null, "Alice", "CUSTOMER1@BOOKSY.COM", "CUSTOMER1@BOOKSY.COM", "AQAAAAIAAYagAAAAEGX1chMx09mvGzjjv63uuKpSCEblvHVWPCYYSoFkDO7pYyyeAcjtYoeFAF3jhN9LvA==", null, false, "en", null, true, new DateTime(2026, 7, 12, 14, 50, 7, 584, DateTimeKind.Utc).AddTicks(1851), "9a8526b7-d393-4c6b-9128-281f1fc63a04", null, null, null, null, false, "customer1@booksy.com", null },
                    { "00000000-0000-0000-0000-000000000003", 0, null, "7fc5cf2c-6ec9-416a-bc2a-bc7eb416897d", null, null, "customer2@booksy.com", true, null, null, true, null, null, false, null, "Bob", "CUSTOMER2@BOOKSY.COM", "CUSTOMER2@BOOKSY.COM", "AQAAAAIAAYagAAAAEGCxftyP1AT4ikTIDY+Ei6zx7m1RSyor8OoxMIhlXGd8QISaCk+Xi0jFs0nHHxRFaA==", null, false, "en", null, true, new DateTime(2026, 7, 12, 14, 50, 7, 641, DateTimeKind.Utc).AddTicks(5815), "7b8647b0-665b-455c-add5-41cc826d1457", null, null, null, null, false, "customer2@booksy.com", null }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "Author of Harry Potter series", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(5682), "J.K. Rowling", "jk-rowling", null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "Author of Game of Thrones series", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(7289), "George R.R. Martin", "george-rr-martin", null },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "Author of The Lord of the Rings", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(7538), "J.R.R. Tolkien", "jrr-tolkien", null },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "Famous mystery and crime writer", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(7699), "Agatha Christie", "agatha-christie", null },
                    { new Guid("20000000-0000-0000-0000-000000000005"), "Renowned horror and thriller author", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(7875), "Stephen King", "stephen-king", null },
                    { new Guid("20000000-0000-0000-0000-000000000006"), "Author of The Da Vinci Code and Robert Langdon series", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(8028), "Dan Brown", "dan-brown", null },
                    { new Guid("20000000-0000-0000-0000-000000000007"), "Author of The Hunger Games trilogy", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(8170), "Suzanne Collins", "suzanne-collins", null },
                    { new Guid("20000000-0000-0000-0000-000000000008"), "American novelist and Nobel Prize winner", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(8318), "Ernest Hemingway", "ernest-hemingway", null },
                    { new Guid("20000000-0000-0000-0000-000000000009"), "American writer and humorist", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(9691), "Mark Twain", "mark-twain", null },
                    { new Guid("20000000-0000-0000-0000-000000000010"), "Science fiction and non-fiction author", new DateTime(2026, 7, 12, 14, 50, 7, 712, DateTimeKind.Utc).AddTicks(9892), "Isaac Asimov", "isaac-asimov", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 7, 12, 14, 50, 7, 700, DateTimeKind.Utc).AddTicks(4472), "Fiction", "fiction", null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 7, 12, 14, 50, 7, 710, DateTimeKind.Utc).AddTicks(9737), "Non-Fiction", "non-fiction", null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(5412), "Science", "science", null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(5607), "Children", "children", null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(5750), "Fantasy", "fantasy", null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(5901), "Mystery", "mystery", null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(6036), "Thriller", "thriller", null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(6167), "Romance", "romance", null },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(6299), "Horror", "horror", null },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(6553), "Biography", "biography", null },
                    { new Guid("10000000-0000-0000-0000-000000000011"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(6715), "Self-Help", "self-help", null },
                    { new Guid("10000000-0000-0000-0000-000000000012"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(7449), "History", "history", null },
                    { new Guid("10000000-0000-0000-0000-000000000013"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(7629), "Poetry", "poetry", null },
                    { new Guid("10000000-0000-0000-0000-000000000014"), new DateTime(2026, 7, 12, 14, 50, 7, 711, DateTimeKind.Utc).AddTicks(7777), "Science Fiction", "science-fiction", null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000005"), null, new DateTime(2026, 7, 12, 14, 50, 7, 714, DateTimeKind.Utc).AddTicks(7450), null, "9780747532699", 19.99m, null, 0, "harry-potter-and-the-philosophers-stone", 50, "Harry Potter and the Philosopher's Stone", null },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000005"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(91), null, "9780747538493", 19.99m, null, 0, "harry-potter-and-the-chamber-of-secrets", 45, "Harry Potter and the Chamber of Secrets", null },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(328), null, "9780553103540", 24.99m, null, 0, "a-game-of-thrones", 40, "A Game of Thrones", null },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(487), null, "9780553108033", 24.99m, null, 0, "a-clash-of-kings", 35, "A Clash of Kings", null },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000009"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(641), null, "9780385121675", 17.99m, null, 0, "the-shining", 30, "The Shining", null },
                    { new Guid("30000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000009"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(818), null, "9780450411434", 18.99m, null, 0, "it", 25, "It", null },
                    { new Guid("30000000-0000-0000-0000-000000000007"), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000005"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(957), null, "9780547928210", 22.99m, null, 0, "the-lord-of-the-rings-fellowship", 40, "The Lord of the Rings: Fellowship", null },
                    { new Guid("30000000-0000-0000-0000-000000000008"), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000006"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(1130), null, "9780062073501", 14.99m, null, 0, "murder-on-the-orient-express", 30, "Murder on the Orient Express", null },
                    { new Guid("30000000-0000-0000-0000-000000000009"), new Guid("20000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000007"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(1291), null, "9780307474278", 16.99m, null, 0, "the-da-vinci-code", 25, "The Da Vinci Code", null },
                    { new Guid("30000000-0000-0000-0000-000000000010"), new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(1447), null, "9780439023481", 18.99m, null, 0, "the-hunger-games", 35, "The Hunger Games", null },
                    { new Guid("30000000-0000-0000-0000-000000000011"), new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(1598), null, "9780439023498", 18.99m, null, 0, "catching-fire", 35, "Catching Fire", null },
                    { new Guid("30000000-0000-0000-0000-000000000012"), new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(1742), null, "9780439023511", 18.99m, null, 0, "mockingjay", 35, "Mockingjay", null },
                    { new Guid("30000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(1877), null, "9780553293357", 15.99m, null, 0, "foundation", 25, "Foundation", null },
                    { new Guid("30000000-0000-0000-0000-000000000014"), new Guid("20000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(2012), null, "9780553294385", 15.99m, null, 0, "i-robot", 25, "I, Robot", null },
                    { new Guid("30000000-0000-0000-0000-000000000015"), new Guid("20000000-0000-0000-0000-000000000008"), new Guid("10000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(2162), null, "9780684801223", 12.99m, null, 0, "the-old-man-and-the-sea", 20, "The Old Man and The Sea", null },
                    { new Guid("30000000-0000-0000-0000-000000000016"), new Guid("20000000-0000-0000-0000-000000000009"), new Guid("10000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(2323), null, "9780486280615", 11.99m, null, 0, "adventures-of-huckleberry-finn", 20, "Adventures of Huckleberry Finn", null }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comment", "CreatedAt", "Rating", "ReviewerName", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000001"), "Absolutely loved this book! A must-read for everyone.", new DateTime(2026, 7, 12, 14, 50, 7, 715, DateTimeKind.Utc).AddTicks(9003), 5, "Alice", 1, null, "00000000-0000-0000-0000-000000000002" },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000003"), "Great story and world-building, though a bit lengthy.", new DateTime(2026, 7, 12, 14, 50, 7, 716, DateTimeKind.Utc).AddTicks(469), 4, "Bob", 1, null, "00000000-0000-0000-0000-000000000003" },
                    { new Guid("40000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000005"), "Terrifying but amazing. Couldn't put it down!", new DateTime(2026, 7, 12, 14, 50, 7, 716, DateTimeKind.Utc).AddTicks(592), 5, "Alice", 1, null, "00000000-0000-0000-0000-000000000002" },
                    { new Guid("40000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000010"), "The most thrilling read I've had all year!", new DateTime(2026, 7, 12, 14, 50, 7, 716, DateTimeKind.Utc).AddTicks(713), 5, "Bob", 1, null, "00000000-0000-0000-0000-000000000003" },
                    { new Guid("40000000-0000-0000-0000-000000000005"), new Guid("30000000-0000-0000-0000-000000000007"), "Classic fantasy at its finest!", new DateTime(2026, 7, 12, 14, 50, 7, 716, DateTimeKind.Utc).AddTicks(833), 5, "Alice", 1, null, "00000000-0000-0000-0000-000000000002" },
                    { new Guid("40000000-0000-0000-0000-000000000006"), new Guid("30000000-0000-0000-0000-000000000013"), "Interesting sci-fi concepts, great foundation for thought.", new DateTime(2026, 7, 12, 14, 50, 7, 716, DateTimeKind.Utc).AddTicks(958), 4, "Bob", 1, null, "00000000-0000-0000-0000-000000000003" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_IsDeleted",
                table: "Authors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Author_Name",
                table: "Authors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Author_Slug",
                table: "Authors",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId_IsDeleted",
                table: "Books",
                columns: new[] { "CategoryId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Book_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Slug",
                table: "Books",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Title",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PromotionId",
                table: "Books",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTag_TagId",
                table: "BookTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_BookId",
                table: "CartItem",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId1",
                table: "CartItem",
                column: "CartId1");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IsDeleted",
                table: "Categories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Slug",
                table: "Categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_PromotionId",
                table: "Coupon",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_BookId",
                table: "Discount",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_PromotionId",
                table: "Discount",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BookId_Status",
                table: "Reviews",
                columns: new[] { "BookId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Review_IsDeleted",
                table: "Reviews",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Review_Status",
                table: "Reviews",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_OrderId",
                table: "Shipment",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_IsDeleted",
                table: "Tags",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                table: "Tags",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Slug",
                table: "Tags",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOTPs_ApplicationUserId",
                table: "UserOTPs",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

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
                name: "BookTag");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "UserOTPs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
