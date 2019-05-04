using System;
using System.Net;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize]
    public class LoaiPhongController : Controller
    {
        LoaiPhongService loaiPhongService = new LoaiPhongService();
        // GET: Admin/LoaiPhong
        public ActionResult Index()
        {
            var model = loaiPhongService.ListAll();
           
            
            return View(model);
        }

        // GET: Admin/LoaiPhong/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/LoaiPhong/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/LoaiPhong/Create
        [HttpPost]
        public ActionResult Create(LOAIPHONG collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    loaiPhongService.Add(collection);
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/LoaiPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            var loaiPhong = loaiPhongService.FindById(id);
            if(loaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(loaiPhong);
        }

        // POST: Admin/LoaiPhong/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LOAIPHONG collection)
        {
            try
            {
                // TODO: Add update logic here
                var result = loaiPhongService.Update(collection);
                if(!result)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: Admin/LoaiPhong/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = loaiPhongService.Delete(id);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy loại phòng";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return Json(result);
                }
                result.StatusCode = HttpStatusCode.OK;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Message = "Có lỗi trong quá trình xử lý";
                result.StatusCode = HttpStatusCode.ExpectationFailed;
                return Json(result);
            }
        }
    }
}
