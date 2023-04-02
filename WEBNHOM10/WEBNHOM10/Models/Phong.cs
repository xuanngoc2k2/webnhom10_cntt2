using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class Phong
{
    [DisplayName("Mã phòng")]
    public int MaPhong { get; set; }

    [DisplayName("Tên phòng")]
    public string? TenPhong { get; set; }

    public int? Songuoitoida { get; set; }

    public string? LoaiPhong { get; set; }

    public int? Songuoidango { get; set; }

    public int? GiaPhong { get; set; }

    public string? Ghichu { get; set; }

    public int? MaNha { get; set; }

    public string? AnhDaiDienPhong { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; } = new List<ChiTietHoaDon>();

    public virtual ICollection<CtanhPhong> CtanhPhongs { get; } = new List<CtanhPhong>();

    public virtual Nha? MaNhaNavigation { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; } = new List<SinhVien>();

    public virtual ICollection<ThietBi> ThietBis { get; } = new List<ThietBi>();
}
