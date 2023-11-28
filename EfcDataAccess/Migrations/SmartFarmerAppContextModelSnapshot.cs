﻿// <auto-generated />
using System;
using EfcDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EfcDataAccess.Migrations
{
    [DbContext(typeof(SmartFarmerAppContext))]
    partial class SmartFarmerAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("Domain.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocationData")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Fields");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LocationData = "(-100.123, 50.456), (-100.789, 50.456), (-100.789, 50.123), (-100.123, 50.123)",
                            Name = "RolfMark1",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 2,
                            LocationData = "(-101.123, 51.456), (-101.789, 51.456), (-101.789, 51.123), (-101.123, 51.123)",
                            Name = "RolfMark2",
                            OwnerId = 1
                        },
                        new
                        {
                            Id = 3,
                            LocationData = "(-102.123, 52.456), (-102.789, 52.456), (-102.789, 52.123), (-102.123, 52.123)",
                            Name = "AlecsenMark1",
                            OwnerId = 2
                        },
                        new
                        {
                            Id = 4,
                            LocationData = "(-103.123, 53.456), (-103.789, 53.456), (-103.789, 53.123), (-103.123, 53.123)",
                            Name = "AlecsenMark2",
                            OwnerId = 2
                        },
                        new
                        {
                            Id = 5,
                            LocationData = "(-104.123, 54.456), (-104.789, 54.456), (-104.789, 54.123), (-104.123, 54.123)",
                            Name = "MariasMark1",
                            OwnerId = 3
                        });
                });

            modelBuilder.Entity("Domain.Models.WeatherStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Evaporation")
                        .HasColumnType("REAL");

                    b.Property<int>("FieldId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Precipitation")
                        .HasColumnType("REAL");

                    b.Property<string>("WindDirection")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("WeatherStations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Evaporation = 0.0,
                            FieldId = 0,
                            Precipitation = 2.5,
                            WindDirection = "Syd",
                            WindSpeed = 4.0
                        },
                        new
                        {
                            Id = 2,
                            Evaporation = -1.0,
                            FieldId = 0,
                            Precipitation = 1.0,
                            WindDirection = "Nord",
                            WindSpeed = 2.0
                        },
                        new
                        {
                            Id = 3,
                            Evaporation = -4.0,
                            FieldId = 0,
                            Precipitation = 0.0,
                            WindDirection = "Syd",
                            WindSpeed = 6.0
                        },
                        new
                        {
                            Id = 4,
                            Evaporation = 0.0,
                            FieldId = 0,
                            Precipitation = 2.5,
                            WindDirection = "Vest",
                            WindSpeed = 8.0
                        });
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Hallssti 29",
                            Birthday = new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user1@example.com",
                            Name = "User One",
                            Password = "1234",
                            Phone = "53299870",
                            Role = "Admin",
                            Sex = "male",
                            Username = "Rolf"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Hallssti 29",
                            Birthday = new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user2@example.com",
                            Name = "User Two",
                            Password = "1234",
                            Phone = "53299870",
                            Role = "User",
                            Sex = "male",
                            Username = "Alecsen"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Hallssti 29",
                            Birthday = new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user3@example.com",
                            Name = "User Three",
                            Password = "1234",
                            Phone = "53299870",
                            Role = "User",
                            Sex = "male",
                            Username = "Maria"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Hallssti 29",
                            Birthday = new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user4@example.com",
                            Name = "User Four",
                            Password = "1234",
                            Phone = "53299870",
                            Role = "Manager",
                            Sex = "male",
                            Username = "Røde"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Hallssti 29",
                            Birthday = new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user5@example.com",
                            Name = "User Five",
                            Password = "1234",
                            Phone = "53299870",
                            Role = "Manager",
                            Sex = "male",
                            Username = "user5"
                        });
                });

            modelBuilder.Entity("Domain.Models.Field", b =>
                {
                    b.HasOne("User", "Owner")
                        .WithMany("Fields")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}
