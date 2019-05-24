using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteBanHang.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        [HttpGet]
        public ActionResult addCommentPartial(string hinh)
        {
            ViewBag.Hinh = hinh;
            return PartialView();
        }
        [HttpPost]
        public ActionResult addCommentPartial(int id,string comment,int sosao )
        {
            return RedirectToAction("d");
        }
    }
}