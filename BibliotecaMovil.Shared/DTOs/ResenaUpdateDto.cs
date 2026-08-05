using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public class ResenaUpdateDto
    {
        public decimal? Puntuacion { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
