using BibliotecaMovil.Shared.DTOs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BibliotecaMovil.Server.Repositories;

public class CloudinaryRepository
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryRepository(IConfiguration config)
    {
        var cloudName = config["CloudinarySettings:CloudName"];
        var apiKey = config["CloudinarySettings:ApiKey"];
        var apiSecret = config["CloudinarySettings:ApiSecret"];

        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<CloudinaryUploadResultDto?> SubirImagenAsync(
        Stream stream,
        string nombreArchivo,
        string carpeta)
    {
        if (stream == null)
            return null;

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(nombreArchivo, stream),
            Folder = carpeta,
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result == null || result.SecureUrl == null)
            return null;

        return new CloudinaryUploadResultDto
        {
            Url = result.SecureUrl.ToString(),
            PublicId = result.PublicId ?? "",
            Carpeta = carpeta
        };
    }
}