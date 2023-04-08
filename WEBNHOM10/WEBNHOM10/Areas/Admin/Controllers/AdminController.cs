using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBNHOM10.Models;

namespace WEBNHOM10.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class AdminController : Controller
    {
        QlKtxContext db = new QlKtxContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("quanliycthue")]
        public IActionResult QuanLyYeucauThue()
        {
            var lstSinhvien = db.SinhViens
                .Include(x=>x.MaPhongNavigation)
                .Include(x=>x.MaLopNavigation)
                .Include(x=>x.MaQueNavigation)
                .ToList();
            return View(lstSinhvien);
        }

        [Route("duyetycthue")]
        [HttpGet]
        public IActionResult DuyetYcThue(int maSV)
        {
            var sv = db.SinhViens
                .Include(x => x.MaLopNavigation)
                .Include(x => x.MaQueNavigation)
                .Include(x => x.MaPhongNavigation)
                .Include(x=>x.MaHopDongNavigation)
                .SingleOrDefault(x=>x.MaSinhVien==maSV);
            ViewBag.sv = sv;
            return View();
        }
        [Route("duyetycthue")]
        [HttpPost]
        public IActionResult DuyetYcThue(int maSV, HopDong hopDong)
        {
            TempData["DuyetYC"] = "";
            var sv = db.SinhViens
                .Include(x => x.MaLopNavigation)
                .Include(x => x.MaQueNavigation)
                .Include(x => x.MaPhongNavigation)
                .SingleOrDefault(x => x.MaSinhVien == maSV);
            ViewBag.sv = sv;
            int sothang = Convert.ToInt32((hopDong.NgayHetHan - hopDong.NgayKi).Days /30);
            if(sothang < 3)
            {
                TempData["DuyetYC"] = "Thời gian tối thiếu 3 tháng !!";
                return View(hopDong);
            }
            else
            {
                var mahd = db.HopDongs.OrderByDescending(c => c.MaHopDong).FirstOrDefault();
                int nextMaHD = mahd.MaHopDong == null ? 1 : mahd.MaHopDong + 1;
                HopDong newHopdong = new HopDong
                {
                    MaHopDong = nextMaHD,
                    NgayKi = hopDong.NgayKi,
                    NgayHetHan = hopDong.NgayHetHan,
                    GiaThue = hopDong.GiaThue,
                    GhiChu = hopDong.GhiChu
                };
                db.HopDongs.Add(newHopdong);
                db.SaveChanges();
                sv.MaHopDong = newHopdong.MaHopDong;
                sv.MaHopDongNavigation = newHopdong;
                sv.TrangThai = 1;
                db.SaveChanges();
                TempData["DuyetYC"] = "Đã duyệt !!";
                return RedirectToAction("quanliycthue", "admin");
            }
            return View(hopDong);
        }
        [Route("xoayeucau")]
        [HttpGet]
        public IActionResult XoaYeuCau(int masv)
        {
            var sv = db.SinhViens.Where(x=>x.MaSinhVien == masv);
            return View(sv);
        }

        [Route("xemhopdong")]
        [HttpGet]
        public IActionResult XemHopDong(int masv)
        {
            var sv = db.SinhViens
                .Include(x => x.MaLopNavigation)
                .Include(x => x.MaQueNavigation)
                .Include(x => x.MaPhongNavigation)
                .Include(x => x.MaHopDongNavigation)
                .SingleOrDefault(x => x.MaSinhVien == masv);
            ViewBag.sv = sv;
            return View(sv);
        }

        [Route("ThemHoaDon")]
        [HttpGet]
        public IActionResult ThemHoaDon()
        {
            return View();
        }
        [Route("ThemHoaDon")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ThemHoaDon(HoaDon maHD)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(maHD);
                db.SaveChanges();
                return RedirectToAction("ThongTinHoaDon");
            }
            return View(maHD);
        }

        [Route("ChiTietHoaDon")]
        [HttpGet]
        public IActionResult ChiTietHoaDon(int MaHoaDon)
        {
            var hoaDon = db.ChiTietHoaDons.SingleOrDefault(x => x.MaHoaDon == MaHoaDon);
            return View(hoaDon);
        }

        [Route("SuaHoaDon")]
        [HttpGet]
        public IActionResult SuaHoaDon(int MaHoaDon)
        {

            var hoaDon = db.HoaDons.Find(MaHoaDon);
            return View(hoaDon);
        }
        [Route("SuaHoaDon")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHoaDon(HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinHoaDon", "Admin");
            }
            return View(hoaDon);
        }

        [Route("XoaHoaDon")]
        [HttpGet]
        public IActionResult XoaHoaDon(int MaHoaDon)
        {
            TempData["Message"] = "";
            var ChiTietHoaDon = db.ChiTietHoaDons.Where(x => x.MaHoaDon == MaHoaDon).ToList();
            if (ChiTietHoaDon.Count() > 0)
            {
                TempData["Message"] = "Không xóa được hóa đơn này!!!";
                return RedirectToAction("ThongTinHoaDon", "Admin");
            }
            db.Remove(db.HoaDons.Find(MaHoaDon));
            db.SaveChanges();
            TempData["Message"] = "Hóa đơn đã được xóa!";
            return RedirectToAction("ThongTinHoaDon", "Admin");
        }

        [Route("thongtinhoadon")]
        public IActionResult ThongTinHoaDon()
        {
            var lsHoaDon = db.HoaDons.ToList();
            return View(lsHoaDon);
        }
    }
}
