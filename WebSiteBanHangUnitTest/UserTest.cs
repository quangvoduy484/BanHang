using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteBanHang;
using WebSiteBanHang.Controllers;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;


namespace WebSiteBanHangUnitTest
{
    [TestClass]
    public class UserTest
    {
        //viết unitest cho  3 trường hợp :

        // TH1 : Kiểm tra đăng xuất có thành công hay không
        // case pass : đăng xuất thành công và chuyến về trang mong đợi
        // case failed : Không điều hướng được đến trang Index (Test bằng cách đăng nhập thành công chuyển đến một trang cụ khác Index)

        [TestMethod]
        public void TestDetailsRedirect()
        {
            var controller = new UserController();

            var action = (RedirectToRouteResult)controller.Logout();
            action.RouteValues["action"].Equals("Index");
            action.RouteValues["controller"].Equals("Home");
            Assert.AreEqual("Index", action.RouteValues["action"]);
            Assert.AreEqual("Homepage", action.RouteValues["controller"]);
        }
    }
}
