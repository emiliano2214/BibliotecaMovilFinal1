using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaMovil.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Autores",
                schema: "dbo",
                columns: table => new
                {
                    IdAutor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.IdAutor);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                schema: "dbo",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Editoriales",
                schema: "dbo",
                columns: table => new
                {
                    IdEditorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoriales", x => x.IdEditorial);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                schema: "dbo",
                columns: table => new
                {
                    IdLibro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resumen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnioPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEditorial = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    AutorIdAutor = table.Column<int>(type: "int", nullable: true),
                    CategoriaIdCategoria = table.Column<int>(type: "int", nullable: true),
                    EditorialIdEditorial = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.IdLibro);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_AutorIdAutor",
                        column: x => x.AutorIdAutor,
                        principalSchema: "dbo",
                        principalTable: "Autores",
                        principalColumn: "IdAutor");
                    table.ForeignKey(
                        name: "FK_Libros_Categorias_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalSchema: "dbo",
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria");
                    table.ForeignKey(
                        name: "FK_Libros_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalSchema: "dbo",
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Libros_Editoriales_EditorialIdEditorial",
                        column: x => x.EditorialIdEditorial,
                        principalSchema: "dbo",
                        principalTable: "Editoriales",
                        principalColumn: "IdEditorial");
                    table.ForeignKey(
                        name: "FK_Libros_Editoriales_IdEditorial",
                        column: x => x.IdEditorial,
                        principalSchema: "dbo",
                        principalTable: "Editoriales",
                        principalColumn: "IdEditorial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "dbo",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ejemplares",
                schema: "dbo",
                columns: table => new
                {
                    IdEjemplar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    CodigoInventario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejemplares", x => x.IdEjemplar);
                    table.ForeignKey(
                        name: "FK_Ejemplares_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalSchema: "dbo",
                        principalTable: "Libros",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibroAutor",
                schema: "dbo",
                columns: table => new
                {
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    IdAutor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroAutor", x => new { x.IdLibro, x.IdAutor });
                    table.ForeignKey(
                        name: "FK_LibroAutor_Autores_IdAutor",
                        column: x => x.IdAutor,
                        principalSchema: "dbo",
                        principalTable: "Autores",
                        principalColumn: "IdAutor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibroAutor_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalSchema: "dbo",
                        principalTable: "Libros",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                schema: "dbo",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    FechaAgregado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => new { x.IdUsuario, x.IdLibro });
                    table.ForeignKey(
                        name: "FK_Favoritos_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalSchema: "dbo",
                        principalTable: "Libros",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favoritos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "dbo",
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resenas",
                schema: "dbo",
                columns: table => new
                {
                    IdResena = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Puntuacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaResena = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resenas", x => x.IdResena);
                    table.ForeignKey(
                        name: "FK_Resenas_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalSchema: "dbo",
                        principalTable: "Libros",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resenas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "dbo",
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                schema: "dbo",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    LibroIdLibro = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Libros_LibroIdLibro",
                        column: x => x.LibroIdLibro,
                        principalSchema: "dbo",
                        principalTable: "Libros",
                        principalColumn: "IdLibro");
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalSchema: "dbo",
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                schema: "dbo",
                columns: table => new
                {
                    IdPrestamo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdEjemplar = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.IdPrestamo);
                    table.ForeignKey(
                        name: "FK_Prestamos_Ejemplares_IdEjemplar",
                        column: x => x.IdEjemplar,
                        principalSchema: "dbo",
                        principalTable: "Ejemplares",
                        principalColumn: "IdEjemplar",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "dbo",
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrestamoDetalle",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrestamo = table.Column<int>(type: "int", nullable: false),
                    IdEjemplar = table.Column<int>(type: "int", nullable: false),
                    PrestamoIdPrestamo = table.Column<int>(type: "int", nullable: true),
                    EjemplarIdEjemplar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestamoDetalle", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_PrestamoDetalle_Ejemplares_EjemplarIdEjemplar",
                        column: x => x.EjemplarIdEjemplar,
                        principalSchema: "dbo",
                        principalTable: "Ejemplares",
                        principalColumn: "IdEjemplar");
                    table.ForeignKey(
                        name: "FK_PrestamoDetalle_Prestamos_PrestamoIdPrestamo",
                        column: x => x.PrestamoIdPrestamo,
                        principalSchema: "dbo",
                        principalTable: "Prestamos",
                        principalColumn: "IdPrestamo");
                });

            migrationBuilder.CreateTable(
                name: "Sanciones",
                schema: "dbo",
                columns: table => new
                {
                    IdSancion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPrestamo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pagada = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    PrestamoIdPrestamo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanciones", x => x.IdSancion);
                    table.ForeignKey(
                        name: "FK_Sanciones_Prestamos_PrestamoIdPrestamo",
                        column: x => x.PrestamoIdPrestamo,
                        principalSchema: "dbo",
                        principalTable: "Prestamos",
                        principalColumn: "IdPrestamo");
                    table.ForeignKey(
                        name: "FK_Sanciones_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalSchema: "dbo",
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ejemplares_IdLibro",
                schema: "dbo",
                table: "Ejemplares",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_IdLibro",
                schema: "dbo",
                table: "Favoritos",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_LibroAutor_IdAutor",
                schema: "dbo",
                table: "LibroAutor",
                column: "IdAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_AutorIdAutor",
                schema: "dbo",
                table: "Libros",
                column: "AutorIdAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_CategoriaIdCategoria",
                schema: "dbo",
                table: "Libros",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_EditorialIdEditorial",
                schema: "dbo",
                table: "Libros",
                column: "EditorialIdEditorial");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_IdCategoria",
                schema: "dbo",
                table: "Libros",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_IdEditorial",
                schema: "dbo",
                table: "Libros",
                column: "IdEditorial");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamoDetalle_EjemplarIdEjemplar",
                table: "PrestamoDetalle",
                column: "EjemplarIdEjemplar");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamoDetalle_PrestamoIdPrestamo",
                table: "PrestamoDetalle",
                column: "PrestamoIdPrestamo");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_IdEjemplar",
                schema: "dbo",
                table: "Prestamos",
                column: "IdEjemplar");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_IdUsuario",
                schema: "dbo",
                table: "Prestamos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Resenas_IdLibro",
                schema: "dbo",
                table: "Resenas",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Resenas_IdUsuario",
                schema: "dbo",
                table: "Resenas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_LibroIdLibro",
                schema: "dbo",
                table: "Reservas",
                column: "LibroIdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioIdUsuario",
                schema: "dbo",
                table: "Reservas",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_PrestamoIdPrestamo",
                schema: "dbo",
                table: "Sanciones",
                column: "PrestamoIdPrestamo");

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_UsuarioIdUsuario",
                schema: "dbo",
                table: "Sanciones",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                schema: "dbo",
                table: "Usuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LibroAutor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrestamoDetalle");

            migrationBuilder.DropTable(
                name: "Resenas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Reservas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sanciones",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Prestamos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Ejemplares",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Libros",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Autores",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categorias",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Editoriales",
                schema: "dbo");
        }
    }
}
