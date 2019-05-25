using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class PhieuNhapHang_NCCController : Controller
    {
        PhieuNhapHang_NCCService PNHService = new PhieuNhapHang_NCCService();
        // GET: Admin/PhieuNhapHangNCC
        public ActionResult Index()
        {
            var model = PNHService.ListAll();
            return View(model);
        }
        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var ncc = PNHService.GetAll(dataModel);

            return Json(ncc);
        }
        // GET: Admin/PhieuNhapHangNCC/Details/5
        public ActionResult Details()
        {
            
            return View();
        }

        // GET: Admin/PhieuNhapHangNCC/Create
        public ActionResult Create(int id)
        {
            var detail = PNHService.GetChiTietDatHangs(id);
            return View(detail);
        }

        // POST: Admin/PhieuNhapHangNCC/Create
        [HttpPost]
        public ActionResult Create(int id,PhieuNhapHang_NCCViewModel model)
        {

            var result = new ReponseMessage();
           
            try
            {
                if ( model == null || model.ChiTietPhieuNhaps?.Count == 0)
                {
                    result.Message = "Dữ liệu truyền vào không chính xác";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    return Json(result);

                }
                // TODO: Add add logic here
                var kq = PNHService.AddPhieuNhapHangNCC(id,model);
                if (kq == false)
                {
                    result.Message = "Có lỗi trong qúa trình xử lý";
                    result.StatusCode = HttpStatusCode.BadRequest;
                }
                result.StatusCode = HttpStatusCode.OK;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Message = "Có lỗi trong quá trình";
                result.StatusCode = HttpStatusCode.ExpectationFailed;
                return Json(result);
            }
            
        }
        // GET: Admin/PhieuNhapHangNCC/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/PhieuNhapHangNCC/Edit/5
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

        // GET: Admin/PhieuNhapHangNCC/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/PhieuNhapHangNCC/Delete/5
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
