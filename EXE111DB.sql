-- Create Database
CREATE DATABASE EXE111DB
GO

USE EXE111DB
GO

-- Create Users table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NOT NULL,
    AvatarUrl NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastLogin DATETIME2,
    Status NVARCHAR(20) DEFAULT 'Active',
    Description NVARCHAR(500),
    CONSTRAINT CHK_Status CHECK (Status IN ('Active', 'Inactive', 'Banned'))
)
GO

-- Create Roles table
CREATE TABLE Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE
)
GO

-- Create UserRoles table
CREATE TABLE UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id)
)
GO

-- Create Interests table
CREATE TABLE Interests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InterestName NVARCHAR(100) NOT NULL UNIQUE,
    IsApproved BIT NOT NULL DEFAULT 0
)
GO

-- Create UserInterests table
CREATE TABLE UserInterests (
    UserId INT NOT NULL,
    InterestId INT NOT NULL,
    CONSTRAINT PK_UserInterests PRIMARY KEY (UserId, InterestId),
    CONSTRAINT FK_UserInterests_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_UserInterests_Interests FOREIGN KEY (InterestId) REFERENCES Interests(Id)
)
GO

-- Create ProposedInterests table
CREATE TABLE ProposedInterests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InterestName NVARCHAR(100) NOT NULL,
    UserId INT NOT NULL,
    ProposedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    ReviewedAt DATETIME2,
    AdminID INT,
    CONSTRAINT FK_ProposedInterests_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_ProposedInterests_Admin FOREIGN KEY (AdminID) REFERENCES Users(Id),
    CONSTRAINT CHK_ProposedStatus CHECK (Status IN ('Pending', 'Approved', 'Rejected'))
)
GO

-- Create Messages table
CREATE TABLE Messages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SenderId INT NOT NULL,
    RecerverId INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    SendAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsRead BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Messages_Sender FOREIGN KEY (SenderId) REFERENCES Users(Id),
    CONSTRAINT FK_Messages_Receiver FOREIGN KEY (RecerverId) REFERENCES Users(Id)
)
GO

-- Create Conversations table
CREATE TABLE Conversations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    User1Id INT NOT NULL,
    User2Id INT NOT NULL,
    StartedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Conversations_User1 FOREIGN KEY (User1Id) REFERENCES Users(Id),
    CONSTRAINT FK_Conversations_User2 FOREIGN KEY (User2Id) REFERENCES Users(Id)
)
GO

-- Create indexes for better performance
CREATE INDEX IX_Users_Username ON Users(Username)
CREATE INDEX IX_Users_Email ON Users(Email)
CREATE INDEX IX_Messages_SenderId ON Messages(SenderId)
CREATE INDEX IX_Messages_RecerverId ON Messages(RecerverId)
CREATE INDEX IX_Conversations_Users ON Conversations(User1Id, User2Id)

-- Insert default roles
INSERT INTO Roles (RoleName) VALUES ('User'), ('Admin')
GO