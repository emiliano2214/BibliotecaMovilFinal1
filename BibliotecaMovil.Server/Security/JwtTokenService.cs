using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BibliotecaMovil.Server.Models; // <-- o el namespace real donde está UsuarioAuthData
namespace BibliotecaMovil.Server.Security;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UsuarioAuthData usuario)
    {
        // Lee config desde appsettings.json -> "Jwt"
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"];
        var expireMinutesStr = _configuration["Jwt:ExpireMinutes"];

        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Jwt:Key no está configurado en appsettings.json");

        if (!int.TryParse(expireMinutesStr, out var expireMinutes))
            expireMinutes = 60; // fallback seguro

        // Claims: identidad + autorización
        var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
        new(ClaimTypes.Email, usuario.Email),
        new(ClaimTypes.Role, usuario.NombreRol ?? string.Empty),

        // 👇 útiles para UI/autorizar por rolId también
        new("rolId", usuario.IdRol.ToString()),

        new("nombre", usuario.Nombre ?? string.Empty),
        new("apellido", usuario.Apellido ?? string.Empty),
    };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
