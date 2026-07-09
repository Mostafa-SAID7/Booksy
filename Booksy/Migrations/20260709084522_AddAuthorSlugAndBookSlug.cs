using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorSlugAndBookSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Authors",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "ae184e16-1012-4586-b027-7b7c668f060b", "AQAAAAIAAYagAAAAEBqAiSD64I7IFkjkEftcao3WW58ubncGT3KTiWMvv08UtIqRaDOCCh6RLyhLnXQm1w==", new DateTime(2026, 7, 9, 8, 45, 20, 663, DateTimeKind.Utc).AddTicks(9767), "f6d85dc6-8965-4c4f-8ab8-99c19bf9cb22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "06b727a1-65b6-4c72-a946-4007055a7e41", "AQAAAAIAAYagAAAAEH7A53sA2qNG/wj2TSUl6/MiRDiH1IPmrgTqZcXidEqEPIJ829J7Ovua4ZLPi9sOmQ==", new DateTime(2026, 7, 9, 8, 45, 20, 775, DateTimeKind.Utc).AddTicks(6476), "f0509ac9-7cd5-419f-9866-a9ac7606ab1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "72cc0e0e-d06e-498e-a1f6-cf2211029336", "AQAAAAIAAYagAAAAEHf54+u3Vs/257kS1Sx7zQm9g3q1o88a/NusO/AZejf0Hcszvvy7FvIs6yCrREV+pQ==", new DateTime(2026, 7, 9, 8, 45, 20, 886, DateTimeKind.Utc).AddTicks(7255), "98ac2587-5c55-4394-8026-0430afafcd19" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(4465), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5710), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5714), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5716), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5726), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5735), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5737), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5739), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5742), "" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 7, 9, 8, 45, 21, 7, DateTimeKind.Utc).AddTicks(5746), "" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 9, DateTimeKind.Utc).AddTicks(6143));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 9, DateTimeKind.Utc).AddTicks(9996));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(5));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(34));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(38));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(41));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(45));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(51));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(54));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(90));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(94));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(98));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(101));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(104));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(2860));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4364));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4379));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4381));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4383));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4384));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4389));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4391));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4411));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4414));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 6, DateTimeKind.Utc).AddTicks(4415));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 10, DateTimeKind.Utc).AddTicks(8167));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 11, DateTimeKind.Utc).AddTicks(992));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 11, DateTimeKind.Utc).AddTicks(1000));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 11, DateTimeKind.Utc).AddTicks(1002));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 11, DateTimeKind.Utc).AddTicks(1005));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 45, 21, 11, DateTimeKind.Utc).AddTicks(1013));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Authors");

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
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(4503));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7494));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7507));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7536));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7540));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7543));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7547));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7556));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7598));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 930, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(6618));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7451));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7484));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7486));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7487));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7501));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7502));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7504));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7507));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7521));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 9, 8, 38, 41, 928, DateTimeKind.Utc).AddTicks(7522));

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
    }
}
