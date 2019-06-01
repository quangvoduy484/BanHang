using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Component;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;


namespace WebSiteBanHang.Controllers
{

    public class UserController : Controller
    {
        BanHangContext db = new BanHangContext();
        // GET: Registraton
        [HttpGet]
        public ActionResult Registor()
        {
            var Thangs = new SelectList(
            new List<SelectListItem>
            {
                    new SelectListItem { Text = "Tháng một", Value = "1"},
                    new SelectListItem { Text = "Tháng hai", Value = "2"},
                    new SelectListItem { Text = "Tháng ba", Value = "3"},
                    new SelectListItem { Text = "Tháng bốn", Value = "4"},
                    new SelectListItem { Text = "Tháng năm", Value = "5"},
                    new SelectListItem { Text = "Tháng sáu", Value = "6"},
                    new SelectListItem { Text = "Tháng bảy", Value = "7"},
                    new SelectListItem { Text = "Tháng tám", Value = "8"},
                    new SelectListItem { Text = "Tháng chín", Value = "9"},
                    new SelectListItem { Text = "Tháng mười", Value = "10"},
                    new SelectListItem { Text = "Tháng mười một", Value = "11"},
                    new SelectListItem { Text = "Tháng mười hai", Value = "12"}

            }, "Value", "Text");

            ViewBag.Ngay = DayMonthYear.GetNgays();
            ViewBag.Thang = Thangs;
            ViewBag.Nam = DayMonthYear.GetNams();

            var Customer = new Customer();
            return View(Customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registor(Customer customer)
        {

            KHACHHANG KhachHang = new KHACHHANG();
            var CheckEmail = db.KHACHHANGs.Where(x => x.Email == customer.Email).ToList();
            var CheckPhone = db.KHACHHANGs.Where(x => x.SoDienThoai == customer.SoDienThoai).ToList();

            if (ModelState.IsValid)
            {
                if (CheckEmail.Count > 0)
                {
                    ModelState.AddModelError("Email", "Email của bạn đã tồn tại");
                }

                if (CheckPhone.Count > 0)
                {
                    ModelState.AddModelError("SoDienThoai", "Số điện thoại  của bạn tồn tại");
                }

                if (CheckEmail.Count == 0 && CheckPhone.Count == 0)
                {
                    var thoigian = customer.Ngay + "/" + customer.Thang + "/" + customer.Nam;
                    KhachHang = new KHACHHANG
                    {
                        TenKhachHang = customer.TenKhachHang,
                        DiaChi = customer.DiaChi,
                        SoDienThoai = customer.SoDienThoai,
                        Email = customer.Email,
                        PassWord = Helper.GenHash.GenSHA1(customer.PassWord),
                        Id_LoaiKhachHang = 2

                    };

                    if (thoigian != " " && !thoigian.Contains("//"))
                    {
                        KhachHang.NgaySinh = DateTime.Parse(thoigian);

                    }
                    // tổng chi +1,
                    // dfsdfsd

                    var aNewKhachHang = db.KHACHHANGs.Add(KhachHang);
                    KhachHang.Id_KhachHang = aNewKhachHang.Id_KhachHang;

                    DIACHI DiaChi = new DIACHI()
                    {
                        Id_KhachHang = aNewKhachHang.Id_KhachHang,
                        SoDienThoai = aNewKhachHang.SoDienThoai,
                        TenKhachHang = aNewKhachHang.TenKhachHang,
                        DiaChi = aNewKhachHang.DiaChi,
                        TrangThai = true
                    };

                    db.DIACHIs.Add(DiaChi);
                    db.SaveChanges();


                    return RedirectToAction("Index", "Homepage");

                }
            }

            var Thangs = new SelectList(
            new List<SelectListItem>
       {
                    new SelectListItem { Text = "Tháng một", Value = "1"},
                    new SelectListItem { Text = "Tháng hai", Value = "2"},
                    new SelectListItem { Text = "Tháng ba", Value =  "3"},
                    new SelectListItem { Text = "Tháng bốn", Value = "4"},
                    new SelectListItem { Text = "Tháng năm", Value = "5"},
                    new SelectListItem { Text = "Tháng sáu", Value = "6"},
                    new SelectListItem { Text = "Tháng bảy", Value = "7"},
                    new SelectListItem { Text = "Tháng tám", Value = "8"},
                    new SelectListItem { Text = "Tháng chín", Value="9"},
                    new SelectListItem { Text = "Tháng mười", Value = "10"},
                    new SelectListItem { Text = "Tháng một", Value = "11"},
                    new SelectListItem { Text = "Tháng hai", Value = "12"}

       }, "Value", "Text");

            ViewBag.Ngay = DayMonthYear.GetNgays();
            ViewBag.Thang = Thangs;
            ViewBag.Nam = DayMonthYear.GetNams();

            return View(customer);

        }


        public ActionResult PartialViewLogin()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult UserLogin(string name, string pass)
        {
            string messageError = string.Empty;
            List<string> listError = new List<string>();
            if (ModelState.IsValid)
            {
                var genpass = Helper.GenHash.GenSHA1(pass);
                var customer = db.KHACHHANGs.Where(x => x.PassWord == genpass && (x.Email == name || x.SoDienThoai == name)).SingleOrDefault();
                if (customer == null)
                {

                    if (string.IsNullOrEmpty(name))
                    {
                        messageError += "Email hoặc Phone không được để trống,";
                    }
                    if (string.IsNullOrEmpty(pass))
                    {
                        messageError += "Mật khẩu không được để trống,";
                    }

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pass))
                    {
                        var checkEmailorPhone = db.KHACHHANGs.Where(x => x.Email != name || x.SoDienThoai != name).ToList();
                        if (checkEmailorPhone.Count > 0)
                        {
                            messageError += "Email hoặc số điện thoại không tồn tại,";
                        }

                        var checkPassWord = db.KHACHHANGs.Where(x => x.PassWord != genpass).ToList();
                        if (checkPassWord.Count > 0)
                        {
                            messageError += "Mật khẩu không đúng";
                        }
                    }

                    messageError = messageError.Trim(',');
                    listError = messageError.Split(',').ToList();
                }
                else
                {
                    SessionUser.SetSession(new User() { Id = customer.Id_KhachHang, Name = customer.TenKhachHang });
                    RedirectToAction("Index", "Homepage");
                }

            }

            return Json(new { listError = listError }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["productCarts"] = null;
            return RedirectToAction("Index", "Homepage");

        }

    }
}