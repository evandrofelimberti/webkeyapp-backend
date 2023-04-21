using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppKey.Migrations
{
    public partial class controleCustoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "valorcompra",
                table: "produtosaldo",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "valormediocompra",
                table: "produtosaldo",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "valorvenda",
                table: "produtosaldo",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "valorcompra",
                table: "produtosaldo");

            migrationBuilder.DropColumn(
                name: "valormediocompra",
                table: "produtosaldo");

            migrationBuilder.DropColumn(
                name: "valorvenda",
                table: "produtosaldo");
        }
    }
}
