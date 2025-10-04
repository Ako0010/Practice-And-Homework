CREATE DATABASE [Car]
GO

USE [Car]

CREATE TABLE [dbo].[Cars] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Model] NVARCHAR (50) NOT NULL,
    [Year]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
