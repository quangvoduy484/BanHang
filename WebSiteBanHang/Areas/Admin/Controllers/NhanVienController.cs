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
        // GET: Admin/NhanVien/i
        public ActionResult Index(int id)
        {
            ViewBag.ID_Group = id;
            return View();
        }
        [HttpPost]
        public ActionResult GetAll(int id,DataTableAjaxPostModel dataModel)
        {
            var NVs = nv.GetAll(dataModel,id);

            return Json(NVs);
        }

        public ActionResult GetNhanViens(string search,int id)
        {
            var nhanViens = nv.GetAllDropDownList(search,id);
            return Json(nhanViens, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/NhanVien/Details/5
        

        // GET: Admin/NhanVien/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanVien/Create
        [HttpPost]
        public ActionResult Create(PhanNhomNVViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    nv.Add(collection);
                    return RedirectToAction("Index","PhanNhomNV");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }



        // GET: Admin/NhanVien/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/NhanVien/Edit/5
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

       

        // GET: Admin/NhanVien/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Admin/NhanVien/Delete/5
        [HttpPost]
        public ActionResult Delete(int groupid,string username)
        {
            var result = new ReponseMessage();
            try
            {
                var kq = nv.Delete(groupid, username);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy nhân viên";
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
