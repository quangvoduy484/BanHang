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
    public class PhieuNhapHang_NCCService
    {
        private BanHangContext context = null;
        public PhieuNhapHang_NCCService()
        {
            context = new BanHangContext();

        }
        // Danh sách phiếu nhập
        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.PHIEUNHAP_NCCs.AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    model = model.Where(t => t..Contains(search));
            //}
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "MaDatHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.MAPHIEUDAT)
                            : model.OrderBy(t => t.MAPHIEUDAT);
                    break;
               

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
                    MaPhieuNhap=t.MAPHIEUNHAP,
                    NgayNhap=t.NGAYNHAP,
                    NguoiNhap=t.NGUOINHAP,
                    TongTien = t.TONGTIEN,
                    TrangThai = t.TRANGTHAI == 0 ? TinhTrangDatHang.DaHuy :
                                    (t.TRANGTHAI == 1) ? TinhTrangDatHang.DangXyLy : (t.TRANGTHAI == 2) ?
                                    TinhTrangDatHang.DaNhan : TinhTrangDatHang.DangDat,

                }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }

        //Lấy chi tiết phiếu nhập
        public PhieuNhapHang_NCCViewModel GetChiTietNhapHangs(int id)
        {

            var datHang = context.PHIEUNHAP_NCCs
                  .Where(t => t.MAPHIEUNHAP == id)
                  .Select(t => new PhieuNhapHang_NCCViewModel()
                  {
                      MaPhieuDat = t.MAPHIEUDAT,
                      MaPhieuNhap = t.MAPHIEUNHAP,
                      TongTien = t.TONGTIEN,
                      NguoiNhap = t.NGUOINHAP,
                      NgayNhap = t.NGAYNHAP,
                      //thong tin them
                      TrangThai = t.TRANGTHAI == 0 ? TinhTrangDatHang.DaHuy :
                                    (t.TRANGTHAI == 1) ? TinhTrangDatHang.DangXyLy : (t.TRANGTHAI == 2) ?
                                    TinhTrangDatHang.DaNhan : TinhTrangDatHang.DangDat,

                      ChiTietPhieuNhaps = t.CTPHIEUNHAP_NCC
                      .Select(k => new CT_PhieuNhapHangNCCViewModel()
                      {
                          MaPhieuNhap = k.MAPHIEUNHAP,
                          MaSP = k.MASANPHAM,
                          TenSanPham = k.SANPHAM.TenSanPham,
                          SL = k.SOLUONGNHAP,
                          GiaNhap = k.GIANHAP,
                          ThanhTien = k.SOLUONGNHAP * k.GIANHAP,
                      }).ToList(),
                  })
                  .FirstOrDefault();
            if (datHang == null) return null;
            return datHang;
        }

        //Lấy thông tin đơn đặt hàng để tạo phiếu nhập hàng
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
                                    (t.TRANGTHAI == 1) ? TinhTrangDatHang.DangXyLy : (t.TRANGTHAI == 2) ?
                                    TinhTrangDatHang.DaNhan : TinhTrangDatHang.DangDat,
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

        // Lập phiếu nhập hàng từ phiếu đặt
        public bool AddPhieuNhapHangNCC(int id, PhieuNhapHang_NCCViewModel model)
        {
            
            var tongTien = model.ChiTietPhieuNhaps.Sum(t => t.SL * t.GiaNhap);
            var phieuDat = new PHIEUNHAP_NCC()
            {
                MAPHIEUDAT = id,
                NGAYNHAP = DateTime.Now,
                NGUOINHAP = HttpContext.Current.User.Identity.Name,
                TONGTIEN = tongTien.Value,
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
                
              var sp= context.SANPHAMs.Where(t => t.Id_SanPham == detail.MaSP).FirstOrDefault();
                sp.SoLuongTon += detail.SL;
                context.CTPHIEUNHAP_NCCs.Add(chiTiet);

            }
            UpdateTrangThaiDonHang(id);
           // CapNhatSLTon(nhapHang);
            context.SaveChanges();

            return true;
        }

        private void CapNhatSLTon(PHIEUNHAP_NCC datHang)
        {
            var chiTiets = datHang.CTPHIEUNHAP_NCC.ToList();
            foreach (var chiTiet in chiTiets)
            {
                int sl = chiTiet.SOLUONGNHAP;
                chiTiet.SANPHAM.SoLuongTon += sl;
            }
            
        }


        public void UpdateTrangThaiDonHang(int maDatHang)
        {
            PHIEUDATHANG_NCC datHang = context.PHIEUDATHANG_NCCs.
                FirstOrDefault(t => t.MAPHIEUDAT == maDatHang && t.TRANGTHAI==3);
            
            if (datHang == null)
            {
                return;
            }
            datHang.TRANGTHAI = 2;
            datHang.NGAYDAT = DateTime.Now;

            context.SaveChanges();
        }

        // Chưa sử dụng
        public bool DeleteNhapHangNCC(int maNhapHang)
        {
            PHIEUNHAP_NCC datHangExist = context.PHIEUNHAP_NCCs.FirstOrDefault(t => t.TRANGTHAI == 1 && t.MAPHIEUDAT == maNhapHang);
            if (datHangExist == null)
            {
                return false;
            }
            datHangExist.TRANGTHAI = 0;
            context.SaveChanges();
            return true;
        }
        //Xuất PDF
        #region Declaration
        int _totalColumnDetail = 5;
        int _totalColumn = 4;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable;
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        PhieuNhapHang_NCCViewModel _datHang;
        #endregion
        public byte[] PrepareDatHang(PhieuNhapHang_NCCViewModel datHangs)
        {
            #region
            _datHang = datHangs;
            //_totalColumn = 6;
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);

            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);

            BaseFont bf = BaseFont.CreateFont("C:/windows/fonts/Arial.ttf",
                            BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            _fontStyle = new Font(bf, 12);

            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();

            //render Dathang
            RenderDatHang();

            //render chi tiet dat hang
            RenderChiTietDatHang();

            //add description
            var description = "";
            var para = new Paragraph(description, _fontStyle);
            _document.Add(para);

            // Add signature
            RenderSignature();

            _document.Close();
            return _memoryStream.ToArray();
        }

        private void RenderSignature()
        {
            _pdfTable = new PdfPTable(3)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f,
                SpacingBefore = 10.1f,
            };
            _pdfTable.SetWidths(new float[] { 40f, 30f, 30f });

            //render
            _pdfCell = new PdfPCell(new Phrase("Xác định thanh toán", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Người đặt hàng", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Nhà cung cấp", _fontStyle))
            {
                Colspan = _totalColumn,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
            _document.Add(_pdfTable);
        }

        private void RenderChiTietDatHang()
        {
            _pdfTable = new PdfPTable(_totalColumnDetail)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f,
                SpacingBefore = 10.1f,
            };

            _pdfTable.SetWidths(new float[] { 10f, 40f, 10f, 20f, 20f });
            #endregion
            this.ReportHeader();
            this.ReportBody();
            _document.Add(_pdfTable);
        }
        private void RenderDatHang()
        {

            _pdfTable = new PdfPTable(_totalColumn)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f,
                SpacingBefore = 10.1f,
            };
            _pdfTable.DefaultCell.Border = Rectangle.NO_BORDER;

            _pdfTable.SetWidths(new float[] { 20f, 30f, 20f, 30f });
            _pdfTable.HeaderRows = 2;
            string path = HttpContext.Current.Server.MapPath("~/Content/Images/nha-xinh-logo.jpg");
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
            // add header
            _pdfCell = new PdfPCell(new Phrase("PHIẾU NHẬP HÀNG", _fontStyle))
            {
                Colspan = _totalColumn,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
                ExtraParagraphSpace = 5f,
                PaddingBottom = 5f,
            };
            _pdfTable.AddCell(image);
            _pdfTable.CompleteRow();
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            //add content
            AddRowDatHang("Mã phiếu đặt:");
            AddRowDatHang(_datHang.MaPhieuDat.ToString());
            AddRowDatHang("Ngày nhập:");
            AddRowDatHang(_datHang.NgayNhap?.ToString("dd/MM/yyyy"));
            _pdfTable.CompleteRow();

            
            AddRowDatHang("Người nhập:");
            AddRowDatHang(_datHang.NguoiNhap.ToString());
            AddRowDatHang("Tổng tiền:");
            AddRowDatHang(_datHang.TongTien.ToString());
            _pdfTable.CompleteRow();

            
            AddRowDatHang("Ngày xuất đơn đặt:");
            AddRowDatHang(DateTime.Now.ToString("dd/MM/yyyy"));
            _pdfTable.CompleteRow();

            _document.Add(_pdfTable);
        }

        private void ReportHeader(/*DatHangKHViewModel mode*/)
        {


            //_pdfCell = new PdfPCell(new Phrase("# CHI TIẾT ĐẶT HÀNG", _fontStyle))
            //{
            //    Colspan = _totalColumnDetail,
            //    HorizontalAlignment = Element.ALIGN_LEFT,
            //    Border = 0,
            //    BackgroundColor = BaseColor.WHITE,
            //    ExtraParagraphSpace = 5f,
            //    PaddingBottom= 5f,
            //};
            //_pdfTable.AddCell(_pdfCell);
            //_pdfTable.CompleteRow();
        }
        private void ReportBody()
        {
            #region Table header
            AddHeader("STT");
            //AddHeader("Mã đặt hàng");
            AddHeader("Tên sản phẩm");
            AddHeader("Số lượng");
            AddHeader("Giá nhập");
            AddHeader("Thành tiền");
            _pdfTable.CompleteRow();
            #endregion

            #region Table Body
            int serialNumber = 1;

            foreach (var chiTiet in _datHang.ChiTietPhieuNhaps)
            {
                AddRowChiTietDatHang(serialNumber++.ToString());
                AddRowChiTietDatHang(chiTiet.TenSanPham.ToString());
                AddRowChiTietDatHang(chiTiet.SL.ToString());
                AddRowChiTietDatHang(chiTiet.GiaNhap.ToString());
                AddRowChiTietDatHang(chiTiet.ThanhTien.ToString());
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

        private void AddRowDatHang(string value)
        {
            _pdfCell = new PdfPCell(new Phrase(value, _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.WHITE,
                Border = 0,
            };
            _pdfTable.AddCell(_pdfCell);
        }

        private void AddRowChiTietDatHang(string value)
        {
            _pdfCell = new PdfPCell(new Phrase(value, _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.WHITE,
            };
            _pdfTable.AddCell(_pdfCell);
        }

    }
}