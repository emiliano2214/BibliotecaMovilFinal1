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
        return await _favoritoRepository.GetFavoritosByUsuarioIdAsync(usuarioId);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorito([FromBody] FavoritoDto favorito)
    {
        var result = await _favoritoRepository.AddFavoritoAsync(favorito);
        if (result) return Ok();
        return BadRequest();
    }
,
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFavorito(int id)
    {
        var result = await _favoritoRepository.RemoveFavoritoAsync(id);
        if (result) return Ok();
        return NotFound();
    }
}
