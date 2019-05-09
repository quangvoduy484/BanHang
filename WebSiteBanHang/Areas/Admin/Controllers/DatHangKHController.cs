using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
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
            return View();
        }

        // POST: Admin/DatHangKH/Edit/5
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

        // GET: Admin/DatHangKH/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/DatHangKH/Delete/5
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
