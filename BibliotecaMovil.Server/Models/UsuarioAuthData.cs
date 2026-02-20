namespace BibliotecaMovil.Server.Models
{
    public sealed class UsuarioAuthData
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string? Apellido { get; set; }
        public string? ImgUrl { get; set; }
    }
}
