using Microsoft.Extensions.Logging;

namespace BibliotecaMovil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            string serverUrl;
#if ANDROID
            serverUrl = "http://10.0.2.2:7250";
#else
            serverUrl = "http://localhost:7250";
#endif

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serverUrl) });

            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IAutorService, BibliotecaMovil.Services.AutorService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.ILibroService, BibliotecaMovil.Services.LibroService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.ICategoriaService, BibliotecaMovil.Services.CategoriaService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IEditorialService, BibliotecaMovil.Services.EditorialService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IEjemplarService, BibliotecaMovil.Services.EjemplarService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IFavoritoService, BibliotecaMovil.Services.FavoritoService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IPrestamoService, BibliotecaMovil.Services.PrestamoService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IResenaService, BibliotecaMovil.Services.ResenaService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IReservaService, BibliotecaMovil.Services.ReservaService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IRolService, BibliotecaMovil.Services.RolService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.ISancionService, BibliotecaMovil.Services.SancionService>();
            //builder.Services.AddScoped<BibliotecaMovil.Shared.Interfaces.IUsuarioService, BibliotecaMovil.Services.UsuarioService>();
            
            return builder.Build();
        }
    }
}
