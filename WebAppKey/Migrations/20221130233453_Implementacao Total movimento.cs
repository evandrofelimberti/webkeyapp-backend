using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppKey.Migrations
{
    public partial class ImplementacaoTotalmovimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "total",
                table: "movimento",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total",
                table: "movimento");
            
        }
    }
}
