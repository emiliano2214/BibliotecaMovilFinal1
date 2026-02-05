using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Controllers;

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFavorito(int id)
    {
        var result = await _favoritoRepository.RemoveFavoritoAsync(id);
        return result ? Ok() : NotFound();
    }
}
