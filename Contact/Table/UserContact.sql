CREATE TABLE [dbo].[UserContact]
(
	[IdUser] INT NOT NULL ,
	[IdContact] INT NOT NULL, 
    CONSTRAINT [PK_UserContact] PRIMARY KEY (IdUser,IdContact), 
    CONSTRAINT [FK_UserContact_ToUser] FOREIGN KEY (IdUser) REFERENCES [User]([Id]),
    CONSTRAINT [FK_UserContact_ToContact] FOREIGN KEY (IdContact) REFERENCES [Contact]([Id])
)
