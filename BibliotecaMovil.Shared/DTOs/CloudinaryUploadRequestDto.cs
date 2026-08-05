using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public class CloudinaryUploadRequestDto
    {
        public IFormFile Archivo { get; set; } = default!;
        public string Carpeta { get; set; } = string.Empty;
    }
}
