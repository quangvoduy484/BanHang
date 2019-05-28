using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        BanHangContext db = new BanHangContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel user, string ReturnUrl)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var login = db.TBL_LOGINs.Where(x => x.USERNAME == user.UserName && x.PASSWORD == user.PassWord).First();
                    if (login != null)
                    {
                        if (login.ACTIVATE == true)
                        {
                            FormsAuthentication.SetAuthCookie(user.UserName, false);

                            Session["Menu"] = UserRole.GetMenuByUser(user.UserName);
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Dashboard");

                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tài khoản ngừng hoạt động");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}
