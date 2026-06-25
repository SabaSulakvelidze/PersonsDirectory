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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE TABLE [Cities] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Cities] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE TABLE [Persons] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Gender] int NOT NULL,
        [PersonalNumber] nvarchar(11) NOT NULL,
        [DateOfBirth] date NOT NULL,
        [CityId] int NOT NULL,
        [ImagePath] nvarchar(500) NOT NULL,
        CONSTRAINT [PK_Persons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Persons_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE TABLE [PersonRelations] (
        [Id] int NOT NULL IDENTITY,
        [RelationType] int NOT NULL,
        [PersonId] int NOT NULL,
        [RelatedPersonId] int NOT NULL,
        CONSTRAINT [PK_PersonRelations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PersonRelations_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PersonRelations_Persons_RelatedPersonId] FOREIGN KEY ([RelatedPersonId]) REFERENCES [Persons] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE TABLE [PhoneNumbers] (
        [Id] int NOT NULL IDENTITY,
        [Type] int NOT NULL,
        [Number] nvarchar(50) NOT NULL,
        [PersonId] int NOT NULL,
        CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PhoneNumbers_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Cities]'))
        SET IDENTITY_INSERT [Cities] ON;
    EXEC(N'INSERT INTO [Cities] ([Id], [Name])
    VALUES (1, N''Tbilisi''),
    (2, N''Batumi''),
    (3, N''Kutaisi''),
    (4, N''Rustavi''),
    (5, N''Zugdidi'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Cities]'))
        SET IDENTITY_INSERT [Cities] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Cities_Name] ON [Cities] ([Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PersonRelations_PersonId_RelatedPersonId] ON [PersonRelations] ([PersonId], [RelatedPersonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PersonRelations_RelatedPersonId] ON [PersonRelations] ([RelatedPersonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Persons_CityId] ON [Persons] ([CityId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Persons_PersonalNumber] ON [Persons] ([PersonalNumber]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PhoneNumbers_PersonId] ON [PhoneNumbers] ([PersonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624153910_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260624153910_InitialCreate', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624185422_MakeImagePathNullable'
)
BEGIN
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persons]') AND [c].[name] = N'ImagePath');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Persons] DROP CONSTRAINT ' + @var + ';');
    ALTER TABLE [Persons] ALTER COLUMN [ImagePath] nvarchar(500) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260624185422_MakeImagePathNullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260624185422_MakeImagePathNullable', N'10.0.9');
END;

COMMIT;
GO

