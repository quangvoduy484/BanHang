using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class QuyenController : Controller
    {
        QuyenService quyenService = new QuyenService();
        // GET: Admin/Group
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Group/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult GetQuyens(string search, int id)
        {
            var quyens = quyenService.GetAllDropDownList(search, id);
            return Json(quyens, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Group/Create
        [HttpPost]
        public ActionResult Create(TBL_GROUP_ROLE collection)
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

        // GET: Admin/Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Group/Edit/5
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

        // GET: Admin/Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Group/Delete/5
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
