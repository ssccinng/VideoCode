﻿// <auto-generated />
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EF8Complex.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Move", b =>
                {
                    b.Property<int>("MoveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Names", "Move.Names#Names", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Chinese")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("English")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Japanese")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.ComplexProperty<Dictionary<string, object>>("NameDetail", "Move.Names#Names.NameDetail#NameDetail", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("NameDescription")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("NameType")
                                        .IsRequired()
                                        .HasColumnType("TEXT");
                                });
                        });

                    b.HasKey("MoveId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("Pokemon", b =>
                {
                    b.Property<int>("PokemonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Names", "Pokemon.Names#Names", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Chinese")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("English")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Japanese")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.ComplexProperty<Dictionary<string, object>>("NameDetail", "Pokemon.Names#Names.NameDetail#NameDetail", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("NameDescription")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("NameType")
                                        .IsRequired()
                                        .HasColumnType("TEXT");
                                });
                        });

                    b.HasKey("PokemonId");

                    b.ToTable("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}