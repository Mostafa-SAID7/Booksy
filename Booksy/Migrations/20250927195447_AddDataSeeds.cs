using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booksy.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "State", "Street", "TwoFactorEnabled", "UserName", "ZipCode" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", 0, null, "a3568feb-50e7-4383-8dd5-45bb78f076c8", "admin@booksy.com", true, false, null, "System Admin", "ADMIN@BOOKSY.COM", "ADMIN@BOOKSY.COM", "AQAAAAIAAYagAAAAEEuKHVyY5Q0L8QEcGRzPQ0d5sGLZ9pzyeCWVkL/FSYtHBHX5VXqo0Vmzg2XXaWJfvw==", null, false, "1160f88d-24dc-4faa-99ed-c3490ad834ae", null, null, false, "admin@booksy.com", null },
                    { "00000000-0000-0000-0000-000000000002", 0, null, "a7e37399-bc6e-4b0f-9582-1475608a56ca", "customer1@booksy.com", true, false, null, "Alice", "CUSTOMER1@BOOKSY.COM", "CUSTOMER1@BOOKSY.COM", "AQAAAAIAAYagAAAAEPoeHWFHCUXCh4xbvTKoFpikX2V70TBWVR9mUAVeiFEbWw/kD81gg9xDDO/C+X+nyg==", null, false, "d9aee5a8-9939-47ae-9184-adc2510d220e", null, null, false, "customer1@booksy.com", null },
                    { "00000000-0000-0000-0000-000000000003", 0, null, "4fd23e08-9b87-4768-b567-bbac48ec6d74", "customer2@booksy.com", true, false, null, "Bob", "CUSTOMER2@BOOKSY.COM", "CUSTOMER2@BOOKSY.COM", "AQAAAAIAAYagAAAAEHT+j9jQUak1P24K1AkkmfMtgElzkGRqPC09VUb/TvVRjqtY0KOqb4gBaqppxOd+IQ==", null, false, "07742095-cf7f-48cf-8101-910acc78425c", null, null, false, "customer2@booksy.com", null }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Author of Harry Potter", new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(9874), false, "J.K. Rowling", null },
                    { 2, "Author of Game of Thrones", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1020), false, "George R.R. Martin", null },
                    { 3, "Author of The Lord of the Rings", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1024), false, "J.R.R. Tolkien", null },
                    { 4, "Famous mystery writer", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1026), false, "Agatha Christie", null },
                    { 5, "Horror and thriller author", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1033), false, "Stephen King", null },
                    { 6, "Author of Da Vinci Code", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1041), false, "Dan Brown", null },
                    { 7, "Author of Hunger Games", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1042), false, "Suzanne Collins", null },
                    { 8, "American novelist", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1044), false, "Ernest Hemingway", null },
                    { 9, "Famous American writer", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1045), false, "Mark Twain", null },
                    { 10, "Science fiction author", new DateTime(2025, 9, 27, 19, 54, 44, 937, DateTimeKind.Utc).AddTicks(1048), false, "Isaac Asimov", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(1489), false, "Fiction", null },
                    { 2, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2881), false, "Non-Fiction", null },
                    { 3, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2885), false, "Science", null },
                    { 4, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2923), false, "Children", null },
                    { 5, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2925), false, "Fantasy", null },
                    { 6, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2942), false, "Mystery", null },
                    { 7, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2943), false, "Thriller", null },
                    { 8, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2945), false, "Romance", null },
                    { 9, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2946), false, "Horror", null },
                    { 10, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2949), false, "Biography", null },
                    { 11, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2950), false, "Self-Help", null },
                    { 12, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2967), false, "History", null },
                    { 13, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2969), false, "Poetry", null },
                    { 14, new DateTime(2025, 9, 27, 19, 54, 44, 936, DateTimeKind.Utc).AddTicks(2970), false, "Science Fiction", null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "Discount", "ISBN", "IsDeleted", "Price", "PromotionId", "Quantity", "Stock", "Title", "Traffic", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 5, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(5379), null, 0m, "9780747532699", false, 19.99m, null, 0, 50, "Harry Potter and the Philosopher's Stone", 0, null },
                    { 2, 1, 5, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9079), null, 0m, "9780747538493", false, 19.99m, null, 0, 45, "Harry Potter and the Chamber of Secrets", 0, null },
                    { 3, 2, 1, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9084), null, 0m, "9780553103540", false, 24.99m, null, 0, 40, "A Game of Thrones", 0, null },
                    { 4, 2, 1, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9087), null, 0m, "9780553108033", false, 24.99m, null, 0, 35, "A Clash of Kings", 0, null },
                    { 5, 5, 9, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9089), null, 0m, "9780385121675", false, 17.99m, null, 0, 30, "The Shining", 0, null },
                    { 6, 5, 9, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9109), null, 0m, "9780450411434", false, 18.99m, null, 0, 25, "It", 0, null },
                    { 7, 3, 5, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9112), null, 0m, "9780547928210", false, 22.99m, null, 0, 40, "The Lord of the Rings: Fellowship", 0, null },
                    { 8, 4, 6, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9114), null, 0m, "9780062073501", false, 14.99m, null, 0, 30, "Murder on the Orient Express", 0, null },
                    { 9, 6, 7, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9116), null, 0m, "9780307474278", false, 16.99m, null, 0, 25, "The Da Vinci Code", 0, null },
                    { 10, 7, 14, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9120), null, 0m, "9780439023481", false, 18.99m, null, 0, 35, "The Hunger Games", 0, null },
                    { 11, 7, 14, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9122), null, 0m, "9780439023498", false, 18.99m, null, 0, 35, "Catching Fire", 0, null },
                    { 12, 7, 14, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9124), null, 0m, "9780439023511", false, 18.99m, null, 0, 35, "Mockingjay", 0, null },
                    { 13, 10, 14, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9127), null, 0m, "9780553293357", false, 15.99m, null, 0, 25, "Foundation", 0, null },
                    { 14, 10, 14, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9129), null, 0m, "9780553294385", false, 15.99m, null, 0, 25, "I, Robot", 0, null },
                    { 15, 8, 1, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9131), null, 0m, "9780684801223", false, 12.99m, null, 0, 20, "The Old Man and The Sea", 0, null },
                    { 16, 9, 1, null, new DateTime(2025, 9, 27, 19, 54, 44, 938, DateTimeKind.Utc).AddTicks(9133), null, 0m, "9780486280615", false, 11.99m, null, 0, 20, "Adventures of Huckleberry Finn", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comment", "CreatedAt", "IsDeleted", "Rating", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Loved this book!", new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(4991), false, 5, 1, null, "00000000-0000-0000-0000-000000000002" },
                    { 2, 3, "Great story.", new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7743), false, 4, 1, null, "00000000-0000-0000-0000-000000000003" },
                    { 3, 5, "Terrifying but amazing.", new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7747), false, 5, 1, null, "00000000-0000-0000-0000-000000000002" },
                    { 4, 10, "Could not put it down!", new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7749), false, 5, 1, null, "00000000-0000-0000-0000-000000000003" },
                    { 5, 7, "Classic fantasy!", new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7751), false, 5, 1, null, "00000000-0000-0000-0000-000000000002" },
                    { 6, 13, "Interesting sci-fi.", new DateTime(2025, 9, 27, 19, 54, 44, 939, DateTimeKind.Utc).AddTicks(7758), false, 4, 1, null, "00000000-0000-0000-0000-000000000003" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003");

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
