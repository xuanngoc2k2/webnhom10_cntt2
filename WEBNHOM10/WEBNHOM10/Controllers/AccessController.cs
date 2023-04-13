using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBNHOM10.Models;

namespace WEBNHOM10.Controllers
{
    public class AccessController : Controller
    {
        QlKtxContext db = new QlKtxContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TaiKhoan")==null)
            {
                return View();
            }
            else
            {
                if(HttpContext.Session.GetInt32("Admin")==1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public IActionResult Login(SinhVien sinhVien)
        {
            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                var u = db.SinhViens.FirstOrDefault(x=>x.TaiKhoan==sinhVien.TaiKhoan && x.MatKhau==sinhVien.MatKhau);
                if (u!=null)
                {
                    if (u.TrangThai == -1)
                    {
                        HttpContext.Session.SetInt32("Admin", 1);
                        HttpContext.Session.SetString("TaiKhoan", u.TaiKhoan.ToString());
                        return RedirectToAction("Index", "Admin");
                    }
                    HttpContext.Session.SetInt32("Admin", 0);
                    HttpContext.Session.SetString("TaiKhoan", u.TaiKhoan.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
