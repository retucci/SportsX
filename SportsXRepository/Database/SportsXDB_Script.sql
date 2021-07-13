IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [Tipo] int NOT NULL,
    [Nome] varchar(100) NOT NULL,
    [RazaoSocial] varchar(100) NULL,
    [Cpf] varchar(11) NULL,
    [Cnpj] varchar(14) NULL,
    [Cep] varchar(8) NULL,
    [Classificacao] int NOT NULL,
    [Email] nvarchar(max) NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ClienteTelefones] (
    [Id] int NOT NULL IDENTITY,
    [Numero] varchar(14) NOT NULL,
    [ClienteId] int NOT NULL,
    CONSTRAINT [PK_ClienteTelefones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClienteTelefones_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_ClienteTelefones_ClienteId] ON [ClienteTelefones] ([ClienteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210713003552_SportsXDB', N'3.1.0');

GO