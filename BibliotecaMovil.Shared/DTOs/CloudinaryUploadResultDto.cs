using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public class CloudinaryUploadResultDto
    {
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public string Carpeta { get; set; } = string.Empty;
    }
}
