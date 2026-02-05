using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public int RolId { get; set; }
        public string? ImgUrl { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        public Rol? Rol { get; set; }
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
        public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
        public ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<Sancion> Sanciones { get; set; } = new List<Sancion>();
    }
}
