using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;


namespace WebSiteBanHang.Controllers
{
    public class ProductDetailsController : Controller
    {
        BanHangContext db = new BanHangContext();
        // GET: ProductDetails
        public ActionResult GetDetailProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sp = db.SANPHAMs.Where(x => x.Id_SanPham == 32).SingleOrDefault();

            var product = db.SANPHAMs
                    .Include(t => t.HINHs)
                    .Include(t => t.GIASANPHAMs)
                    .SingleOrDefault(t => t.Id_SanPham == id && t.TrangThai != false);


            if (product == null)
            {
                return HttpNotFound();
            }
            var result = new ProductDetail()
            {
                Id_SanPham = product.Id_SanPham,
                TenSanPham = product.TenSanPham,
                GiaBan = product.GIASANPHAMs.Where(g => g.TrangThai != false).OrderByDescending(g => g.NgayLap)
                                            .Select(g => g.GiaBan).FirstOrDefault(),
                KichThuoc = product.KichThuoc,
                VatLieu = product.VatLieu,
                MauSac = product.MauSac,
                XuatXu = product.XuatXu,
                Mota = product.Mota,
                HinhAnh = product.HinhAnh ?? string.Empty
            };
            if (!string.IsNullOrWhiteSpace(product.HinhAnh))
            {
                result.Hinhs.Add(product.HinhAnh);
            }
            if (product.HINHs.Count > 0)
            {
                result.Hinhs.AddRange(product.HINHs
                    .Select(t => t.Link).
                    Where(t => !string.IsNullOrEmpty(t)));
            }
            return View(result);
        }
    }
}