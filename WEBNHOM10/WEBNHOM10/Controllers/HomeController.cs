using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Drawing;
using WEBNHOM10.Models;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}