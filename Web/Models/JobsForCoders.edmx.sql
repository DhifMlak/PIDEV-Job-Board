
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/14/2019 09:20:00
-- Generated from EDMX file: D:\[Pidev]]\JobsForCoders\Models\JobsForCoders.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_JobPosting_Employer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offres] DROP CONSTRAINT [FK_JobPosting_Employer];
GO
IF OBJECT_ID(N'[dbo].[FK_JobID_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Applications] DROP CONSTRAINT [FK_JobID_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_JobSeekerID_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Applications] DROP CONSTRAINT [FK_JobSeekerID_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidatEntreprise_Candidat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CandidatEntreprise] DROP CONSTRAINT [FK_CandidatEntreprise_Candidat];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidatEntreprise_Entreprise]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CandidatEntreprise] DROP CONSTRAINT [FK_CandidatEntreprise_Entreprise];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Entreprises]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entreprises];
GO
IF OBJECT_ID(N'[dbo].[Offres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offres];
GO
IF OBJECT_ID(N'[dbo].[Candidats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Candidats];
GO
IF OBJECT_ID(N'[dbo].[Applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Applications];
GO
IF OBJECT_ID(N'[dbo].[CandidatEntreprise]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CandidatEntreprise];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Entreprises'
CREATE TABLE [dbo].[Entreprises] (
    [EntrepriseID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Cellphone] nvarchar(max)  NOT NULL,
    [Website] nvarchar(max)  NOT NULL,
    [Logo] nvarchar(max)  NULL
);
GO

-- Creating table 'Offres'
CREATE TABLE [dbo].[Offres] (
    [JobID] int IDENTITY(1,1) NOT NULL,
    [EntrepriseID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Position] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Buzz_Words] nvarchar(max)  NOT NULL,
    [Salary] float  NOT NULL
);
GO

-- Creating table 'Candidats'
CREATE TABLE [dbo].[Candidats] (
    [JobSeekerID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Birthdate] datetime  NOT NULL,
    [Gender] nvarchar(50)  NOT NULL,
    [Education] nvarchar(max)  NOT NULL,
    [Objectives] nvarchar(max)  NOT NULL,
    [Introduction] nvarchar(max)  NOT NULL,
    [Links] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Cellphone] nvarchar(max)  NOT NULL,
    [Buzz_Words] nvarchar(max)  NOT NULL,
    [Operator] nvarchar(max)  NOT NULL,
    [Photo1] nvarchar(max)  NULL,
    [CV] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Applications'
CREATE TABLE [dbo].[Applications] (
    [ApplicationID] int IDENTITY(1,1) NOT NULL,
    [JobID] int  NOT NULL,
    [JobSeekerID] int  NOT NULL,
    [Application_Date] datetime  NOT NULL
);
GO

-- Creating table 'CandidatEntreprise'
CREATE TABLE [dbo].[CandidatEntreprise] (
    [Candidats_JobSeekerID] int  NOT NULL,
    [Entreprises_EntrepriseID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EntrepriseID] in table 'Entreprises'
ALTER TABLE [dbo].[Entreprises]
ADD CONSTRAINT [PK_Entreprises]
    PRIMARY KEY CLUSTERED ([EntrepriseID] ASC);
GO

-- Creating primary key on [JobID] in table 'Offres'
ALTER TABLE [dbo].[Offres]
ADD CONSTRAINT [PK_Offres]
    PRIMARY KEY CLUSTERED ([JobID] ASC);
GO

-- Creating primary key on [JobSeekerID] in table 'Candidats'
ALTER TABLE [dbo].[Candidats]
ADD CONSTRAINT [PK_Candidats]
    PRIMARY KEY CLUSTERED ([JobSeekerID] ASC);
GO

-- Creating primary key on [ApplicationID] in table 'Applications'
ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [PK_Applications]
    PRIMARY KEY CLUSTERED ([ApplicationID] ASC);
GO

-- Creating primary key on [Candidats_JobSeekerID], [Entreprises_EntrepriseID] in table 'CandidatEntreprise'
ALTER TABLE [dbo].[CandidatEntreprise]
ADD CONSTRAINT [PK_CandidatEntreprise]
    PRIMARY KEY CLUSTERED ([Candidats_JobSeekerID], [Entreprises_EntrepriseID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EntrepriseID] in table 'Offres'
ALTER TABLE [dbo].[Offres]
ADD CONSTRAINT [FK_JobPosting_Employer]
    FOREIGN KEY ([EntrepriseID])
    REFERENCES [dbo].[Entreprises]
        ([EntrepriseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobPosting_Employer'
CREATE INDEX [IX_FK_JobPosting_Employer]
ON [dbo].[Offres]
    ([EntrepriseID]);
GO

-- Creating foreign key on [JobID] in table 'Applications'
ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [FK_JobID_ToTable]
    FOREIGN KEY ([JobID])
    REFERENCES [dbo].[Offres]
        ([JobID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobID_ToTable'
CREATE INDEX [IX_FK_JobID_ToTable]
ON [dbo].[Applications]
    ([JobID]);
GO

-- Creating foreign key on [JobSeekerID] in table 'Applications'
ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [FK_JobSeekerID_ToTable]
    FOREIGN KEY ([JobSeekerID])
    REFERENCES [dbo].[Candidats]
        ([JobSeekerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobSeekerID_ToTable'
CREATE INDEX [IX_FK_JobSeekerID_ToTable]
ON [dbo].[Applications]
    ([JobSeekerID]);
GO

-- Creating foreign key on [Candidats_JobSeekerID] in table 'CandidatEntreprise'
ALTER TABLE [dbo].[CandidatEntreprise]
ADD CONSTRAINT [FK_CandidatEntreprise_Candidat]
    FOREIGN KEY ([Candidats_JobSeekerID])
    REFERENCES [dbo].[Candidats]
        ([JobSeekerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Entreprises_EntrepriseID] in table 'CandidatEntreprise'
ALTER TABLE [dbo].[CandidatEntreprise]
ADD CONSTRAINT [FK_CandidatEntreprise_Entreprise]
    FOREIGN KEY ([Entreprises_EntrepriseID])
    REFERENCES [dbo].[Entreprises]
        ([EntrepriseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidatEntreprise_Entreprise'
CREATE INDEX [IX_FK_CandidatEntreprise_Entreprise]
ON [dbo].[CandidatEntreprise]
    ([Entreprises_EntrepriseID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------