using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class AddTagEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookTag",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTag", x => new { x.BooksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BookTag_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "bc6ce32d-7990-4286-ae58-38b5dfdbf5e6", "AQAAAAIAAYagAAAAEJ8wkxD7lHdpfCIwB/Ncb6xaM0tUeX/7GUdp+HbAT9HEENBKheCguzR5UGwKP/oWpw==", new DateTime(2026, 7, 9, 8, 32, 29, 230, DateTimeKind.Utc).AddTicks(7868), "63268002-a952-4109-868b-fefaed098076" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "2fdffe86-f4e4-4414-ae51-fa7ea39e53bd", "AQAAAAIAAYagAAAAEAQwk4OOSAsuUyMsYY86PRxh2UE3mSFWJ4NQv1Ux4Qwt4rSkGynU0V8uBdFF6iZszg==", new DateTime(2026, 7, 9, 8, 32, 29, 613, DateTimeKind.Utc).AddTicks(6159), "653ab1aa-4fd4-403a-b102-21fd803892b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "ea0266c5-42be-4f9b-a914-53e994929d98", "AQAAAAIAAYagAAAAENpU7D1Flsz3Z13Oh7d2Cn4YfQYwE07+HiRAnHNW6IlQTiU1glJe1Uu6WEQZn86Z8w==", new DateTime(2026, 7, 9, 8, 32, 29, 974, DateTimeKind.Utc).AddTicks(3860), "a8bf9238-e34b-4cf0-a365-8d347723ee70" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(1904));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4287));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4301));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4306));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4348));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4351));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4354));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 319, DateTimeKind.Utc).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(1963));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8487));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8511));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8521));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8544));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8551));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8557));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8564));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8573));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8578));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8584));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8589));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8593));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8598));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 325, DateTimeKind.Utc).AddTicks(8602));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 316, DateTimeKind.Utc).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 316, DateTimeKind.Utc).AddTicks(3731));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 316, DateTimeKind.Utc).AddTicks(3737));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3109));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3121));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3141));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3145));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3148));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3152));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3195));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3199));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3202));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3205));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 317, DateTimeKind.Utc).AddTicks(3208));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 327, DateTimeKind.Utc).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 327, DateTimeKind.Utc).AddTicks(8803));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 327, DateTimeKind.Utc).AddTicks(8812));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 327, DateTimeKind.Utc).AddTicks(8817));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 327, DateTimeKind.Utc).AddTicks(8823));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 32, 30, 327, DateTimeKind.Utc).AddTicks(8889));

            migrationBuilder.CreateIndex(
                name: "IX_BookTag_TagsId",
                table: "BookTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "12d00e07-676f-4164-abee-2826302446d6", "AQAAAAIAAYagAAAAEDtbeFEqBEYegxhJil3L1ZTLPtC9k3rHe8MQdbEGPl8yCtNgtyGM1rJp/S4U2Z56ng==", new DateTime(2025, 9, 28, 21, 44, 43, 701, DateTimeKind.Utc).AddTicks(9723), "6a43a8cf-1700-4119-8d83-112082a0d30e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "5a00dbdc-510c-4664-b51c-ae4bd2b1e926", "AQAAAAIAAYagAAAAELBhze3CZob2mgzWAaIYu6cuMOaerE3MWifT41W3+WXYIW0lY91lsrEuYGHLFgBEow==", new DateTime(2025, 9, 28, 21, 44, 43, 812, DateTimeKind.Utc).AddTicks(7094), "c51ae7ce-c3a5-4b01-9270-f3c4c83e612c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "e2f91d1a-4c1b-42ae-97a2-7ee913a2064a", "AQAAAAIAAYagAAAAEKsERPYFsD67V5hnU550r01VEZ/8hGvPLFMQgmkAhfDwGlYTNYZY0897jjsaMfV/xA==", new DateTime(2025, 9, 28, 21, 44, 43, 914, DateTimeKind.Utc).AddTicks(6573), "a3788f1c-ef84-4718-9669-8057abb976a7" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(5301));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7182));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7192));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7208));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7230));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7235));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 52, DateTimeKind.Utc).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 55, DateTimeKind.Utc).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5329));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5349));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5356));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5402));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5410));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5417));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5483));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5493));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5496));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5501));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5505));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 56, DateTimeKind.Utc).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 50, DateTimeKind.Utc).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(470));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(472));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(475));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(498));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(506));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(509));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(515));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(520));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 51, DateTimeKind.Utc).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 58, DateTimeKind.Utc).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 58, DateTimeKind.Utc).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 58, DateTimeKind.Utc).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 58, DateTimeKind.Utc).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 58, DateTimeKind.Utc).AddTicks(4712));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 21, 44, 44, 58, DateTimeKind.Utc).AddTicks(4737));
        }
    }
}
