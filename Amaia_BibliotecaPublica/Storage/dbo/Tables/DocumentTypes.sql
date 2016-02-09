CREATE TABLE [dbo].[DocumentTypes] (
    [Id]			INT NOT NULL IDENTITY,
    [Type]			VARCHAR (50) NOT NULL,
    [Code]			VARCHAR (10) NOT NULL,

    CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);
