using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WEBNHOM10.Models;

public partial class QlKtxContext : DbContext
{
    public QlKtxContext()
    {
    }

    public QlKtxContext(DbContextOptions<QlKtxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<CtanhPhong> CtanhPhongs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<HopDong> HopDongs { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    public virtual DbSet<Nha> Nhas { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<Que> Ques { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<ThietBi> ThietBis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-9ATFPMT\\SQLEXPRESS;Initial Catalog=QL_KTX;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => new { e.MaHoaDon, e.MaPhong }).HasName("pk_ChiTietHoaDon");

            entity.ToTable("ChiTietHoaDon");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__MaHoa__36B12243");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__MaPho__37A5467C");
        });

        modelBuilder.Entity<CtanhPhong>(entity =>
        {
            entity.HasKey(e => new { e.MaPhong, e.LinkAnh }).HasName("pk_AnhPhong");

            entity.ToTable("CTAnhPhong");

            entity.Property(e => e.LinkAnh)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.CtanhPhongs)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTAnhPhon__MaPho__60A75C0F");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("pk_HoaDon");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHoaDon).ValueGeneratedNever();
            entity.Property(e => e.HanThanhToan).HasColumnType("datetime");
        });

        modelBuilder.Entity<HopDong>(entity =>
        {
            entity.HasKey(e => e.MaHopDong).HasName("pk_HopDong");

            entity.ToTable("HopDong");

            entity.Property(e => e.MaHopDong).ValueGeneratedNever();
            entity.Property(e => e.GhiChu).HasMaxLength(200);
            entity.Property(e => e.NgayHetHan).HasColumnType("datetime");
            entity.Property(e => e.NgayKi).HasColumnType("datetime");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("pk_Khoa");

            entity.ToTable("Khoa");

            entity.Property(e => e.MaKhoa).ValueGeneratedNever();
            entity.Property(e => e.TenKhoa).HasMaxLength(20);
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("pk_Lop");

            entity.ToTable("Lop");

            entity.Property(e => e.MaLop).ValueGeneratedNever();
            entity.Property(e => e.TenLop).HasMaxLength(20);

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__Lop__MaKhoa__2A4B4B5E");
        });

        modelBuilder.Entity<Nha>(entity =>
        {
            entity.HasKey(e => e.MaNha).HasName("pk_Nha");

            entity.ToTable("Nha");

            entity.Property(e => e.MaNha).ValueGeneratedNever();
            entity.Property(e => e.TenNha).HasMaxLength(20);
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("pk_Phong");

            entity.ToTable("Phong");

            entity.Property(e => e.MaPhong).ValueGeneratedNever();
            entity.Property(e => e.AnhDaiDienPhong)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Ghichu).HasMaxLength(200);
            entity.Property(e => e.LoaiPhong).HasMaxLength(20);
            entity.Property(e => e.TenPhong).HasMaxLength(20);

            entity.HasOne(d => d.MaNhaNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.MaNha)
                .HasConstraintName("FK__Phong__MaNha__2F10007B");
        });

        modelBuilder.Entity<Que>(entity =>
        {
            entity.HasKey(e => e.MaQue).HasName("pk_Que");

            entity.ToTable("Que");

            entity.Property(e => e.MaQue).ValueGeneratedNever();
            entity.Property(e => e.TenQue).HasMaxLength(50);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSinhVien).HasName("pk_SinhVien");

            entity.ToTable("SinhVien");

            entity.Property(e => e.MaSinhVien).ValueGeneratedNever();
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(20);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.SoCccd)
                .HasMaxLength(50)
                .HasColumnName("SoCCCD");
            entity.Property(e => e.SoDienThoat).HasMaxLength(20);
            entity.Property(e => e.TaiKhoan).HasMaxLength(20);
            entity.Property(e => e.TenSinhVien).HasMaxLength(50);

            entity.HasOne(d => d.MaHopDongNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.MaHopDong)
                .HasConstraintName("FK__SinhVien__MaHopD__44FF419A");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.MaLop)
                .HasConstraintName("FK__SinhVien__MaLop__47DBAE45");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK__SinhVien__MaPhon__46E78A0C");

            entity.HasOne(d => d.MaQueNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.MaQue)
                .HasConstraintName("FK__SinhVien__MaQue__45F365D3");
        });

        modelBuilder.Entity<ThietBi>(entity =>
        {
            entity.HasKey(e => e.MaThietBi).HasName("pk_ThietBi");

            entity.ToTable("ThietBi");

            entity.Property(e => e.MaThietBi).ValueGeneratedNever();
            entity.Property(e => e.AnhThietBi)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.TenThietBi).HasMaxLength(50);

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.ThietBis)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK__ThietBi__MaPhong__31EC6D26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
