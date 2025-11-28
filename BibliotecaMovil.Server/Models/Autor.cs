using System;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;

        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
