using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public sealed class UsuarioCreadoDto
    {
        public string Nombre { get; set; } = "";
        public string? Apellido { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = ""; // plano
        public int IdRol { get; set; } = 1;
        public string? ImgUrl { get; set; }
    }
}
