using System;

namespace Biblioteca.Models
{
    public class Sancion
    {
        public int IdSancion { get; set; }
        public int IdUsuario { get; set; }
        public int IdPrestamo { get; set; }
        public decimal Monto { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }
        public bool Pagada { get; set; }

        public Usuario? Usuario { get; set; }
        public Prestamo? Prestamo { get; set; }
    }
}
