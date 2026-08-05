using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.LogicaNegocio
{
    public class CloudinaryService
    {
        private readonly CloudinaryRepository _repository;

        public CloudinaryService(CloudinaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CloudinaryUploadResultDto?> SubirImagenAsync(IFormFile archivo, string carpeta)
        {
            if (archivo == null || archivo.Length == 0)
                return null;

            if (string.IsNullOrWhiteSpace(carpeta))
                return null;

            using var stream = archivo.OpenReadStream();

            return await _repository.SubirImagenAsync(stream, archivo.FileName, carpeta);
        }
    }
}