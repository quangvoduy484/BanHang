using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        NhanVienService nv = new NhanVienService();

        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            var model = nv.ListAll();
            return View(model);
        }

        // GET: Admin/NhanVien/Create
        public ActionResult Create()
        {
            var loainv = nv.GetAllGroup();
            SelectList loainvSelect = new SelectList(loainv, "ID", "GROUPNAME");
            ViewBag.loainvSelect = loainvSelect;
            return View();
        }
        // GET: Admin/NhanVien/Details
        
        // POST: Admin/NhanVien/Create
        [HttpPost]
        public ActionResult Create(TBL_LOGIN collection)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    nv.Add(collection);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("","Thêm thành công");
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/NhanVien/Edit/5
        public ActionResult Edit(string id)
        {
            var nvExist = nv.FindById(id);
            if (nvExist == null)
            {
                return HttpNotFound();
            }
            return View(nvExist);

        }

        // POST: Admin/NhanVien/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, TBL_LOGIN collection)
        {
            try
            {
                // TODO: Add update logic here

                var result = nv.Update(collection);
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

        // GET: Admin/NhanVien/Delete/5
        public ActionResult Delete(string id)
        {
            var nvExist = nv.FindById(id);
            if (nvExist == null)
            {
                return HttpNotFound();
            }
            return View(nvExist);
        }

        // POST: Admin/NhanVien/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, TBL_LOGIN collection)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here


                // TODO: Add delete logic here
                var kq = nv.Delete(collection);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy nhân viên";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return Json(result);
                }
                result.StatusCode = HttpStatusCode.OK;
                return Json(result);
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
