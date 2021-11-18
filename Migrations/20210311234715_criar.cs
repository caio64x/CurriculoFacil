using Microsoft.EntityFrameworkCore.Migrations;

namespace MontagemCurriculo.Migrations
{
    public partial class criar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoCursos",
                columns: table => new
                {
                    TipoCursoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCursos", x => x.TipoCursoID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuaioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Senha = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuaioID);
                });

            migrationBuilder.CreateTable(
                name: "Curriculos",
                columns: table => new
                {
                    CurriculoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    UsuarioID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculos", x => x.CurriculoID);
                    table.ForeignKey(
                        name: "FK_Curriculos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuaioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformacoesLogin",
                columns: table => new
                {
                    InformacaoLoginID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnderecoIP = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: false),
                    Horario = table.Column<string>(nullable: false),
                    UsuarioID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacoesLogin", x => x.InformacaoLoginID);
                    table.ForeignKey(
                        name: "FK_InformacoesLogin_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuaioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciasProfissionais",
                columns: table => new
                {
                    ExperienciaProfissionalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEmpresa = table.Column<string>(maxLength: 50, nullable: false),
                    Cargo = table.Column<string>(maxLength: 50, nullable: false),
                    AnoInicio = table.Column<int>(maxLength: 50, nullable: false),
                    AnoFim = table.Column<int>(maxLength: 50, nullable: false),
                    DescricaoAtividades = table.Column<string>(maxLength: 500, nullable: false),
                    CurriculoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciasProfissionais", x => x.ExperienciaProfissionalID);
                    table.ForeignKey(
                        name: "FK_ExperienciasProfissionais_Curriculos_CurriculoID",
                        column: x => x.CurriculoID,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormacoesAcademicas",
                columns: table => new
                {
                    FormacaoAcademicaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCursoID = table.Column<int>(nullable: false),
                    Instituicao = table.Column<string>(maxLength: 50, nullable: false),
                    AnoInicio = table.Column<int>(nullable: false),
                    AnoFim = table.Column<int>(nullable: false),
                    NomeCurso = table.Column<string>(maxLength: 50, nullable: false),
                    CurriculoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormacoesAcademicas", x => x.FormacaoAcademicaID);
                    table.ForeignKey(
                        name: "FK_FormacoesAcademicas_Curriculos_CurriculoID",
                        column: x => x.CurriculoID,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormacoesAcademicas_TipoCursos_TipoCursoID",
                        column: x => x.TipoCursoID,
                        principalTable: "TipoCursos",
                        principalColumn: "TipoCursoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    IdiomaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Nivel = table.Column<string>(maxLength: 50, nullable: false),
                    CurriculoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.IdiomaID);
                    table.ForeignKey(
                        name: "FK_Idiomas_Curriculos_CurriculoID",
                        column: x => x.CurriculoID,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    ObjetivoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrição = table.Column<string>(maxLength: 1000, nullable: false),
                    CurriculoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.ObjetivoID);
                    table.ForeignKey(
                        name: "FK_Objetivos_Curriculos_CurriculoID",
                        column: x => x.CurriculoID,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curriculos_Nome",
                table: "Curriculos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculos_UsuarioID",
                table: "Curriculos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciasProfissionais_CurriculoID",
                table: "ExperienciasProfissionais",
                column: "CurriculoID");

            migrationBuilder.CreateIndex(
                name: "IX_FormacoesAcademicas_CurriculoID",
                table: "FormacoesAcademicas",
                column: "CurriculoID");

            migrationBuilder.CreateIndex(
                name: "IX_FormacoesAcademicas_TipoCursoID",
                table: "FormacoesAcademicas",
                column: "TipoCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Idiomas_CurriculoID",
                table: "Idiomas",
                column: "CurriculoID");

            migrationBuilder.CreateIndex(
                name: "IX_Idiomas_Nome",
                table: "Idiomas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InformacoesLogin_UsuarioID",
                table: "InformacoesLogin",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_CurriculoID",
                table: "Objetivos",
                column: "CurriculoID");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCursos_Tipo",
                table: "TipoCursos",
                column: "Tipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienciasProfissionais");

            migrationBuilder.DropTable(
                name: "FormacoesAcademicas");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropTable(
                name: "InformacoesLogin");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "TipoCursos");

            migrationBuilder.DropTable(
                name: "Curriculos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
