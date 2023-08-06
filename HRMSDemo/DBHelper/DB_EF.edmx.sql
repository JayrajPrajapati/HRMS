
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/27/2022 21:16:48
-- Generated from EDMX file: D:\Work\HRMS\HRMSDemo\HRMSDemo\DBHelper\DB_EF.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HRMSDEMO(NEW)];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CompanyResponse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyResponse];
GO
IF OBJECT_ID(N'[dbo].[Prospects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prospects];
GO
IF OBJECT_ID(N'[dbo].[ProspectSkillMaping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProspectSkillMaping];
GO
IF OBJECT_ID(N'[dbo].[Skills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Skills];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Prospects'
CREATE TABLE [dbo].[Prospects] (
    [ProspectID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [DOB] datetime  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Mobile] decimal(18,0)  NOT NULL,
    [Gender] bit  NOT NULL,
    [Address] nvarchar(200)  NOT NULL,
    [FileName] nvarchar(100)  NOT NULL,
    [Status] tinyint  NOT NULL
);
GO

-- Creating table 'ProspectSkillMapings'
CREATE TABLE [dbo].[ProspectSkillMapings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProspectID] int  NOT NULL,
    [SkillID] int  NOT NULL
);
GO

-- Creating table 'Skills'
CREATE TABLE [dbo].[Skills] (
    [SkillID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'CompanyResponses'
CREATE TABLE [dbo].[CompanyResponses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProspectID] int  NULL,
    [Status] tinyint  NULL,
    [ScheduleDate] datetime  NULL,
    [UserID] int  NULL,
    [Comments] nvarchar(500)  NULL,
    [CreatedDate] datetime  NULL,
    [CreatedBy] int  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [RoleType] tinyint  NULL,
    [Active] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ProspectID] in table 'Prospects'
ALTER TABLE [dbo].[Prospects]
ADD CONSTRAINT [PK_Prospects]
    PRIMARY KEY CLUSTERED ([ProspectID] ASC);
GO

-- Creating primary key on [ID] in table 'ProspectSkillMapings'
ALTER TABLE [dbo].[ProspectSkillMapings]
ADD CONSTRAINT [PK_ProspectSkillMapings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [SkillID] in table 'Skills'
ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [PK_Skills]
    PRIMARY KEY CLUSTERED ([SkillID] ASC);
GO

-- Creating primary key on [ID] in table 'CompanyResponses'
ALTER TABLE [dbo].[CompanyResponses]
ADD CONSTRAINT [PK_CompanyResponses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------