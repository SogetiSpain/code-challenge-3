using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Biblioteca
{
    public class BibliotecaService : IBibliotecaService
    {
        public bool LoanBook(int libroId, int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                db.Prestamos.Add(new Prestamos()
                {
                    LibroId = libroId,
                    UsuarioId = usuarioId,
                    FechaPrestamo = DateTime.Now.Date
                });

                db.SaveChanges();
                return true;
            }
        }

        public bool RegistreBook(string name)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                db.Libros.Add(new Libros {Titulo = name});
                db.SaveChanges();
                return true;
            }
        }

        public bool ReturnBook(int id)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                var prestamo = db.Prestamos.First(x => x.LibroId == id);
                db.Prestamos.Remove(prestamo);
                db.SaveChanges();
                return true;
            }
        }

        public Libros FindBook(string titulo)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                var id = db.Libros.ToList().First(x => x.Titulo.Trim() == titulo).Id;
                return db.Libros.Find(id);
            }
        }

        public Libros FindBook(int libroId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Libros.Find(libroId);
            }
        }

        public List<Libros> GetAllBooks()
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Libros.ToList();
            }
        }

        public Usuarios FindUsuario(string nombre)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Usuarios.FirstOrDefault(x => x.Nombre.Trim() == nombre);
            }
        }

        public bool CrearUsuario(string nombreUsuario)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                db.Usuarios.Add(new Usuarios()
                {
                    Nombre = nombreUsuario
                });

                db.SaveChanges();
                return true;
            }
        }

        public bool ExistUser(string nombreUsuario)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Usuarios.Any(x => x.Nombre.Trim() == nombreUsuario);
            }
        }

        public bool IsBookBorrowed(int libroId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Prestamos.ToList().Any(x => x.LibroId == libroId);
            }
        }

        public bool CanBorrowBooks(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Prestamos.Count(x => x.UsuarioId == usuarioId) < 3;
            }
        }

        public List<Prestamos> GetOwnBorrowedBooks(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Prestamos.Where(x => x.UsuarioId == usuarioId).ToList();
            }
        }

        public bool AddFine(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                db.Multas.Add(new Multas()
                {
                    UsuarioId = usuarioId
                });
                db.SaveChanges();
                return true;
            }
        }

        public bool HasFines(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                return db.Multas.Any(x => x.UsuarioId == usuarioId);
            }
        }
    }
}