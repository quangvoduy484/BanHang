using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
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
            var detail = PDHService.GETCTDH_NCC(id);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<CT_PhieuDatHangNCCViewModel> ct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var i in ct)
                    {
                        PDHService.Add(i);
                    }

                    ViewBag.Message = "Thành công";
                    ModelState.Clear();
                    ct = new List<CT_PhieuDatHangNCCViewModel> { new CT_PhieuDatHangNCCViewModel {MaSP=0,SL=0 ,GiaNhap=0,ThanhTien=0, } };
                    
                }
                return View(ct);
            }
            catch
            {
                return View();
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/PhieuDatHang_NCC/Delete/5
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
