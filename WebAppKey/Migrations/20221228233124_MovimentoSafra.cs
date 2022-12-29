using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppKey.Migrations
{
    public partial class MovimentoSafra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "safraid",
                table: "movimentolavoura",
                type: "integer",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateIndex(
                name: "ix_movimentolavoura_safraid",
                table: "movimentolavoura",
                column: "safraid");

            migrationBuilder.AddForeignKey(
                name: "fk_movimentolavoura_safra_safraid",
                table: "movimentolavoura",
                column: "safraid",
                principalTable: "safra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_movimentolavoura_safra_safraid",
                table: "movimentolavoura");

            migrationBuilder.DropIndex(
                name: "ix_movimentolavoura_safraid",
                table: "movimentolavoura");

            migrationBuilder.DropColumn(
                name: "safraid",
                table: "movimentolavoura");
        }
    }
}
