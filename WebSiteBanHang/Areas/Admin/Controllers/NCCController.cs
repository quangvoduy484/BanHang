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
    [Authorize] //bat user phải login
    public class NCCController : Controller
    {
        NCCService NCCService = new NCCService();
        // GET: Admin/NCC
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var ncc = NCCService.GetAll(dataModel);

            return Json(ncc);
        }

        // GET: Admin/NCC/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/NCC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NCC/Create
        [HttpPost]
        public ActionResult Create(NHACUNGCAP collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    NCCService.Add(collection);
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/NCC/Edit/5
        public ActionResult Edit(int id)
        {
            var ncc = NCCService.FindById(id);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }

        // POST: Admin/NCC/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, NHACUNGCAP collection)
        {
            try
            {
                // TODO: Add update logic here

                var result = NCCService.Update(collection);
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

        // GET: Admin/NCC/Delete/5
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Admin/NCC/Delete/5
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = NCCService.Delete(id);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy nhà cung cấp nào";
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
