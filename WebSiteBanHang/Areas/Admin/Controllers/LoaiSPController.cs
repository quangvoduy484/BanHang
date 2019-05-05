using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize]
    public class LoaiSPController : Controller
    {
        private BanHangContext db = new BanHangContext();
        LoaiSPService iplCate = new LoaiSPService();
        // GET: Admin/LoaiSP
        public ActionResult Index()
        {

            //var model = db.LOAISANPHAMs.Select(t => new LoaiSPViewModel()
            //{
            //    MaLoai = t.MALOAI,
            //    TenLoai = t.TENLOAI,
            //    LoaiPhong = t.LOAIPHONG.TENPHONG
            //}).ToList();

            return View();
        }
        // GET: Admin/LoaiSP/GetAll
        [HttpPost]
        public ActionResult GetAll(DataTableAjaxPostModel dataModel)
        {

            var sortBy = dataModel.columns[dataModel.order[0].column].data;
            var orderBy = dataModel.order[0].dir.ToLower();
            var search = dataModel.search.value;

            var model = db.LOAISANPHAMs.AsQueryable();

            //search
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.TenLoai.Contains(search)
                    || t.LOAIPHONG.TenLoaiPhong.Contains(search));
            }

            var totalRecords = model.Count();

            //sorting
            switch (sortBy)
            {
                case "TenLoai":
                    model = orderBy == "desc" ? model.OrderByDescending(t => t.TenLoai)
                            : model.OrderBy(t => t.TenLoai);
                    break;
                case "LoaiPhong":
                    model = orderBy == "desc" ? model.OrderByDescending(t => t.LOAIPHONG.TenLoaiPhong)
                            : model.OrderBy(t => t.LOAIPHONG.TenLoaiPhong);
                    break;
                default:
                    model = model.OrderBy(t => t.TenLoai);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);

            var data = model.Select(t => new LoaiSPViewModel()
            {
                MaLoai = t.Id_LoaiSanPham,
                TenLoai = t.TenLoai,
                LoaiPhong = t.LOAIPHONG.TenLoaiPhong
            })
            .ToList();

            return Json(new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = data
            });
        }

        // GET: Admin/LoaiSP/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/LoaiSP/Create
        public ActionResult Create()
        {
            var loaiphongs = iplCate.GetAllLoaiPhong();
            SelectList listPhong = new SelectList(loaiphongs, "Id_LoaiPhong", "TenLoaiPhong");
            ViewBag.LoaiPhong = listPhong;
            return View();
        }

        // POST: Admin/LoaiSP/Create
        [HttpPost]
        public ActionResult Create(LOAISANPHAM collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.LOAISANPHAMs.Add(collection);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/LoaiSP/Edit/5
        public ActionResult Edit(int? id)
        {

            LOAISANPHAM LoaiSP = db.LOAISANPHAMs.Find(id);
            if (LoaiSP == null)
            {
                return HttpNotFound();
            }
            var loaiphongs = iplCate.GetAllLoaiPhong();
            SelectList listPhong = new SelectList(loaiphongs, "Id_LoaiPhong", "TenLoaiPhong");
            ViewBag.LoaiPhong = listPhong;

            return View(LoaiSP);
        }

        // POST: Admin/LoaiSP/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LOAISANPHAM collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(collection).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(collection);

            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/LoaiSP/Delete/5
        public ActionResult Delete(int? id)
        {
            LOAISANPHAM LoaiSP = db.LOAISANPHAMs.Find(id);
            if (LoaiSP == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Admin/LoaiSP/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                var loaiSP = db.LOAISANPHAMs.Find(id);
                if (loaiSP == null)
                {
                    result.Message = "Không tìm thấy ...";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return Json(result);
                }
                db.LOAISANPHAMs.Remove(loaiSP);
                db.SaveChanges();

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
