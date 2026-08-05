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

        public decimal? Puntuacion { get; set; }
        public string Comentario { get; set; } = string.Empty;  
        public DateTime FechaResena { get; set; }               

        public Libro? Libro { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
