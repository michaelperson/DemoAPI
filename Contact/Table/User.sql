CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [LastName] NVARCHAR(250) NOT NULL, 
    [FirstName] NVARCHAR(250) NOT NULL, 
    [Email] NVARCHAR(300) NOT NULL, 
    [Password] VARBINARY(128) NOT NULL, 
    [Salt] VARBINARY(128) NOT NULL
)
