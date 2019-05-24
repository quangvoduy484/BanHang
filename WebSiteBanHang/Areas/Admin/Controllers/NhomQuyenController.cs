﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class NhomQuyenController : Controller
    {
        NhomQuyenService PQService = new NhomQuyenService();
        // GET: Admin/PhanQuyenNV

        // GET: Admin/NhanVien/i
        public ActionResult Index(int id)
        {
            ViewBag.ID_Group = id;
            return View();
        }
        [HttpPost]
        public ActionResult GetAll(int id, DataTableAjaxPostModel dataModel)
        {
            var Lists = PQService.GetAll(dataModel, id);

            return Json(Lists);
        }
        // POST: Admin/PhanQuyenNV/Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            //var khuyenMai = PQService.FindById(id);
            //if (khuyenMai == null)
            //{
            //    return HttpNotFound();
            //}
            //SelectList sanPhams = new SelectList(khuyenMai.SanPhamDropdowns, "id", "text");
            //ViewBag.SanPhamList = sanPhams;
            
            ViewBag.idgroup = id;
            return View();
        }

        // GET: Admin/PhanQuyenNV/Create
        [HttpPost]
        public ActionResult Create(int id, PhanQuyenNVViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    PQService.Add(id,collection);

                    //return RedirectToAction("Index");
                    
                    return RedirectToAction("Index", "PhanNhomNV");

                }
                return View(collection);
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return View();
            }
        }

        


        // GET: Admin/NhomQuyen/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/NhomQuyen/Edit/5
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

        // GET: Admin/NhomQuyen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/NhomQuyen/Delete/5
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
