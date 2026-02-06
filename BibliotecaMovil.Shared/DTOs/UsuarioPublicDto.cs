using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public sealed class UsuarioPublicoDto
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}".Trim();

        public string Email { get; set; } = string.Empty;

        public string? ImgUrl { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }
    }

}
