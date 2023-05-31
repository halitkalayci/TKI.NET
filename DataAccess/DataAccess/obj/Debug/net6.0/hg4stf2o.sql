IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Brands] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LogoUrl] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [DeletedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cars] (
    [Id] int NOT NULL IDENTITY,
    [Plate] nvarchar(max) NOT NULL,
    [Kilometer] int NOT NULL,
    [ModelYear] int NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [IsAutomatic] bit NOT NULL,
    [BrandId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [DeletedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cars_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Cars_BrandId] ON [Cars] ([BrandId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230530084058_init', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cars] ADD [Color] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230530085225_Add_Color_To_Car', N'7.0.5');
GO

COMMIT;
GO

