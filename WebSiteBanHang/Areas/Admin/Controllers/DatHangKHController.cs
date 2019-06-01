using System;
using System.Net;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Services;
using System.IO;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize]
    public class DatHangKHController : Controller
    {
        DatHangKHService datHang = new DatHangKHService();
        KhachHangService khachHang = new KhachHangService();

        // GET: Admin/DatHangKH
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var datHangs = datHang.GetAll(dataModel);

            return Json(datHangs);
        }


        // GET: Admin/DatHangKH/Details/5
        public ActionResult Details(int id)
        {
            var detail = datHang.GetChiTietDatHangs(id);
            return View(detail);
        }
        // GET: Admin/DatHangKH/Create
        public ActionResult Create()
        {
            var tenKH = khachHang.ListAll();
            SelectList ListTenKH = new SelectList(tenKH, "Id_KhachHang", "TenKhachHang");
            ViewBag.tenKH = ListTenKH;
            return View();
        }

        // POST: Admin/DatHangKH/Create
        [HttpPost]
        public ActionResult Create(DatHangKHViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    datHang.Add(collection);
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/DatHangKH/Edit/5
        public ActionResult Edit(int id)
        {
            var dathang = datHang.findbyId(id);
            if (dathang == null)
            {
                return HttpNotFound();
            }
            return View(dathang);
        }

        // POST: Admin/DatHangKH/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DatHangKHViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                var result = datHang.UpdateDHKH(collection);
                if (!result)
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


        // POST: Admin/DatHangKH/DeleteChiTiet/5
        [HttpPost]
        public ActionResult DeleteDetail(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = datHang.DeleteDetail(id);
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

        //-------Xoá đơn đặt hàng------------//
        // POST: Admin/DatHangKH/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = datHang.DeleteDatHangKH(id);
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

        [HttpPost]
        public ActionResult UpdatectDatHang(int id, ChiTietDatHangViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model?.SoLuong > 0)
                {
                    // TODO: Add update logic here
                    model.MaChiTiet = id;
                    var result = datHang.UpdateCTDH(model);
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

        public ActionResult PrintPdf(int id)
        {
            try
            {
                datHang.UpdateTrangThaiDonHang(id);
                var datHangKH = this.datHang.GetChiTietDatHangs(id);
                if (datHangKH == null)
                {
                    return HttpNotFound();
                }
                var abyte = this.datHang.PrepareDatHang(datHangKH);
                return File(abyte, "application/pdf");
            }
            catch (Exception ex)
            {

                string error = ex.Message;
                return Json(error, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult UpdateTrangThaiDaGiao(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = datHang.UpdateTrangThaiDaGiao(id);
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
        public ActionResult UpdateTrangThaiKhongNhanSP(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = datHang.UpdateTrangThaiKhongNhanHang(id);
                if (kq == false)
                {
                    result.Message = "Đơn hàng này không thể trả lại";
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
    }
}
