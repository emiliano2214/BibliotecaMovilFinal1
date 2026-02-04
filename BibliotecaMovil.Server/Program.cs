using BibliotecaMovil.Server.Data;
using Microsoft.EntityFrameworkCore;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
