﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShowroomCarIS220.Data;

#nullable disable

namespace ShowroomCarIS220.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221215050840_AddModel")]
    partial class AddModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShowroomCarIS220.Models.CTHD", b =>
                {
                    b.Property<string>("macar")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("mahd")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HoaDonmahd")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HoaDonmakh")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HoaDonmanv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("gia")
                        .HasColumnType("bigint");

                    b.Property<Guid>("id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("soluong")
                        .HasColumnType("int");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("macar", "mahd");

                    b.HasIndex("HoaDonmahd", "HoaDonmakh", "HoaDonmanv");

                    b.ToTable("CTHD");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.Car", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("advice")
                        .HasColumnType("bit");

                    b.Property<string>("congsuatcucdai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("dongco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dungtich")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("gia")
                        .HasColumnType("bigint");

                    b.Property<string>("hinhanh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kichthuoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("macar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mausac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("namsanxuat")
                        .HasColumnType("int");

                    b.Property<string>("nguongoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("socho")
                        .HasColumnType("int");

                    b.Property<int>("soluong")
                        .HasColumnType("int");

                    b.Property<string>("ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("thuonghieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tieuhaonhienlieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("vantoctoida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.Form", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.HoaDon", b =>
                {
                    b.Property<string>("mahd")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("makh")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("manv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ngayhd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenkh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tinhtrang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("trigia")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("mahd", "makh", "manv");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.News", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("dateSource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.Token", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("chucvu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("diachi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gioitinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mauser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ngaysinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("verifyToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.CTHD", b =>
                {
                    b.HasOne("ShowroomCarIS220.Models.HoaDon", null)
                        .WithMany("cthds")
                        .HasForeignKey("HoaDonmahd", "HoaDonmakh", "HoaDonmanv");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.Token", b =>
                {
                    b.HasOne("ShowroomCarIS220.Models.User", null)
                        .WithMany("token")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.HoaDon", b =>
                {
                    b.Navigation("cthds");
                });

            modelBuilder.Entity("ShowroomCarIS220.Models.User", b =>
                {
                    b.Navigation("token");
                });
#pragma warning restore 612, 618
        }
    }
}
