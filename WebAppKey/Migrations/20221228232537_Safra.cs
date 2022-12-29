using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAppKey.Migrations
{
    public partial class Safra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_movimentolavoura_movimentoid",
                table: "movimentolavoura");

            migrationBuilder.CreateTable(
                name: "safra",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    datainicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datafim = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_safra", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_movimentolavoura_movimentoid",
                table: "movimentolavoura",
                column: "movimentoid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "safra");

            migrationBuilder.DropIndex(
                name: "ix_movimentolavoura_movimentoid",
                table: "movimentolavoura");

            migrationBuilder.CreateIndex(
                name: "ix_movimentolavoura_movimentoid",
                table: "movimentolavoura",
                column: "movimentoid");
        }
    }
}
