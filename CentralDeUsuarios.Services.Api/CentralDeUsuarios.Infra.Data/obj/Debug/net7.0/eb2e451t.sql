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

CREATE TABLE [Usuario] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(max) NULL,
    [Email] nvarchar(450) NULL,
    [Senha] nvarchar(max) NULL,
    [DataHoraDeCriacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Usuario_Email] ON [Usuario] ([Email]) WHERE [Email] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221230231938_Initial', N'7.0.1');
GO

COMMIT;
GO

