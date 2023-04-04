using System.ComponentModel;

namespace WEBNHOM10.Areas.Admin.Models.SinhVienAPI
{
    public class SinhVienAPI
    {
        public int MaSinhVien { get; set; }
        public string? TenSinhVien { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? SoDienThoai { get; set; }
        public int? MaHopDong { get; set; }
        public int? MaPhong { get; set; }
        public int? MaLop { get; set; }
        public int? MaQue { get; set; }
        public string? TaiKhoan { get; set; }
        public string? MatKhau { get; set; }
        public string? Anh { get; set; }
        public string? SoCccd { get; set; }
        public int? TrangThai { get; set; }
    }
}
