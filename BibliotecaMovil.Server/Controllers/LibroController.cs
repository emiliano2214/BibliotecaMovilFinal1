using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaMovil.Server.Controllers;

[Authorize(Roles = "Lector,Admin,Bibliotecario")]
[ApiController]
[Route("api/[controller]")]
public class LibroController : ControllerBase
{
    private readonly ILibroRepository _libroRepository;

    public LibroController(ILibroRepository libroRepository)
    {
        _libroRepository = libroRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<LibroDto>>> GetLibros()
    {
        return await _libroRepository.GetAllLibrosAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LibroDto>> GetLibro(int id)
    {
        var libro = await _libroRepository.GetLibroByIdAsync(id);
        if (libro == null) return NotFound();
        return Ok(libro);
    }

    [HttpGet("categoria/{categoriaId:int}")]
    public async Task<ActionResult<List<LibroDto>>> GetLibrosByCategoria(int categoriaId)
    {
        return await _libroRepository.GetLibrosByCategoriaAsync(categoriaId);
    }

    [Authorize(Roles = "Admin,Bibliotecario")]
    [HttpPost]
    public async Task<ActionResult> CreateLibro([FromBody] LibroCreateDto dto)
    {
        try
        {
            var idLibro = await _libroRepository.AddLibroAsync(dto);

            // devuelve 201 + Location: api/Libro/{id}
            return CreatedAtAction(nameof(GetLibro), new { id = idLibro }, new { idLibro });
        }
        catch (Exception ex)
        {
            // Para mostrar el error real (como vos querías ver en el front)
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin,Bibliotecario")]
    [HttpPut("{idLibro:int}")]
    public async Task<ActionResult> UpdateLibro(int idLibro, [FromBody] LibroUpdateDto dto)
    {
        try
        {
            var ok = await _libroRepository.UpdateLibroAsync(idLibro, dto);
            if (!ok) return NotFound();
            //prueba 2
            if (string.IsNullOrWhiteSpace(dto.ImagenUrl))
                return BadRequest("CONTROLLER UPDATE: ImagenUrl llegó vacía");
            return NoContent(); // 204
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin,Bibliotecario")]
    [HttpDelete("{idLibro:int}")]
    public async Task<ActionResult> DeleteLibro(int idLibro)
    {
        try
        {
            var ok = await _libroRepository .DeleteLibroAsync(idLibro);
            if (!ok) return NotFound();

            return NoContent(); // 204
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("debug-sql/{id:int}")]
    public async Task<ActionResult> DebugSql(
    int id,
    [FromServices] IConfiguration config)
    {
        var cs = config.GetConnectionString("DefaultConnection");

        await using var conn = new Microsoft.Data.SqlClient.SqlConnection(cs);
        await conn.OpenAsync();

        await using var cmd = new Microsoft.Data.SqlClient.SqlCommand(
            "SELECT IdLibro, Titulo, ImagenUrl FROM dbo.Libros WHERE IdLibro = @id", conn);

        cmd.Parameters.AddWithValue("@id", id);

        await using var reader = await cmd.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return NotFound();

        return Ok(new
        {
            IdLibro = reader["IdLibro"],
            Titulo = reader["Titulo"]?.ToString(),
            ImagenUrl = reader["ImagenUrl"] == DBNull.Value ? null : reader["ImagenUrl"]?.ToString()
        });
    }
}