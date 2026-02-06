using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public sealed class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public int ExpiresInMinutes { get; set; }
        public UsuarioPublicoDto User { get; set; } = new();
    }
}
