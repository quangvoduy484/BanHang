using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using static WebSiteBanHang.Areas.Admin.Helpers.Constant;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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
                model = model.Where(t => t.NGUOIDAT.Contains(search));
                model = model.Where(t => t.NGAYDAT.ToString().Contains(search));
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
                                    (t.TRANGTHAI == 1) ? TinhTrangDatHang.DangXyLy : (t.TRANGTHAI == 2) ?
                                    TinhTrangDatHang.DaNhan : TinhTrangDatHang.DangDat

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
                                    (t.TRANGTHAI == 1) ? TinhTrangDatHang.DangXyLy : (t.TRANGTHAI == 2) ?
                                    TinhTrangDatHang.DangDat : TinhTrangDatHang.DaNhan,
                      ChiTietPhieuDats = t.CT_PHIEUDATNCCs.Where(k=> k.TRANGTHAI !=0)
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
            if (trangThai == TinhTrangDatHang.DaNhan)
            {
                return 2;
            }
            return 3;
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
                    TRANGTHAI = 1,
                };
                context.CT_PHIEUDATNCCs.Add(chiTiet);
            }
            context.SaveChanges();
            return true;
        }

        public bool AddCTPhieuDatHangNCC(int id,PhieuDatHang_NCCViewModel model)
        {
            var tongTien = model.ChiTietPhieuDats.Sum(t => t.SL * t.GiaNhap);
  
            foreach (var detail in model.ChiTietPhieuDats)
            {
                var chiTiet = new CT_PHIEUDATNCC()
                {
                    MAPHIEUDAT = id,
                    MASANPHAM = detail.MaSP,
                    SOLUONG = detail.SL,
                    GIANHAP = detail.GiaNhap,
                    THANHTIEN = detail.SL * detail.GiaNhap,
                    TRANGTHAI = 1,
                };
                context.CT_PHIEUDATNCCs.Add(chiTiet);
            }
            context.SaveChanges();
            return true;
        }

        // Không sử dụng
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

            var datHang = context.PHIEUDATHANG_NCCs
                      .Include(t => t.CT_PHIEUDATNCCs)
                     .FirstOrDefault(t => t.MAPHIEUDAT == CTDHExit.MAPHIEUDAT && t.TRANGTHAI !=0);
            if (datHang == null)
            {
                return false;
            }

            ////////tinh tong tien

            var newTongTien = datHang.CT_PHIEUDATNCCs.Where(t => t.TRANGTHAI == 1).
                Sum(t => t.SOLUONG * t.GIANHAP);

            //update tong tien

            datHang.TONGTIEN = newTongTien;

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
                   .Include(t => t.CT_PHIEUDATNCCs)
                   .FirstOrDefault(t => t.MAPHIEUDAT == CTDHExit.MAPHIEUDAT && t.TRANGTHAI == 1);
            if (datHang == null)
            {
                return false;
            }

            //tinh tong tien

            var newTongTien = datHang.CT_PHIEUDATNCCs.Where(t => t.TRANGTHAI != 0).
                Sum(t => t.SOLUONG * t.GIANHAP);

            //update tong tien

            datHang.TONGTIEN = newTongTien;

            //save
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

        // Xuất file PDF
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

        // Định dạng PDF
        #region Declaration
        int _totalColumnDetail = 5;
        int _totalColumn = 4;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable;
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        PhieuDatHang_NCCViewModel _datHang;
        #endregion
        public byte[] PrepareDatHang(PhieuDatHang_NCCViewModel datHangs)
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

            // add header
            _pdfCell = new PdfPCell(new Phrase("PHIẾU ĐẶT HÀNG", _fontStyle))
            {
                Colspan = _totalColumn,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
                ExtraParagraphSpace = 5f,
                PaddingBottom = 5f,
            };

            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();


            //add content
            AddRowDatHang("Mã phiếu đặt:");
            AddRowDatHang(_datHang.MaPhieuDat.ToString());
            AddRowDatHang("Tên nhà cung cấp:");
            AddRowDatHang(_datHang.TenNCC.ToString());
            _pdfTable.CompleteRow();

            AddRowDatHang("Ngày đặt:");
            AddRowDatHang(_datHang.NgayDat?.ToString("dd/MM/yyyy"));
            AddRowDatHang("Người đặt:");
            AddRowDatHang(_datHang.NguoiDat.ToString());
            _pdfTable.CompleteRow();

            AddRowDatHang("Tổng tiền:");
            AddRowDatHang(_datHang.TongTien.ToString());
            AddRowDatHang("Ngày xuấtđơn đặt:");
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
            AddHeader("Giá đặt");
            AddHeader("Thành tiền");
            _pdfTable.CompleteRow();
            #endregion

            #region Table Body
            int serialNumber = 1;

            foreach (var chiTiet in _datHang.ChiTietPhieuDats)
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