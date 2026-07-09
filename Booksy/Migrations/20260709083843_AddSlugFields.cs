using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Tags",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Books",
                type: "nvarchar(220)",
                maxLength: 220,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "9fd51a01-1571-4306-9845-7632d6905532", "AQAAAAIAAYagAAAAEPYjUSqFEgOO39Pkqf8zuXLSqMSBz+b50N+7EMO70M6hB7xD6WrakqnXBqOZSiiW5Q==", new DateTime(2026, 7, 9, 8, 38, 41, 593, DateTimeKind.Utc).AddTicks(1537), "df1cd8e8-8ef6-42ae-8395-9626eb8fd10a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "2fd8af13-1086-4964-b727-438dc512360d", "AQAAAAIAAYagAAAAEPAHskMopBeN9DIQ3qP0jXXbdAy6gaomWbIHkjgag5sGpQ96fug6TsgcFAeOjqfySQ==", new DateTime(2026, 7, 9, 8, 38, 41, 711, DateTimeKind.Utc).AddTicks(172), "c495fd2a-f42f-4369-8934-7b7b300fb91d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "a85e6c59-6f7c-4b42-9e57-16ccc4c08bf4", "AQAAAAIAAYagAAAAEEUzoSJyaZNajtlpoQI99vy9kk1+KnYmDxmjhuiseCqGcOJ+32vsYUzK2SYXLOktpQ==", new DateTime(2026, 7, 9, 8, 38, 41, 821, DateTimeKind.Utc).AddTicks(4435), "8a3ccc51-36a0-4329-89e6-894e8268cf5f" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3783));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3786));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3787));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3794));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3799));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3800));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3803));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 929, DateTimeKind.Utc).AddTicks(3805));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(4503), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7494), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7503), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7507), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7510), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7536), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7540), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7543), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7547), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7553), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7556), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7559), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7562), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7595), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7598), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7601), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(6618), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7451), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7484), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7486), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7487), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7501), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7502), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7503), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7504), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7507), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7508), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7520), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7521), "" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7522), "" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 931, DateTimeKind.Utc).AddTicks(6178));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 931, DateTimeKind.Utc).AddTicks(8311));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 931, DateTimeKind.Utc).AddTicks(8316));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 931, DateTimeKind.Utc).AddTicks(8318));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 931, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 931, DateTimeKind.Utc).AddTicks(8329));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Books");

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
        }
    }
}
