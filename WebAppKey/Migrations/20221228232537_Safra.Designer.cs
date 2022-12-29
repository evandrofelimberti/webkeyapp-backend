﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebAppKey.Data;

#nullable disable

namespace WebAppKey.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221228232537_Safra")]
    partial class Safra
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebAppKey.Models.Lavoura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AreaHa")
                        .HasColumnType("double precision")
                        .HasColumnName("areaha");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.HasKey("Id")
                        .HasName("pk_lavoura");

                    b.ToTable("lavoura", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.Movimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datainclusao");

                    b.Property<int>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("numero");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("observacao");

                    b.Property<int>("Situacao")
                        .HasColumnType("integer")
                        .HasColumnName("situacao");

                    b.Property<int>("TipoMovimentoId")
                        .HasColumnType("integer")
                        .HasColumnName("tipomovimentoid");

                    b.Property<double>("Total")
                        .HasColumnType("double precision")
                        .HasColumnName("total");

                    b.HasKey("Id")
                        .HasName("pk_movimento");

                    b.HasIndex("TipoMovimentoId")
                        .HasDatabaseName("ix_movimento_tipomovimentoid");

                    b.ToTable("movimento", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.MovimentoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datainclusao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("descricao");

                    b.Property<int>("MovimentoId")
                        .HasColumnType("integer")
                        .HasColumnName("movimentoid");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("integer")
                        .HasColumnName("produtoid");

                    b.Property<double>("Quantidade")
                        .HasColumnType("double precision")
                        .HasColumnName("quantidade");

                    b.Property<double>("Total")
                        .HasColumnType("double precision")
                        .HasColumnName("total");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("pk_movimentoitem");

                    b.HasIndex("MovimentoId")
                        .HasDatabaseName("ix_movimentoitem_movimentoid");

                    b.HasIndex("ProdutoId")
                        .HasDatabaseName("ix_movimentoitem_produtoid");

                    b.ToTable("movimentoitem", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.MovimentoLavoura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataRealizado")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datarealizado");

                    b.Property<int>("LavouraId")
                        .HasColumnType("integer")
                        .HasColumnName("lavouraid");

                    b.Property<int>("MovimentoId")
                        .HasColumnType("integer")
                        .HasColumnName("movimentoid");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("observacao");

                    b.HasKey("Id")
                        .HasName("pk_movimentolavoura");

                    b.HasIndex("LavouraId")
                        .HasDatabaseName("ix_movimentolavoura_lavouraid");

                    b.HasIndex("MovimentoId")
                        .IsUnique()
                        .HasDatabaseName("ix_movimentolavoura_movimentoid");

                    b.ToTable("movimentolavoura", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("codigo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("descricao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("nome");

                    b.Property<int>("TipoProdutoId")
                        .HasColumnType("integer")
                        .HasColumnName("tipoprodutoid");

                    b.Property<int>("UnidadeId")
                        .HasColumnType("integer")
                        .HasColumnName("unidadeid");

                    b.HasKey("Id")
                        .HasName("pk_produto");

                    b.HasIndex("TipoProdutoId")
                        .HasDatabaseName("ix_produto_tipoprodutoid");

                    b.HasIndex("UnidadeId")
                        .HasDatabaseName("ix_produto_unidadeid");

                    b.ToTable("produto", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.Safra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datafim");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datainicio");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.HasKey("Id")
                        .HasName("pk_safra");

                    b.ToTable("safra", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.TipoMovimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("descricao");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("pk_tipomovimento");

                    b.ToTable("tipomovimento", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.TipoProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("descricao");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("sigla");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("pk_tipoproduto");

                    b.ToTable("tipoproduto", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.Unidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("descricao");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("sigla");

                    b.HasKey("Id")
                        .HasName("pk_unidade");

                    b.ToTable("unidade", (string)null);
                });

            modelBuilder.Entity("WebAppKey.Models.Movimento", b =>
                {
                    b.HasOne("WebAppKey.Models.TipoMovimento", "TipoMovimento")
                        .WithMany()
                        .HasForeignKey("TipoMovimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movimento_tipomovimento_tipomovimentoid");

                    b.Navigation("TipoMovimento");
                });

            modelBuilder.Entity("WebAppKey.Models.MovimentoItem", b =>
                {
                    b.HasOne("WebAppKey.Models.Movimento", "Movimento")
                        .WithMany("Itens")
                        .HasForeignKey("MovimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movimentoitem_movimento_movimentoid");

                    b.HasOne("WebAppKey.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movimentoitem_produto_produtoid");

                    b.Navigation("Movimento");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("WebAppKey.Models.MovimentoLavoura", b =>
                {
                    b.HasOne("WebAppKey.Models.Lavoura", "Lavoura")
                        .WithMany()
                        .HasForeignKey("LavouraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movimentolavoura_lavoura_lavouraid");

                    b.HasOne("WebAppKey.Models.Movimento", "Movimento")
                        .WithOne("MovimentoLavoura")
                        .HasForeignKey("WebAppKey.Models.MovimentoLavoura", "MovimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movimentolavoura_movimento_movimentoid");

                    b.Navigation("Lavoura");

                    b.Navigation("Movimento");
                });

            modelBuilder.Entity("WebAppKey.Models.Produto", b =>
                {
                    b.HasOne("WebAppKey.Models.TipoProduto", "TipoProduto")
                        .WithMany()
                        .HasForeignKey("TipoProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_produto_tipoproduto_tipoprodutoid");

                    b.HasOne("WebAppKey.Models.Unidade", "Unidade")
                        .WithMany()
                        .HasForeignKey("UnidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_produto_unidade_unidadeid");

                    b.Navigation("TipoProduto");

                    b.Navigation("Unidade");
                });

            modelBuilder.Entity("WebAppKey.Models.Movimento", b =>
                {
                    b.Navigation("Itens");

                    b.Navigation("MovimentoLavoura")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
