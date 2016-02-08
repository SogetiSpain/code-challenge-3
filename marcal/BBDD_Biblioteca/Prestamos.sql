CREATE TABLE [dbo].[Prestamos]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LibroId] INT NOT NULL, 
    [UsuarioId] INT NOT NULL, 
    [FechaPrestamo] DATETIME NOT NULL
	FOREIGN KEY (LibroId) REFERENCES Libros(Id)
	FOREIGN KEY ([UsuarioId]) REFERENCES Usuarios(Id)
)
