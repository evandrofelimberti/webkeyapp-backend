using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAppKey.Migrations
{
    public partial class MovimentoLavoura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lavoura",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    areaha = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lavoura", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movimentolavoura",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movimentoid = table.Column<int>(type: "integer", nullable: false),
                    lavouraid = table.Column<int>(type: "integer", nullable: false),
                    datarealizado = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    observacao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movimentolavoura", x => x.id);
                    table.ForeignKey(
                        name: "fk_movimentolavoura_lavoura_lavouraid",
                        column: x => x.lavouraid,
                        principalTable: "lavoura",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movimentolavoura_movimento_movimentoid",
                        column: x => x.movimentoid,
                        principalTable: "movimento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_movimentolavoura_lavouraid",
                table: "movimentolavoura",
                column: "lavouraid");

            migrationBuilder.CreateIndex(
                name: "ix_movimentolavoura_movimentoid",
                table: "movimentolavoura",
                column: "movimentoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimentolavoura");

            migrationBuilder.DropTable(
                name: "lavoura");
        }
    }
}
