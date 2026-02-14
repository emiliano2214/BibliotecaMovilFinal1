using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMovil.Server.Models
{
    public class Resena
    {
        [Key]
        public int IdResena { get; set; }

        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }

        // ✅ columnas reales en BD
        public decimal? Puntuacion { get; set; }
        public string Comentario { get; set; } = string.Empty;  // en BD: Comentario
        public DateTime FechaResena { get; set; }               // en BD: FechaResena

        public Libro? Libro { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
