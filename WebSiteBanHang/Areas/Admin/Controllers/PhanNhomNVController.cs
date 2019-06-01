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
    [Authorize]
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
        [HttpPost]
        public ActionResult AddGroup(TBL_GROUP collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    PNService.AddGroup(collection);

                    return Json("Thêm thành công");
                       

                }
                return Json("Thêm thất bại");
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(er);
            }
        }

        [HttpPost]
        public ActionResult AddAccount(TBL_LOGIN collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    PNService.AddAccount(collection);
                    return Json("Thêm thành công");


                }
                return Json("Thêm thất bại");
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(er);
            }
        }
        // GET: Admin/PhanNhomNV/Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            //var nv = PNService.GetNV(id);
            //SelectList listNV = new SelectList(nv, "USERNAME", "USERNAME");
            //ViewBag.listNV = listNV;
            
            ViewBag.idgroup = id;
            return View();
        }

        // POST: Admin/PhanNhomNV/Create
        [HttpPost]
        public ActionResult Create(int id, PhanNhomNVViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    PNService.Add(id, collection);

                 

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



        // POST: Admin/PhanNhomNV/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                var kq = PNService.Delete(id);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy loại nhóm";
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
