CREATE TABLE [dbo].[Loans] (
    [Id]           INT NOT NULL IDENTITY,
    [UserId]       INT NOT NULL,
    [DocumentId]   INT NOT NULL,
    [LoanDate]     DATETIME NOT NULL,
    [DueData]	   DATETIME NOT NULL,
    [ReturnedData] DATETIME NULL,
	[CreateUser]			VARCHAR(80) NOT NULL,
	[CreateDate]			DATETIME NOT NULL,
	[UpdateUser]			VARCHAR(80) NULL,
	[UpdateDate]			DATETIME  NULL,

	CONSTRAINT [PK_Loans] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Loans_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Loans_Documents] FOREIGN KEY ([DocumentId]) REFERENCES [Documents]([Id]),
);