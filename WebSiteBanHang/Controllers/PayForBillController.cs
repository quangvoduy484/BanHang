using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Controllers
{

    public class PayForBillController : Controller
    {

        BanHangContext db = new BanHangContext();
        SuccessBillService TTDH = new SuccessBillService();
        DatHangKHService datHang = new DatHangKHService();

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
            // tổng tiền trong giổ hàng
            var sumTotal = products.Sum(x => x.soluong * x.giagiam);

            var customerlogin = SessionUser.GetSession();
            var customer = db.KHACHHANGs.Where(x => x.Id_KhachHang == customerlogin.Id).FirstOrDefault();
            var address = db.DIACHIs.Where(x => x.Id_KhachHang == customer.Id_KhachHang && x.TrangThai == true).FirstOrDefault();


            DATHANG orderForm = new DATHANG
            {
                NgayDat = DateTime.Now,
                TrangThai = 1,
                DiaChiGiao = address.DiaChi,
                SoDienThoai = address.SoDienThoai,
                Id_KhachHang = customer.Id_KhachHang,
                TenNguoiNhan = address.TenKhachHang,
                // 50 tiền ship 
                TongTien = sumTotal + 50
            };
            var aNewOder = db.DATHANGs.Add(orderForm);
            orderForm.Id_DatHang = aNewOder.Id_DatHang;
           // nếu nó có check thì lưu thêm cột giảm giá 
            if (Accumulated == true)
            {
                orderForm.TongTienSauGiamGia = orderForm.TongTien - (double.Parse(customer.DiemTichLuy.ToString())*1000);
                orderForm.DiemTichLuy = customer.DiemTichLuy;
                customer.DiemTichLuy = 0;
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

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
            return RedirectToAction("succesBill", "PayForBill", new { idDatHang = orderForm.Id_DatHang });

        }



        public ActionResult succesBill(int idDatHang)
        {
            var userlogin = SessionUser.GetSession();
            if (userlogin == null || Session["productCarts"] == null || GetProductCarts()?.Count <= 0)
                return  RedirectToAction("Index","HomePage");

            var DH = TTDH.GetThongTinDatHang(userlogin.Id, idDatHang);

            if (DH != null)
            {
                try
                {
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Client/Template/neworder.html"));
                    //Thông tin Đơn đặt hàng và Thông tin khách hàng
                    content = content.Replace("{{MaPhieuDat}}", DH.MaDatHang.ToString());
                    content = content.Replace("{{CustomerName}}", DH.TenKhachHangDat);
                    content = content.Replace("{{Phone}}", DH.SoDienThoaiDat);
                    content = content.Replace("{{NgayDat}}", DH.NgayDatHang.ToString());
                    content = content.Replace("{{NguoiNhan}}",DH.TenKhachHang);
                    content = content.Replace("{{Total}}", DH.TongTien.ToString());
                    content = content.Replace("{{Phone_Nhan}}", DH.SoDienThoai);
                    content = content.Replace("{{Address}}", DH.DiaChiGiao);
                    content = content.Replace("{{Total}}", DH.TongTien.ToString());

                    // Thông tin CTDH
                    //foreach(var ctdh in DH.ChiTietDatHangs)
                    //{
                    //    content = content.Replace("{{TenSanPham}}", DH.ChiTietDatHangs[0].TenSanPham);
                    //    content = content.Replace("{{SoLuong}} ", DH.ChiTietDatHangs[0].SoLuong.ToString());
                    //    content = content.Replace("{{GiaBan}}", DH.ChiTietDatHangs[0].GiaBan.ToString());
                    //    content = content.Replace("{{ThanhTien}}", DH.ChiTietDatHangs[0].ThanhTien.ToString());

                    //}
                    var totalRow = string.Empty;
                    foreach (var ctdh in DH.ChiTietDatHangs)
{
                        var tenSanPham = "<td>" + ctdh.TenSanPham + "</td>";
                        var soluong = "<td>" + ctdh.SoLuong + "</td>";
                        var giaban = "<td>" + ctdh.GiaBan + "</td>";
                        var thanhTien = "<td>" + ctdh.ThanhTien + "</td>";
                        var row = "<tr>" + tenSanPham + soluong + giaban + thanhTien + "</tr>";
                        totalRow += row;
                    }
                   
                    content = content.Replace("{{row}}", totalRow);

                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    new MailHelper().SendMail(toEmail, "Đơn hàng mới từ NhàXinh", content);
                }
                catch (Exception ex)
                {

                    return View("Error");
                }
                Session["productCarts"] = null;
                return View(DH);


            }
            return View("Error");

        }


    }
}