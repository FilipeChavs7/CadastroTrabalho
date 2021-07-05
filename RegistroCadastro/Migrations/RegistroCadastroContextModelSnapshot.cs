﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroCadastro.Models;

namespace RegistroCadastro.Migrations
{
    [DbContext(typeof(RegistroCadastroContext))]
    partial class RegistroCadastroContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RegistroCadastro.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CEP");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<string>("Estado")
                        .HasMaxLength(2);

                    b.Property<string>("Logradouro");

                    b.Property<int>("PessoaId");

                    b.Property<int>("TipoDeRua");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("RegistroCadastro.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("CPF");

                    b.Property<int>("Idade");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Sexo");

                    b.HasKey("Id");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("RegistroCadastro.Models.Endereco", b =>
                {
                    b.HasOne("RegistroCadastro.Models.Pessoa", "Pessoa")
                        .WithMany("Endereco")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
