﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ProjetoContext))]
    partial class ProjetoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.PessoaAggregate.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("PessoaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Domain.PessoaAggregate.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Domain.PessoaAggregate.Endereco", b =>
                {
                    b.HasOne("Domain.PessoaAggregate.Pessoa", null)
                        .WithMany("Enderecos")
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("Domain.PessoaAggregate.Pessoa", b =>
                {
                    b.OwnsOne("Domain.PessoaAggregate.CPF", "Cpf", b1 =>
                        {
                            b1.Property<int>("PessoaId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)");

                            b1.HasKey("PessoaId");

                            b1.ToTable("Pessoas");

                            b1.WithOwner()
                                .HasForeignKey("PessoaId");
                        });

                    b.Navigation("Cpf");
                });

            modelBuilder.Entity("Domain.PessoaAggregate.Pessoa", b =>
                {
                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}