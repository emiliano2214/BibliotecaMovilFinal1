using System;

namespace Biblioteca.Models
{
    public class Favorito
    {
        public int IdFavorito { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }

        public Libro? Libro { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
