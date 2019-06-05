using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteBanHang.Controllers;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;
using WebSiteBanHang;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebSiteBanHangUnitTest
{
    // Any Unit test is a combianation of following things
    // arrange -setup the test data 
    // act-perorment the actual test 
    // assert- verify the result
    [TestClass]
    public class ProductDetailsTest
    {
        //viết unitest cho  3 trường hợp :

        //setup dữ liệu cho các phương thức test
        public ViewResult setUp(int id)
        {
            var productDetailController = new ProductDetailsController();
            var result = productDetailController.GetDetailProduct(id) as ViewResult;
            return result;
        }

        // TH1 : Kiểm tra có tìm thấy đối product 
        // case pass : Tìm thấy sản phảm với id có trong database 
        // case failed : Ngược lại cho id sản phẩm không có thật 
        [TestMethod]
        public void testFindProduct()
        {
            //arrange -setup
            var result = setUp(32);

            //action - perform
            var productDetail = (ProductDetail)result.ViewData.Model;
            
            // assert-verify
            Assert.IsNotNull(productDetail);
        }
      

        // TH2 : kiếm tra thử thuộc tính trả về có giống như ý muốn hay không
        // case pass : số thuộc tính trả về từ respone đã  đủ với danh thuộc tính yêu cầu ban đầu 
        // case failed : ngược lại ( test bằng cách lấy thiếu một thuộc tính khi trả về sản phẩm ví dụ như giá bán )
        [TestMethod]
        public void checkPropertyObject()
        {
            //arrange -setup
            var result = setUp(32);
            List<string> propertySetup = new List<string>() { "GiaBan", "Hinhs", "Id_SanPham", "TenSanPham", "VatLieu", "KichThuoc", "HinhAnh", "Id_LoaiSanPham" };
            
            //action - perform
            var productDetail = (ProductDetail)result.ViewData.Model;
            //  Convert Product  object to JSON string format 
            var json = JsonConvert.SerializeObject(productDetail);   
            JObject jObject = JObject.Parse(json); 
            List<string> propertyRespone = new List<string>();
            foreach (JProperty property in jObject.Properties())
            {
                if(!string.IsNullOrEmpty(property.Value.ToString()))
                {
                    propertyRespone.Add(property.Name);
                }
               
            }
            // check xem thuộc tính trả về có chứ những thuộc tính đã được setup
            var status = true;
            foreach(var property in propertySetup)
            {
                if(!propertyRespone.Contains(property))
                {
                    status = false;
                    if (status == false)
                        break;

                }
            }

            // assert-verify
            Assert.AreEqual(true, status);
        }



        // TH3 :cho dữ liệu thực , kiểm tra xem dữ liệu mong đợi có chính xác với dữ liệu đã cho 
        // case pass : dữ liệu trả về đúng với giá trị set-up
        // case failed : ngược lại (tăng giá trị sản phẩm trong giá trị set-up test case failed) 
        [TestMethod]
        public void checkDataObject()
        {
            ProductDetail productDetailSetup = new ProductDetail()
            {
                Id_SanPham = 32,
                TenSanPham = "Bench SOUL",
                GiaBan = 7110000,
                KichThuoc = "D1600-R350-C450 mm",
                VatLieu = "Gỗ Sồi - chân kim loại sơn",
                MauSac= null,
                XuatXu =null,
                Mota =null,
                HinhAnh= "https://i.imgur.com/BIHv7ga.jpg",
                Hinhs = new List<string>()
                {   "https://i.imgur.com/BIHv7ga.jpg",
                    "https://i.imgur.com/F7r7j5y.jpg",
                    "https://i.imgur.com/4RyGBkh.jpg",
                    "https://i.imgur.com/BIHv7ga.jpg",
                    "https://i.imgur.com/iAzCSAs.jpg"
                }
            };
            //arrange-setup
            var result = setUp(32);

            //action-perform
            var productDetail = (ProductDetail)result.ViewData.Model;
            var jsonSetup = JsonConvert.SerializeObject(productDetailSetup);
            var jsonRespone= JsonConvert.SerializeObject(productDetail);
            bool status = false;
            if(jsonSetup == jsonRespone)
            {
                status = true;
            }

            // assert-verify
            Assert.AreEqual(true, status);

        }


        

    }
}
