using System;
using System.Linq;

namespace Biblioteca
{
    public class BibliotecaService : IBibliotecaService
    {
        public bool LoanBook(int id)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                var libros = db.Set<Libros>();
                var libro = libros.ToList().First(x => x.Id == id);
                libro.Disponible = false;

                db.SaveChanges();

                return true;
            }
        }

        public bool RegistreBook(string name)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                var libros = db.Set<Libros>();
                libros.Add(new Libros { Titulo = name, Disponible = true });

                db.SaveChanges();
            }

            return true;
        }

        public bool ReturnBook(int id)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                var libros = db.Set<Libros>();
                var libro = libros.ToList().First(x => x.Id == id);
                libro.Disponible = true;

                db.SaveChanges();

                return true;
            }
        }

        public Libros Find(string titulo)
        {
            using (var db = new BBDD_BibliotecaEntities())
            {
                var libros = db.Set<Libros>();
                var id = libros.ToList().First(x => x.Titulo.Trim() == titulo).Id;
                return libros.Find(id);
            }
        }
        
    }
}