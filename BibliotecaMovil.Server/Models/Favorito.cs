using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMovil.Server.Models
{
    [Table("Favoritos", Schema = "dbo")]
    public class Favorito
    {
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }

        public DateTime FechaAgregado { get; set; }

        public Libro? Libro { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
