using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ISancionLogicaNegocioService
{
    Task<SancionDto?> CrearSancionPorTardanzaAsync(int prestamoId);
}