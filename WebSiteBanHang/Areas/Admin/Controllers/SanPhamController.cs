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
using Newtonsoft.Json;
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
        public async Task<object> GetAll(DataTableAjaxPostModel dataModel)
        {
            var sanPhams = await sanPhamService.GetAllAsync(dataModel);

            return Json(sanPhams);
        }

        public ActionResult GetSanPhams(string search)
        {
            var sanPhams = sanPhamService.GetAllDropDownList(search);
            return Json(sanPhams, JsonRequestBehavior.AllowGet);
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
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    var result = sanPhamService.Update(collection);
                    if (result == false)
                    {
                        return HttpNotFound();
                    }
                    return Json("success");
                }
                return View(collection);
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
        public async Task UpLoadImage(int id)
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
                    request.AddHeader("accept-encoding", "gzip, deflate");
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["tokenUpload"].ToString());
                    request.AddHeader("content-type", "multipart/form-data");
                    request.AddParameter("application/octet-stream", ms, ParameterType.RequestBody);

                    var response = await client.ExecuteTaskAsync(request);
                    var result = JsonConvert.DeserializeObject<ResponseUploadImageModel>(response.Content);
                    if (result != null && result.Status == HttpStatusCode.OK)
                    {
                        sanPhamService.UpdateImage(id, result.Data.Link);
                    }
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

        public ActionResult GetImages(int id)
        {
            return Json(sanPhamService.getHinhAnhs(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteImage(int? idHinhAnh, int? idSanPham)
        {
            var result = new ReponseMessage();
            try
            {
                // TODO: Add delete logic here
                bool kq = false;
                if (idSanPham.HasValue && idSanPham.Value > 0)
                {
                    kq = sanPhamService.DeletePrimaryImage(idSanPham);
                }
                else if (idHinhAnh.HasValue && idHinhAnh.Value > 0)
                {
                    kq = sanPhamService.DeleteImage(idHinhAnh.Value);
                }
                if (kq == false)
                {
                    result.Message = "Không tìm thấy hình ảnh";
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
    }

}
