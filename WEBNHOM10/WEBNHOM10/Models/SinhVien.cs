using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class SinhVien
{
    [DisplayName("Mã sinh viên")]
    public int MaSinhVien { get; set; }

    [DisplayName("Tên sinh viên")]
    public string? TenSinhVien { get; set; }

    [DisplayName("Ngày sinh")]
    public DateTime? NgaySinh { get; set; }

    [DisplayName("Giới tính")]
    public string? GioiTinh { get; set; }

    [DisplayName("Số điện thoại")]
    public string? SoDienThoat { get; set; }

    [DisplayName("Mã hợp đồng")]
    public int? MaHopDong { get; set; }

    [DisplayName("Mã phòng")]
    public int? MaPhong { get; set; }

    [DisplayName("Mã lớp")]
    public int? MaLop { get; set; }

    [DisplayName("Mã quê")]
    public int? MaQue { get; set; }

    [DisplayName("Tài khoản")]
    public string? TaiKhoan { get; set; }

    [DisplayName("Mật khẩu")]
    public string? MatKhau { get; set; }

    [DisplayName("Ảnh")]
    public string? Anh { get; set; }

    [DisplayName("Số CCCD/CMND")]
    public string? SoCccd { get; set; }

    [DisplayName("Trạng thái")]
    public int? TrangThai { get; set; }

    public virtual HopDong? MaHopDongNavigation { get; set; }

    public virtual Lop? MaLopNavigation { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }

    public virtual Que? MaQueNavigation { get; set; }
}
