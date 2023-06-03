﻿// <auto-generated />
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230602020530_Uf_Municipio_Cep")]
    partial class Uf_Municipio_Cep
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Api.Domain.Entities.CepEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<Guid>("MunicipioId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Numero")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Cep");

                    b.HasIndex("MunicipioId");

                    b.ToTable("Cep");
                });

            modelBuilder.Entity("Api.Domain.Entities.MunicipioEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CodIbge")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<Guid>("UfId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CodIbge");

                    b.HasIndex("UfId");

                    b.ToTable("Municipio");
                });

            modelBuilder.Entity("Api.Domain.Entities.UfEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Sigla")
                        .IsUnique();

                    b.ToTable("Uf");

                    b.HasData(
                        new
                        {
                            Id = new Guid("953aec48-babc-4cea-9aaa-5b74ebb4f836"),
                            CreateAt = new DateTime(2023, 6, 2, 2, 5, 29, 579, DateTimeKind.Utc).AddTicks(9378),
                            Nome = "Ceará",
                            Sigla = "CE"
                        },
                        new
                        {
                            Id = new Guid("6f426888-069c-4de1-9b3e-59c8ef6b1786"),
                            CreateAt = new DateTime(2023, 6, 2, 2, 5, 29, 579, DateTimeKind.Utc).AddTicks(9428),
                            Nome = "São Paulo",
                            Sigla = "SP"
                        });
                });

            modelBuilder.Entity("Api.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8c030b13-9b0d-4d19-a0b9-58df15ebf1dd"),
                            CreateAt = new DateTime(2023, 6, 1, 23, 5, 29, 578, DateTimeKind.Local).AddTicks(3740),
                            Email = "adm@mail.com",
                            Name = "Admin",
                            UpdateAt = new DateTime(2023, 6, 1, 23, 5, 29, 578, DateTimeKind.Local).AddTicks(4788)
                        });
                });

            modelBuilder.Entity("Api.Domain.Entities.CepEntity", b =>
                {
                    b.HasOne("Api.Domain.Entities.MunicipioEntity", "Municipio")
                        .WithMany("Ceps")
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipio");
                });

            modelBuilder.Entity("Api.Domain.Entities.MunicipioEntity", b =>
                {
                    b.HasOne("Api.Domain.Entities.UfEntity", "Uf")
                        .WithMany("Municipios")
                        .HasForeignKey("UfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uf");
                });

            modelBuilder.Entity("Api.Domain.Entities.MunicipioEntity", b =>
                {
                    b.Navigation("Ceps");
                });

            modelBuilder.Entity("Api.Domain.Entities.UfEntity", b =>
                {
                    b.Navigation("Municipios");
                });
#pragma warning restore 612, 618
        }
    }
}