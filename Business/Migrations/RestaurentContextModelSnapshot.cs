﻿// <auto-generated />
using System;
using Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Business.Migrations
{
    [DbContext(typeof(RestaurentContext))]
    partial class RestaurentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Entities.Ban", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("TenBan")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Ban", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.HoaDon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BanId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("MaHoaDon")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ThanhToan")
                        .HasColumnType("bit");

                    b.Property<double>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("HoaDon", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.MonAn", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Gia")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long?>("SetId")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SetId");

                    b.ToTable("MonAn", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.NhanVien", b =>
                {
                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<long?>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("MaNhanvien")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.Property<string>("TenNhanvien")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.ToTable("NhanVien", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Set", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Gia")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Url")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Set", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.SetMonAn", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<long?>("MonAnId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SetId")
                        .HasColumnType("bigint");

                    b.Property<string>("TenMonAn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("MonAnId");

                    b.HasIndex("SetId");

                    b.ToTable("SetMonAN", (string)null);
                });

            modelBuilder.Entity("Models.Entities.ChamCong", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaNhanVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChamCong");
                });

            modelBuilder.Entity("Models.Entities.HoaDonMonAn", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid>("HoaDonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("MonAnId")
                        .HasColumnType("bigint");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<double>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("HoaDonMonAn");
                });

            modelBuilder.Entity("Models.Entities.HoaDonSetMonAn", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid>("HoaDonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("SetId")
                        .HasColumnType("bigint");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<double>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("HoaDonSetMonAn");
                });

            modelBuilder.Entity("Entities.Entities.MonAn", b =>
                {
                    b.HasOne("Entities.Entities.Set", null)
                        .WithMany("MonAns")
                        .HasForeignKey("SetId");
                });

            modelBuilder.Entity("Entities.Entities.SetMonAn", b =>
                {
                    b.HasOne("Entities.Entities.MonAn", "MonAn")
                        .WithMany()
                        .HasForeignKey("MonAnId");

                    b.HasOne("Entities.Entities.Set", "Set")
                        .WithMany("SetMonAn")
                        .HasForeignKey("SetId");

                    b.Navigation("MonAn");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("Entities.Entities.Set", b =>
                {
                    b.Navigation("MonAns");

                    b.Navigation("SetMonAn");
                });
#pragma warning restore 612, 618
        }
    }
}
