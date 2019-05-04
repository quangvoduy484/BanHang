using System;
using System.Net;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize]
    public class SanPhamController : Controller
    {
        SanPhamService sanPhamService = new SanPhamService();
        LoaiSPService loaiSPService = new LoaiSPService();
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            //var model = sanPhamService.ListAll();
            return View();
        }
        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {
            var sanPhams = sanPhamService.GetAll(dataModel);

            return Json(sanPhams);
        }
        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            var sanpham = sanPhamService.Details(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            var loaiSP = loaiSPService.ListAll();
            SelectList listloaiSP = new SelectList(loaiSP, "Id_LoaiSanPham", "TenLoai");
            ViewBag.LoaiSP = listloaiSP;
            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        public ActionResult Create(SanPhamViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
               if(ModelState.IsValid)
                {
                    sanPhamService.Add(collection);
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            var sanpham = sanPhamService.Details(id);
            var loaiSP = loaiSPService.ListAll();
            SelectList listloaiSP = new SelectList(loaiSP, "Id_LoaiSanPham", "TenLoai");
            ViewBag.LoaiSP = listloaiSP;
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        public ActionResult Edit(SanPhamViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                var result = sanPhamService.Update(collection);
                if (result == false)
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
        // POST: Admin/SanPham/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var kq = sanPhamService.Delete(id);
                if (kq == false)
                {
                    result.Message = "Không tìm thấy loại phòng";
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
