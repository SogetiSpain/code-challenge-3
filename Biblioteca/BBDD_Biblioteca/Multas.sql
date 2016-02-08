CREATE TABLE [dbo].[Multas]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UsuarioId] INT NOT NULL
	FOREIGN KEY ([UsuarioId]) REFERENCES Usuarios(Id)
)
