namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifydata : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.CT_PHIEUDATNCC",
            //    c => new
            //        {
            //            MACTPD = c.Int(nullable: false, identity: true),
            //            MAPHIEUDAT = c.Int(nullable: false),
            //            MASANPHAM = c.Int(nullable: false),
            //            SOLUONG = c.Int(),
            //            NGUOIDAT = c.String(),
            //            TBL_LOGIN_USERNAME = c.String(maxLength: 20),
            //            SANPHAM_Id_SanPham = c.Int(),
            //        })
            //    .PrimaryKey(t => t.MACTPD)
            //    .ForeignKey("dbo.PHIEUDATHANG_NCC", t => t.MAPHIEUDAT, cascadeDelete: true)
            //    .ForeignKey("dbo.TBL_LOGIN", t => t.TBL_LOGIN_USERNAME)
            //    .ForeignKey("dbo.SANPHAM", t => t.SANPHAM_Id_SanPham)
            //    .Index(t => t.MAPHIEUDAT)
            //    .Index(t => t.TBL_LOGIN_USERNAME)
            //    .Index(t => t.SANPHAM_Id_SanPham);
            
            //CreateTable(
            //    "dbo.PHIEUDATHANG_NCC",
            //    c => new
            //        {
            //            MAPHIEUDAT = c.Int(nullable: false, identity: true),
            //            MANCC = c.Int(nullable: false),
            //            NGAYDAT = c.DateTime(),
            //            GHICHU = c.String(maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.MAPHIEUDAT)
            //    .ForeignKey("dbo.NHACUNGCAP", t => t.MANCC, cascadeDelete: true)
            //    .Index(t => t.MANCC);
            
            //CreateTable(
            //    "dbo.NHACUNGCAP",
            //    c => new
            //        {
            //            MANCC = c.Int(nullable: false, identity: true),
            //            TENNCC = c.String(nullable: false, maxLength: 100),
            //            DIACHI = c.String(maxLength: 100),
            //            SODIENTHOAI = c.String(maxLength: 10),
            //            TRANGTHAI = c.Boolean(),
            //        })
            //    .PrimaryKey(t => t.MANCC);
            
            //CreateTable(
            //    "dbo.PHIEUNHAP_NCC",
            //    c => new
            //        {
            //            MAPHIEUNHAP = c.Int(nullable: false, identity: true),
            //            MAPHIEUDAT = c.Int(nullable: false),
            //            MANHANVIEN = c.Int(nullable: false),
            //            NGAYNHAP = c.DateTime(),
            //            TONGTIEN = c.Decimal(precision: 18, scale: 2),
            //            GHICHU = c.String(maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.MAPHIEUNHAP)
            //    .ForeignKey("dbo.PHIEUDATHANG_NCC", t => t.MAPHIEUDAT, cascadeDelete: true)
            //    .Index(t => t.MAPHIEUDAT);
            
            //CreateTable(
            //    "dbo.CTPHIEUNHAP_NCC",
            //    c => new
            //        {
            //            MACTPN = c.Int(nullable: false, identity: true),
            //            MAPHIEUNHAP = c.Int(nullable: false),
            //            MASANPHAM = c.Int(nullable: false),
            //            SOLUONGNHAP = c.Int(),
            //            GIANHAP = c.Decimal(precision: 18, scale: 2),
            //            THANHTIEN = c.Decimal(precision: 18, scale: 2),
            //            NGUOINHAP = c.String(),
            //            SANPHAM_Id_SanPham = c.Int(),
            //            TBL_LOGIN_USERNAME = c.String(maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.MACTPN)
            //    .ForeignKey("dbo.PHIEUNHAP_NCC", t => t.MAPHIEUNHAP, cascadeDelete: true)
            //    .ForeignKey("dbo.SANPHAM", t => t.SANPHAM_Id_SanPham)
            //    .ForeignKey("dbo.TBL_LOGIN", t => t.TBL_LOGIN_USERNAME)
            //    .Index(t => t.MAPHIEUNHAP)
            //    .Index(t => t.SANPHAM_Id_SanPham)
            //    .Index(t => t.TBL_LOGIN_USERNAME);
            
            //AddColumn("dbo.CHITIETDATHANG", "ThanhTien", c => c.Double(nullable: false));
            //AddColumn("dbo.CHITIETDATHANG", "TrangThai", c => c.Boolean());
            //AddColumn("dbo.DATHANG", "DiaChiGiao", c => c.String());
            //AddColumn("dbo.DATHANG", "SoDienThoai", c => c.String());
            //AlterColumn("dbo.DATHANG", "TrangThai", c => c.Int(nullable: false));
            //DropColumn("dbo.CHITIETDATHANG", "DiaChiGiao");
            //DropColumn("dbo.CHITIETDATHANG", "SoDienThoai");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.CHITIETDATHANG", "SoDienThoai", c => c.String());
            //AddColumn("dbo.CHITIETDATHANG", "DiaChiGiao", c => c.String());
            //DropForeignKey("dbo.CT_PHIEUDATNCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM");
            //DropForeignKey("dbo.PHIEUNHAP_NCC", "MAPHIEUDAT", "dbo.PHIEUDATHANG_NCC");
            //DropForeignKey("dbo.CT_PHIEUDATNCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN");
            //DropForeignKey("dbo.CTPHIEUNHAP_NCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN");
            //DropForeignKey("dbo.CTPHIEUNHAP_NCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM");
            //DropForeignKey("dbo.CTPHIEUNHAP_NCC", "MAPHIEUNHAP", "dbo.PHIEUNHAP_NCC");
            //DropForeignKey("dbo.PHIEUDATHANG_NCC", "MANCC", "dbo.NHACUNGCAP");
            //DropForeignKey("dbo.CT_PHIEUDATNCC", "MAPHIEUDAT", "dbo.PHIEUDATHANG_NCC");
            //DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "TBL_LOGIN_USERNAME" });
            //DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "SANPHAM_Id_SanPham" });
            //DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "MAPHIEUNHAP" });
            //DropIndex("dbo.PHIEUNHAP_NCC", new[] { "MAPHIEUDAT" });
            //DropIndex("dbo.PHIEUDATHANG_NCC", new[] { "MANCC" });
            //DropIndex("dbo.CT_PHIEUDATNCC", new[] { "SANPHAM_Id_SanPham" });
            //DropIndex("dbo.CT_PHIEUDATNCC", new[] { "TBL_LOGIN_USERNAME" });
            //DropIndex("dbo.CT_PHIEUDATNCC", new[] { "MAPHIEUDAT" });
            //AlterColumn("dbo.DATHANG", "TrangThai", c => c.Int());
            //DropColumn("dbo.DATHANG", "SoDienThoai");
            //DropColumn("dbo.DATHANG", "DiaChiGiao");
            //DropColumn("dbo.CHITIETDATHANG", "TrangThai");
            //DropColumn("dbo.CHITIETDATHANG", "ThanhTien");
            //DropTable("dbo.CTPHIEUNHAP_NCC");
            //DropTable("dbo.PHIEUNHAP_NCC");
            //DropTable("dbo.NHACUNGCAP");
            //DropTable("dbo.PHIEUDATHANG_NCC");
            //DropTable("dbo.CT_PHIEUDATNCC");
        }
    }
}
