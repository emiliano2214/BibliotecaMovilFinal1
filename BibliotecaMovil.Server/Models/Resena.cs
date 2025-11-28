using System;

namespace Biblioteca.Models
{
    public class Resena
    {
        public int IdResena { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        public int Puntaje { get; set; }   // 1-5
        public string Comentario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }

        public Libro? Libro { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
