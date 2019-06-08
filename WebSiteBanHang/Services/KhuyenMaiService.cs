using System.Collections.Generic;
using System.Linq;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using System.Data.Entity;
using System;

namespace WebSiteBanHang.Services
{
    public class KhuyenMaiService
    {
        private BanHangContext context = null;
        public KhuyenMaiService()
        {
            context = new BanHangContext();

        }
        //public List<KHUYENMAI> ListAll()
        //{
        //    return context.KHUYENMAIs.ToList().Select(t => new KHUYENMAI()
        //    {
        //        Id_KhuyenMai = t.Id_KhuyenMai,
        //        TenKhuyenMai = t.TenKhuyenMai,
        //        NgayBatDau = t.NgayBatDau,
        //        NgayKetThuc = t.NgayKetThuc,
        //        GiaTriKhuyenMai = t.GiaTriKhuyenMai
        //    }).ToList();
        //}

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.KHUYENMAIs.AsQueryable(); //lấy sản phẩm (chưa thực thi)
            //Where(t => t.TrangThai != 0).
            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.TenKhuyenMai.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "Id_KhuyenMai":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.Id_KhuyenMai)
                            : model.OrderBy(t => t.Id_KhuyenMai);
                    break;
                case "TenKhuyenMai":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TenKhuyenMai)
                            : model.OrderBy(t => t.TenKhuyenMai);
                    break;
                case "NgayBatDau":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NgayBatDau)
                            : model.OrderBy(t => t.NgayBatDau);
                    break;
                case "NgayKetThuc":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NgayKetThuc)
                            : model.OrderBy(t => t.NgayKetThuc);
                    break;
                case "GiaTriKhuyenMai":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.GiaTriKhuyenMai)
                            : model.OrderBy(t => t.GiaTriKhuyenMai);
                    break;
                default:
                    model = model.OrderBy(t => t.TenKhuyenMai);
                    break;
            };
            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model
                .AsEnumerable()
                .Select(t => new KhuyenMaiViewModel()
                {
                    Id_KhuyenMai=t.Id_KhuyenMai,
                    TenKhuyenMai=t.TenKhuyenMai,
                    NgayBatDau=t.NgayBatDau,
                    NgayKetThuc=t.NgayKetThuc,
                    GiaTriKhuyenMai=t.GiaTriKhuyenMai,
                }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }

        public void Add(KhuyenMaiViewModel model)
        {
            var khuyenMai = new KHUYENMAI()
            {
                TenKhuyenMai = model.TenKhuyenMai,
                GiaTriKhuyenMai = model.GiaTriKhuyenMai,
                NgayBatDau = model.NgayBatDau,
                NgayKetThuc = model.NgayKetThuc,
            };

            context.KHUYENMAIs.Add(khuyenMai);
            context.SaveChanges();

            if (model.SanPhams?.Count > 0)
            {
                //foreach (var item in model.SanPhams)
                //{
                //    var sanPham = context.SANPHAMs.FirstOrDefault(t => t.Id_SanPham == item && t.TrangThai != false);
                //    if (sanPham == null) continue;
                //    sanPham.Id_KhuyenMai = khuyenMai.Id_KhuyenMai;
                //}
                UpdateKhuyenMaiForSanPham(model.SanPhams, khuyenMai.Id_KhuyenMai, true);
            }
            context.SaveChanges();
        }
        public void UpdateKhuyenMaiForSanPham(List<int> sanPhams, int id_KhuyenMai, bool status)
        {
            foreach (var item in sanPhams)
            {
                var sanPham = context.SANPHAMs.FirstOrDefault(t => t.Id_SanPham == item && t.TrangThai != false);
                if (sanPham == null) continue;
                if (status) // true : add new
                {
                    sanPham.Id_KhuyenMai = id_KhuyenMai;

                }
                else  // false : remove old
                {
                    sanPham.Id_KhuyenMai = null;
                }
            }
        }
        public bool Update(KhuyenMaiViewModel khuyenMai)
        {

            KHUYENMAI khuyenMaiExist = context.KHUYENMAIs
                .Include(t=>t.SANPHAMs)
                .FirstOrDefault(t=>t.Id_KhuyenMai == khuyenMai.Id_KhuyenMai);
            if (khuyenMaiExist == null)
            {
                return false;
            }
            khuyenMaiExist.TenKhuyenMai = khuyenMai.TenKhuyenMai;
            khuyenMaiExist.NgayBatDau = khuyenMai.NgayBatDau;
            khuyenMaiExist.NgayKetThuc = khuyenMai.NgayKetThuc;
            khuyenMaiExist.GiaTriKhuyenMai = khuyenMai.GiaTriKhuyenMai;

            //update san pham apply

            var existSanPhamIds = khuyenMaiExist.SANPHAMs.Where(t => t.TrangThai != false)
                .Select(t => t.Id_SanPham).ToList();
            // New 
            var newIds = khuyenMai.SanPhams.Except(existSanPhamIds).ToList();

            if (newIds.Count > 0)
            {
                UpdateKhuyenMaiForSanPham(newIds, khuyenMai.Id_KhuyenMai, true);
            }
            // remove old

            var removeIds = existSanPhamIds.Except(khuyenMai.SanPhams).ToList();
            if (removeIds.Count > 0)
            {
                UpdateKhuyenMaiForSanPham(removeIds, khuyenMai.Id_KhuyenMai, false);
            }

            context.SaveChanges();
            return true;
        }

        public KhuyenMaiViewModel FindById(int? id)
        {
            var km = context.KHUYENMAIs
                 .Include(t => t.SANPHAMs)
                 .Where(t => t.Id_KhuyenMai == id)
                 .Select(t => new KhuyenMaiViewModel()
                 {
                     TenKhuyenMai = t.TenKhuyenMai,
                     GiaTriKhuyenMai = t.GiaTriKhuyenMai,
                     NgayKetThuc = t.NgayKetThuc,
                     NgayBatDau = t.NgayBatDau,
                     Id_KhuyenMai = t.Id_KhuyenMai,
                     SanPhamDropdowns = t.SANPHAMs.Select(x => new SanPhamDropDownViewModel()
                     {
                         id = x.Id_SanPham,
                         text = x.TenSanPham
                     }).ToList()
                 }).FirstOrDefault();
            return km;
        }
        public bool Delete(int maKM)
        {
            KHUYENMAI khuyenMaiExist = context.KHUYENMAIs.Find(maKM);
            if (khuyenMaiExist == null)
            {
                return false;
            }
            context.KHUYENMAIs.Remove(khuyenMaiExist);
            context.SaveChanges();
            return true;
        }
    }
}
