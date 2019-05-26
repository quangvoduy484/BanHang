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
        NhanVienService NVService = new NhanVienService();
        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            var nv = NVService.ListAll();
            return View(nv);
        }

        // GET: Admin/NhanVien/Details/5
        

        // GET: Admin/NhanVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanVien/Create
        [HttpPost]
        public ActionResult Create(TBL_LOGIN collection)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    var kq = NVService.Add(collection);
                    if (kq == false)
                    {
                        result.Message = "Tài khoản đã tồn tại";
                        result.StatusCode = HttpStatusCode.BadRequest;
                        return Json(result);
                    }
                    result.StatusCode = HttpStatusCode.OK;
                    return Json(result);
                }
                else
                {
                    result.Message = "Vui lòng nhập đầy đủ thông tin";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = "Có lỗi trong quá trình xử lý";
                result.StatusCode = HttpStatusCode.ExpectationFailed;
                return Json(result);
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

        //Admin/NhanVien/Delete?userName=admin
        [HttpPost]
        public ActionResult Delete(string userName)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = NVService.Delete(userName);
                if (kq == false)
                {
                    result.Message = "Không thể xóa nhân viên này";
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

        // POST: Admin/NhanVien/Delete/5
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
