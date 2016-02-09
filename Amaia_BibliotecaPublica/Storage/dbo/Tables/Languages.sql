CREATE TABLE [dbo].[Languages] (
    [Id]			INT NOT NULL IDENTITY,
    [Code]			VARCHAR (50) NOT NULL,
    [Text]			VARCHAR (50) NOT NULL,

    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([Id] ASC)
);