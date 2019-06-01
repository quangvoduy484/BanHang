using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Controllers
{
    public class PayForBillController : Controller
    {
        BanHangContext db = new BanHangContext();
        public List<ProductCart> GetProductCarts()
        {

            List<ProductCart> productCarts = Session["productCarts"] as List<ProductCart>;
            return productCarts;
        }



        // mặc định thì nó sẽ tìm kiếm những paramenter cơ bản trong uri
        // muốn gắn nó vào cái formbody thì phải thuộc tính body ở trước 

        [HttpGet]
        public ActionResult fillFormCustomer(string id, bool Accumulated)
        {
            List<ProductCart> productCarts = GetProductCarts();
            var strimId = id.Trim(',');
            var listHadCheck = strimId.Split(',').Select(int.Parse).ToList();

            //lấy ra sản phẩm trong giỏ hàng từ các sản phẩm đã chọn
            var products = productCarts.Where(x => listHadCheck.Contains(x.Id_SanPham));
            var sumTotal = products.Sum(x => x.soluong * x.giagiam);

            var customerlogin = SessionUser.GetSession();
            var customer = db.KHACHHANGs.Where(x => x.Id_KhachHang == customerlogin.Id).FirstOrDefault();
            var address = db.DIACHIs.Where(x => x.Id_KhachHang == customer.Id_KhachHang && x.TrangThai == true).FirstOrDefault();


            DATHANG orderForm = new DATHANG
            {
                NgayDat = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")),
                TrangThai = 1,
                DiaChiGiao = address.DiaChi,
                SoDienThoai = address.SoDienThoai,
                Id_KhachHang = customer.Id_KhachHang,
                TenNguoiNhan = address.TenKhachHang
            };
            var aNewOder = db.DATHANGs.Add(orderForm);
            orderForm.Id_DatHang = aNewOder.Id_DatHang;

            if (Accumulated == true)
            {
                orderForm.TongTien = sumTotal - double.Parse(customer.DiemTichLuy.ToString()) + 50;
                customer.DiemTichLuy = 0;
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            else
            {
                orderForm.TongTien = sumTotal + 50;
            }


            foreach (var product in products)
            {
                CHITIETDATHANG detailOrder = new CHITIETDATHANG();
                detailOrder.Id_DatHang = aNewOder.Id_DatHang;
                detailOrder.Id_SanPham = product.Id_SanPham;
                detailOrder.SoLuong = product.soluong;
                detailOrder.GiaBan = product.giagiam;
                detailOrder.ThanhTien = product.soluong * product.giagiam;
                detailOrder.TrangThai = true;
                db.CHITIETDATHANGs.Add(detailOrder);

            }

            db.SaveChanges();
            productCarts.RemoveAll(x => listHadCheck.Contains(x.Id_SanPham));
            return RedirectToAction("succesBill");

        }


        public ActionResult succesBill()
        {

            return View();
        }


    }
}