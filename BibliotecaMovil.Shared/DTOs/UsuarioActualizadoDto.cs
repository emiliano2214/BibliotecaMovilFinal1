using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public sealed class UsuarioActualizadoDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = "";
        public string? Apellido { get; set; }
        public int IdRol { get; set; }
        public string? ImgUrl { get; set; }
        public bool Activo { get; set; }
    }
}
