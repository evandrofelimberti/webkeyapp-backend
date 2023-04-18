using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAppKey.Migrations
{
    public partial class ProdutoSaldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produtosaldo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    produtoid = table.Column<int>(type: "integer", nullable: false),
                    valorsaldo = table.Column<double>(type: "double precision", nullable: false),
                    valorentrada = table.Column<double>(type: "double precision", nullable: false),
                    valorsaida = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produtosaldo", x => x.id);
                    table.ForeignKey(
                        name: "fk_produtosaldo_produto_produtoid",
                        column: x => x.produtoid,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_produtosaldo_produtoid",
                table: "produtosaldo",
                column: "produtoid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produtosaldo");
        }
    }
}
