using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class NewReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewerName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6d24adb-6318-4e4c-b290-7e49e08a6010", "AQAAAAIAAYagAAAAEKu41fO8q6swgXSU5GrjkCqoSgqxqJ/gVQ+S0gFjs7XWbgGTpsooLH2rDIXg98QQ3A==", "a37c47c8-e9d2-4133-8ab6-521948b9fca5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd3086b3-95d0-4ef7-8b6b-69a1b0aab96d", "AQAAAAIAAYagAAAAEF35fCepg9Arj+h5nCxruC02n7vBgoE7XB+BUnnE2tbUbEudFY1630/qEX5+9FUOxQ==", "1da4277a-c6cd-4c8d-8cff-6ebecde49f67" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6c58d32-f5c4-4211-9a81-4f39311bf6bf", "AQAAAAIAAYagAAAAEExarm+MDvrSNQca0r/ql4lHra+Xc5XmrH5tRIA4XnTVqC/vemUCvROKGMR5DUxk7g==", "06e40e00-6548-4be2-9b12-c6ab6edd8b2d" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(3779));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5085));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5089));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5091));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5101));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5113));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 480, DateTimeKind.Utc).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(4350));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8791));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8802));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8807));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8811));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8883));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8888));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8898));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8902));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8905));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8915));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 482, DateTimeKind.Utc).AddTicks(8919));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(3249));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4964));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4967));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4985));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4988));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4991));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4993));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(4998));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(5017));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(5018));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 479, DateTimeKind.Utc).AddTicks(5021));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2025, 9, 28, 18, 38, 58, 483, DateTimeKind.Utc).AddTicks(7753), null });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1097), null });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1104), null });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1107), null });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1108), null });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1120), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewerName",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3568feb-50e7-4383-8dd5-45bb78f076c8", "AQAAAAIAAYagAAAAEEuKHVyY5Q0L8QEcGRzPQ0d5sGLZ9pzyeCWVkL/FSYtHBHX5VXqo0Vmzg2XXaWJfvw==", "1160f88d-24dc-4faa-99ed-c3490ad834ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7e37399-bc6e-4b0f-9582-1475608a56ca", "AQAAAAIAAYagAAAAEPoeHWFHCUXCh4xbvTKoFpikX2V70TBWVR9mUAVeiFEbWw/kD81gg9xDDO/C+X+nyg==", "d9aee5a8-9939-47ae-9184-adc2510d220e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fd23e08-9b87-4768-b567-bbac48ec6d74", "AQAAAAIAAYagAAAAEHT+j9jQUak1P24K1AkkmfMtgElzkGRqPC09VUb/TvVRjqtY0KOqb4gBaqppxOd+IQ==", "07742095-cf7f-48cf-8101-910acc78425c" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(9874));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1020));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1024));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1026));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1041));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1042));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1044));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1045));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1048));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(5379));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9084));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9087));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9089));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9109));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9112));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9114));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9116));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9120));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9122));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9124));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9127));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9129));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9133));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(1489));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2923));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2925));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2942));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2943));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2945));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2949));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2950));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2967));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2969));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2970));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(4991));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7743));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7747));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7749));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7751));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7758));
        }
    }
}
