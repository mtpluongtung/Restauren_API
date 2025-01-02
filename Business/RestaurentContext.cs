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
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Ban> Ban { get; set; }

        public virtual DbSet<MonAn> MonAn { get; set; }

        public virtual DbSet<NhanVien> NhanVien { get; set; }

        public virtual DbSet<Set> Set { get; set; }

        public virtual DbSet<HoaDon> HoaDon { get; set; }

        public virtual DbSet<SetMonAn> SetMonAn { get; set; }

        public virtual DbSet<HoaDonSetMonAn> HoaDonSetMonAn { get; set; }

        public virtual DbSet<HoaDonMonAn> HoaDonMonAn { get; set; }

        public virtual DbSet<ChamCong> ChamCong { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<Bep> Beps { get; set; }
		public virtual DbSet<CaLamViec> CaLamViecs { get; set; }
        public virtual DbSet<CaLamViecNhanVien> CaLamViecNhanVien { get; set; }
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

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity
                    .ToTable("NhanVien");
                entity.Property(e => e.MaNhanvien)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.TenNhanvien)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

       

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity
                    .ToTable("HoaDon");
            });

			// Ánh xạ entity Set tới bảng "Set"
			modelBuilder.Entity<Set>(entity =>
			{
				entity.ToTable("Set");
				entity.HasKey(e => e.Id); // Khóa chính
				entity.Property(e => e.Name).HasMaxLength(50);
				entity.Property(e => e.CreatedBy).HasMaxLength(50);
				entity.Property(e => e.CreatedDate).HasColumnType("datetime");
			});

			// Ánh xạ entity SetMonAn tới bảng "SetMonAn"
			modelBuilder.Entity<SetMonAn>(entity =>
			{
				entity.ToTable("SetMonAn");
				entity.HasKey(e => e.Id); // Khóa chính
				entity.HasOne(e => e.MonAn) // Mối quan hệ với MonAn
					  .WithMany(m => m.SetMonAn)
					  .HasForeignKey(e => e.MonAnId);

				entity.Property(e => e.CreatedBy).HasMaxLength(50);
				entity.Property(e => e.CreatedDate).HasColumnType("datetime");
			});

			// Ánh xạ entity MonAn tới bảng "MonAn"
			modelBuilder.Entity<MonAn>(entity =>
			{
				entity.ToTable("MonAn");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).HasMaxLength(50);
				entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");
			});
            // Ánh xạ entity MonAn tới bảng "MonAn"
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.Property(e => e.Phone).HasMaxLength(15);
                entity.Property(e => e.TenKhachHang).HasMaxLength(250);
            });
        }

    }
}
