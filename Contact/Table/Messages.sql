CREATE TABLE [dbo].[Messages]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[Message] nvarchar(Max) not null,
	Sender int not null,
	Recipient int null,
	ToGroup int not null, 
    CONSTRAINT [FK_Messages_ToSenderUser] FOREIGN KEY ([Sender]) REFERENCES [User]([Id]),
	CONSTRAINT [FK_Messages_ToRecipientUser] FOREIGN KEY ([Recipient]) REFERENCES [User]([Id]),
	CONSTRAINT [FK_Messages_ToGroup] FOREIGN KEY (ToGroup) REFERENCES Groupes([Id]),


)

GO

CREATE INDEX [IX_Messages_SenderRecipient] ON [dbo].[Messages] (Sender, Recipient)

GO

CREATE INDEX [IX_Messages_SenderGroupe] ON [dbo].[Messages] (Sender, ToGroup)
