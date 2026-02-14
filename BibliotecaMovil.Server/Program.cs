using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Security;
using BibliotecaMovil.Server.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IAutorRepository, BibliotecaMovil.Server.Repositories.AutorRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.ILibroRepository, BibliotecaMovil.Server.Repositories.LibroRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.ICategoriaRepository, BibliotecaMovil.Server.Repositories.CategoriaRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IEditorialRepository, BibliotecaMovil.Server.Repositories.EditorialRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IEjemplarRepository, BibliotecaMovil.Server.Repositories.EjemplarRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IFavoritoRepository, BibliotecaMovil.Server.Repositories.FavoritoRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IPrestamoRepository, BibliotecaMovil.Server.Repositories.PrestamoRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IResenaRepository, BibliotecaMovil.Server.Repositories.ResenaRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IReservaRepository, BibliotecaMovil.Server.Repositories.ReservaRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IRolRepository, BibliotecaMovil.Server.Repositories.RolRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.ISancionRepository, BibliotecaMovil.Server.Repositories.SancionRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IUsuarioAuthRepository, BibliotecaMovil.Server.Repositories.UsuarioAuthRepository>();
builder.Services.AddScoped<BibliotecaMovil.Server.Repositories.IUsuarioRepository, BibliotecaMovil.Server.Repositories.UsuarioRepository>();
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
builder.Services.AddSwaggerGen(c =>
{
    // (Opcional) Info básica
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BibliotecaMovil API",
        Version = "v1"
    });

    // ✅ Esquema de seguridad: Bearer JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pegá: Bearer {tu_token}   (sin comillas)"
    });

    // ✅ Requisito de seguridad global: aplica a endpoints (si tienen [Authorize], Swagger te pide token)
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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
