using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace DataAccessLayer
{
    public class BibliotecaService : IBibliotecaService
    {
        public bool LoanBook(int libroId, int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    db.Prestamos.Add(new Prestamos
                    {
                        LibroId = libroId,
                        UsuarioId = usuarioId,
                        FechaPrestamo = DateTime.Now.Date
                    });

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool RegistreBook(string name)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    db.Libros.Add(new Libros {Titulo = name});
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool ReturnBook(int id)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    var prestamo = db.Prestamos.First(x => x.LibroId == id);
                    db.Prestamos.Remove(prestamo);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public Libros FindBook(string titulo)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    var id = db.Libros.ToList().First(x => x.Titulo.Trim() == titulo).Id;
                    return db.Libros.Find(id);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public Libros FindBook(int libroId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Libros.Find(libroId);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public List<Libros> GetAllBooks()
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Libros.ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public Usuarios FindUsuario(string nombre)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Usuarios.FirstOrDefault(x => x.Nombre.Trim() == nombre);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool CrearUsuario(string nombreUsuario)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    db.Usuarios.Add(new Usuarios
                    {
                        Nombre = nombreUsuario
                    });

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool ExistUser(string nombreUsuario)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Usuarios.Any(x => x.Nombre.Trim() == nombreUsuario);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool IsBookBorrowed(int libroId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Prestamos.ToList().Any(x => x.LibroId == libroId);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool CanBorrowBooks(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Prestamos.Count(x => x.UsuarioId == usuarioId) < 3;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public List<Prestamos> GetOwnBorrowedBooks(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Prestamos.Where(x => x.UsuarioId == usuarioId).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool AddFine(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    db.Multas.Add(new Multas
                    {
                        UsuarioId = usuarioId
                    });
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool HasFines(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Multas.Any(x => x.UsuarioId == usuarioId);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void PayFine(int usuarioId)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    var multa = db.Multas.First(x => x.UsuarioId == usuarioId);
                    db.Multas.Remove(multa);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public bool ExistBook(string titulo)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                try
                {
                    return db.Libros.Any(x => x.Titulo.Trim() == titulo);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}