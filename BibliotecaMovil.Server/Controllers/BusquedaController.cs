using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Controllers;
[Authorize(Roles = "Admin,Bibliotecario")]
[ApiController]
[Route("api/[controller]")]
public sealed class BusquedaController : ControllerBase
{
    private readonly BibliotecaDbContext _db;
    public BusquedaController(BibliotecaDbContext db) => _db = db;

    // GET: /api/busqueda?usuario=...&libro=...&prestamo=...&favorito=...&resena=...
    [HttpGet]
    public async Task<ActionResult<BusquedaResultadoDto>> Buscar(
        [FromQuery] string? usuario,
        [FromQuery] string? libro,
        [FromQuery] string? prestamo,
        [FromQuery] string? favorito,
        [FromQuery] string? resena,
        CancellationToken ct)
    {
        var result = new BusquedaResultadoDto();

        // -------- USUARIOS (Nombre/Apellido/Email)
        if (!string.IsNullOrWhiteSpace(usuario))
        {
            var u = usuario.Trim();

            result.Usuarios = await _db.Usuarios
                .AsNoTracking()
                .Where(x =>
                    x.Nombre.Contains(u) ||
                    x.Apellido.Contains(u) ||
                    x.Email.Contains(u))
                .OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                .Take(20)
                .Select(x => new UsuarioPublicoDto
                {
                    IdUsuario = x.IdUsuario,
                    Email = x.Email,
                    IdRol = x.IdRol,
                    NombreRol = x.Rol.Nombre,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Activo = x.Activo
                })
                .ToListAsync(ct);
        }

        if (!string.IsNullOrWhiteSpace(libro))
        {
            var q = libro.Trim();

            // Si el usuario escribe un número, buscar por IdLibro
            var isId = int.TryParse(q, out var idLibro);

            var query = _db.Libros.AsNoTracking();

            if (isId)
            {
                query = query.Where(l => l.IdLibro == idLibro);
            }
            else
            {
                query = query.Where(l =>
                    l.Titulo.Contains(q) ||
                    l.Resumen.Contains(q) ||
                    (l.Editorial != null && l.Editorial.Nombre.Contains(q)) ||
                    (l.Categoria != null && l.Categoria.Nombre.Contains(q)) ||
                    l.LibroAutores.Any(la =>
                        (la.Autor != null) &&
                        (la.Autor.Nombre.Contains(q) || la.Autor.Apellido.Contains(q))
                    )
                );
            }

            result.Libros = await query
                .OrderBy(l => l.Titulo)
                .Take(30)
                .Select(l => new LibroDto
                {
                    IdLibro = l.IdLibro,
                    Titulo = l.Titulo,
                    Resumen = l.Resumen,
                    AnioPublicacion = l.AnioPublicacion,
                    IdCategoria = l.IdCategoria,
                    IdEditorial = l.IdEditorial
                })
                .ToListAsync(ct);
        }


        // -------- PRESTAMOS (por Id o por Estado o por email del usuario)
        if (!string.IsNullOrWhiteSpace(prestamo))
        {
            var p = prestamo.Trim();

            result.Prestamos = await _db.Prestamos
                .AsNoTracking()
                .Where(pr =>
                    pr.Estado.Contains(p) ||
                    pr.IdPrestamo.ToString().Contains(p) ||
                    pr.Usuario!.Email.Contains(p))
                .OrderByDescending(pr => pr.FechaInicio)
                .Take(30)
                .Select(pr => new PrestamoDto
                {
                    IdPrestamo = pr.IdPrestamo,
                    IdUsuario = pr.IdUsuario,              
                    FechaPrestamo = pr.FechaInicio,        
                    FechaVencimiento = pr.FechaVencimiento,
                    FechaDevolucion = pr.FechaDevolucion,
                    Estado = pr.Estado
                })
                .ToListAsync(ct);
        }

        // -------- FAVORITOS
        if (!string.IsNullOrWhiteSpace(favorito) ||
            (!string.IsNullOrWhiteSpace(usuario) && int.TryParse(usuario.Trim(), out _)) ||
            (!string.IsNullOrWhiteSpace(libro) && int.TryParse(libro.Trim(), out _)))
        {
            IQueryable<Favorito> favQuery = _db.Favoritos.AsNoTracking();

            // includes (opcionales, pero útiles si filtrás por Usuario/Libro)
            favQuery = favQuery
                .Include(x => x.Usuario)
                .Include(x => x.Libro);

            // filtro principal: favorito (id o texto)
            var f = (favorito ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(f))
            {
                if (int.TryParse(f, out var id))
                    favQuery = favQuery.Where(x => x.IdUsuario == id || x.IdLibro == id);
                else
                    favQuery = favQuery.Where(x =>
                        (x.Usuario != null && x.Usuario.Email.Contains(f)) ||
                        (x.Libro != null && x.Libro.Titulo.Contains(f)));
            }

            // filtro cruzado por usuario (si es numérico)
            if (!string.IsNullOrWhiteSpace(usuario) && int.TryParse(usuario.Trim(), out var userId))
                favQuery = favQuery.Where(x => x.IdUsuario == userId);

            // filtro cruzado por libro (si es numérico)
            if (!string.IsNullOrWhiteSpace(libro) && int.TryParse(libro.Trim(), out var libroId))
                favQuery = favQuery.Where(x => x.IdLibro == libroId);

            result.Favoritos = await favQuery
                .OrderByDescending(x => x.FechaAgregado)
                .Take(50)
                .Select(x => new FavoritoDto
                {
                    IdUsuario = x.IdUsuario,
                    IdLibro = x.IdLibro,
                    FechaAgregado = x.FechaAgregado
                })
                .ToListAsync(ct);
        }

        // -------- RESEÑAS (Comentario/Puntaje/Fecha) + búsqueda por Id si es número
        if (!string.IsNullOrWhiteSpace(resena))
        {
            var q = resena.Trim();
            var isId = int.TryParse(q, out var id);

            var query = _db.Resenas.AsNoTracking();

            if (isId)
            {
                // podés ajustar esto a tu gusto: por id reseña / id libro / id usuario
                query = query.Where(x =>
                    x.IdResena == id ||
                    x.IdLibro == id ||
                    x.IdUsuario == id
                );
            }
            else
            {
                query = query.Where(x =>
                    x.Comentario.Contains(q) ||
                    (x.Libro != null && x.Libro.Titulo.Contains(q)) ||
                    (x.Usuario != null && x.Usuario.Email.Contains(q))
                );
            }

            result.Resenas = await query
                .OrderByDescending(x => x.FechaResena)
                .Take(30)
                .Select(x => new ResenaDto
                {
                    IdResena = x.IdResena,
                    IdUsuario = x.IdUsuario,
                    IdLibro = x.IdLibro,
                    Titulo = null,
                    Contenido = x.Comentario,
                    Puntuacion = x.Puntuacion,
                    FechaCreacion = x.FechaResena,
                    FechaModificacion = null
                })
                .ToListAsync(ct);
        }


        return Ok(result);
    }
}
