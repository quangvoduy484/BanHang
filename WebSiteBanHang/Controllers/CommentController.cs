using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        BanHangContext db = new BanHangContext();
        [HttpGet]
        public ActionResult addCommentPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult addCommentPartial(int id, string textcomment, int sumstar)
        {
            var aNewComment = new DANHGIA();
            var user = SessionUser.GetSession();
            if (user != null)
            {
                aNewComment = new DANHGIA()
                {
                    Id_KhachHang = user.Id,   
                    SoSao = sumstar,
                    Comment = textcomment,
                    NgayDanhGia = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")),
                    Id_SanPham = id
                };
                db.DANHGIAs.Add(aNewComment);
                db.SaveChanges();
                return Json(new { result = true,comment = aNewComment,name =user.Name }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult loadComment(int id)
        {
            var comments = db.DANHGIAs.Join(db.KHACHHANGs, dg => dg.Id_KhachHang, kh => kh.Id_KhachHang, (dg, kh) => new { DANHGIA = dg, KHACHHANG = kh })
                            .Where(x => x.DANHGIA.Id_SanPham == id && x.DANHGIA.SoSao >= 3 )
                            .Select(x => new
                            {
                                id= x.DANHGIA.Id_DanhGia,
                                user = x.KHACHHANG.TenKhachHang,
                                content = x.DANHGIA.Comment,
                                start = x.DANHGIA.SoSao,
                                date = x.DANHGIA.NgayDanhGia

                            }).AsQueryable().ToList();

            return Json(new { comments = comments }, JsonRequestBehavior.AllowGet);


        }
    }
}