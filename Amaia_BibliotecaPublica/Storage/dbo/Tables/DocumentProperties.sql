CREATE TABLE [dbo].[DocumentProperties] (
    [Id]			INT NOT NULL IDENTITY,
    [TypeId]		INT NOT NULL,
    [LanguageId]	INT NOT NULL,
    [CanBeLent]		BIT NOT NULL,
    [MaxDaysRent]	INT NOT NULL,
    [FinePrice]		INT	NOT NULL,
	[CreateUser]	VARCHAR(80) NOT NULL,
	[CreateDate]	DATETIME NOT NULL,
	[UpdateUser]	VARCHAR(80) NULL,
	[UpdateDate]	DATETIME  NULL,

    CONSTRAINT [PK_DocumentProperties] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_DocumentProperties_DocumentTypes] FOREIGN KEY ([TypeId]) REFERENCES [DocumentTypes]([Id]),
	CONSTRAINT [FK_DocumentProperties_Languages] FOREIGN KEY ([LanguageId]) REFERENCES [Languages]([Id])
);
