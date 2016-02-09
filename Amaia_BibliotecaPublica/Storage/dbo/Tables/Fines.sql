CREATE TABLE [dbo].[Fines] (
    [Id]					INT NOT NULL IDENTITY,
    [UserId]				INT NOT NULL,
    [FineValue]				INT NOT NULL,
    [IsPaid]				BIT NOT NULL,
    [PaidDate]				DATETIME NULL,
	[CreateUser]			VARCHAR(80) NOT NULL,
	[CreateDate]			DATETIME NOT NULL,
	[UpdateUser]			VARCHAR(80) NULL,
	[UpdateDate]			DATETIME  NULL,

    CONSTRAINT [PK_Fines] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Fines_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);