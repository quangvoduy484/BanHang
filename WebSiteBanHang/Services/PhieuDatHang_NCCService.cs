using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using static WebSiteBanHang.Areas.Admin.Helpers.Constant;

namespace WebSiteBanHang.Services
{
    public class PhieuDatHang_NCCService
    {
        private BanHangContext context = null;
        public PhieuDatHang_NCCService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.PHIEUDATHANG_NCCs.AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.NHACUNGCAP.TENNCC.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "MaDatHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.MAPHIEUDAT)
                            : model.OrderBy(t => t.MAPHIEUDAT);
                    break;
                case "TenNCC":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NHACUNGCAP.TENNCC)
                            : model.OrderBy(t => t.NHACUNGCAP.TENNCC);
                    break;
                case "NguoiDat":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGUOIDAT)
                            : model.OrderBy(t => t.NHACUNGCAP.TENNCC);
                    break;
                case "TrangThai":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TRANGTHAI)
                            : model.OrderBy(t => t.TRANGTHAI);
                    break;
                case "NgayDat":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGAYDAT)
                            : model.OrderBy(t => t.NGAYDAT);
                    break;

                default:
                    model = model.OrderBy(t => t.MAPHIEUDAT);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.
                Select(t => new PhieuDatHang_NCCViewModel()
                {
                    MaPhieuDat = t.MAPHIEUDAT,
                    TenNCC = t.NHACUNGCAP.TENNCC,
                    TongTien = t.TONGTIEN,
                    NguoiDat = t.NGUOIDAT,
                    NgayDat = t.NGAYDAT,
                    TrangThai = t.TRANGTHAI == 0 ? TinhTrangDatHang.DaHuy :
                                    (t.TRANGTHAI == 1 ? TinhTrangDatHang.DangXyLy : TinhTrangDatHang.DaNhan)

                }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }




        public PhieuDatHang_NCCViewModel GetChiTietDatHangs(int id)
        {

            var datHang = context.PHIEUDATHANG_NCCs
                  .Where(t => t.MAPHIEUDAT == id)
                  .Select(t => new PhieuDatHang_NCCViewModel()
                  {
                    MaPhieuDat=t.MAPHIEUDAT,
                    TenNCC=t.NHACUNGCAP.TENNCC,
                    NgayDat=t.NGAYDAT,
                    NguoiDat=t.NGUOIDAT,
                    TongTien=t.TONGTIEN,
                      //thong tin them

                      TrangThai = t.TRANGTHAI == 0 ? TinhTrangDatHang.DaHuy :
                        (t.TRANGTHAI == 1 ? TinhTrangDatHang.DangXyLy : TinhTrangDatHang.DaGiao),
                      ChiTietPhieuDats = t.CT_PHIEUDATNCC.Where(k=> k.TRANGTHAI !=0)
                      .Select(k => new CT_PhieuDatHangNCCViewModel()
                      {
                          TenSanPham = k.SANPHAM.TenSanPham,
                          MaCTPhieuDat = k.MACTPD,
                          SL = k.SOLUONG,
                          GiaNhap = k.GIANHAP,
                          ThanhTien = k.SOLUONG * k.GIANHAP,
                      }).ToList(),
                  })
                  .FirstOrDefault();
            if (datHang == null) return null;
            return datHang;

        }

        public List<NHACUNGCAP> GetAllNCC()
        {
            return context.NHACUNGCAPs.ToList();
        }

        private int ConvertToTrangThaiInt(string trangThai)
        {
            if (trangThai == TinhTrangDatHang.DaHuy)
            {
                return 0;
            }
            if (trangThai == TinhTrangDatHang.DangXyLy)
            {
                return 1;
            }
            return 2;
        }
        public int Add(CT_PhieuDatHangNCCViewModel model)
        {
            var sanPham = new CT_PHIEUDATNCC
            {
                MASANPHAM=model.MaSP,
                SOLUONG=model.SL,
                GIANHAP=model.GiaNhap,
                THANHTIEN=model.ThanhTien,

               
            };
            context.CT_PHIEUDATNCCs.Add(sanPham);
            context.SaveChanges();
            return sanPham.MACTPD;
        }

        public bool AddPhieuDatHangNCC(PhieuDatHang_NCCViewModel model)
        {
            var tongTien = model.ChiTietPhieuDats.Sum(t => t.SL * t.GiaNhap);
            var phieuDat = new PHIEUDATHANG_NCC()
            {
                MANCC = model.MaNCC,
                NGAYDAT = DateTime.Now,
                NGUOIDAT = HttpContext.Current.User.Identity.Name,
                TONGTIEN = tongTien,
                TRANGTHAI = 1,
            };

            context.PHIEUDATHANG_NCCs.Add(phieuDat);
            context.SaveChanges();

            foreach (var detail in model.ChiTietPhieuDats)
            {
                var chiTiet = new CT_PHIEUDATNCC()
                {
                    MAPHIEUDAT = phieuDat.MAPHIEUDAT,
                    MASANPHAM = detail.MaSP,
                    SOLUONG = detail.SL,
                    GIANHAP = detail.GiaNhap,
                    THANHTIEN = detail.SL * detail.GiaNhap,
                };
                context.CT_PHIEUDATNCCs.Add(chiTiet);
            }
            context.SaveChanges();
            return true;
        }

        public bool Update(PhieuDatHang_NCCViewModel model)
        {

            PHIEUDATHANG_NCC pdh = context.PHIEUDATHANG_NCCs.FirstOrDefault(t => t.MAPHIEUDAT == model.MaPhieuDat);

            if (pdh == null)
            {
                return false;
            }
            pdh.MANCC = model.MaNCC;

            context.SaveChanges();
            return true;
        }
        //Xóa chi tiết đơn đặt hàng
        public bool DeleteDetail(int maCTDH)
        {
            CT_PHIEUDATNCC CTDHExit = context.CT_PHIEUDATNCCs.FirstOrDefault(t=>t.TRANGTHAI!=0 && t.MACTPD == maCTDH);
            if (CTDHExit == null)
            {
                return false;
            }
            CTDHExit.TRANGTHAI = 0;
            // update tong tien
            //lay dat hang

            //var datHang = context.PHIEUDATHANG_NCCs
            //          .Include(t => t.CT_PHIEUDATNCCs)
            //         .FirstOrDefault(t => t.MAPHIEUDAT == CTDHExit.MACTPD && t.TRANGTHAI != 0);
            //if (datHang == null)
            //{
            //    return false;
            //}

            ////////tinh tong tien

            //var newTongTien = datHang.CT_PHIEUDATNCC.Where(t => t.TRANGTHAI == 1).
            //    Sum(t => t.SOLUONG * t.GIANHAP);

            ////update tong tien

            //datHang.TONGTIEN = newTongTien;

            //save
            context.SaveChanges();
            return true;
        }
        // Cập nhật chi tiết đơn hàng
        public bool UpdateCTDH(CT_PhieuDatHangNCCViewModel model)
        {
            CT_PHIEUDATNCC CTDHExit = context.CT_PHIEUDATNCCs.FirstOrDefault(t => t.TRANGTHAI != 0 && t.MACTPD == model.MaCTPhieuDat);
            if (CTDHExit == null)
            {
                return false;
            }
            CTDHExit.SOLUONG = model.SL;
            CTDHExit.GIANHAP = model.GiaNhap;
            CTDHExit.THANHTIEN = model.SL * CTDHExit.GIANHAP;
            var datHang = context.PHIEUDATHANG_NCCs
                   //.Include(t => t.PHIEUDATHANG_NCC)
                   .FirstOrDefault(t => t.MAPHIEUDAT == CTDHExit.MAPHIEUDAT && t.TRANGTHAI == 1);
            if (datHang == null)
            {
                return false;
            }

            //tinh tong tien

            //var newTongTien = datHang.CT_PHIEUDATNCC.Where(t => t.TRANGTHAI != 0).
            //    Sum(t => t.SOLUONG * t.GIANHAP);

            ////update tong tien

            //datHang.TONGTIEN = newTongTien;

            ////save
            context.SaveChanges();
            return true;
        }
        // Xóa đơn đặt hàng
        public bool DeleteDatHangNCC(int maDatHang)
        {
            PHIEUDATHANG_NCC datHangExist = context.PHIEUDATHANG_NCCs.FirstOrDefault(t => t.TRANGTHAI == 1 && t.MAPHIEUDAT == maDatHang);
            if (datHangExist == null)
            {
                return false;
            }
            datHangExist.TRANGTHAI = 0;
            context.SaveChanges();
            return true;
        }

    }
}