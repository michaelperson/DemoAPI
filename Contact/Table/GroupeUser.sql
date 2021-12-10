CREATE TABLE [dbo].[GroupeUser]
(
	[IdUser] INT NOT NULL, 
    [IdGroupe] INT NOT NULL, 
    [IsAdmin] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_USER_GROUPES] PRIMARY KEY (IdUser,IdGroupe),
    CONSTRAINT [FK_GroupeUser_ToUser] FOREIGN KEY (IdUser) REFERENCES [User](Id),    
    CONSTRAINT [FK_GroupeUser_ToGroupe] FOREIGN KEY (IdGroupe) REFERENCES [User](Id)
)
