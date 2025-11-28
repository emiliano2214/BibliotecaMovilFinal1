var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Repositories
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IAutorRepository, BibliotecaMovil.Server.Repositories.AutorRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.ILibroRepository, BibliotecaMovil.Server.Repositories.LobroRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.ICategoriaRepository, BibliotecaMovil.Server.Repositories.CategoriaRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IEditorialRepository, BibliotecaMovil.Server.Repositories.EditorialRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IEjemplarRepository, BibliotecaMovil.Server.Repositories.EjemplarRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IFavoritoRepository, BibliotecaMovil.Server.Repositories.FavoritoRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IPrestamoRepository, BibliotecaMovil.Server.Repositories.PrestamoRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IResenaRepository, BibliotecaMovil.Server.Repositories.ResenaRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IReservaRepository, BibliotecaMovil.Server.Repositories.ReservaRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IRolRepository, BibliotecaMovil.Server.Repositories.RolRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.ISancionRepository, BibliotecaMovil.Server.Repositories.SancionRepository>();
builder.Services.AddSingleton<BibliotecaMovil.Shared.Interfaces.IUsuarioRepository, BibliotecaMovil.Server.Repositories.UsuarioRepository>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
