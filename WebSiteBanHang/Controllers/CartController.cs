using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Controllers
{
    public class CartController : Controller
    {
        BanHangContext db = new BanHangContext();


        public List<ProductCart> GetProductCarts()
        {

            List<ProductCart> productCarts = Session["productCarts"] as List<ProductCart>;
            if (productCarts == null)
            {
                productCarts = new List<ProductCart>();
                Session["productCarts"] = productCarts;
            }
            return productCarts;
        }



        public JsonResult createProduct(int id)
        {
            // lúc này thêm bao nhiêu nữa thì nó cũng thêm vào cái list lưu seesion nên sẽ lấy được 
            var product = db.SANPHAMs.Where(x => x.Id_SanPham == id).SingleOrDefault();
            List<ProductCart> productCarts = GetProductCarts();

            // tao moi mot san pham
            ProductCart productCart = new ProductCart();
            if (product != null)
            {
                var gia = db.GIASANPHAMs.Where(g => g.TrangThai != false && g.Id_SanPham == product.Id_SanPham).OrderByDescending(g => g.NgayLap).Select(g => g.GiaBan).FirstOrDefault();
                var phantram = db.KHUYENMAIs.Where(k => k.Id_KhuyenMai == product.Id_KhuyenMai).OrderByDescending(k => k.NgayKetThuc).Select(g => g.GiaTriKhuyenMai).FirstOrDefault();
                productCart = new ProductCart()
                {
                    Id_SanPham = product.Id_SanPham,
                    TenSanPham = product.TenSanPham,
                    HinhAnh = product.HinhAnh,
                    loaisp = product.LOAISANPHAM.TenLoai,
                    giagoc = gia,
                    KichThuoc = product.KichThuoc,
                    MauSac = product.MauSac,
                    soluong = 1,
                    phantramgiamgia = phantram,
                    giagiam = gia * ((100 - phantram) / 100)


                };

            }

            var productIncart = productCarts.Where(p => p.Id_SanPham == id).SingleOrDefault();
            if (productIncart == null)
            {
                productCarts.Add(productCart);
            }
            else
            {
                productIncart.soluong += 1;

            }
            var totatQuantity = totalQuantity();

            return Json(new { productCart = productCart, totalQuantity = totatQuantity }, JsonRequestBehavior.AllowGet);

        }

        public int totalQuantity()
        {
            List<ProductCart> productCarts = GetProductCarts();
            if (productCarts != null)
                return productCarts.Sum(x => x.soluong);
            return 0;
        }

        public double totalMoney()
        {
            List<ProductCart> productCarts = GetProductCarts();
            if (productCarts != null)
                return productCarts.Sum(x => x.giagiam * x.soluong);
            return 0;

        }

        public JsonResult stateProductCars()
        {
            var quantity = totalQuantity();
            var money = totalMoney();
            if (quantity != 0 && money != 0)
            {
                return Json(new { quantity = quantity, money = money }, JsonRequestBehavior.AllowGet);

            }
            return null;

        }

        // show current ProductCarts
        public JsonResult currentProductCarts(string id, int? totalCheck)
        {
            List<ProductCart> productCarts = GetProductCarts();
            var quantity = totalQuantity();
            var strimId = id.Trim(',');
            var listId = strimId.Split(',').Select(int.Parse).ToList();
            double currentTotal = 0;

            foreach (var item in listId)
            {
                foreach (var product in productCarts)
                {
                    if (item == product.Id_SanPham)
                    {
                        currentTotal += product.giagiam * double.Parse(product.soluong.ToString());

                    }
                }

            }

            var giaohang = 50;
            var sumTotal = currentTotal +50;
            return Json(new { currentTotal = currentTotal, giaohang = giaohang, sumTotal = sumTotal, totatQuantity = totalCheck }, JsonRequestBehavior.AllowGet);
        }

        // render ProductCarts
        public ActionResult renderProductCarts()
        {
            List<ProductCart> productCarts = GetProductCarts();
            return View(productCarts);
        }

        // showpopup when add ProductCarts
        public ActionResult PartialProduct()
        {
            return PartialView();
        }


        // show total quantity ProductCarts
        public ActionResult PartialIcon()
        {

            return PartialView();
        }

        //show partial delete one item 
        public ActionResult PartialDeleteItem()
        {
            return PartialView();
        }

        // show parital  delte all item 
        public ActionResult PartialDeleteAll()
        {
            return PartialView();
        }


        // upordown item ProductCarts

        public ActionResult upordownProducCarts(int productID, char action)
        {
            List<ProductCart> productCarts = GetProductCarts();
            var product = productCarts.Where(x => x.Id_SanPham == productID).SingleOrDefault();
            if (action == '-')
            {
                product.soluong -= 1;
            }
            else
            {
                product.soluong += 1;
            }

            return RedirectToAction("renderProductCarts");

        }

        public ActionResult upQuantityByTextBox(int productID, int total)
        {
            List<ProductCart> productCarts = GetProductCarts();
            var product = productCarts.Where(x => x.Id_SanPham == productID).SingleOrDefault();
            product.soluong = total;

            return RedirectToAction("renderProductCarts");

        }


        public ActionResult deleteProducCarts(int? id, string listId)
        {
            List<ProductCart> productCarts = GetProductCarts();
            if (!string.IsNullOrEmpty(listId))
            {
                var list = listId.Trim(',');
                var listdelete = list.Split(',').Select(int.Parse);
                foreach (var item in listdelete)
                {
                    productCarts.RemoveAll(x => x.Id_SanPham == item);
                }

            }
            else
            {
                productCarts.RemoveAll(x => x.Id_SanPham == id);
            }
            return RedirectToAction("renderProductCarts");
        }

     






    }
}