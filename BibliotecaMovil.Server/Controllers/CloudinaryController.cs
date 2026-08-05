using BibliotecaMovil.Server.LogicaNegocio;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/cloudinary")]
public class CloudinaryController : ControllerBase
{
    private readonly CloudinaryService _service;

    public CloudinaryController(CloudinaryService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<CloudinaryUploadResultDto>> SubirImagen([FromForm] CloudinaryUploadRequestDto request)
    {
        if (request.Archivo == null || string.IsNullOrWhiteSpace(request.Carpeta))
            return BadRequest("Archivo y carpeta son obligatorios.");

        var resultado = await _service.SubirImagenAsync(request.Archivo, request.Carpeta);

        if (resultado == null)
            return BadRequest("No se pudo subir la imagen.");

        return Ok(resultado);
    }
}