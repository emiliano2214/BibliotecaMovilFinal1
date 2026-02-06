using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http) => _http = http;

        public Task<List<UsuarioDto>?> GetAllAsync()
            => _http.GetFromJsonAsync<List<UsuarioDto>>("api/Usuario");

        public Task<UsuarioDto?> GetByIdAsync(int id)
            => _http.GetFromJsonAsync<UsuarioDto>($"api/Usuario/{id}");

        public Task<bool> CreateAsync(UsuarioDto usuario)
            => _http.PostAsJsonAsync("api/Usuario", usuario)
                    .ContinueWith(task => task.Result.IsSuccessStatusCode);
        public Task<bool> UpdateAsync(UsuarioDto usuario)
            => _http.PutAsJsonAsync($"api/Usuario/{usuario.IdUsuario}", usuario)
                    .ContinueWith(task => task.Result.IsSuccessStatusCode);
        public Task<bool> DeleteAsync(int id)
            => _http.DeleteAsync($"api/Usuario/{id}")
                    .ContinueWith(task => task.Result.IsSuccessStatusCode);
    }
}
