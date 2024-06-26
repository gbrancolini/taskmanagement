CREATE DATABASE TaskManagementDb;
GO

USE TaskManagementDb;
GO

CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Priority INT,
    DueDate DATE,
    Status NVARCHAR(50)
);
GO
