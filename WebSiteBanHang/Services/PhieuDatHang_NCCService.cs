using Microsoft.AspNet.Identity;
using System;
using System.Activities.Statements;
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
                            : model.OrderBy(t => t.NHACUNGCAP.TRANGTHAI);
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

        //internal void Add(PhieuDatHang_NCCViewModel collection)
        //{
        //    throw new NotImplementedException();
        //}

        public List<CT_PhieuDatHangNCCViewModel> GETCTDH_NCC(int id)
        {

            var chiTiets = context.CT_PHIEUDATNCCs.Where(t => t.MAPHIEUDAT == id)
                .Select(t => new CT_PhieuDatHangNCCViewModel()
                {
                    NgayDat=t.PHIEUDATHANG_NCC.NGAYDAT,
                    TenNguoiDat=t.PHIEUDATHANG_NCC.NGUOIDAT,
                    TenSanPham = t.SANPHAM.TenSanPham,
                    SL = t.SOLUONG,
                    GiaNhap=t.GIANHAP,
                    ThanhTien=t.THANHTIEN,
                    TongTien=t.PHIEUDATHANG_NCC.TONGTIEN,
                    MaSP = t.SANPHAM.Id_SanPham,
                    MaNCC=t.PHIEUDATHANG_NCC.MANCC,
                 
 
                }).ToList();
            return chiTiets;
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


    }
}