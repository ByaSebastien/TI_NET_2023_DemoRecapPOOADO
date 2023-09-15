CREATE TABLE [dbo].[Member]
(
	Id INT IDENTITY,
	username nvarchar(50) NOT NULL,
	[password] nvarchar(max) NOT NULL,
	roles int NOT NULL,
	CONSTRAINT PK_MEMBER PRIMARY KEY (Id),
	CONSTRAINT CK_MEMBER_USERNAME CHECK (username != '')
)
