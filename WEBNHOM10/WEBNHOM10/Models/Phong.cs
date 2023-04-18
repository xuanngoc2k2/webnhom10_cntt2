using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBNHOM10.Models;

public partial class Phong
{
    [DisplayName("Mã phòng")]
    public int MaPhong { get; set; }

    [DisplayName("Tên phòng")]
    public string? TenPhong { get; set; }
    [DisplayName("Số người ở tối đa")]
    public int? Songuoitoida { get; set; }

    [DisplayName("Loại phòng")]
    public string? LoaiPhong { get; set; }
    [DisplayName("Số người đang ở")]
    public int? Songuoidango { get; set; }
    [DisplayName("Giá phòng")]
    public int? GiaPhong { get; set; }
    [DisplayName("Ghi chú")]
    public string? Ghichu { get; set; }
    [DisplayName("Mã nhà")]
    public int? MaNha { get; set; }
    public string? AnhDaiDienPhong { get; set; }
    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; } = new List<ChiTietHoaDon>();
    public virtual ICollection<CtanhPhong> CtanhPhongs { get; } = new List<CtanhPhong>();
    public virtual Nha? MaNhaNavigation { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; } = new List<SinhVien>();

    public virtual ICollection<ThietBi> ThietBis { get; } = new List<ThietBi>();

    [NotMapped]
    public IFormFile? ImagePhong { get; set; }
}
