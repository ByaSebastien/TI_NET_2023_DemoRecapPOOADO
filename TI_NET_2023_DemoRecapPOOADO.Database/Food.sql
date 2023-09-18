CREATE TABLE [dbo].[Food]
(
	[Id] INT IDENTITY,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	CONSTRAINT PK_Food PRIMARY KEY (Id)
)
