using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RestaurentContext : DbContext
    {
        public RestaurentContext(DbContextOptions<RestaurentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ban> Bans { get; set; }

        public virtual DbSet<MonAn> MonAns { get; set; }

        public virtual DbSet<NhanVien> NhanViens { get; set; }

        public virtual DbSet<Set> Sets { get; set; }

        public virtual DbSet<HoaDon> SetBanMonAns { get; set; }

        public virtual DbSet<SetMonAn> SetMonAns { get; set; }
        public virtual DbSet<HoaDonSetMonAn> HoaDonSetMonAns { get; set; }
        public virtual DbSet<HoaDonMonAn> HoaDonMonAns { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ban>(entity =>
            {
                entity
                    .ToTable("Ban");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.TenBan).HasMaxLength(50);
            });

            modelBuilder.Entity<MonAn>(entity =>
            {
                entity.ToTable("MonAn");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("NhanVien");

                entity.Property(e => e.CheckOutTime).HasColumnType("datetime");
                entity.Property(e => e.CheckinTime).HasColumnType("datetime");
                entity.Property(e => e.MaNhanvien)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.TenNhanvien)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("Set");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreatedBy).HasMaxLength(50);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.UpdatedBy).HasMaxLength(50);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
                entity.Property(e => e.Url).HasMaxLength(500);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("SetBanMonAn");
            });

            modelBuilder.Entity<SetMonAn>(entity =>
            {
                entity.ToTable("SetMonAN");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreatedBy).HasMaxLength(50);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.TenMonAn).HasMaxLength(50);
                entity.Property(e => e.UpdatedBy).HasMaxLength(50);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });
        }

    }
}
