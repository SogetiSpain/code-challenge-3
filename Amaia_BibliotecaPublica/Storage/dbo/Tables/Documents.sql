CREATE TABLE [dbo].[Documents] (
    [Id]					INT NOT NULL IDENTITY,
    [Title]					VARCHAR (50) NOT NULL,
    [DocumentPropertyId]	INT NOT NULL,
	[CreateUser]			VARCHAR(80) NOT NULL,
	[CreateDate]			DATETIME NOT NULL,
	[UpdateUser]			VARCHAR(80) NULL,
	[UpdateDate]			DATETIME  NULL,

    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Documents_DocumentProperties] FOREIGN KEY ([DocumentPropertyId]) REFERENCES [DocumentProperties]([Id])
);


