CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AppSettings" (
        "Id" uuid NOT NULL,
        "Key" character varying(100) NOT NULL,
        "Value" character varying(500) NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_AppSettings" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AspNetRoles" (
        "Id" text NOT NULL,
        "Name" character varying(256),
        "NormalizedName" character varying(256),
        "ConcurrencyStamp" text,
        CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AspNetUsers" (
        "Id" text NOT NULL,
        "Name" character varying(100) NOT NULL,
        "FirstName" character varying(100),
        "LastName" character varying(100),
        "ProfilePictureUrl" character varying(200),
        "Street" character varying(200),
        "City" character varying(100),
        "State" character varying(100),
        "ZipCode" character varying(20),
        "Country" character varying(100),
        "Gender" character varying(10),
        "DateOfBirth" timestamp with time zone,
        "PreferredLanguage" character varying(50),
        "TwoFactorEnabled" boolean NOT NULL,
        "LastLoginDate" timestamp with time zone,
        "RegisteredDate" timestamp with time zone NOT NULL,
        "TimeZone" character varying(50),
        "IsActive" boolean NOT NULL,
        "ReceiveNewsletter" boolean NOT NULL,
        "ThemePreference" text,
        "UserName" character varying(256),
        "NormalizedUserName" character varying(256),
        "Email" character varying(256),
        "NormalizedEmail" character varying(256),
        "EmailConfirmed" boolean NOT NULL,
        "PasswordHash" text,
        "SecurityStamp" text,
        "ConcurrencyStamp" text,
        "PhoneNumber" text,
        "PhoneNumberConfirmed" boolean NOT NULL,
        "LockoutEnd" timestamp with time zone,
        "LockoutEnabled" boolean NOT NULL,
        "AccessFailedCount" integer NOT NULL,
        CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Authors" (
        "Id" uuid NOT NULL,
        "Name" character varying(100) NOT NULL,
        "Slug" character varying(120) NOT NULL,
        "Bio" character varying(1000),
        "IsDeleted" boolean NOT NULL DEFAULT FALSE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Authors" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Categories" (
        "Id" uuid NOT NULL,
        "Name" character varying(100) NOT NULL,
        "Slug" character varying(120) NOT NULL,
        "IsDeleted" boolean NOT NULL DEFAULT FALSE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Promotions" (
        "Id" uuid NOT NULL,
        "Code" character varying(50) NOT NULL,
        "Type" integer NOT NULL,
        "Value" numeric NOT NULL,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "IsDeleted" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Promotions" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Tags" (
        "Id" uuid NOT NULL,
        "Name" character varying(100) NOT NULL,
        "Slug" character varying(120) NOT NULL,
        "Description" text,
        "IsDeleted" boolean NOT NULL DEFAULT FALSE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Tags" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AspNetRoleClaims" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "RoleId" text NOT NULL,
        "ClaimType" text,
        "ClaimValue" text,
        CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AspNetUserClaims" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "UserId" text NOT NULL,
        "ClaimType" text,
        "ClaimValue" text,
        CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AspNetUserLogins" (
        "LoginProvider" text NOT NULL,
        "ProviderKey" text NOT NULL,
        "ProviderDisplayName" text,
        "UserId" text NOT NULL,
        CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
        CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AspNetUserRoles" (
        "UserId" text NOT NULL,
        "RoleId" text NOT NULL,
        CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
        CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "AspNetUserTokens" (
        "UserId" text NOT NULL,
        "LoginProvider" text NOT NULL,
        "Name" text NOT NULL,
        "Value" text,
        CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
        CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Carts" (
        "Id" uuid NOT NULL,
        "UserId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Carts" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Carts_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Orders" (
        "Id" uuid NOT NULL,
        "UserId" text NOT NULL,
        "OrderDate" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "TransactionStatus" integer NOT NULL,
        "TransactionId" text,
        "SessionId" text,
        "IsDeleted" boolean NOT NULL,
        "OrderStatus" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Orders" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Orders_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "UserOTPs" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "ApplicationUserId" text NOT NULL,
        "OTPNumber" text NOT NULL,
        "ValidTo" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_UserOTPs" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserOTPs_AspNetUsers_ApplicationUserId" FOREIGN KEY ("ApplicationUserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Books" (
        "Id" uuid NOT NULL,
        "Title" character varying(200) NOT NULL,
        "Slug" character varying(220) NOT NULL,
        "Price" numeric(10,2) NOT NULL,
        "Description" text,
        "Stock" integer NOT NULL,
        "CoverImageUrl" text,
        "Discount" numeric(10,2) NOT NULL DEFAULT 0.0,
        "Traffic" integer NOT NULL DEFAULT 0,
        "IsDeleted" boolean NOT NULL DEFAULT FALSE,
        "CategoryId" uuid NOT NULL,
        "AuthorId" uuid NOT NULL,
        "ISBN" character varying(20) NOT NULL,
        "Quantity" integer NOT NULL,
        "PromotionId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Books" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Books_Authors_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES "Authors" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_Books_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_Books_Promotions_PromotionId" FOREIGN KEY ("PromotionId") REFERENCES "Promotions" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Coupon" (
        "Id" uuid NOT NULL,
        "Code" character varying(50) NOT NULL,
        "IsUsed" boolean NOT NULL,
        "ExpiryDate" timestamp with time zone NOT NULL,
        "IsDeleted" boolean NOT NULL,
        "PromotionId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Coupon" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Coupon_Promotions_PromotionId" FOREIGN KEY ("PromotionId") REFERENCES "Promotions" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Shipment" (
        "Id" uuid NOT NULL,
        "OrderId" uuid NOT NULL,
        "CarrierName" character varying(100) NOT NULL,
        "CarrierTrackingId" character varying(50) NOT NULL,
        "ShippedDate" timestamp with time zone NOT NULL,
        "DeliveredDate" timestamp with time zone,
        "Status" integer NOT NULL,
        "IsDeleted" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Shipment" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Shipment_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "BookTag" (
        "BookId" uuid NOT NULL,
        "TagId" uuid NOT NULL,
        CONSTRAINT "PK_BookTag" PRIMARY KEY ("BookId", "TagId"),
        CONSTRAINT "FK_BookTag_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_BookTag_Tags_TagId" FOREIGN KEY ("TagId") REFERENCES "Tags" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "CartItem" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "BookId" uuid NOT NULL,
        "Quantity" integer NOT NULL,
        "CartId" integer NOT NULL,
        "CartId1" uuid NOT NULL,
        CONSTRAINT "PK_CartItem" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_CartItem_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_CartItem_Carts_CartId1" FOREIGN KEY ("CartId1") REFERENCES "Carts" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Discount" (
        "Id" uuid NOT NULL,
        "Name" character varying(50) NOT NULL,
        "Type" integer NOT NULL,
        "Value" numeric NOT NULL,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "IsDeleted" boolean NOT NULL,
        "BookId" uuid,
        "PromotionId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Discount" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Discount_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id"),
        CONSTRAINT "FK_Discount_Promotions_PromotionId" FOREIGN KEY ("PromotionId") REFERENCES "Promotions" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "OrderItems" (
        "Id" uuid NOT NULL,
        "OrderId" uuid NOT NULL,
        "BookId" uuid NOT NULL,
        "Quantity" integer NOT NULL,
        "TotalPrice" numeric NOT NULL,
        "IsDeleted" boolean NOT NULL,
        "Price" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_OrderItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_OrderItems_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE TABLE "Reviews" (
        "Id" uuid NOT NULL,
        "BookId" uuid NOT NULL,
        "UserId" character varying(450) NOT NULL,
        "Rating" integer NOT NULL,
        "Comment" character varying(1000),
        "IsDeleted" boolean NOT NULL DEFAULT FALSE,
        "Status" integer NOT NULL,
        "ReviewerName" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Reviews" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Reviews_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Reviews_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    INSERT INTO "AspNetUsers" ("Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "LastLoginDate", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredLanguage", "ProfilePictureUrl", "ReceiveNewsletter", "RegisteredDate", "SecurityStamp", "State", "Street", "ThemePreference", "TimeZone", "TwoFactorEnabled", "UserName", "ZipCode")
    VALUES ('00000000-0000-0000-0000-000000000001', 0, NULL, '90f186ef-c477-4e95-b023-317b04cd3edf', NULL, NULL, 'admin@booksy.com', TRUE, NULL, NULL, TRUE, NULL, NULL, FALSE, NULL, 'System Admin', 'ADMIN@BOOKSY.COM', 'ADMIN@BOOKSY.COM', 'AQAAAAIAAYagAAAAEEVgao+rr+oOcRFsCn55hmn7j8Fa2FFb9r3DTfud1rrar1BHmYQon9fFxUxJxhq3OQ==', NULL, FALSE, 'en', NULL, TRUE, TIMESTAMPTZ '2026-07-12T14:50:07.500338Z', '275b7daa-8c8b-43a9-94d9-043f1aace741', NULL, NULL, NULL, NULL, FALSE, 'admin@booksy.com', NULL);
    INSERT INTO "AspNetUsers" ("Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "LastLoginDate", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredLanguage", "ProfilePictureUrl", "ReceiveNewsletter", "RegisteredDate", "SecurityStamp", "State", "Street", "ThemePreference", "TimeZone", "TwoFactorEnabled", "UserName", "ZipCode")
    VALUES ('00000000-0000-0000-0000-000000000002', 0, NULL, '49d646bf-2275-4d15-9d5c-03d8bcb0e7eb', NULL, NULL, 'customer1@booksy.com', TRUE, NULL, NULL, TRUE, NULL, NULL, FALSE, NULL, 'Alice', 'CUSTOMER1@BOOKSY.COM', 'CUSTOMER1@BOOKSY.COM', 'AQAAAAIAAYagAAAAEGX1chMx09mvGzjjv63uuKpSCEblvHVWPCYYSoFkDO7pYyyeAcjtYoeFAF3jhN9LvA==', NULL, FALSE, 'en', NULL, TRUE, TIMESTAMPTZ '2026-07-12T14:50:07.584185Z', '9a8526b7-d393-4c6b-9128-281f1fc63a04', NULL, NULL, NULL, NULL, FALSE, 'customer1@booksy.com', NULL);
    INSERT INTO "AspNetUsers" ("Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "LastLoginDate", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredLanguage", "ProfilePictureUrl", "ReceiveNewsletter", "RegisteredDate", "SecurityStamp", "State", "Street", "ThemePreference", "TimeZone", "TwoFactorEnabled", "UserName", "ZipCode")
    VALUES ('00000000-0000-0000-0000-000000000003', 0, NULL, '7fc5cf2c-6ec9-416a-bc2a-bc7eb416897d', NULL, NULL, 'customer2@booksy.com', TRUE, NULL, NULL, TRUE, NULL, NULL, FALSE, NULL, 'Bob', 'CUSTOMER2@BOOKSY.COM', 'CUSTOMER2@BOOKSY.COM', 'AQAAAAIAAYagAAAAEGCxftyP1AT4ikTIDY+Ei6zx7m1RSyor8OoxMIhlXGd8QISaCk+Xi0jFs0nHHxRFaA==', NULL, FALSE, 'en', NULL, TRUE, TIMESTAMPTZ '2026-07-12T14:50:07.641581Z', '7b8647b0-665b-455c-add5-41cc826d1457', NULL, NULL, NULL, NULL, FALSE, 'customer2@booksy.com', NULL);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000001', 'Author of Harry Potter series', TIMESTAMPTZ '2026-07-12T14:50:07.712568Z', 'J.K. Rowling', 'jk-rowling', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000002', 'Author of Game of Thrones series', TIMESTAMPTZ '2026-07-12T14:50:07.712728Z', 'George R.R. Martin', 'george-rr-martin', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000003', 'Author of The Lord of the Rings', TIMESTAMPTZ '2026-07-12T14:50:07.712753Z', 'J.R.R. Tolkien', 'jrr-tolkien', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000004', 'Famous mystery and crime writer', TIMESTAMPTZ '2026-07-12T14:50:07.712769Z', 'Agatha Christie', 'agatha-christie', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000005', 'Renowned horror and thriller author', TIMESTAMPTZ '2026-07-12T14:50:07.712787Z', 'Stephen King', 'stephen-king', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000006', 'Author of The Da Vinci Code and Robert Langdon series', TIMESTAMPTZ '2026-07-12T14:50:07.712802Z', 'Dan Brown', 'dan-brown', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000007', 'Author of The Hunger Games trilogy', TIMESTAMPTZ '2026-07-12T14:50:07.712817Z', 'Suzanne Collins', 'suzanne-collins', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000008', 'American novelist and Nobel Prize winner', TIMESTAMPTZ '2026-07-12T14:50:07.712831Z', 'Ernest Hemingway', 'ernest-hemingway', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000009', 'American writer and humorist', TIMESTAMPTZ '2026-07-12T14:50:07.712969Z', 'Mark Twain', 'mark-twain', NULL);
    INSERT INTO "Authors" ("Id", "Bio", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('20000000-0000-0000-0000-000000000010', 'Science fiction and non-fiction author', TIMESTAMPTZ '2026-07-12T14:50:07.712989Z', 'Isaac Asimov', 'isaac-asimov', NULL);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000001', TIMESTAMPTZ '2026-07-12T14:50:07.700447Z', 'Fiction', 'fiction', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-07-12T14:50:07.710973Z', 'Non-Fiction', 'non-fiction', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-07-12T14:50:07.711541Z', 'Science', 'science', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000004', TIMESTAMPTZ '2026-07-12T14:50:07.71156Z', 'Children', 'children', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000005', TIMESTAMPTZ '2026-07-12T14:50:07.711575Z', 'Fantasy', 'fantasy', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000006', TIMESTAMPTZ '2026-07-12T14:50:07.71159Z', 'Mystery', 'mystery', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000007', TIMESTAMPTZ '2026-07-12T14:50:07.711603Z', 'Thriller', 'thriller', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000008', TIMESTAMPTZ '2026-07-12T14:50:07.711616Z', 'Romance', 'romance', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000009', TIMESTAMPTZ '2026-07-12T14:50:07.711629Z', 'Horror', 'horror', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000010', TIMESTAMPTZ '2026-07-12T14:50:07.711655Z', 'Biography', 'biography', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000011', TIMESTAMPTZ '2026-07-12T14:50:07.711671Z', 'Self-Help', 'self-help', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000012', TIMESTAMPTZ '2026-07-12T14:50:07.711744Z', 'History', 'history', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000013', TIMESTAMPTZ '2026-07-12T14:50:07.711762Z', 'Poetry', 'poetry', NULL);
    INSERT INTO "Categories" ("Id", "CreatedAt", "Name", "Slug", "UpdatedAt")
    VALUES ('10000000-0000-0000-0000-000000000014', TIMESTAMPTZ '2026-07-12T14:50:07.711777Z', 'Science Fiction', 'science-fiction', NULL);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000001', '20000000-0000-0000-0000-000000000001', '10000000-0000-0000-0000-000000000005', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.714745Z', NULL, '9780747532699', 19.99, NULL, 0, 'harry-potter-and-the-philosophers-stone', 50, 'Harry Potter and the Philosopher''s Stone', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000002', '20000000-0000-0000-0000-000000000001', '10000000-0000-0000-0000-000000000005', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715009Z', NULL, '9780747538493', 19.99, NULL, 0, 'harry-potter-and-the-chamber-of-secrets', 45, 'Harry Potter and the Chamber of Secrets', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000003', '20000000-0000-0000-0000-000000000002', '10000000-0000-0000-0000-000000000001', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715032Z', NULL, '9780553103540', 24.99, NULL, 0, 'a-game-of-thrones', 40, 'A Game of Thrones', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000004', '20000000-0000-0000-0000-000000000002', '10000000-0000-0000-0000-000000000001', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715048Z', NULL, '9780553108033', 24.99, NULL, 0, 'a-clash-of-kings', 35, 'A Clash of Kings', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000005', '20000000-0000-0000-0000-000000000005', '10000000-0000-0000-0000-000000000009', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715064Z', NULL, '9780385121675', 17.99, NULL, 0, 'the-shining', 30, 'The Shining', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000006', '20000000-0000-0000-0000-000000000005', '10000000-0000-0000-0000-000000000009', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715081Z', NULL, '9780450411434', 18.99, NULL, 0, 'it', 25, 'It', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000007', '20000000-0000-0000-0000-000000000003', '10000000-0000-0000-0000-000000000005', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715095Z', NULL, '9780547928210', 22.99, NULL, 0, 'the-lord-of-the-rings-fellowship', 40, 'The Lord of the Rings: Fellowship', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000008', '20000000-0000-0000-0000-000000000004', '10000000-0000-0000-0000-000000000006', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715113Z', NULL, '9780062073501', 14.99, NULL, 0, 'murder-on-the-orient-express', 30, 'Murder on the Orient Express', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000009', '20000000-0000-0000-0000-000000000006', '10000000-0000-0000-0000-000000000007', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715129Z', NULL, '9780307474278', 16.99, NULL, 0, 'the-da-vinci-code', 25, 'The Da Vinci Code', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000010', '20000000-0000-0000-0000-000000000007', '10000000-0000-0000-0000-000000000014', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715144Z', NULL, '9780439023481', 18.99, NULL, 0, 'the-hunger-games', 35, 'The Hunger Games', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000011', '20000000-0000-0000-0000-000000000007', '10000000-0000-0000-0000-000000000014', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715159Z', NULL, '9780439023498', 18.99, NULL, 0, 'catching-fire', 35, 'Catching Fire', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000012', '20000000-0000-0000-0000-000000000007', '10000000-0000-0000-0000-000000000014', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715174Z', NULL, '9780439023511', 18.99, NULL, 0, 'mockingjay', 35, 'Mockingjay', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000013', '20000000-0000-0000-0000-000000000010', '10000000-0000-0000-0000-000000000014', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715187Z', NULL, '9780553293357', 15.99, NULL, 0, 'foundation', 25, 'Foundation', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000014', '20000000-0000-0000-0000-000000000010', '10000000-0000-0000-0000-000000000014', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715201Z', NULL, '9780553294385', 15.99, NULL, 0, 'i-robot', 25, 'I, Robot', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000015', '20000000-0000-0000-0000-000000000008', '10000000-0000-0000-0000-000000000001', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715216Z', NULL, '9780684801223', 12.99, NULL, 0, 'the-old-man-and-the-sea', 20, 'The Old Man and The Sea', NULL);
    INSERT INTO "Books" ("Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "ISBN", "Price", "PromotionId", "Quantity", "Slug", "Stock", "Title", "UpdatedAt")
    VALUES ('30000000-0000-0000-0000-000000000016', '20000000-0000-0000-0000-000000000009', '10000000-0000-0000-0000-000000000001', NULL, TIMESTAMPTZ '2026-07-12T14:50:07.715232Z', NULL, '9780486280615', 11.99, NULL, 0, 'adventures-of-huckleberry-finn', 20, 'Adventures of Huckleberry Finn', NULL);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    INSERT INTO "Reviews" ("Id", "BookId", "Comment", "CreatedAt", "Rating", "ReviewerName", "Status", "UpdatedAt", "UserId")
    VALUES ('40000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000001', 'Absolutely loved this book! A must-read for everyone.', TIMESTAMPTZ '2026-07-12T14:50:07.7159Z', 5, 'Alice', 1, NULL, '00000000-0000-0000-0000-000000000002');
    INSERT INTO "Reviews" ("Id", "BookId", "Comment", "CreatedAt", "Rating", "ReviewerName", "Status", "UpdatedAt", "UserId")
    VALUES ('40000000-0000-0000-0000-000000000002', '30000000-0000-0000-0000-000000000003', 'Great story and world-building, though a bit lengthy.', TIMESTAMPTZ '2026-07-12T14:50:07.716046Z', 4, 'Bob', 1, NULL, '00000000-0000-0000-0000-000000000003');
    INSERT INTO "Reviews" ("Id", "BookId", "Comment", "CreatedAt", "Rating", "ReviewerName", "Status", "UpdatedAt", "UserId")
    VALUES ('40000000-0000-0000-0000-000000000003', '30000000-0000-0000-0000-000000000005', 'Terrifying but amazing. Couldn''t put it down!', TIMESTAMPTZ '2026-07-12T14:50:07.716059Z', 5, 'Alice', 1, NULL, '00000000-0000-0000-0000-000000000002');
    INSERT INTO "Reviews" ("Id", "BookId", "Comment", "CreatedAt", "Rating", "ReviewerName", "Status", "UpdatedAt", "UserId")
    VALUES ('40000000-0000-0000-0000-000000000004', '30000000-0000-0000-0000-000000000010', 'The most thrilling read I''ve had all year!', TIMESTAMPTZ '2026-07-12T14:50:07.716071Z', 5, 'Bob', 1, NULL, '00000000-0000-0000-0000-000000000003');
    INSERT INTO "Reviews" ("Id", "BookId", "Comment", "CreatedAt", "Rating", "ReviewerName", "Status", "UpdatedAt", "UserId")
    VALUES ('40000000-0000-0000-0000-000000000005', '30000000-0000-0000-0000-000000000007', 'Classic fantasy at its finest!', TIMESTAMPTZ '2026-07-12T14:50:07.716083Z', 5, 'Alice', 1, NULL, '00000000-0000-0000-0000-000000000002');
    INSERT INTO "Reviews" ("Id", "BookId", "Comment", "CreatedAt", "Rating", "ReviewerName", "Status", "UpdatedAt", "UserId")
    VALUES ('40000000-0000-0000-0000-000000000006', '30000000-0000-0000-0000-000000000013', 'Interesting sci-fi concepts, great foundation for thought.', TIMESTAMPTZ '2026-07-12T14:50:07.716095Z', 4, 'Bob', 1, NULL, '00000000-0000-0000-0000-000000000003');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Author_IsDeleted" ON "Authors" ("IsDeleted");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Author_Name" ON "Authors" ("Name");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Author_Slug" ON "Authors" ("Slug");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Book_AuthorId" ON "Books" ("AuthorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Book_CategoryId" ON "Books" ("CategoryId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Book_CategoryId_IsDeleted" ON "Books" ("CategoryId", "IsDeleted");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Book_ISBN" ON "Books" ("ISBN");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Book_Slug" ON "Books" ("Slug");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Book_Title" ON "Books" ("Title");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Books_PromotionId" ON "Books" ("PromotionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_BookTag_TagId" ON "BookTag" ("TagId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_CartItem_BookId" ON "CartItem" ("BookId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_CartItem_CartId1" ON "CartItem" ("CartId1");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Carts_UserId" ON "Carts" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Category_IsDeleted" ON "Categories" ("IsDeleted");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Category_Name" ON "Categories" ("Name");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Category_Slug" ON "Categories" ("Slug");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Coupon_PromotionId" ON "Coupon" ("PromotionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Discount_BookId" ON "Discount" ("BookId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Discount_PromotionId" ON "Discount" ("PromotionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_OrderItems_BookId" ON "OrderItems" ("BookId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_OrderItems_OrderId" ON "OrderItems" ("OrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Orders_UserId" ON "Orders" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Review_BookId" ON "Reviews" ("BookId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Review_BookId_Status" ON "Reviews" ("BookId", "Status");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Review_IsDeleted" ON "Reviews" ("IsDeleted");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Review_Status" ON "Reviews" ("Status");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Review_UserId" ON "Reviews" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Shipment_OrderId" ON "Shipment" ("OrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Tag_IsDeleted" ON "Tags" ("IsDeleted");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_Tag_Name" ON "Tags" ("Name");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Tag_Slug" ON "Tags" ("Slug");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    CREATE INDEX "IX_UserOTPs_ApplicationUserId" ON "UserOTPs" ("ApplicationUserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260712145008_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260712145008_InitialCreate', '9.0.9');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    ALTER TABLE "CartItem" DROP CONSTRAINT "FK_CartItem_Carts_CartId1";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    DROP INDEX "IX_CartItem_CartId1";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    ALTER TABLE "CartItem" DROP COLUMN "CartId1";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    ALTER TABLE "CartItem" ALTER COLUMN "CartId" TYPE uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "AspNetUsers" SET "ConcurrencyStamp" = 'e24767d1-fb3b-4151-ad5e-d616b79ea1e8', "PasswordHash" = 'AQAAAAIAAYagAAAAEP3qUmTlgzUNekmK+WlTRzNI2dMoi+dmN8oER60lf1lwmtbxegfpU+/mqQOnloy7ZA==', "RegisteredDate" = TIMESTAMPTZ '2026-09-07T03:15:07.743249Z', "SecurityStamp" = '2fd974ba-40d5-464d-b2a6-f1fdb911376e'
    WHERE "Id" = '00000000-0000-0000-0000-000000000001';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "AspNetUsers" SET "ConcurrencyStamp" = 'b20a2f35-8c15-4c37-9f6e-351604a2138b', "PasswordHash" = 'AQAAAAIAAYagAAAAEM+P2PWD4JFwrtYP1WAQ1lSBTz2u/1gR9pyzD3f4VIItqFDmRKzD9+qpa7EIFI/5cg==', "RegisteredDate" = TIMESTAMPTZ '2026-09-07T03:15:08.207747Z', "SecurityStamp" = 'f33da3e2-7fc5-4a2b-a996-b633e39628a9'
    WHERE "Id" = '00000000-0000-0000-0000-000000000002';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "AspNetUsers" SET "ConcurrencyStamp" = 'b4ced78c-9028-40f5-abb3-c4e2081542cb', "PasswordHash" = 'AQAAAAIAAYagAAAAEGcr4imPOL1vvWAr1arSuicocaYFnVmokeKo6tTxYCbQTMEy5nk2KAWSirCuXyT0+A==', "RegisteredDate" = TIMESTAMPTZ '2026-09-07T03:15:08.466836Z', "SecurityStamp" = 'f4beac6c-d24a-4d31-93ec-3161c3972312'
    WHERE "Id" = '00000000-0000-0000-0000-000000000003';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.809745Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000001';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810048Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000002';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810072Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000003';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810087Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000004';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810096Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000005';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810105Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000006';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810114Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000007';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810122Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000008';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810128Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000009';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Authors" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.810138Z'
    WHERE "Id" = '20000000-0000-0000-0000-000000000010';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.82299Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000001';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823543Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000002';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823571Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000003';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823581Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000004';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823589Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000005';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823601Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000006';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823615Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000007';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823629Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000008';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.82364Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000009';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823649Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000010';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823657Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000011';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823664Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000012';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823668Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000013';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.823673Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000014';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.82368Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000015';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Books" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.82369Z'
    WHERE "Id" = '30000000-0000-0000-0000-000000000016';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.791937Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000001';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.807311Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000002';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808383Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000003';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808398Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000004';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808408Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000005';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808415Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000006';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808419Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000007';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808424Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000008';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808431Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000009';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808436Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000010';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808449Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000011';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808485Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000012';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808491Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000013';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Categories" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.808494Z'
    WHERE "Id" = '10000000-0000-0000-0000-000000000014';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Reviews" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.824807Z'
    WHERE "Id" = '40000000-0000-0000-0000-000000000001';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Reviews" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.825189Z'
    WHERE "Id" = '40000000-0000-0000-0000-000000000002';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Reviews" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.825192Z'
    WHERE "Id" = '40000000-0000-0000-0000-000000000003';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Reviews" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.825193Z'
    WHERE "Id" = '40000000-0000-0000-0000-000000000004';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Reviews" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.825194Z'
    WHERE "Id" = '40000000-0000-0000-0000-000000000005';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    UPDATE "Reviews" SET "CreatedAt" = TIMESTAMPTZ '2026-09-07T03:15:08.825196Z'
    WHERE "Id" = '40000000-0000-0000-0000-000000000006';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    CREATE INDEX "IX_CartItem_CartId" ON "CartItem" ("CartId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    ALTER TABLE "CartItem" ADD CONSTRAINT "FK_CartItem_Carts_CartId" FOREIGN KEY ("CartId") REFERENCES "Carts" ("Id") ON DELETE CASCADE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260907031512_FixCartItemForeignKey') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260907031512_FixCartItemForeignKey', '9.0.9');
    END IF;
END $EF$;
COMMIT;

