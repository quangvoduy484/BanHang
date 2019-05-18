using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class PhanNhomNVController : Controller
    {
        PhanNhomNVService PNService = new PhanNhomNVService();
        
        // GET: Admin/PhanNhomNV
        public ActionResult Index()
        {
            var pn = PNService.getNhom();
            return View(pn);
        }
        
        // GET: Admin/PhanNhomNV/Details/5
        public ActionResult Details(int id)
        {
            var details = PNService.GetNV(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        // GET: Admin/PhanNhomNV/Create
        public ActionResult Create(int id)
        {
            var nv = PNService.GetNV(id);
            SelectList listNV = new SelectList(nv, "USERNAME", "USERNAME");
            ViewBag.listNV = listNV;
            var nhom = PNService.getNhom();
            SelectList listNhom = new SelectList(nhom, "ID", "GROUPNAME");
            ViewBag.listNhom = listNhom;
            return View();
        }

        // POST: Admin/PhanNhomNV/Create
        [HttpPost]
        public ActionResult Create(TBL_GROUP_LOGIN collection,int id)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    PNService.Add(collection,id);
                    ViewBag.Message("Thêm thành công");
                   return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateGroup (TBL_GROUP collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    PNService.AddGroup(collection);
                    ViewBag.Message("Thêm thành công");
                    return RedirectToAction("Index");
                }
                return View(collection);
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
