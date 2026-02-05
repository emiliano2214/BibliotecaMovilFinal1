using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public sealed class UsuarioAuthDto
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; }
    }
}
