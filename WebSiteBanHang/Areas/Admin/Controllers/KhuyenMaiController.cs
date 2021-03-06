﻿using System.Net;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize]
    public class KhuyenMaiController : Controller
    {
        KhuyenMaiService KMService = new KhuyenMaiService();
        //GET: Admin/KhuyenMai
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var khuyenMai = KMService.GetAll(dataModel);

            return Json(khuyenMai);
        }


        // GET: Admin/KhuyenMai/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/KhuyenMai/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhuyenMai/Create
        [HttpPost]
        public ActionResult Create(KhuyenMaiViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    KMService.Add(collection);
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/KhuyenMai/Edit/5
        public ActionResult Edit(int id)
        {
            var khuyenMai = KMService.FindById(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            SelectList sanPhams = new SelectList(khuyenMai.SanPhamDropdowns, "id", "text");
            ViewBag.SanPhamList = sanPhams;
            return View(khuyenMai);
        }

        // POST: Admin/KhuyenMai/Edit/5
        [HttpPost]
        public ActionResult Edit( KhuyenMaiViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                var result = KMService.Update(model);
                if (!result)
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

        // GET: Admin/KhuyenMai/Delete/5
      

        // POST: Admin/KhuyenMai/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                var kq = KMService.Delete(id);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy loại phòng";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return Json(result);
                }
                result.StatusCode = HttpStatusCode.OK;
                return Json(result);
                // TODO: Add delete logic here

            }
            catch
            {
                result.Message = "Có lỗi trong quá trình xử lý";
                result.StatusCode = HttpStatusCode.ExpectationFailed;
                return Json(result);
            }
        }
    }
}
