﻿CREATE TABLE [dbo].[Produce]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Price] MONEY NOT NULL
)