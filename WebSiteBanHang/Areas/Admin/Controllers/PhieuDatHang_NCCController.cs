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
    public class PhieuDatHang_NCCController : Controller
    {
        // GET: Admin/PhieuDatHang_NCC
        PhieuDatHang_NCCService PDHService = new PhieuDatHang_NCCService();
        NCCService NCCService = new NCCService();
        SanPhamService SPService = new SanPhamService();
        BanHangContext db = new BanHangContext();
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var pdh = PDHService.GetAll(dataModel);

            return Json(pdh);
        }

        // GET: Admin/PhieuDatHang_NCC/Details/5
        public ActionResult Details(int id)
        {
            // Lấy tên SP
            var SP = SPService.GetTenSPForPhieuDat(id);
            SelectList listSP = new SelectList(SP, "MaSanPham", "TenSanPham");
            ViewBag.listSP = listSP;
            
            var detail = PDHService.GetChiTietDatHangs(id);
            return View(detail);
        }

        // GET: Admin/PhieuDatHang_NCC/Create
        public ActionResult Create()
        { 
            // Láy tên NCC
            var NCC = NCCService.GetAllNcc();
            SelectList listNCC = new SelectList(NCC, "MaNCC", "TenNCC");
            ViewBag.listNCC = listNCC;
            // Lấy tên SP
            var SP = SPService.GetTenSP();
            SelectList listSP = new SelectList(SP, "MaSanPham", "TenSanPham");
            ViewBag.listSP = listSP;
            return View();
        }

        // POST: Admin/PhieuDatHang_NCC/Create
        [HttpPost]
        public ActionResult Create(PhieuDatHang_NCCViewModel model)
        {

            var result = new ReponseMessage();
            try
            {
                if(!ModelState.IsValid || model == null || model.ChiTietPhieuDats?.Count == 0)
                {
                    result.Message = "Dữ liệu truyền vào không chính xác";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    return Json(result);

                }
                // TODO: Add delete logic here
                var kq = PDHService.AddPhieuDatHangNCC(model);
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
                result.Message = "Có lỗi trong quá trình xử lý";
                result.StatusCode = HttpStatusCode.ExpectationFailed;
                return Json(result);
            }
            
        }

    // GET: Admin/PhieuDatHang_NCC/Edit/5
    public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/PhieuDatHang_NCC/Edit/5
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

        // GET: Admin/PhieuDatHang_NCC/Delete/5
        public ActionResult DeleteDetail(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = PDHService.DeleteDetail(id);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy dữ liệu";
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

        // POST: Admin/PhieuDatHang_NCC/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = PDHService.DeleteDatHangNCC(id);
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

        //POST: Cập nhật chi tiết đơn đặt hàng
        [HttpPost]
        public ActionResult UpdateCtDatHang(int id, CT_PhieuDatHangNCCViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.GiaNhap > 0 && model.SL>0 )
                {
                    // TODO: Add update logic here
                    model.MaCTPhieuDat = id;
                    var result = PDHService.UpdateCTDH(model);
                    if (result == false)
                    {
                        return HttpNotFound();
                    }
                    return Json("success");
                }
                return View(model);
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
                PDHService.UpdateTrangThaiDonHang(id);
                var datHangKH = this.PDHService.GetChiTietDatHangs(id);
                if (datHangKH == null)
                {
                    return HttpNotFound();
                }
                var abyte = this.PDHService.PrepareDatHang(datHangKH);
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
