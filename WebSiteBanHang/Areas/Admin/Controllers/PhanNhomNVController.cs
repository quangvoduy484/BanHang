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
    public class PhanNhomNVController : Controller
    {
        PhanNhomNVService NVService = new PhanNhomNVService();
        BanHangContext db = new BanHangContext();
        // GET: Admin/PhanNhomNV
        public ActionResult Index( )
        {
            return View();
        }

        public ActionResult GetAll(DataTableAjaxPostModel dataModel, string id)
        {
            var nv = NVService.GetAll(dataModel, id);

            return Json(nv);
        }

        // GET: Admin/PhanNhomNV/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/PhanNhomNV/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PhanNhomNV/Create
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

        // GET: Admin/PhanNhomNV/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/PhanNhomNV/Edit/5
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

        // GET: Admin/PhanNhomNV/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/PhanNhomNV/Delete/5
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
