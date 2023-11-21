﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Bolgeler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BolgeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bolgelers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BolgeName = "Merkez"
                        },
                        new
                        {
                            Id = 2,
                            BolgeName = "Alaca"
                        });
                });

            modelBuilder.Entity("Entities.Models.EczaneBilgileri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BolgelerId")
                        .HasColumnType("int");

                    b.Property<string>("EczaneAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EczaneAdresTarifi")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("EczaneAdresi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("EczaneKordinatLatitude")
                        .HasColumnType("float");

                    b.Property<double>("EczaneKordinatLongitude")
                        .HasColumnType("float");

                    b.Property<string>("EczaneMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EczaneSahibiAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EczaneSahibiSoyadi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EczaneSahibiTC")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("EczaneSahibiTelefon")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("EczaneTelefon")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("SehirlerId")
                        .HasColumnType("int");

                    b.Property<int>("YasakliEzcaneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BolgelerId");

                    b.HasIndex("SehirlerId");

                    b.ToTable("EczaneBilgileris");
                });

            modelBuilder.Entity("Entities.Models.MazeretBilgileri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Acıklama")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("BaslamaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("EczaneBilgileriId")
                        .HasColumnType("int");

                    b.Property<DateTime>("IslemTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EczaneBilgileriId");

                    b.ToTable("MazeretBilgileri");
                });

            modelBuilder.Entity("Entities.Models.NobetDagilim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EczaneId")
                        .HasColumnType("int");

                    b.Property<int>("NobetSayisi")
                        .HasColumnType("int");

                    b.Property<int>("NobetTuruId")
                        .HasColumnType("int");

                    b.Property<int>("YasaklıEczaneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NobetTuruId");

                    b.ToTable("NobetDagilim");
                });

            modelBuilder.Entity("Entities.Models.NobetTurleri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NobetTuru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NobetTuru");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NobetTuru = "Resmi Tatil"
                        },
                        new
                        {
                            Id = 2,
                            NobetTuru = "Cumartesi"
                        },
                        new
                        {
                            Id = 3,
                            NobetTuru = "Pazar"
                        },
                        new
                        {
                            Id = 4,
                            NobetTuru = "Haftaici"
                        });
                });

            modelBuilder.Entity("Entities.Models.Nobetler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Acıklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EczaneBilgileriId")
                        .HasColumnType("int");

                    b.Property<string>("NobetTuru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EczaneBilgileriId");

                    b.ToTable("Nobetlers");
                });

            modelBuilder.Entity("Entities.Models.ResmiTatiller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Acıklama")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("IslemTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ResmiTatiller");
                });

            modelBuilder.Entity("Entities.Models.Sehirler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Sehir")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sehirlers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Sehir = "Çorm"
                        },
                        new
                        {
                            Id = 2,
                            Sehir = "Ankara"
                        });
                });

            modelBuilder.Entity("Entities.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Editor"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "User"
                        },
                        new
                        {
                            Id = 4,
                            RoleName = "Visitor"
                        });
                });

            modelBuilder.Entity("Entities.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin",
                            IsEmailConfirmed = true,
                            Password = "Admin",
                            UserName = "Admin",
                            UserRoleId = 1
                        });
                });

            modelBuilder.Entity("Entities.Models.EczaneBilgileri", b =>
                {
                    b.HasOne("Entities.Models.Bolgeler", "EczaneBolge")
                        .WithMany()
                        .HasForeignKey("BolgelerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Sehirler", "Sehirler")
                        .WithMany()
                        .HasForeignKey("SehirlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EczaneBolge");

                    b.Navigation("Sehirler");
                });

            modelBuilder.Entity("Entities.Models.MazeretBilgileri", b =>
                {
                    b.HasOne("Entities.Models.EczaneBilgileri", "EczaneBilgileri")
                        .WithMany()
                        .HasForeignKey("EczaneBilgileriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EczaneBilgileri");
                });

            modelBuilder.Entity("Entities.Models.NobetDagilim", b =>
                {
                    b.HasOne("Entities.Models.NobetTurleri", "NobetTuru")
                        .WithMany()
                        .HasForeignKey("NobetTuruId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NobetTuru");
                });

            modelBuilder.Entity("Entities.Models.Nobetler", b =>
                {
                    b.HasOne("Entities.Models.EczaneBilgileri", "EczaneBilgileri")
                        .WithMany()
                        .HasForeignKey("EczaneBilgileriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EczaneBilgileri");
                });

            modelBuilder.Entity("Entities.Models.Users", b =>
                {
                    b.HasOne("Entities.Models.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
