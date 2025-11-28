using System;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
