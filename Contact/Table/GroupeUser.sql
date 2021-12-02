CREATE TABLE [dbo].[GroupeUser]
(
	[IdUser] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [IdGroupe] INT NOT NULL, 
    [IsAdmin] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_GroupeUser_ToUser] FOREIGN KEY (IdUser) REFERENCES [User](Id),    
    CONSTRAINT [FK_GroupeUser_ToGroupe] FOREIGN KEY (IdGroupe) REFERENCES [User](Id)
)
