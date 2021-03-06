﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.Helpers;
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
                case "NgayDatHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NgayDat)
                            : model.OrderBy(t => t.NgayDat);
                    break;
                case "TongTien":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TongTien)
                            : model.OrderBy(t => t.TongTien);
                    break;
                case "TongTienSauGiamGia":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TongTienSauGiamGia)
                            : model.OrderBy(t => t.TongTienSauGiamGia);
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
                    model = model.OrderByDescending(t => t.NgayDat); //mặc định sắp xếp giảm dần
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
                    //TongTienSauGiamGia = t.TongTienSauGiamGia,
                    TongTien = t.TongTienSauGiamGia != null && t.TongTienSauGiamGia > 0 ? t.TongTienSauGiamGia.Value : t.TongTien,
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
                      TongTienSauGiamGia = t.TongTienSauGiamGia,
                      KhauTru = t.DiemTichLuy.HasValue ? t.DiemTichLuy.Value * Constant.DiemQuyDoi : 0,
                      TenNguoiNhan = t.TenNguoiNhan,
                      NgayGiao = t.NgayGiao,
                      SDTNguoiDat = t.KHACHHANG.SoDienThoai,
                      GhiChu = t.GhiChu,
                      TrangThai = t.TrangThai == 0 ? TinhTrangDatHang.DaHuy :
                      (t.TrangThai == 1) ? TinhTrangDatHang.DangXyLy : (t.TrangThai == 2)
                        ? TinhTrangDatHang.DangGiao : (t.TrangThai == 3)
                        ? TinhTrangDatHang.DaGiao : TinhTrangDatHang.KhongNhanHang,
                      ChiTietDatHangs = t.CHITIETDATHANGs.Where(k => k.TrangThai != false)
                      .Select(k => new ChiTietDatHangViewModel()
                      {
                          TenSanPham = k.SANPHAM.TenSanPham,
                          MaChiTiet = k.Id_ChiTietDatHang,
                          SoLuong = k.SoLuong,
                          BaoHanh = k.SANPHAM.BaoHanh,
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
            if (trangThai == 2)
            {
                return TinhTrangDatHang.DangGiao;
            }
            if (trangThai == 3)
            {
                return TinhTrangDatHang.DaGiao;
            }
            return TinhTrangDatHang.KhongNhanHang;

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
            if (trangThai == TinhTrangDatHang.DangGiao)
            {
                return 2;
            }
            if (trangThai == TinhTrangDatHang.DaGiao)
            {
                return 3;
            }
            return 4;
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
            if (datHangExist.DiemTichLuy != null || datHangExist.DiemTichLuy != 0)
            {
                datHangExist.KHACHHANG.DiemTichLuy += datHangExist.DiemTichLuy;
            }
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
        public void UpdateTrangThaiDonHang(int maDatHang)
        {
            DATHANG datHang = context.DATHANGs.
                Include(t => t.CHITIETDATHANGs).
                FirstOrDefault(t => t.Id_DatHang == maDatHang && t.TrangThai == 1);
            if (datHang == null)
            {
                return;
            }
            // kiem tra sl ton
            KiemTraSLTon(datHang);
            // update đặt  hàng
            datHang.TrangThai = 2; //Xuất hoá đơn trạng thái ==2: dang giao
            datHang.NgayGiao = DateTime.Now;
            context.Entry(datHang).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();


        }
        //Update đã giao
        public bool UpdateTrangThaiDaGiao(int maDatHang)
        {
            DATHANG datHang = context.DATHANGs.
               Include(t => t.CHITIETDATHANGs).
               FirstOrDefault(t => t.Id_DatHang == maDatHang && t.TrangThai == 2);
            if (datHang == null)
            {
                return false;
            }
            //update SL ton
            UpdateSLTon(datHang);

            datHang.TrangThai = 3;

            //update Diem tich luy
            int diemTichLuy = 0;
            if (datHang.KHACHHANG.TongChi == null)
            {
                datHang.KHACHHANG.TongChi = 0;
            }
            if (datHang.TongTienSauGiamGia == null || datHang.TongTienSauGiamGia == 0)
            {
                // tính điểm tích lũy cho khách hàng và mỗi điểm là một ngàn
                if (datHang.KHACHHANG.Id_LoaiKhachHang == 1)//đăng nhập  là thành viên , và mới đăng nhập là thành viên bình thường 2, vip là 1
                {
                    diemTichLuy = (int)datHang.TongTien / Constant.DiemNhanVIP;
                }
                else
                {
                    diemTichLuy = (int)datHang.TongTien / Constant.DiemNhan;
                }
                datHang.KHACHHANG.TongChi += datHang.TongTien;
            }
            else
            {
                if (datHang.KHACHHANG.Id_LoaiKhachHang == 1)//đăng nhập  là thành viên , và mới đăng nhập là thành viên bình thường 2, vip là 1
                {
                    diemTichLuy = (int)datHang.TongTienSauGiamGia / Constant.DiemNhanVIP;
                }
                else
                {
                    diemTichLuy = (int)datHang.TongTienSauGiamGia / Constant.DiemNhan;
                }
                datHang.KHACHHANG.TongChi += datHang.TongTienSauGiamGia;
            }


            // do cột trong table để là null nên phải gán nó lại không nó mới cộng được
            if (datHang.KHACHHANG.DiemTichLuy == null)
            {
                datHang.KHACHHANG.DiemTichLuy = 0;
            }
            if (datHang.TongTienSauGiamGia == null)
            {
                datHang.TongTienSauGiamGia = 0;
            }
            if (datHang.DiemTichLuy == null)
            {
                datHang.DiemTichLuy = 0;
            }

            datHang.KHACHHANG.DiemTichLuy += diemTichLuy;


            //update Loại khách hàng vì khi nó đủ điểm mới set lên 1 , nên ko cần set trường hợp bằng 2

            if (datHang.KHACHHANG.TongChi >= Constant.TongChiVIP)
            {
                datHang.KHACHHANG.Id_LoaiKhachHang = 1;

            }
            context.SaveChanges();
            return true;

        }

        //update Không nhận hàng
        public bool UpdateTrangThaiKhongNhanHang(int maDatHang)
        {
            DATHANG datHang = context.DATHANGs.
                Include(t => t.CHITIETDATHANGs).
                FirstOrDefault(t => t.Id_DatHang == maDatHang && t.TrangThai == 2);
            if (datHang == null)
            {
                return false;
            }
            // update đặt  hàng
            datHang.TrangThai = 4; //Xuất hoá đơn trạng thái ==4: không nhận
            datHang.GhiChu = "Khách hàng không nhận sản phẩm";
            if (datHang.DiemTichLuy != null || datHang.DiemTichLuy != 0)
            {
                datHang.KHACHHANG.DiemTichLuy += datHang.DiemTichLuy;
                datHang.DiemTichLuy = 0;
            }
            context.Entry(datHang).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;


        }

        //Kiểm tra số lượng tồn
        private void KiemTraSLTon(DATHANG datHang)
        {
            var chiTiets = datHang.CHITIETDATHANGs.Where(x => x.TrangThai != false).ToList();
            foreach (var chiTiet in chiTiets)
            {
                int sl = chiTiet.SoLuong;
                int slTon = chiTiet.SANPHAM.SoLuongTon ?? 0;
                if (sl > slTon)
                {
                    throw new Exception("Số lượng sản phẩm " + chiTiet.SANPHAM.TenSanPham + " không đủ cung cấp");
                }

                //chiTiet.SANPHAM.SoLuongTon = slTon - sl;
            }
        }

        //Cập nhật số lượng tồn
        private void UpdateSLTon(DATHANG datHang)
        {
            var chiTiets = datHang.CHITIETDATHANGs.Where(x => x.TrangThai != false).ToList();
            foreach (var chiTiet in chiTiets)
            {
                int sl = chiTiet.SoLuong;
                int slTon = chiTiet.SANPHAM.SoLuongTon ?? 0;
                chiTiet.SANPHAM.SoLuongTon = slTon - sl;
            }
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

        //Xuất file PDF
        //Định dạng pdf
        #region Declaration
        int _totalColumnDetail = 5;
        int _totalColumn = 4;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable;
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        DatHangKHViewModel _datHang;
        BaseFont bf;
        #endregion
        public byte[] PrepareDatHang(DatHangKHViewModel datHangs)
        {
            #region
            _datHang = datHangs;
            //_totalColumn = 6;
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);

            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);

            bf = BaseFont.CreateFont("C:/windows/fonts/Arial.ttf",
                BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            _fontStyle = new Font(bf, 12);

            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();

            //render Dathang
            RenderDatHang();

            //render chi tiet dat hang
            RenderChiTietDatHang();

            //add description
            var description = "Cảm ơn quý khách đã chọn mua sản phẩm của chúng tôi." +
                " Xin quý khách vui lòng kiểm tra lại tên và thiết bị. Nếu có gì sai sót, " +
                "xin quý khách báo lại cho công ty.";
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
            _pdfCell = new PdfPCell(new Phrase("Xác nhận thanh toán", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Người giao hàng", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Người mua hàng", _fontStyle))
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
            //Thêm hình
            string path = HttpContext.Current.Server.MapPath("~/Content/Images/nha-xinh-logo.jpg");
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
            //add header
            _pdfCell = new PdfPCell(new Phrase("HOÁ ĐƠN GIAO HÀNG", _fontStyle))
            {
                Colspan = _totalColumn,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER,
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

            AddRowDatHang("Mã đặt hàng:");
            AddRowDatHang(_datHang.MaDatHang.ToString());
            AddRowDatHang("Tổng tiền:");
            AddRowDatHang(String.Format("{0:0,00}", _datHang.TongTien));
            _pdfTable.CompleteRow();

            if (_datHang.TongTienSauGiamGia != null && _datHang.TongTienSauGiamGia != 0 && _datHang.KhauTru != null && _datHang.KhauTru != 0)
            {
                AddRowDatHang("Tiền khấu trừ:");
                double KhauTru = _datHang.KhauTru.Value;
                AddRowDatHang(String.Format("{0:0,00}", KhauTru));
                AddRowDatHang("Còn lại :");
                AddRowDatHang(String.Format("{0:0,00}", _datHang.TongTienSauGiamGia));
                _pdfTable.CompleteRow();
            }

            AddRowDatHang("Tên người đặt:");
            AddRowDatHang(_datHang.TenKhachHang);
            AddRowDatHang("SĐT người đặt:");
            AddRowDatHang(_datHang.SDTNguoiDat);
            _pdfTable.CompleteRow();

            if (_datHang.SoDienThoai != _datHang.SDTNguoiDat && !string.IsNullOrWhiteSpace(_datHang.SDTNguoiDat))
            {
                if (!string.IsNullOrWhiteSpace(_datHang.TenNguoiNhan))
                {
                    AddRowDatHang("Người nhận:");
                    AddRowDatHang(_datHang.TenNguoiNhan);
                }
                AddRowDatHang("SĐT người nhận:");
                AddRowDatHang(_datHang.SoDienThoai);
                _pdfTable.CompleteRow();
            }

            AddRowDatHang("Ngày giao:");
            AddRowDatHang(_datHang.NgayGiao?.ToString("dd/MM/yyyy"));
            AddRowDatHang("Địa chỉ giao:");
            AddRowDatHang(_datHang.DiaChiGiao);
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
            AddHeader("Giá bán");
            AddHeader("Thành tiền");
            _pdfTable.CompleteRow();
            #endregion

            #region Table Body
            int serialNumber = 1;

            foreach (var chiTiet in _datHang.ChiTietDatHangs)
            {
                AddRowChiTietDatHang(serialNumber++.ToString());
                //AddRow(chiTiet.MaChiTiet.ToString()); // San pham A (5 tháng); 

                AddRowChiTietDatHang(chiTiet.TenSanPham.Trim() + " (" + chiTiet.BaoHanh + ")");
                AddRowChiTietDatHang(chiTiet.SoLuong.ToString());
                AddRowChiTietDatHang(chiTiet.GiaBan.ToString());
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