using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using static WebSiteBanHang.Areas.Admin.Helpers.Constant;

namespace WebSiteBanHang.Services
{
    public class PhieuNhapHang_NCCService
    {
        private BanHangContext context = null;
        public PhieuNhapHang_NCCService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.PHIEUNHAP_NCCs.AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    model = model.Where(t => t.NHACUNGCAP.TENNCC.Contains(search));
            //}
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "MaDatHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.MAPHIEUDAT)
                            : model.OrderBy(t => t.MAPHIEUDAT);
                    break;
                //case "TenNCC":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.NHACUNGCAP.TENNCC)
                //            : model.OrderBy(t => t.NHACUNGCAP.TENNCC);
                //    break;
                //case "NguoiDat":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGUOIDAT)
                //            : model.OrderBy(t => t.NHACUNGCAP.TENNCC);
                //    break;
                //case "TrangThai":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.TRANGTHAI)
                //            : model.OrderBy(t => t.NHACUNGCAP.TRANGTHAI);
                //    break;
                //case "NgayDat":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGAYDAT)
                //            : model.OrderBy(t => t.NGAYDAT);
                //    break;

                default:
                    model = model.OrderBy(t => t.MAPHIEUDAT);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.
                Select(t => new PhieuNhapHang_NCCViewModel()
                {
                    MaPhieuDat = t.MAPHIEUDAT,
                    TongTien = t.TONGTIEN,
                    //NguoiDat = t.PHIEUDATHANG_NCC.NGUOIDAT,
                    //NgayDat = t.PHIEUDATHANG_NCC.NGAYDAT,
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

        public List<PHIEUNHAP_NCC> ListAll()
        {
            return context.PHIEUNHAP_NCCs.Where(t=>t.TRANGTHAI!=0)
            .ToList().Select(t => new PHIEUNHAP_NCC()
            {
                MAPHIEUDAT = t.MAPHIEUDAT,
                TONGTIEN = t.TONGTIEN,
                NGUOINHAP = t.NGUOINHAP,
                NGAYNHAP = t.NGAYNHAP,
                
            }).ToList();
        }
        public PhieuDatHang_NCCViewModel GetChiTietDatHangs(int id)
        {

            var datHang = context.PHIEUDATHANG_NCCs
                  .Where(t => t.MAPHIEUDAT == id)
                  .Select(t => new PhieuDatHang_NCCViewModel()
                  {
                      MaPhieuDat = t.MAPHIEUDAT,
                      TenNCC = t.NHACUNGCAP.TENNCC,
                      NgayDat = t.NGAYDAT,
                      NguoiDat = t.NGUOIDAT,
                      TongTien = t.TONGTIEN,
                      //thong tin them

                      TrangThai = t.TRANGTHAI == 0 ? TinhTrangDatHang.DaHuy :
                        t.TRANGTHAI == 1 ? TinhTrangDatHang.DangXyLy:
                        (t.TRANGTHAI==2? TinhTrangDatHang.DangDat : TinhTrangDatHang.DaNhan),

                      ChiTietPhieuDats = t.CT_PHIEUDATNCCs.Where(k => k.TRANGTHAI != 0)
                      .Select(k => new CT_PhieuDatHangNCCViewModel()
                      {
                          MaSP=k.MASANPHAM,
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

        public bool AddPhieuNhapHangNCC(int id,PhieuNhapHang_NCCViewModel model)
        {
            var tongTien = model.ChiTietPhieuNhaps.Sum(t => t.SL * t.GiaNhap);
            var phieuDat = new PHIEUNHAP_NCC()
            {
                MAPHIEUDAT=id,
                NGAYNHAP = DateTime.Now,
                NGUOINHAP = HttpContext.Current.User.Identity.Name,
                TONGTIEN = tongTien,
                TRANGTHAI = 2,
            };

            context.PHIEUNHAP_NCCs.Add(phieuDat);
            context.SaveChanges();

            foreach (var detail in model.ChiTietPhieuNhaps)
            {
                var chiTiet = new CTPHIEUNHAP_NCC()
                {
                    MAPHIEUNHAP = phieuDat.MAPHIEUNHAP,
                    MASANPHAM = detail.MaSP,
                    SOLUONGNHAP = detail.SL,
                    GIANHAP = detail.GiaNhap,
                    THANHTIEN = detail.SL * detail.GiaNhap,
                   
                };
                
                context.CTPHIEUNHAP_NCCs.Add(chiTiet);
            }
            
            context.SaveChanges();
            return true;
        }
        
        public void UpdateTrangThaiDonHang(int maDatHang)
        {
            PHIEUDATHANG_NCC datHang = context.PHIEUDATHANG_NCCs.
                FirstOrDefault(t => t.MAPHIEUDAT == maDatHang && t.TRANGTHAI == 1);
            
            if (datHang == null)
            {
                return;
            }
            datHang.TRANGTHAI = 3;
            datHang.NGAYDAT = DateTime.Now;

            context.SaveChanges();
        }
    }
}