using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Drawing;
using WEBNHOM10.Models;
using WEBNHOM10.Models.Authentication;

namespace WEBNHOM10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHost;
        QlKtxContext db = new QlKtxContext();
        public HomeController(ILogger<HomeController> logger,IWebHostEnvironment webHost)
        {
            _logger = logger;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            var lstp = db.Phongs.ToList();
            return View(lstp);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("guiyeucauthue")]
        [HttpGet]
        public IActionResult GuiYeuCauThue()
        {
            if (HttpContext.Session.GetString("TaiKhoan")!=null)
            {
                TempData["Message"] = "Không thể gửi thêm";
                return View();
            }
            TempData["Message"] = "";
            ViewBag.MaPhong = new SelectList(db.Phongs.Where(x=>x.Songuoidango<x.Songuoitoida).ToList(), "MaPhong", "TenPhong");
            return View();
        }
        [Route("guiyeucauthue")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuiYeuCauThue(SVModel sinhVien)
        {
            TempData["Message"] = "";
            Console.WriteLine(ModelState.IsValid.ToString());
            if (ModelState.IsValid)
            {
                var lop = db.Lops.FirstOrDefault(l => l.TenLop == sinhVien.MaLopNavigation.TenLop);
                var que = db.Ques.FirstOrDefault(q => q.TenQue == sinhVien.MaQueNavigation.TenQue);
                if (lop == null)
                {
                    var malop = db.Lops.OrderByDescending(c => c.MaLop).FirstOrDefault();
                    int nextMaLop = malop.MaLop == null ? 1 : malop.MaLop + 1;
                    var newlop = new Lop() { MaLop = nextMaLop, TenLop = sinhVien.MaLopNavigation.TenLop };
                    db.Lops.Add(newlop);
                    sinhVien.MaLop = nextMaLop;
                    sinhVien.MaLopNavigation = newlop;
                    db.SaveChanges();
                }
                else
                {
                    sinhVien.MaLop = lop.MaLop;
                    sinhVien.MaLopNavigation = lop;
                }
                if (que == null)
                {
                    var maque = db.Ques.OrderByDescending(c => c.MaQue).FirstOrDefault();
                    int nextMaQue = maque.MaQue == null ? 1 : maque.MaQue + 1;
                    var newque = new Que() { MaQue = nextMaQue, TenQue = sinhVien.MaQueNavigation.TenQue };
                    db.Ques.Add(newque);
                    db.SaveChanges();
                    sinhVien.MaQue = nextMaQue;
                    sinhVien.MaQueNavigation = newque;
                }
                else
                {
                    sinhVien.MaQue = que.MaQue;
                    sinhVien.MaQueNavigation = que;
                }
                string uniqueFilename = null;
                if (sinhVien.Image != null)
                {
                    string uploadFoler = Path.Combine(_webHost.WebRootPath, "images/SinhVien/");
                    uniqueFilename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + sinhVien.Image.FileName;
                    string filePath = Path.Combine(uploadFoler, uniqueFilename);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        sinhVien.Image.CopyTo(fileStream);
                    }
                    sinhVien.Anh = uniqueFilename;
                }
                sinhVien.TrangThai = 0;
                Console.WriteLine("TenLop: " + sinhVien.MaQue.ToString());
                db.SinhViens.Add(new SinhVien
                {
                    MaSinhVien = sinhVien.MaSinhVien,
                    TenSinhVien = sinhVien.TenSinhVien,
                    NgaySinh = sinhVien.NgaySinh,
                    GioiTinh = sinhVien.GioiTinh,
                    SoDienThoat = sinhVien.SoDienThoat,
                    MaHopDong = sinhVien.MaHopDong,
                    MaPhong = sinhVien.MaPhong,
                    MaLop = sinhVien.MaLop,
                    MaQue = sinhVien.MaQue,
                    TaiKhoan = sinhVien.TaiKhoan,
                    MatKhau = sinhVien.MatKhau,
                    Anh = sinhVien.Anh,
                    SoCccd = sinhVien.SoCccd,
                    TrangThai = sinhVien.TrangThai
                });
                db.SaveChanges();
                TempData["Message"] = "Gửi thành công !!";
                return View();
            }
            ViewBag.MaPhong = new SelectList(db.Phongs.ToList(), "MaPhong", "TenPhong");
            return View();
        }
        [Route("xemhopdong")]
        [HttpGet]
        [Authentication]
        public IActionResult Xemhopdong() {
            string taikhoan = HttpContext.Session.GetString("TaiKhoan");
            if(taikhoan == null)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {
                Console.WriteLine(taikhoan);
                var sv = db.SinhViens
                .Include(x => x.MaLopNavigation)
                .Include(x => x.MaQueNavigation)
                .Include(x => x.MaPhongNavigation)
                .Include(x => x.MaHopDongNavigation)
                .FirstOrDefault(x => x.TaiKhoan == taikhoan);
                ViewBag.sv = sv;
                return View(sv);
            }
        }
        [Route("xemhoadon")]
        [HttpGet]
        [Authentication]
        public IActionResult Xemhoadon()
        {
            string taikhoan = HttpContext.Session.GetString("TaiKhoan");
            /*if (taikhoan == null)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {*/
                Console.WriteLine(taikhoan);
                var sv = db.SinhViens
                    .Include(x => x.MaPhongNavigation)
                    .FirstOrDefault(x => x.TaiKhoan == taikhoan);
                var hd = db.ChiTietHoaDons.Include(x => x.MaHoaDonNavigation).OrderBy(x=>x.MaHoaDonNavigation.HanThanhToan)
                    .Include(x => x.MaPhongNavigation).FirstOrDefault(x => x.MaPhongNavigation.MaPhong == sv.MaPhong);
                return View(hd);
            /*}*/
        }
        [Route("danhsachphong")]
        [HttpGet]
        public IActionResult DanhSachPhong()
        {
            var lstPhong = db.Phongs.ToList();
            return View(lstPhong);
        }
		[HttpGet]
        public IActionResult DanhSachPhongTheoTen(string searchString, int? priceFilter)
        {
            var phongs = db.Phongs.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                phongs = phongs.Where(p => p.TenPhong.ToLower().Contains(searchString.ToLower())).ToList();
            }
            if (priceFilter.HasValue)
            {
                switch (priceFilter)
                {
                    case 1:
                        phongs = phongs.Where(p => p.GiaPhong < 1500000).ToList();
                        break;
                    case 2:
                        phongs = phongs.Where(p => p.GiaPhong >= 1500000 && p.GiaPhong < 2000000).ToList();
                        break;
                    case 3:
                        phongs = phongs.Where(p => p.GiaPhong >= 2000000 && p.GiaPhong < 3000000).ToList();
                        break;
                    case 4:
                        phongs = phongs.Where(p => p.GiaPhong >= 3000000).ToList();
                        break;
                }
            }
            return View(phongs);
        }
		[Route("ChiTietPhong")]
        [HttpGet]
        public IActionResult ChiTietPhong(int id)
        {
            var phong = db.Phongs.Find(id);
            ViewBag.anhs = db.CtanhPhongs.Where(x => x.MaPhong == id).ToList();
            ViewBag.anhtb = db.ThietBis.Where(x=>x.MaPhong==id).ToList();
            return View(phong);
        }
        [HttpGet]
        public IActionResult Login()
        {
            string taikhoan = HttpContext.Session.GetString("TaiKhoan");
            if (taikhoan == null)
            {
                return RedirectToAction("Login", "Access");
            }
            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}