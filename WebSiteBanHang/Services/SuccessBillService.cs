using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Services
{
    public class SuccessBillService
    {
        private BanHangContext context = null;
        public SuccessBillService()
        {
            context = new BanHangContext();

        }

        public DatHangKHViewModel GetThongTinDatHang(int id_KH, int idDatHang)
        {

            var address = context.DIACHIs.Where(x => x.Id_KhachHang == id_KH && x.TrangThai == true).SingleOrDefault();
            var datHang = context.DATHANGs
                  .Where(t => t.Id_KhachHang == id_KH && t.Id_DatHang == idDatHang && t.TrangThai == 1)

                  .Select(t => new DatHangKHViewModel()
                  {
                      //Thông tin đơn hàng
                      MaDatHang = t.Id_DatHang,
                      NgayDatHang = t.NgayDat,
                      TongTien = t.TongTien,

                      //Thông tin khách hàng
                      DiaChiGiao = t.DiaChiGiao,
                      SoDienThoai = t.SoDienThoai,
                      TenKhachHang = address.TenKhachHang,
                      TenKhachHangDat = t.KHACHHANG.TenKhachHang,
                      SoDienThoaiDat = t.KHACHHANG.SoDienThoai,
                      Email = t.KHACHHANG.Email,
                      // Thông tin chi tiết đơn đặt hàng
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

        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enanledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;

            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;


            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enanledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);

            //var client = new SmtpClient();
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            //client.EnableSsl = true;
            //client.Host = "smtp.gmail.com";

            //client.Port = 587;
            //client.Send(message);


            






        }
    }
}