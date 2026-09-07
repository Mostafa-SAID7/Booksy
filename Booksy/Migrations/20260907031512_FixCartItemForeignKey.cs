using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class FixCartItemForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Carts_CartId1",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_CartId1",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "CartItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "CartItem",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "e24767d1-fb3b-4151-ad5e-d616b79ea1e8", "AQAAAAIAAYagAAAAEP3qUmTlgzUNekmK+WlTRzNI2dMoi+dmN8oER60lf1lwmtbxegfpU+/mqQOnloy7ZA==", new DateTime(2026, 9, 7, 3, 15, 7, 743, DateTimeKind.Utc).AddTicks(2499), "2fd974ba-40d5-464d-b2a6-f1fdb911376e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "b20a2f35-8c15-4c37-9f6e-351604a2138b", "AQAAAAIAAYagAAAAEM+P2PWD4JFwrtYP1WAQ1lSBTz2u/1gR9pyzD3f4VIItqFDmRKzD9+qpa7EIFI/5cg==", new DateTime(2026, 9, 7, 3, 15, 8, 207, DateTimeKind.Utc).AddTicks(7473), "f33da3e2-7fc5-4a2b-a996-b633e39628a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "b4ced78c-9028-40f5-abb3-c4e2081542cb", "AQAAAAIAAYagAAAAEGcr4imPOL1vvWAr1arSuicocaYFnVmokeKo6tTxYCbQTMEy5nk2KAWSirCuXyT0+A==", new DateTime(2026, 9, 7, 3, 15, 8, 466, DateTimeKind.Utc).AddTicks(8366), "f4beac6c-d24a-4d31-93ec-3161c3972312" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 809, DateTimeKind.Utc).AddTicks(7457));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(484));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(876));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(967));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(1054));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(1222));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(1287));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 810, DateTimeKind.Utc).AddTicks(1386));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 822, DateTimeKind.Utc).AddTicks(9903));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(5436));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(5713));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(5814));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(5899));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6011));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6158));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6296));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6401));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6491));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6573));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6687));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6804));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 823, DateTimeKind.Utc).AddTicks(6906));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 791, DateTimeKind.Utc).AddTicks(9372));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 807, DateTimeKind.Utc).AddTicks(3110));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(3836));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(3989));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4089));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4197));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4246));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4315));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4492));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4855));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4911));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 808, DateTimeKind.Utc).AddTicks(4948));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 824, DateTimeKind.Utc).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 825, DateTimeKind.Utc).AddTicks(1894));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 825, DateTimeKind.Utc).AddTicks(1928));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 825, DateTimeKind.Utc).AddTicks(1938));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 825, DateTimeKind.Utc).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 3, 15, 8, 825, DateTimeKind.Utc).AddTicks(1967));

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CartId1",
                table: "CartItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "c56b1914-ee9f-4f4a-8845-d3d4ee89ba6e", "AQAAAAIAAYagAAAAEHHkk7Yt0zt3dQsv0Og+Yr/qRHnllCj0pNo806sTj0y8GSTlAhzuCpBSr3He5qL9jg==", new DateTime(2026, 9, 7, 2, 59, 26, 187, DateTimeKind.Utc).AddTicks(2469), "64051c96-b131-43a3-a55f-2503afb0b662" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "721b5947-b668-499b-bb79-f81cb3930616", "AQAAAAIAAYagAAAAEKMpNzA+aj31TclIpRtBhvJeTsUdKuRZLmFsTfEZQeQ3fHUqIoOhy8K30GZGGxp+vA==", new DateTime(2026, 9, 7, 2, 59, 26, 725, DateTimeKind.Utc).AddTicks(3077), "30b9ae5a-69f7-47b5-b8f8-c6d495ed4208" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredDate", "SecurityStamp" },
                values: new object[] { "ddbba8f0-e332-4199-8ecc-1bdd9c22cdc6", "AQAAAAIAAYagAAAAEJIIZMhfZYxQpH8Yt5p/8lCZPcHLk0OWVBqHB27l8G/Rzd/CAO6Xc27LV4i/2JtcdA==", new DateTime(2026, 9, 7, 2, 59, 27, 147, DateTimeKind.Utc).AddTicks(1964), "d7477373-80bf-4d57-a1a8-e1d908f5cf7b" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 681, DateTimeKind.Utc).AddTicks(3174));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 681, DateTimeKind.Utc).AddTicks(6557));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 681, DateTimeKind.Utc).AddTicks(6868));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 681, DateTimeKind.Utc).AddTicks(7000));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 681, DateTimeKind.Utc).AddTicks(7122));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 681, DateTimeKind.Utc).AddTicks(7214));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 682, DateTimeKind.Utc).AddTicks(2850));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 685, DateTimeKind.Utc).AddTicks(6547));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 685, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 685, DateTimeKind.Utc).AddTicks(7701));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 693, DateTimeKind.Utc).AddTicks(2383));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(3696));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(4228));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(4476));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(4691));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(4825));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(4924));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(5406));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(5745));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(5864));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(6013));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(6227));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 694, DateTimeKind.Utc).AddTicks(6389));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 647, DateTimeKind.Utc).AddTicks(6022));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 678, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7012));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7099));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7167));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7284));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7396));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(8510));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 679, DateTimeKind.Utc).AddTicks(8561));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 695, DateTimeKind.Utc).AddTicks(9988));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 696, DateTimeKind.Utc).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 696, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 696, DateTimeKind.Utc).AddTicks(4160));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 696, DateTimeKind.Utc).AddTicks(4169));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 9, 7, 2, 59, 27, 696, DateTimeKind.Utc).AddTicks(4200));

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId1",
                table: "CartItem",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Carts_CartId1",
                table: "CartItem",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
