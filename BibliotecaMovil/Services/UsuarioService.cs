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
        public UsuarioService(HttpClient http)
        {
            _http = http;
        }
        public async Task<UsuarioDetalleDto?> GetDetalleAsync(int idUsuario)
        {
            var resp = await _http.GetAsync($"api/Usuario/{idUsuario}/detalle");

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n{body}");
            }

            return await resp.Content.ReadFromJsonAsync<UsuarioDetalleDto>();
        }

        public async Task<List<UsuarioPublicoDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<UsuarioPublicoDto>>("api/Usuario") ?? new();

        public Task<UsuarioPublicoDto?> GetByIdAsync(int id)
            => _http.GetFromJsonAsync<UsuarioPublicoDto>($"api/Usuario/{id}");

        public async Task<bool> CreateAsync(UsuarioCreadoDto usuario)
        {
            var resp = await _http.PostAsJsonAsync("api/Usuario", usuario);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UsuarioActualizadoDto usuario)
        {
            var resp = await _http.PutAsJsonAsync($"api/Usuario/{usuario.IdUsuario}", usuario);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Usuario/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
