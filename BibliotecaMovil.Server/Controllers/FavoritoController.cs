using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaMovil.Server.Controllers;
[Authorize(Roles = "Lector,Admin")]
[ApiController]
[Route("api/[controller]")]
public class FavoritoController : ControllerBase
{
    private readonly IFavoritoRepository _favoritoRepository;

    public FavoritoController(IFavoritoRepository favoritoRepository)
    {
        _favoritoRepository = favoritoRepository;
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<List<FavoritoDto>>> GetFavoritosByUsuario(int usuarioId)
    {
        var favoritos = await _favoritoRepository.GetFavoritosByUsuarioIdAsync(usuarioId);
        return Ok(favoritos);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorito([FromBody] FavoritoDto favorito)
    {
        var result = await _favoritoRepository.AddFavoritoAsync(favorito);
        return result ? Ok() : BadRequest();
    }

    // ✅ PK compuesta: usuarioId + libroId
    [HttpDelete("usuario/{usuarioId}/libro/{libroId}")]
    public async Task<IActionResult> RemoveFavorito(int usuarioId, int libroId)
    {
        var result = await _favoritoRepository.RemoveFavoritoAsync(usuarioId, libroId);
        return result ? Ok() : NotFound();
    }
}
