using System;
using System.Collections.Generic;
using System.Linq;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class SanPhamService
    {
        private BanHangContext context = null;
        public SanPhamService()
        {
            context = new BanHangContext();

        }
        //public List<SanPhamViewModel> ListAll()
        //{
        //    return context.SANPHAMs.ToList().Select(t => new SanPhamViewModel()
        //    {
        //        MaSanPham = t.MASANPHAM,
        //        TenLoai=t.LOAISANPHAM.TENLOAI,
        //        KichThuoc=t.KICHTHUOC,
        //        TenSanPham=t.TENSANPHAM,
        //        DVT = t.DONVITINH,
        //        XuatXu = t.XUATXU,
        //    }).ToList();
        //}

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.SANPHAMs.Where(t => t.TrangThai != false).AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.TenSanPham.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "TenSanPham":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TenSanPham)
                            : model.OrderBy(t => t.TenSanPham);
                    break;
                case "TenLoai":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.LOAISANPHAM.TenLoai)
                            : model.OrderBy(t => t.LOAISANPHAM.TenLoai);
                    break;
                case "DVT":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.DonViTinh)
                            : model.OrderBy(t => t.DonViTinh);
                    break;
                case "KichThuoc":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.KichThuoc)
                            : model.OrderBy(t => t.KichThuoc);
                    break;
                default:
                    model = model.OrderBy(t => t.TenSanPham);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.Select(t => new SanPhamViewModel()
            {
                MaSanPham = t.Id_SanPham,
                TenLoai = t.LOAISANPHAM.TenLoai,
                KichThuoc = t.KichThuoc,
                TenSanPham = t.TenSanPham,
                DVT = t.DonViTinh,
                XuatXu = t.XuatXu,
            }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }
        public List<LOAISANPHAM> GetAllLoaiSP()
        {
            return context.LOAISANPHAMs.ToList();
        }
        public SANPHAM Add(SanPhamViewModel model)
        {
            var sanPham = new SANPHAM
            {
                TenSanPham = model.TenSanPham,
                SoLuongTon = model.SoLuongTon,
                XuatXu = model.XuatXu,
                VatLieu = model.VatLieu,
                Mota = model.MoTa,
                MauSac = model.MauSac,
                Id_LoaiSanPham = model.MaLoai,
                KichThuoc = model.KichThuoc
            };
            //sanPham.HinhAnh = model.HinhAnh;
            context.SANPHAMs.Add(sanPham);
            context.SaveChanges();
            return sanPham;
        }
        public SanPhamViewModel Details(int? id)
        {
            var SanPham = context.SANPHAMs.FirstOrDefault(t => t.TrangThai != false && t.Id_SanPham == id);
            if (SanPham == null)
            {
                return null;
            }
            var result = new SanPhamViewModel()
            {
                MaSanPham = SanPham.Id_SanPham,
                MaLoai = SanPham.Id_LoaiSanPham,
                TenSanPham = SanPham.TenSanPham,
                DVT = SanPham.DonViTinh,
                MauSac = SanPham.MauSac,
                VatLieu = SanPham.VatLieu,
                SoLuongTon = SanPham.SoLuongTon,
                KichThuoc = SanPham.KichThuoc,
                XuatXu = SanPham.XuatXu,
                TenLoai = SanPham.LOAISANPHAM.TenLoai,
                HinhAnh = SanPham.HinhAnh,
                BaoHanh = SanPham.BaoHanh,
                MoTa = SanPham.Mota
            };
            return result;
        }
        public bool Update(SanPhamViewModel model)
        {
            SANPHAM sanPham = context.SANPHAMs.FirstOrDefault(t => t.TrangThai != false && t.Id_SanPham == model.MaSanPham);
            if (sanPham == null)
            {
                return false;
            }
            sanPham.TenSanPham = model.TenSanPham;
            sanPham.SoLuongTon = model.SoLuongTon;
            sanPham.XuatXu = model.XuatXu;
            sanPham.VatLieu = model.VatLieu;
            sanPham.Mota = model.MoTa;
            sanPham.MauSac = model.MauSac;
            sanPham.Id_LoaiSanPham = model.MaLoai;
            sanPham.KichThuoc = model.KichThuoc;
            sanPham.HinhAnh = model.HinhAnh;
            context.SaveChanges();
            return true;
        }
        public bool Delete(int maSanPham)
        {
            SANPHAM sanPhamExist = context.SANPHAMs.FirstOrDefault(t => t.TrangThai != false && t.Id_SanPham == maSanPham);
            if (sanPhamExist == null)
            {
                return false;
            }
            sanPhamExist.TrangThai = false;
            context.SaveChanges();
            return true;
        }
    }
}
