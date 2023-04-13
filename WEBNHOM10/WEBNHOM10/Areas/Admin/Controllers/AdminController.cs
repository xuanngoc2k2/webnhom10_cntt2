using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBNHOM10.Areas.Admin.Models.Authentication;
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
        [Authentication]
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
        [Route("DanhSachSinhVien")]
        public ActionResult DanhSachSinhVien(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var listSinhVien = db.SinhViens.AsNoTracking().Where(x=>x.TrangThai==1).OrderBy(x => x.TenSinhVien)
            .Include(x => x.MaPhongNavigation)
            .Include(x => x.MaLopNavigation)
            .Include(x => x.MaQueNavigation);

            PagedList<SinhVien> lst = new PagedList<SinhVien>(listSinhVien, pageNumber, pageSize);
            return View(lst);
        }
        [Route("SuaSinhVien")]
        [HttpGet]
        public ActionResult SuaSinhVien(int id)
        {
            ViewBag.MaPhong = new SelectList(db.Phongs.ToList(), "MaPhong", "TenPhong");
            ViewBag.MaQue = new SelectList(db.Ques.ToList().Where(q => q.TenQue != null), "MaQue", "TenQue");
            ViewBag.MaLop = new SelectList(db.Lops.ToList().Where(q => q.TenLop != ""), "MaLop", "TenLop");
            var sinhVien = db.SinhViens.Find(id);
            return View(sinhVien);
        }
        // POST: SinhVienControllers/Edit/5
        [Route("SuaSinhVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSinhVien(SinhVien sinhVien)
        {
            try
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DanhSachSinhVien");
            }
            catch
            {
                return View(sinhVien);
            }
        }

        // GET: SinhVienControllers/Delete/5
        [Route("XoaSinhVien")]
        [HttpGet]
        public ActionResult XoaSinhVien(int id)
        {
            var sinhVien = db.SinhViens
                .Include(x => x.MaLopNavigation)
                .Include(x => x.MaQueNavigation)
                .Include(x => x.MaPhongNavigation)
                .Include(x => x.MaHopDongNavigation)
                .SingleOrDefault(x => x.MaSinhVien == id);

            return View(sinhVien);
        }

        // POST: SinhVienControllers/Delete/5
        [Route("XoaSinhVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaSinhVien(SinhVien sinhVien)
        {
            var sv = db.SinhViens.FirstOrDefault(x => x.MaSinhVien == sinhVien.MaSinhVien);
            db.SinhViens.Remove(sv);
            db.SaveChanges();
            TempData["Message"] = "Sinh viên đã được xóa";
            return RedirectToAction("DanhSachSinhVien");

        }
        [Route("danhsachphong")]
        [HttpGet]
        public IActionResult DanhSachPhong()
        {
            var lstPhong = db.Phongs.ToList();
            return View(lstPhong);
        }
        [Route("SuaPhong")]
        [HttpGet]
        public IActionResult SuaPhong(int id)
        {
            ViewBag.MaNha = new SelectList(db.Nhas.ToList(), "MaNha", "TenNha");
            var phong = db.Phongs.Find(id);
            return View(phong);
        }
        [Route("SuaPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaPhong(Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachPhong");
            }
            return View();
        }
        [Route("ChiTietPhong")]
        [HttpGet]
        public IActionResult ChiTietPhong(int id)
        {
            var phong = db.Phongs.Find(id);
            return View(phong);
        }
        [Route("ThemPhong")]
        [HttpGet]
        public IActionResult ThemPhong()
        {
            ViewBag.MaNha = new SelectList(db.Nhas.ToList(), "MaNha", "TenNha");
            return View();
        }
        [Route("ThemPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhong(Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Phongs.Add(phong);
                db.SaveChanges();
                return RedirectToAction("DanhSachPhong");
            }
            return View();
        }
        [Route("XoaPhong")]
        [HttpGet]
        public IActionResult XoaPhong(int maPhong)
        {
            TempData["Message1"] = "";
            var chiTietHoaDon = db.ChiTietHoaDons.Where(x => x.MaPhong == maPhong).ToList();
            if (chiTietHoaDon.Count() > 0)
            {
                TempData["Message1"] = "Không thể xóa phòng này";
                return RedirectToAction("DanhSachPhong", "Admin");
            }
            var thietBi = db.ThietBis.Where(x => x.MaPhong == maPhong).ToList();
            if (thietBi.Any())
                db.RemoveRange(thietBi);
            var anhPhong = db.CtanhPhongs.Where(x => x.MaPhong == maPhong).ToList();
            if (anhPhong.Any())
                db.RemoveRange(anhPhong);
            var sinhVien = db.SinhViens.Where(x => x.MaPhong == maPhong).ToList();
            if (sinhVien.Any())
                db.RemoveRange(sinhVien);
            db.Remove(db.Phongs.Find(maPhong));
            db.SaveChanges();
            TempData["Message1"] = "Xóa phòng thành công";
            return RedirectToAction("DanhSachPhong");
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
