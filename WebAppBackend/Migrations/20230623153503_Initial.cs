using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAppBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UsuarioInclusaoId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioAtualizacaoId = table.Column<int>(type: "integer", nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Acessos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Permissao = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acessos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acessos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "DataCadastro", "Login", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 23, 12, 35, 2, 760, DateTimeKind.Local).AddTicks(1203), "admin", "Rhdfgr7OscxrawPdlBXN+2lDMfPPIdO0UOb8LlLjFEA=" },
                    { 2, new DateTime(2023, 6, 23, 12, 35, 2, 769, DateTimeKind.Local).AddTicks(1729), "guest", "rYpC71w5Vjag0Qc481ASMzGXzKTB9VoZMz9w2Wyo4B4=" }
                });

            migrationBuilder.InsertData(
                table: "Acessos",
                columns: new[] { "Id", "DataCadastro", "Permissao", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 23, 12, 35, 2, 769, DateTimeKind.Local).AddTicks(2025), 1, 2 },
                    { 2, new DateTime(2023, 6, 23, 12, 35, 2, 769, DateTimeKind.Local).AddTicks(2033), 4, 1 },
                    { 3, new DateTime(2023, 6, 23, 12, 35, 2, 769, DateTimeKind.Local).AddTicks(2034), 3, 1 },
                    { 4, new DateTime(2023, 6, 23, 12, 35, 2, 769, DateTimeKind.Local).AddTicks(2035), 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acessos_UsuarioId",
                table: "Acessos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acessos");

            migrationBuilder.DropTable(
                name: "tb_customer");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
