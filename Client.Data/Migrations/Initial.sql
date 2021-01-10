IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AClient] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [DateBirth] datetime2 NOT NULL,
    [EEducationType] int NOT NULL,
    [Password] nvarchar(max) NULL,
    CONSTRAINT [PK_AClient] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Phone] (
    [Id] int NOT NULL IDENTITY,
    [NumberPhone] nvarchar(max) NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_Phone] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Phone_AClient_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [AClient] ([Id])
);

GO

CREATE INDEX [IX_Phone_ClientId] ON [Phone] ([ClientId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210109202057_Initial', N'3.1.6');

GO

