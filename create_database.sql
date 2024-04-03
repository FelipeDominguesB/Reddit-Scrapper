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

CREATE TABLE [Routines] (
    [Id] bigint NOT NULL IDENTITY,
    [SubredditName] nvarchar(max) NOT NULL,
    [SyncRate] int NOT NULL,
    [MaxPostsPerSync] int NOT NULL,
    [PostSorting] int NOT NULL,
    [NextRun] datetime2 NULL,
    [CreationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Routines] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RoutinesHistory] (
    [Id] bigint NOT NULL IDENTITY,
    [RoutineId] bigint NOT NULL,
    [Succeded] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_RoutinesHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoutinesHistory_Routines_RoutineId] FOREIGN KEY ([RoutineId]) REFERENCES [Routines] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RoutinesHistory_RoutineId] ON [RoutinesHistory] ([RoutineId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240103002459_initial', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] bigint NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [PasswordSalt] nvarchar(max) NOT NULL,
    [IsAdmin] bit NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [IsEmailVerified] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240103002542_user-table', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Routines] ADD [UserId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE INDEX [IX_Routines_UserId] ON [Routines] ([UserId]);
GO

ALTER TABLE [Routines] ADD CONSTRAINT [FK_Routines_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240103002705_user-to-routine', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [RoutinesHistory];
GO

CREATE TABLE [RoutinesExecutions] (
    [Id] bigint NOT NULL IDENTITY,
    [RoutineId] bigint NOT NULL,
    [Succeded] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_RoutinesExecutions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoutinesExecutions_Routines_RoutineId] FOREIGN KEY ([RoutineId]) REFERENCES [Routines] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RoutinesExecutions_RoutineId] ON [RoutinesExecutions] ([RoutineId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240104005007_routine-execution', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [RoutinesExecutions];
GO

CREATE TABLE [RoutineExecution] (
    [Id] bigint NOT NULL IDENTITY,
    [RoutineId] bigint NOT NULL,
    [Succeded] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_RoutineExecution] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoutineExecution_Routines_RoutineId] FOREIGN KEY ([RoutineId]) REFERENCES [Routines] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RoutineExecutionFile] (
    [Id] bigint NOT NULL IDENTITY,
    [ExecutionId] bigint NOT NULL,
    [FileName] nvarchar(max) NOT NULL,
    [SourceUrl] nvarchar(max) NOT NULL,
    [DownloadDirectory] nvarchar(max) NULL,
    [Classification] int NOT NULL,
    [Succeded] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_RoutineExecutionFile] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoutineExecutionFile_RoutineExecution_ExecutionId] FOREIGN KEY ([ExecutionId]) REFERENCES [RoutineExecution] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RoutineExecution_RoutineId] ON [RoutineExecution] ([RoutineId]);
GO

CREATE INDEX [IX_RoutineExecutionFile_ExecutionId] ON [RoutineExecutionFile] ([ExecutionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240104005639_routine-execution-files', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [RoutineExecution] DROP CONSTRAINT [FK_RoutineExecution_Routines_RoutineId];
GO

ALTER TABLE [RoutineExecutionFile] DROP CONSTRAINT [FK_RoutineExecutionFile_RoutineExecution_ExecutionId];
GO

ALTER TABLE [RoutineExecutionFile] DROP CONSTRAINT [PK_RoutineExecutionFile];
GO

ALTER TABLE [RoutineExecution] DROP CONSTRAINT [PK_RoutineExecution];
GO

EXEC sp_rename N'[RoutineExecutionFile]', N'RoutineExecutionsFiles';
GO

EXEC sp_rename N'[RoutineExecution]', N'RoutinesExecutions';
GO

EXEC sp_rename N'[RoutineExecutionsFiles].[IX_RoutineExecutionFile_ExecutionId]', N'IX_RoutineExecutionsFiles_ExecutionId', N'INDEX';
GO

EXEC sp_rename N'[RoutinesExecutions].[IX_RoutineExecution_RoutineId]', N'IX_RoutinesExecutions_RoutineId', N'INDEX';
GO

ALTER TABLE [RoutineExecutionsFiles] ADD CONSTRAINT [PK_RoutineExecutionsFiles] PRIMARY KEY ([Id]);
GO

ALTER TABLE [RoutinesExecutions] ADD CONSTRAINT [PK_RoutinesExecutions] PRIMARY KEY ([Id]);
GO

ALTER TABLE [RoutineExecutionsFiles] ADD CONSTRAINT [FK_RoutineExecutionsFiles_RoutinesExecutions_ExecutionId] FOREIGN KEY ([ExecutionId]) REFERENCES [RoutinesExecutions] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [RoutinesExecutions] ADD CONSTRAINT [FK_RoutinesExecutions_Routines_RoutineId] FOREIGN KEY ([RoutineId]) REFERENCES [Routines] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240104010016_routine-execution-files-fixed', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoutinesExecutions]') AND [c].[name] = N'Succeded');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [RoutinesExecutions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [RoutinesExecutions] DROP COLUMN [Succeded];
GO

ALTER TABLE [RoutinesExecutions] ADD [MaxPostsPerSync] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [RoutinesExecutions] ADD [PostSorting] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [RoutinesExecutions] ADD [SyncRate] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [RoutinesExecutions] ADD [TotalLinksFound] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240106024103_routine-execution-properties', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [RoutinesExecutions] ADD [Succeded] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240106030854_routine-execution-succeded-back', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240131015712_routine-entity-update', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoutineExecutionsFiles]') AND [c].[name] = N'FileName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [RoutineExecutionsFiles] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [RoutineExecutionsFiles] ALTER COLUMN [FileName] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoutineExecutionsFiles]') AND [c].[name] = N'Classification');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [RoutineExecutionsFiles] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [RoutineExecutionsFiles] ALTER COLUMN [Classification] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240131022304_routine-execution-file-new-nullables', N'6.0.22');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240329213401_test', N'6.0.22');
GO

COMMIT;
GO

