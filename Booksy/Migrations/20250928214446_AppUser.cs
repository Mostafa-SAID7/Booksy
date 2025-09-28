using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class AppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredLanguage",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveNewsletter",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ThemePreference",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "Country", "DateOfBirth", "FirstName", "Gender", "IsActive", "LastLoginDate", "LastName", "PasswordHash", "PreferredLanguage", "ProfilePictureUrl", "ReceiveNewsletter", "RegisteredDate", "SecurityStamp", "ThemePreference", "TimeZone" },
                values: new object[] { "12d00e07-676f-4164-abee-2826302446d6", null, null, null, null, true, null, null, "AQAAAAIAAYagAAAAEDtbeFEqBEYegxhJil3L1ZTLPtC9k3rHe8MQdbEGPl8yCtNgtyGM1rJp/S4U2Z56ng==", "en", null, true, new DateTime(2025, 9, 28, 21, 44, 43, 701, DateTimeKind.Utc).AddTicks(9723), "6a43a8cf-1700-4119-8d83-112082a0d30e", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "Country", "DateOfBirth", "FirstName", "Gender", "IsActive", "LastLoginDate", "LastName", "PasswordHash", "PreferredLanguage", "ProfilePictureUrl", "ReceiveNewsletter", "RegisteredDate", "SecurityStamp", "ThemePreference", "TimeZone" },
                values: new object[] { "5a00dbdc-510c-4664-b51c-ae4bd2b1e926", null, null, null, null, true, null, null, "AQAAAAIAAYagAAAAELBhze3CZob2mgzWAaIYu6cuMOaerE3MWifT41W3+WXYIW0lY91lsrEuYGHLFgBEow==", "en", null, true, new DateTime(2025, 9, 28, 21, 44, 43, 812, DateTimeKind.Utc).AddTicks(7094), "c51ae7ce-c3a5-4b01-9270-f3c4c83e612c", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "Country", "DateOfBirth", "FirstName", "Gender", "IsActive", "LastLoginDate", "LastName", "PasswordHash", "PreferredLanguage", "ProfilePictureUrl", "ReceiveNewsletter", "RegisteredDate", "SecurityStamp", "ThemePreference", "TimeZone" },
                values: new object[] { "e2f91d1a-4c1b-42ae-97a2-7ee913a2064a", null, null, null, null, true, null, null, "AQAAAAIAAYagAAAAEKsERPYFsD67V5hnU550r01VEZ/8hGvPLFMQgmkAhfDwGlYTNYZY0897jjsaMfV/xA==", "en", null, true, new DateTime(2025, 9, 28, 21, 44, 43, 914, DateTimeKind.Utc).AddTicks(6573), "a3788f1c-ef84-4718-9669-8057abb976a7", null, null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreferredLanguage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceiveNewsletter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegisteredDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThemePreference",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

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
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 483, DateTimeKind.Utc).AddTicks(7753));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1097));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1104));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1107));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1108));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 18, 38, 58, 484, DateTimeKind.Utc).AddTicks(1120));
        }
    }
}
