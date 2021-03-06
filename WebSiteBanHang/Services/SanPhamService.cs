﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Services
{
    public class SanPhamService
    {
        private BanHangContext context = null;
        public SanPhamService()
        {
            context = new BanHangContext();

        }
        // Tìm kiếm, phân trang
        public async Task<object> GetAllAsync(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.SANPHAMs.Where(t => t.TrangThai != false).AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //Tìm kiếm
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.TenSanPham.Contains(search));
            }
            var totalRecord = await model.CountAsync(); //Tổng số sản phẩm
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
            //thêm dòng lệnh await tách dòng lệnh để thực thi

            var data = await model.Select(t => new SanPhamViewModel()
            {
                MaSanPham = t.Id_SanPham,
                TenLoai = t.LOAISANPHAM.TenLoai,
                KichThuoc = t.KichThuoc,
                TenSanPham = t.TenSanPham,
                DVT = t.DonViTinh,
                XuatXu = t.XuatXu,
            }).ToListAsync();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }
        //????
        public List<SanPhamDropDownViewModel> GetAllDropDownList(string search)
        {
            var sanPhams = context.SANPHAMs.Where(t => t.TrangThai != false);
            if (!string.IsNullOrWhiteSpace(search))
            {
                sanPhams = sanPhams.Where(t => t.TenSanPham.Contains(search));

            }
            var result = sanPhams.OrderBy(t => t.TenSanPham)
                .Take(30)
                .Select(t => new SanPhamDropDownViewModel()
                {
                    id = t.Id_SanPham,
                    text = t.TenSanPham
                }).ToList();

            return result;
        }
        //Lấy tên loại sản phẩm
        public List<LOAISANPHAM> GetAllLoaiSP()
        {
            return context.LOAISANPHAMs.ToList();
        }
        public List<SanPhamViewModel> GetTenSP()
        {

            return context.SANPHAMs
                .Where(t => t.TrangThai != false)
                .OrderBy(t => t.TenSanPham)
                 .Select(t => new SanPhamViewModel
                 {
                     MaSanPham = t.Id_SanPham,
                     TenSanPham = t.TenSanPham,
                 })
                    .ToList();
        }
        //lấy tên sẩn phẩm cho CT Phiếu đặt
        public List<SanPhamViewModel> GetTenSPForPhieuDat(int CTDDH)
        {

            return context.SANPHAMs
                .Where(t => t.TrangThai != false)
                .Where(t => t.CT_PHIEUDATNCCs.All(x => x.MACTPD != CTDDH && x.TRANGTHAI == 1))
                .OrderBy(t => t.TenSanPham)
                 .Select(t => new SanPhamViewModel
                 {
                     MaSanPham = t.Id_SanPham,
                     TenSanPham = t.TenSanPham,
                 })
                    .ToList();
        }
        public int Add(SanPhamViewModel model)
        {
            var sanPham = new SANPHAM
            {
                TenSanPham = model.TenSanPham,
                SoLuongTon = 0,
                XuatXu = model.XuatXu,
                VatLieu = model.VatLieu,
                Mota = model.MoTa,
                MauSac = model.MauSac,
                Id_LoaiSanPham = model.MaLoai,
                KichThuoc = model.KichThuoc,
                CREATED_DATE = DateTime.Now,
                CREATED_BY = HttpContext.Current.User.Identity.Name
            };
            //sanPham.HinhAnh = model.HinhAnh;

            context.SANPHAMs.Add(sanPham);
            context.SaveChanges();
            return sanPham.Id_SanPham;
        }
        public SanPhamViewModel Details(int? id)
        {
            var SanPham = context.SANPHAMs
                .Include(t => t.HINHs)
                .Include(t => t.GIASANPHAMs)
                .OrderByDescending(t => t.KHUYENMAI.NgayBatDau)
                .FirstOrDefault(t => t.TrangThai != false && t.Id_SanPham == id /*&& t.KHUYENMAI.NgayKetThuc >= DateTime.Now && t.KHUYENMAI.NgayBatDau <= DateTime.Now*/);
            if (SanPham == null)
            {
                return null;
            }
            var result = new SanPhamViewModel();

            if (SanPham.KHUYENMAI?.NgayBatDau <= DateTime.Now && SanPham.KHUYENMAI?.NgayKetThuc >= DateTime.Now)
            {
                result.GiaTriKhuyenMai = SanPham.KHUYENMAI.GiaTriKhuyenMai;
                result.TenKhuyenMai = SanPham.KHUYENMAI.TenKhuyenMai;
            }

            result.MaSanPham = SanPham.Id_SanPham;
            result.MaLoai = SanPham.Id_LoaiSanPham;
            result.TenSanPham = SanPham.TenSanPham;
            result.GiaSP = SanPham.GIASANPHAMs.Where(t => t.TrangThai != false).OrderByDescending(t => t.NgayLap).Select(t => t.GiaBan).FirstOrDefault();
            result.DVT = SanPham.DonViTinh;
            result.MauSac = SanPham.MauSac;
            result.VatLieu = SanPham.VatLieu;
            result.SoLuongTon = SanPham.SoLuongTon;
            result.KichThuoc = SanPham.KichThuoc;
            result.XuatXu = SanPham.XuatXu;
            result.TenLoai = SanPham.LOAISANPHAM.TenLoai;
            result.HinhAnh = SanPham.HinhAnh ?? string.Empty;
            result.BaoHanh = SanPham.BaoHanh;
            result.MoTa = SanPham.Mota;



            if (!string.IsNullOrEmpty(SanPham.HinhAnh))
            {
                result.HinhAnhs.Add(new HinhAnhViewModel()
                {
                    Link = SanPham.HinhAnh
                });
            }
            if (SanPham.HINHs?.Count > 0)
            {
                result.HinhAnhs.AddRange(SanPham.HINHs.Select(t => new HinhAnhViewModel()
                {
                    Link = t.Link,
                }));
            }
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
            sanPham.DonViTinh = model.DVT;
            sanPham.UPDATED_BY = HttpContext.Current.User.Identity.Name;
            sanPham.UPDATED_DATE = DateTime.Now;
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
            sanPhamExist.UPDATED_BY = HttpContext.Current.User.Identity.Name;
            sanPhamExist.UPDATED_DATE = DateTime.Now;
            sanPhamExist.TrangThai = false;
            context.SaveChanges();
            return true;
        }


        //Xoá hình ảnh trong bảng HINH
        public bool DeleteImage(int id)
        {
            var hinh = context.HINHs.Find(id);
            if (hinh == null)
            {
                return false;
            }

            context.HINHs.Remove(hinh);
            context.SaveChanges();
            return true;
        }
        //Xoá hình ảnh trong bảng SanPham
        public bool DeletePrimaryImage(int? idSanPham)
        {
            var sanPham = context.SANPHAMs.SingleOrDefault(t => t.Id_SanPham == idSanPham && t.TrangThai != false);
            if (sanPham == null)
            {
                return false;
            }
            sanPham.HinhAnh = string.Empty;
            context.SaveChanges();
            return true;
        }

        public List<HinhAnhViewModel> getHinhAnhs(int maSanPham)
        {
            var images = new List<HinhAnhViewModel>();
            var sanPham = context.SANPHAMs
                 .Include(t => t.HINHs)
                .FirstOrDefault(t => t.TrangThai != false && t.Id_SanPham == maSanPham);
            if (sanPham == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.HinhAnh))
            {
                images.Add(new HinhAnhViewModel()
                {
                    Name = sanPham.TenSanPham + ".png",
                    Link = sanPham.HinhAnh,
                    IsPrimary = true
                });
            }
            if (sanPham.HINHs?.Count > 0)
            {
                int i = 1;
                sanPham.HINHs.ToList().ForEach(t =>
                {
                    var image = new HinhAnhViewModel()
                    {

                        Name = sanPham.TenSanPham + "_" + i++ + ".png",
                        Link = t.Link,
                        IsPrimary = false,
                        IdHinhAnh = t.Id_Hinh
                    };
                    images.Add(image);
                });

            }
            return images;

        }

        public void UpdateImage(int maSanPham, string urlImage)
        {
            var hinh = new HINH()
            {
                Link = urlImage,
                Id_SanPham = maSanPham
            };
            context.HINHs.Add(hinh);
            context.SaveChanges();
        }

        public bool UpdateGia(double gia, int maSP)
        {

            GIASANPHAM giaSP = new GIASANPHAM()
            {
                GiaBan = gia,
                NgayLap = DateTime.Now,
                Id_SanPham = maSP,
            };

            var Gianhap = context.CTPHIEUNHAP_NCCs.Where(t => t.MASANPHAM == maSP)
                .OrderByDescending(t => t.PHIEUNHAP_NCC.NGAYNHAP)
                .Select(t => t.GIANHAP).FirstOrDefault();
            if(Gianhap == null && gia <= 0)
            {
                throw new Exception("Giá bán phải lớn hơn 0");
            }
            double giaNhap = Convert.ToDouble(Gianhap);
            if (gia <= giaNhap)
            {
                throw new Exception("Gía bán phải lớn hơn giá nhập");
            }
            var giaSPexit = context.GIASANPHAMs.Where(t => t.Id_SanPham == maSP && t.TrangThai != false).ToList();
            foreach (var item in giaSPexit)
            {
                item.TrangThai = false;
            }
            context.GIASANPHAMs.Add(giaSP);
            context.SaveChanges();
            return true;
        }

        public List<SanPhamModel> GetSanPhamsByKeySearch(string keySearch)
        {
            var sanPhams = context.SANPHAMs
                .Include(t=>t.HINHs)
                .Include(t=>t.KHUYENMAI)
                .Include(t=>t.GIASANPHAMs)
                .Where(t => t.TrangThai != false && t.TenSanPham.Contains(keySearch))
                .OrderBy(t => t.TenSanPham)
                .Take(24)
                .Select(y => new SanPhamModel()
                {
                    MaSanPham = y.Id_SanPham,
                    TenSanPham = y.TenSanPham,
                    HinhAnh = y.HinhAnh ?? y.HINHs.FirstOrDefault().Link,
                    KhuyenMai = y.KHUYENMAI.NgayBatDau <= DateTime.Now && DateTime.Now <= y.KHUYENMAI.NgayKetThuc ?
                                         (int?)y.KHUYENMAI.GiaTriKhuyenMai : null,
                    GiaGoc = y.GIASANPHAMs.Where(x => x.TrangThai != false).OrderByDescending(m => m.NgayLap).FirstOrDefault().GiaBan,
                })
                .ToList();

            return sanPhams;
        }
    }
}
