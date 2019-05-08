using RestSharp;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
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
            SelectList listloaiSP = new SelectList(loaiSP, "MaLoai", "TenLoai");
            ViewBag.listloaiSP = listloaiSP;
            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        public ActionResult Create(SanPhamViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = sanPhamService.Add(collection);
                    if (result > 0)
                    {
                        return Json(new { Id = result }, JsonRequestBehavior.AllowGet);
                    }

                    return View(collection);
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
            SelectList listloaiSP = new SelectList(loaiSP, "MaLoai", "TenLoai");
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

        [HttpPost]
        public void UpLoadImage(int id)
        {
            try
            {
                string fName = string.Empty;
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    var ms = StreamToByteArray(file.InputStream);
                    var client = new RestClient(ConfigurationManager.AppSettings["urlUpload"].ToString());
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Connection", "keep-alive");
                    request.AddHeader("content-length", "209");
                    request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                    request.AddHeader("accept-encoding", "gzip, deflate");
                    request.AddHeader("Host", "api.imgur.com");
                    request.AddHeader("Postman-Token", "e4734b34-44af-4c20-aac2-19d0cd71134c,8e60bfd7-00d3-4c94-8e5e-9c25921c9c55");
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["token"].ToString());
                    request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"image\"\r\n\r\nhttp://www.nhaxinh.com/photo/1000-tu-tivi-vic.jpg\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }
        public static byte[] StreamToByteArray(Stream input)
        {
            input.Position = 0;
            using (var ms = new MemoryStream())
            {
                int length = System.Convert.ToInt32(input.Length);
                input.CopyTo(ms, length);
                return ms.ToArray();
            }
        }
    }

}
