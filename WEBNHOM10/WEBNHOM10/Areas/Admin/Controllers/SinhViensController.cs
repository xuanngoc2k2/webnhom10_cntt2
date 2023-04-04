using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBNHOM10.Areas.Admin.Models.SinhVienAPI;
using WEBNHOM10.Models;

namespace WEBNHOM10.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhViensController : ControllerBase
    {
        QlKtxContext _context = new QlKtxContext();
        public IEnumerable<SinhVienAPI> getAll()
        {
            var lst = (from s in _context.SinhViens
                       select new SinhVienAPI
                       {
                           MaSinhVien = s.MaSinhVien,
                           TenSinhVien = s.TenSinhVien,
                           NgaySinh = s.NgaySinh,
                           GioiTinh = s.GioiTinh,
                           SoDienThoai = s.SoDienThoat,
                           MaHopDong = s.MaHopDong,
                           MaPhong = s.MaPhong,
                           MaLop = s.MaLop,
                           MaQue = s.MaQue,
                           TaiKhoan = s.TaiKhoan,
                           MatKhau = s.MatKhau,
                           SoCccd = s.SoCccd,
                           TrangThai = s.TrangThai
                       }).ToList();
            return lst;
        }
        [HttpGet("{masv}")]
        public IEnumerable<SinhVienAPI> getSv(int masv)
        {
            var lst = (from s in _context.SinhViens
                       where s.MaSinhVien == masv
                       select new SinhVienAPI
                       {
                           MaSinhVien = s.MaSinhVien,
                           TenSinhVien = s.TenSinhVien,
                           NgaySinh = s.NgaySinh,
                           GioiTinh = s.GioiTinh,
                           SoDienThoai = s.SoDienThoat,
                           MaHopDong = s.MaHopDong,
                           MaPhong = s.MaPhong,
                           MaLop = s.MaLop,
                           MaQue = s.MaQue,
                           TaiKhoan = s.TaiKhoan,
                           MatKhau = s.MatKhau,
                           SoCccd = s.SoCccd,
                           TrangThai = s.TrangThai
                       }).ToList();
            return lst;
        }
        [HttpDelete("{masv}")]
        public IActionResult XoaSinhVien(int masv)
        {
            var sv = _context.SinhViens.FirstOrDefault(x => x.MaSinhVien == masv);
            if (sv == null)
            {
                return NotFound();
            }
            _context.SinhViens.Remove(sv);
            _context.SaveChanges();
            return Ok();
        }
    }
}
