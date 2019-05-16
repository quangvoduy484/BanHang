using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using static WebSiteBanHang.Areas.Admin.Helpers.Constant;

namespace WebSiteBanHang.Services
{
    public class DatHangKHService
    {
        private BanHangContext context = null;
        public DatHangKHService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.DATHANGs.AsQueryable(); //lấy sản phẩm (chưa thực thi)
            //Where(t => t.TrangThai != 0).
            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.KHACHHANG.TenKhachHang.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "Id_DatHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.Id_DatHang)
                            : model.OrderBy(t => t.Id_DatHang);
                    break;
                case "NgayDat":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NgayDat)
                            : model.OrderBy(t => t.NgayDat);
                    break;
                case "TongTien":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TongTien)
                            : model.OrderBy(t => t.TongTien);
                    break;
                case "TenKhachHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.KHACHHANG.TenKhachHang)
                            : model.OrderBy(t => t.KHACHHANG.TenKhachHang);
                    break;
                case "NgayGiao":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NgayGiao)
                            : model.OrderBy(t => t.NgayGiao);
                    break;
                default:
                    model = model.OrderBy(t => t.Id_DatHang);
                    break;
            };
            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model
                .Include(t => t.KHACHHANG)
                .AsEnumerable()
                .Select(t => new DatHangKHViewModel()
                {
                    MaDatHang = t.Id_DatHang,
                    NgayDatHang = t.NgayDat,
                    NgayGiao = t.NgayGiao,
                    TongTien = t.TongTien,
                    TrangThai = ConvertToTrangThaiModel(t.TrangThai),
                    TenKhachHang = t.KHACHHANG.TenKhachHang,
                }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }

        public DatHangKHViewModel GetChiTietDatHangs(int id)
        {

            var datHang = context.DATHANGs
                  .Where(t => t.Id_DatHang == id)
                  .Select(t => new DatHangKHViewModel()
                  {
                      MaDatHang = t.Id_DatHang,
                      //thong tin them
                      TenKhachHang = t.KHACHHANG.TenKhachHang,
                      DiaChiGiao = t.DiaChiGiao,
                      SoDienThoai = t.SoDienThoai,
                      TongTien = t.TongTien,
                      NgayGiao = t.NgayGiao,
                      GhiChu = t.GhiChu,
                      TrangThai = t.TrangThai == 0 ? TinhTrangDatHang.DaHuy :
                        (t.TrangThai == 1 ? TinhTrangDatHang.DangXyLy : TinhTrangDatHang.DaGiao),
                      ChiTietDatHangs = t.CHITIETDATHANGs.Where(k => k.TrangThai != false)
                      .Select(k => new ChiTietDatHangViewModel()
                      {
                          TenSanPham = k.SANPHAM.TenSanPham,
                          MaChiTiet = k.Id_ChiTietDatHang,
                          SoLuong = k.SoLuong,
                          GiaBan = k.GiaBan,
                          ThanhTien = k.SoLuong * k.GiaBan,
                      }).ToList(),
                  })
                  .FirstOrDefault();
            if (datHang == null) return null;
            return datHang;

        }
        private string ConvertToTrangThaiModel(int trangThai)
        {
            if (trangThai == 0)
            {
                return TinhTrangDatHang.DaHuy;
            }
            if (trangThai == 1)
            {
                return TinhTrangDatHang.DangXyLy;
            }
            return TinhTrangDatHang.DaGiao;
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
        //public List<DATHANG> ListAll()
        //{
        //    return context.DATHANGs.OrderBy(t => t.NgayDat).ToList();
        //}
        public List<KHACHHANG> GetAllKH()
        {
            return context.KHACHHANGs.ToList();
        }
        public void Add(DatHangKHViewModel model)
        {
            var dathang = new DATHANG
            {
                Id_DatHang = model.MaDatHang,
                Id_KhachHang = model.MaKhachHang,
                NgayDat = model.NgayDatHang,
                DiaChiGiao = model.DiaChiGiao,
                SoDienThoai = model.SoDienThoai,
                GhiChu = model.GhiChu,
                TongTien = model.TongTien,
                TrangThai = ConvertToTrangThaiInt(model.TrangThai)
            };
            context.DATHANGs.Add(dathang);
            context.SaveChanges();
        }
        public bool DeleteDetail(int maCTDH)
        {
            CHITIETDATHANG CTDHExit = context.CHITIETDATHANGs.FirstOrDefault(t => t.TrangThai != false && t.Id_ChiTietDatHang == maCTDH);
            if (CTDHExit == null)
            {
                return false;
            }
            CTDHExit.TrangThai = false;
            // update tong tien
            //lay dat hang

            var datHang = context.DATHANGs
                    .Include(t => t.CHITIETDATHANGs)
                    .FirstOrDefault(t => t.Id_DatHang == CTDHExit.Id_DatHang && t.TrangThai == 1);
            if (datHang == null)
            {
                return false;
            }

            //tinh tong tien

            var newTongTien = datHang.CHITIETDATHANGs.Where(t => t.TrangThai != false).
                Sum(t => t.SoLuong * t.GiaBan);

            //update tong tien

            datHang.TongTien = newTongTien;

            //save
            context.SaveChanges();
            return true;
        }
        public bool DeleteDatHangKH(int maDatHang)
        {
            DATHANG datHangExist = context.DATHANGs.FirstOrDefault(t => t.TrangThai == 1 && t.Id_DatHang == maDatHang);
            if (datHangExist == null)
            {
                return false;
            }
            datHangExist.TrangThai = 0;
            context.SaveChanges();
            return true;
        }
        public bool UpdateCTDH(ChiTietDatHangViewModel model)
        {
            CHITIETDATHANG CTDHExit = context.CHITIETDATHANGs.FirstOrDefault(t => t.TrangThai != false && t.Id_ChiTietDatHang == model.MaChiTiet);
            if (CTDHExit == null)
            {
                return false;
            }
            CTDHExit.SoLuong = model.SoLuong;
            CTDHExit.ThanhTien = model.SoLuong * CTDHExit.GiaBan;
            var datHang = context.DATHANGs
                   .Include(t => t.CHITIETDATHANGs)
                   .FirstOrDefault(t => t.Id_DatHang == CTDHExit.Id_DatHang && t.TrangThai == 1);
            if (datHang == null)
            {
                return false;
            }

            //tinh tong tien

            var newTongTien = datHang.CHITIETDATHANGs.Where(t => t.TrangThai != false).
                Sum(t => t.SoLuong * t.GiaBan);

            //update tong tien

            datHang.TongTien = newTongTien;

            //save
            context.SaveChanges();
            return true;
        }
        public bool UpdateDHKH(DatHangKHViewModel model)
        {
            DATHANG datHang = context.DATHANGs.FirstOrDefault(t => t.Id_DatHang == model.MaDatHang && t.TrangThai == 1);
            if (datHang == null)
            {
                return false;
            }

            datHang.DiaChiGiao = model.DiaChiGiao;
            datHang.SoDienThoai = model.SoDienThoai;
            context.SaveChanges();

            return true;

        }
        public DatHangKHViewModel findbyId(int id)
        {
            return context.DATHANGs
                .Where(t => t.TrangThai == 1 && t.Id_DatHang == id)
                .Select(t => new DatHangKHViewModel()
                {
                    MaDatHang = t.Id_DatHang,
                    DiaChiGiao = t.DiaChiGiao,
                    SoDienThoai = t.SoDienThoai,
                }).FirstOrDefault();
        }
        #region Declaration
        int _totalColumn = 6;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(5);
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        DatHangKHViewModel _datHang;
        #endregion
        public byte[] PrepareDatHang(DatHangKHViewModel datHangs)
        {
            #region
            _datHang = datHangs;
            //_totalColumn = 6;
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            BaseFont bf = BaseFont.CreateFont("C:/windows/fonts/Arial.ttf",
                BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            _fontStyle = new Font(bf, 12);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfTable.SetWidths(new float[] {10f,20f,20f,20f,20f});
            #endregion
            this.ReportHeader();
            this.ReportBody();
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();
        }
        private void ReportHeader(/*DatHangKHViewModel mode*/)
        {
            //_fontStyle = FontFactory.GetFont("Time New Roman", 20f, 1);
            //_pdfCell = new PdfPCell(new Phrase("NỘI THẤT NHÀ XINH", _fontStyle))
            //{
            //    Colspan = -_totalColumn,
            //    HorizontalAlignment = Element.ALIGN_CENTER,
            //    Border = 0,
            //    BackgroundColor = BaseColor.WHITE,
            //    ExtraParagraphSpace = 0
            //};
            //_pdfTable.AddCell(_pdfCell);
            //_pdfTable.CompleteRow();
            //_fontStyle = FontFactory.GetFont("Time New Roman", 9f, 1);
            //_pdfCell = new PdfPCell(new Phrase("DatHang list", _fontStyle))
            //{
            //    Colspan = -_totalColumn,
            //    HorizontalAlignment = Element.ALIGN_CENTER,
            //    Border = 0,
            //    BackgroundColor = BaseColor.WHITE,
            //    ExtraParagraphSpace = 0
            //};
            //_pdfTable.AddCell(_pdfCell);
            //_pdfTable.CompleteRow();
        }
        private void ReportBody()
        {
            #region Table header
            AddHeader("STT");
            AddHeader("Mã đặt hàng");
            AddHeader("Tên sản phẩm");
            AddHeader("Số lượng");
            AddHeader("Giá bán");
            AddHeader("Thành tiền");
            _pdfTable.CompleteRow();
            #endregion

            #region Table Body
            int serialNumber = 1;

            foreach (var chiTiet in _datHang.ChiTietDatHangs)
            {
                AddRow(serialNumber++.ToString());
                AddRow(chiTiet.MaChiTiet.ToString());
                AddRow(chiTiet.TenSanPham.ToString());
                AddRow(chiTiet.SoLuong.ToString());
                AddRow(chiTiet.GiaBan.ToString());
                AddRow(chiTiet.ThanhTien.ToString());

                _pdfTable.CompleteRow();
            }
            #endregion
        }

        private void AddHeader(string nameHeader)
        {
            _pdfCell = new PdfPCell(new Phrase(nameHeader, _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.LIGHT_GRAY
            };
            _pdfTable.AddCell(_pdfCell);
        }

        private void AddRow(string value)
        {
            _pdfCell = new PdfPCell(new Phrase(value, _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.WHITE
            };
            _pdfTable.AddCell(_pdfCell);
        }
    }
}