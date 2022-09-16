using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAppKey.Migrations
{
    public partial class CreateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tipomovimento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipomovimento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipoproduto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    sigla = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipoproduto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    sigla = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unidade", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movimento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datainclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    numero = table.Column<int>(type: "integer", nullable: false),
                    situacao = table.Column<int>(type: "integer", nullable: false),
                    observacao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    tipomovimentoid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movimento", x => x.id);
                    table.ForeignKey(
                        name: "fk_movimento_tipomovimento_tipomovimentoid",
                        column: x => x.tipomovimentoid,
                        principalTable: "tipomovimento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    nome = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    unidadeid = table.Column<int>(type: "integer", nullable: false),
                    tipoprodutoid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produto", x => x.id);
                    table.ForeignKey(
                        name: "fk_produto_tipoproduto_tipoprodutoid",
                        column: x => x.tipoprodutoid,
                        principalTable: "tipoproduto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_produto_unidade_unidadeid",
                        column: x => x.unidadeid,
                        principalTable: "unidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movimentoitem",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datainclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    produtoid = table.Column<int>(type: "integer", nullable: false),
                    movimentoid = table.Column<int>(type: "integer", nullable: false),
                    quantidade = table.Column<double>(type: "double precision", nullable: false),
                    valor = table.Column<double>(type: "double precision", nullable: false),
                    total = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movimentoitem", x => x.id);
                    table.ForeignKey(
                        name: "fk_movimentoitem_movimento_movimentoid",
                        column: x => x.movimentoid,
                        principalTable: "movimento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movimentoitem_produto_produtoid",
                        column: x => x.produtoid,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_movimento_tipomovimentoid",
                table: "movimento",
                column: "tipomovimentoid");

            migrationBuilder.CreateIndex(
                name: "ix_movimentoitem_movimentoid",
                table: "movimentoitem",
                column: "movimentoid");

            migrationBuilder.CreateIndex(
                name: "ix_movimentoitem_produtoid",
                table: "movimentoitem",
                column: "produtoid");

            migrationBuilder.CreateIndex(
                name: "ix_produto_tipoprodutoid",
                table: "produto",
                column: "tipoprodutoid");

            migrationBuilder.CreateIndex(
                name: "ix_produto_unidadeid",
                table: "produto",
                column: "unidadeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimentoitem");

            migrationBuilder.DropTable(
                name: "movimento");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "tipomovimento");

            migrationBuilder.DropTable(
                name: "tipoproduto");

            migrationBuilder.DropTable(
                name: "unidade");
        }
    }
}
