using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public class PrestamoCreateDto
    {
        public int IdUsuario { get; set; }
        public int IdEjemplar { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
