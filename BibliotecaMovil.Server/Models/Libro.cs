using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Biblioteca.Models
{
    public class Libro
    {
        [Key]
        public int IdLibro { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Resumen { get; set; } = string.Empty;
        public DateTime AnioPublicacion { get; set; }
        public string? ImagenUrl { get; set; }
        public int IdAutor { get; set; }
        public int IdEditorial { get; set; }
        public int IdCategoria { get; set; }

        public Autor? Autor { get; set; }
        public Editorial? Editorial { get; set; }
        public Categoria? Categoria { get; set; }

        public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
        public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
        public ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
