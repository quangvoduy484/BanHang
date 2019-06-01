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
    [Authorize]
    public class PhieuNhapHang_NCCController : Controller
    {
        
        PhieuNhapHang_NCCService PNHService = new PhieuNhapHang_NCCService();

        // GET: Admin/PhieuNhapHangNCC
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var pdh = PNHService.GetAll(dataModel);

            return Json(pdh);
        }
        // GET: Admin/PhieuNhapHangNCC/Details/5
        public ActionResult Details(int id)
        {
            var details = PNHService.GetChiTietNhapHangs(id);
            return View(details);
        }

        // GET: Admin/PhieuNhapHangNCC/Creates
        public ActionResult Create(int id)
        {
            // bị trùng phương thức 
            var detailPD = PNHService.GetChiTietDatHangs(id);
            return View(detailPD);
        }

        // POST: Admin/PhieuNhapHangNCC/Create
        [HttpPost]
        public ActionResult Create(int id, PhieuNhapHang_NCCViewModel model)
        {

            var result = new ReponseMessage();

            try
            {
                if (model == null || model.ChiTietPhieuNhaps?.Count == 0 || model.TongTien == 0)
                {
                    result.Message = "Dữ liệu truyền vào không chính xác";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    return Json(result);

                }
                // TODO: Add add logic here
               // PNHService.UpdateTrangThaiDonHang(id);
                var kq = PNHService.AddPhieuNhapHangNCC(id, model);

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
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = PNHService.DeleteNhapHangNCC(id);
                if (kq == false)
                {
                    result.Message = "Đơn hàng này không thể huỷ";
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

        // Xuất phiếu đặt PDF
        public ActionResult PrintPdf(int id)
        {
            try
            {

                var datHangNCC = this.PNHService.GetChiTietNhapHangs(id);
                if (datHangNCC == null)
                {
                    return HttpNotFound();
                }
                var abyte = this.PNHService.PrepareDatHang(datHangNCC);
                return File(abyte, "application/pdf");
            }
            catch (Exception ex)
            {

                string error = ex.Message;
                return Json(error, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
