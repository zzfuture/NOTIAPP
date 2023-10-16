using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreUsuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescAccion = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstadoNotificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreEstado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoNotificaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Formatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreFormato = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formatos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HiloRespuestaNotificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiloRespuestaNotificaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuloMaestros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreModulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuloMaestros", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PermisoGenericos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombrePermiso = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisoGenericos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Radicados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radicados", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubModulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreSubmodulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModulos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoNotificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNotificaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoRequerimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRequerimientos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolVsMaestros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: true),
                    IdMaestro = table.Column<int>(type: "int", nullable: false),
                    ModuloMaestrosId = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolVsMaestros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolVsMaestros_ModuloMaestros_ModuloMaestrosId",
                        column: x => x.ModuloMaestrosId,
                        principalTable: "ModuloMaestros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolVsMaestros_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaestroVsSubModulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdMaestro = table.Column<int>(type: "int", nullable: false),
                    ModuloMaestrosId = table.Column<int>(type: "int", nullable: true),
                    IdSubmodulo = table.Column<int>(type: "int", nullable: false),
                    SubModulosId = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaestroVsSubModulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaestroVsSubModulos_ModuloMaestros_ModuloMaestrosId",
                        column: x => x.ModuloMaestrosId,
                        principalTable: "ModuloMaestros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaestroVsSubModulos_SubModulos_SubModulosId",
                        column: x => x.SubModulosId,
                        principalTable: "SubModulos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Blockchains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdNotificacion = table.Column<int>(type: "int", nullable: false),
                    TipoNotificacionesId = table.Column<int>(type: "int", nullable: true),
                    IdHiloRespuesta = table.Column<int>(type: "int", nullable: false),
                    HiloRespuestaNotificacionesId = table.Column<int>(type: "int", nullable: true),
                    IdAuditoria = table.Column<int>(type: "int", nullable: false),
                    AuditoriasId = table.Column<int>(type: "int", nullable: true),
                    HashGenerado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blockchains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blockchains_Auditorias_AuditoriasId",
                        column: x => x.AuditoriasId,
                        principalTable: "Auditorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blockchains_HiloRespuestaNotificaciones_HiloRespuestaNotific~",
                        column: x => x.HiloRespuestaNotificacionesId,
                        principalTable: "HiloRespuestaNotificaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blockchains_TipoNotificaciones_TipoNotificacionesId",
                        column: x => x.TipoNotificacionesId,
                        principalTable: "TipoNotificaciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuloNotificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AsuntoNotificacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdtpoNotificacion = table.Column<int>(type: "int", nullable: false),
                    TipoNotificacionesId = table.Column<int>(type: "int", nullable: true),
                    IdRadicado = table.Column<int>(type: "int", nullable: false),
                    RadicadosId = table.Column<int>(type: "int", nullable: true),
                    IdEstadoNotificiones = table.Column<int>(type: "int", nullable: false),
                    EstadosNotificacionId = table.Column<int>(type: "int", nullable: true),
                    IdHiloRespuesta = table.Column<int>(type: "int", nullable: false),
                    HiloRespuestaNotificacionesId = table.Column<int>(type: "int", nullable: true),
                    IdFormato = table.Column<int>(type: "int", nullable: false),
                    FormatosId = table.Column<int>(type: "int", nullable: true),
                    IdRequerimiento = table.Column<int>(type: "int", nullable: false),
                    TipoRequerimientosId = table.Column<int>(type: "int", nullable: true),
                    TextoNotificacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuloNotificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuloNotificaciones_EstadoNotificaciones_EstadosNotificacio~",
                        column: x => x.EstadosNotificacionId,
                        principalTable: "EstadoNotificaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuloNotificaciones_Formatos_FormatosId",
                        column: x => x.FormatosId,
                        principalTable: "Formatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuloNotificaciones_HiloRespuestaNotificaciones_HiloRespues~",
                        column: x => x.HiloRespuestaNotificacionesId,
                        principalTable: "HiloRespuestaNotificaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuloNotificaciones_Radicados_RadicadosId",
                        column: x => x.RadicadosId,
                        principalTable: "Radicados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuloNotificaciones_TipoNotificaciones_TipoNotificacionesId",
                        column: x => x.TipoNotificacionesId,
                        principalTable: "TipoNotificaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuloNotificaciones_TipoRequerimientos_TipoRequerimientosId",
                        column: x => x.TipoRequerimientosId,
                        principalTable: "TipoRequerimientos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GenericoVsSubModulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdGenericos = table.Column<int>(type: "int", nullable: false),
                    PermisosGenericosId = table.Column<int>(type: "int", nullable: true),
                    IdSubmodulos = table.Column<int>(type: "int", nullable: false),
                    MaestroVsSubModulosId = table.Column<int>(type: "int", nullable: true),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaModificacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericoVsSubModulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenericoVsSubModulos_MaestroVsSubModulos_MaestroVsSubModulos~",
                        column: x => x.MaestroVsSubModulosId,
                        principalTable: "MaestroVsSubModulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GenericoVsSubModulos_PermisoGenericos_PermisosGenericosId",
                        column: x => x.PermisosGenericosId,
                        principalTable: "PermisoGenericos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GenericoVsSubModulos_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Blockchains_AuditoriasId",
                table: "Blockchains",
                column: "AuditoriasId");

            migrationBuilder.CreateIndex(
                name: "IX_Blockchains_HiloRespuestaNotificacionesId",
                table: "Blockchains",
                column: "HiloRespuestaNotificacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_Blockchains_TipoNotificacionesId",
                table: "Blockchains",
                column: "TipoNotificacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericoVsSubModulos_MaestroVsSubModulosId",
                table: "GenericoVsSubModulos",
                column: "MaestroVsSubModulosId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericoVsSubModulos_PermisosGenericosId",
                table: "GenericoVsSubModulos",
                column: "PermisosGenericosId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericoVsSubModulos_RolesId",
                table: "GenericoVsSubModulos",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_MaestroVsSubModulos_ModuloMaestrosId",
                table: "MaestroVsSubModulos",
                column: "ModuloMaestrosId");

            migrationBuilder.CreateIndex(
                name: "IX_MaestroVsSubModulos_SubModulosId",
                table: "MaestroVsSubModulos",
                column: "SubModulosId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloNotificaciones_EstadosNotificacionId",
                table: "ModuloNotificaciones",
                column: "EstadosNotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloNotificaciones_FormatosId",
                table: "ModuloNotificaciones",
                column: "FormatosId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloNotificaciones_HiloRespuestaNotificacionesId",
                table: "ModuloNotificaciones",
                column: "HiloRespuestaNotificacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloNotificaciones_RadicadosId",
                table: "ModuloNotificaciones",
                column: "RadicadosId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloNotificaciones_TipoNotificacionesId",
                table: "ModuloNotificaciones",
                column: "TipoNotificacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloNotificaciones_TipoRequerimientosId",
                table: "ModuloNotificaciones",
                column: "TipoRequerimientosId");

            migrationBuilder.CreateIndex(
                name: "IX_RolVsMaestros_ModuloMaestrosId",
                table: "RolVsMaestros",
                column: "ModuloMaestrosId");

            migrationBuilder.CreateIndex(
                name: "IX_RolVsMaestros_RolesId",
                table: "RolVsMaestros",
                column: "RolesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blockchains");

            migrationBuilder.DropTable(
                name: "GenericoVsSubModulos");

            migrationBuilder.DropTable(
                name: "ModuloNotificaciones");

            migrationBuilder.DropTable(
                name: "RolVsMaestros");

            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "MaestroVsSubModulos");

            migrationBuilder.DropTable(
                name: "PermisoGenericos");

            migrationBuilder.DropTable(
                name: "EstadoNotificaciones");

            migrationBuilder.DropTable(
                name: "Formatos");

            migrationBuilder.DropTable(
                name: "HiloRespuestaNotificaciones");

            migrationBuilder.DropTable(
                name: "Radicados");

            migrationBuilder.DropTable(
                name: "TipoNotificaciones");

            migrationBuilder.DropTable(
                name: "TipoRequerimientos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ModuloMaestros");

            migrationBuilder.DropTable(
                name: "SubModulos");
        }
    }
}
