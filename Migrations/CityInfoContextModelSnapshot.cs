﻿// <auto-generated />
using CityInfoAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfoApi.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    partial class CityInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("CityInfoAPI.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The one with that big park.",
                            Name = "New York City"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The one with the cathedral that was never really finished.",
                            Name = "Antwerp"
                        },
                        new
                        {
                            Id = 3,
                            Description = "The one with that big tower.",
                            Name = "Paris"
                        });
                });

            modelBuilder.Entity("CityInfoAPI.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointOfInterests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "The most visited urban park in the United States.",
                            Name = "Central Park"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "A 102-story skyscraper located in Midtown Manhattan.",
                            Name = "Empire State Building"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.",
                            Name = "Cathedral of Our Lady"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            Description = "The the finest example of railway architecture in Belgium.",
                            Name = "Antwerp Central Station"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 3,
                            Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.",
                            Name = "Eiffel Tower"
                        },
                        new
                        {
                            Id = 6,
                            CityId = 3,
                            Description = "The world's largest museum.",
                            Name = "The Louvre"
                        });
                });

            modelBuilder.Entity("CityInfoAPI.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfoAPI.Entities.City", "City")
                        .WithMany("PointOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfoAPI.Entities.City", b =>
                {
                    b.Navigation("PointOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}
