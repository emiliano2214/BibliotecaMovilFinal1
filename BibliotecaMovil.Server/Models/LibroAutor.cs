using BibliotecaMovil.Server.Models;

namespace BibliotecaMovil.Server.Models
{
    public class LibroAutor
    {
        public int IdLibro { get; set; }
        public Libro? Libro { get; set; }

        public int IdAutor { get; set; }
        public Autor? Autor { get; set; }
    }
}
