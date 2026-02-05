using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Security;
using BibliotecaMovil.Server.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IAutorRepository, BibliotecaMovil.Server.Repositories.AutorRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.ILibroRepository, BibliotecaMovil.Server.Repositories.LibroRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.ICategoriaRepository, BibliotecaMovil.Server.Repositories.CategoriaRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IEditorialRepository, BibliotecaMovil.Server.Repositories.EditorialRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IEjemplarRepository, BibliotecaMovil.Server.Repositories.EjemplarRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IFavoritoRepository, BibliotecaMovil.Server.Repositories.FavoritoRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IPrestamoRepository, BibliotecaMovil.Server.Repositories.PrestamoRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IResenaRepository, BibliotecaMovil.Server.Repositories.ResenaRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IReservaRepository, BibliotecaMovil.Server.Repositories.ReservaRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IRolRepository, BibliotecaMovil.Server.Repositories.RolRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.ISancionRepository, BibliotecaMovil.Server.Repositories.SancionRepository>();
builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IUsuarioRepository, BibliotecaMovil.Server.Repositories.UsuarioRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Security.IJwtTokenService,BibliotecaMovil.Server.Security.JwtTokenService>();
builder.Services.AddSingleton<BibliotecaMovil.Server.Security.IPasswordService, BibliotecaMovil.Server.Security.PasswordService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var key = builder.Configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Jwt:Key no está configurado en appsettings.json");

        if (string.IsNullOrWhiteSpace(issuer))
            throw new InvalidOperationException("Jwt:Issuer no está configurado.");

        if (string.IsNullOrWhiteSpace(audience))
            throw new InvalidOperationException("Jwt:Audience no está configurado.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
