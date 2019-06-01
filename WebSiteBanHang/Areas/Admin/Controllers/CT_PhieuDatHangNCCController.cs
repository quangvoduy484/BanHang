using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize]
    public class CT_PhieuDatHangNCCController : Controller
    {
        // GET: Admin/CTPhieuDatHang_NCC
        CT_PhieuDatHangNCCService ct = new CT_PhieuDatHangNCCService();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CTPhieuDatHang_NCC/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CTPhieuDatHang_NCC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CTPhieuDatHang_NCC/Create
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

        // GET: Admin/CTPhieuDatHang_NCC/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View();
        }

        // POST: Admin/CTPhieuDatHang_NCC/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CT_PhieuDatHangNCCViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                var result = ct.Update(collection);
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

        // GET: Admin/CTPhieuDatHang_NCC/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CTPhieuDatHang_NCC/Delete/5
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
