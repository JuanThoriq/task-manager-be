CREATE DATABASE TaskManagerDB;
GO

USE TaskManagerDB
GO

-- Tabel Board
CREATE TABLE Board (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrgId NVARCHAR(50) NOT NULL, -- Bisa diisi dengan userID atau organization ID
    Title NVARCHAR(255) NOT NULL,
    ImageId NVARCHAR(100) NOT NULL,
    ImageThumbUrl NVARCHAR(MAX) NOT NULL,
    ImageFullUrl NVARCHAR(MAX) NOT NULL,
    ImageUserName NVARCHAR(MAX) NOT NULL,
    ImageLinkHTML NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- Tabel List
CREATE TABLE List (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(255) NOT NULL,
    [Order] INT NOT NULL, -- Menggunakan nama Order, meskipun keyword, pakai tanda [] untuk SQL Server
    BoardId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_List_Board FOREIGN KEY (BoardId) REFERENCES Board(Id) ON DELETE CASCADE
);
GO

-- Tabel Card
CREATE TABLE Card (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(255) NOT NULL,
    [Order] INT NOT NULL,
    Description NVARCHAR(MAX) NULL,
    ListId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Card_List FOREIGN KEY (ListId) REFERENCES List(Id) ON DELETE CASCADE
);
GO

-- Membuat Index tambahan (opsional)
-- CREATE INDEX IDX_List_BoardId ON List(BoardId);
-- CREATE INDEX IDX_Card_ListId ON Card(ListId);
-- GO
