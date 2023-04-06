using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBNHOM10.Models;
using X.PagedList;

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
        public IActionResult QuanLyYeucauThue(int?page)
        {
            int pagesize = 10;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var lstSinhvien = db.SinhViens
                .Include(x=>x.MaPhongNavigation)
                .Include(x=>x.MaLopNavigation)
                .Include(x=>x.MaQueNavigation)
                .ToList();
            PagedList<SinhVien> lst = new PagedList<SinhVien>(lstSinhvien, pagenumber, pagesize);
            return View(lst);
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
        [Route("yeucaudaduyet")]
        [HttpGet]
        public IActionResult YeuCauDaDuyet(int?page)
        {
            int pagesize = 10;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var sv = db.SinhViens
                .Where(x=>x.TrangThai==1)
                .Include(x => x.MaLopNavigation)
                .Include(x => x.MaQueNavigation)
                .Include(x => x.MaPhongNavigation)
                .Include(x => x.MaHopDongNavigation);
            PagedList<SinhVien> lst = new PagedList<SinhVien>(sv, pagenumber, pagesize);
            return View(lst);
        }
        [Route("yeucauchuaduyet")]
        [HttpGet]
        public IActionResult YeuCauChuaDuyet(int?page)
        {
            int pagesize = 10;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var sv = db.SinhViens
                .Where(x=>x.TrangThai==0)
                .Include(x => x.MaLopNavigation)
                .Include(x => x.MaQueNavigation)
                .Include(x => x.MaPhongNavigation)
                .Include(x => x.MaHopDongNavigation);
            PagedList<SinhVien> lst = new PagedList<SinhVien>(sv, pagenumber, pagesize);
            return View(lst);
        }
    }
}
