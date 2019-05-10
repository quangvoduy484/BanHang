using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class CT_PhieuDatHangNCCController : Controller
    {
        // GET: Admin/CT_PhieuDatHangNCC
        CT_PhieuDatHangNCCService CTPDHService = new CT_PhieuDatHangNCCService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var ctpdh = CTPDHService.GetAll(dataModel);

            return Json(ctpdh);
        }
        // GET: Admin/CT_PhieuDatHangNCC/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CT_PhieuDatHangNCC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CT_PhieuDatHangNCC/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CT_PhieuDatHangNCC/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CT_PhieuDatHangNCC/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CT_PhieuDatHangNCC/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CT_PhieuDatHangNCC/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
