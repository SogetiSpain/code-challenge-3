CREATE TABLE [dbo].[Prestamos]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [LibroId] INT NULL, 
    [UsuarioId] INT NULL, 
    [FechaPrestamo] DATETIME NULL
	FOREIGN KEY (LibroId) REFERENCES Libros(Id)
	FOREIGN KEY ([UsuarioId]) REFERENCES Usuarios(Id)
)
