namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CHITIETDATHANG",
                c => new
                    {
                        Id_DatHang = c.Int(nullable: false),
                        Id_SanPham = c.Int(nullable: false),
                        Id_ChiTietDatHang = c.Int(nullable: false, identity: true),
                        SoLuong = c.Int(nullable: false),
                        GiaBan = c.Double(nullable: false),
                        DiaChiGiao = c.String(),
                        SoDienThoai = c.String(),
                    })
                .PrimaryKey(t => t.Id_ChiTietDatHang)
                .ForeignKey("dbo.DATHANG", t => t.Id_DatHang, cascadeDelete: false)
                .ForeignKey("dbo.SANPHAM", t => t.Id_SanPham, cascadeDelete: false)
                .Index(t => t.Id_DatHang)
                .Index(t => t.Id_SanPham);
            
            CreateTable(
                "dbo.DATHANG",
                c => new
                    {
                        Id_DatHang = c.Int(nullable: false, identity: true),
                        NgayDat = c.DateTime(),
                        GhiChu = c.String(),
                        TongTien = c.Double(nullable: false),
                        TrangThai = c.Int(),
                        Id_KhachHang = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_DatHang)
                .ForeignKey("dbo.KHACHHANG", t => t.Id_KhachHang, cascadeDelete: false)
                .Index(t => t.Id_KhachHang);
            
            CreateTable(
                "dbo.HOADONGIAOHANG",
                c => new
                    {
                        Id_DatHang = c.Int(nullable: false),
                        Id_KhachHang = c.Int(nullable: false),
                        Id_HOADONGIAOHANG = c.Int(nullable: false, identity: true),
                        NgayGiao = c.DateTime(),
                        TrangThai = c.Int(),
                        TongTien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id_HOADONGIAOHANG)
                .ForeignKey("dbo.KHACHHANG", t => t.Id_KhachHang, cascadeDelete: false)
                .ForeignKey("dbo.DATHANG", t => t.Id_DatHang, cascadeDelete: false)
                .Index(t => t.Id_DatHang)
                .Index(t => t.Id_KhachHang);
            
            CreateTable(
                "dbo.CHITIETPHIEUGIAO",
                c => new
                    {
                        Id_HOADONGIAOHANG = c.Int(nullable: false),
                        Id_SanPham = c.Int(nullable: false),
                        Id_ChiTietPhieuGiao = c.Int(nullable: false, identity: true),
                        SoLuongGiao = c.Int(nullable: false),
                        GiaBan = c.Double(nullable: false),
                        ThanhTien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id_ChiTietPhieuGiao)
                .ForeignKey("dbo.HOADONGIAOHANG", t => t.Id_HOADONGIAOHANG, cascadeDelete: false)
                .ForeignKey("dbo.SANPHAM", t => t.Id_SanPham, cascadeDelete: false)
                .Index(t => t.Id_HOADONGIAOHANG)
                .Index(t => t.Id_SanPham);
            
            CreateTable(
                "dbo.SANPHAM",
                c => new
                    {
                        Id_LoaiSanPham = c.Int(nullable: false),
                        Id_KhuyenMai = c.Int(nullable: false),
                        Id_SanPham = c.Int(nullable: false, identity: true),
                        TenSanPham = c.String(),
                        DonViTinh = c.Double(),
                        MauSac = c.String(),
                        VatLieu = c.String(),
                        KichThuoc = c.Double(),
                        HinhAnh = c.String(),
                        Mota = c.String(),
                        TrangThai = c.Int(nullable: false),
                        NgayBatDauBaoHanh = c.DateTime(nullable: false),
                        SoLuongTon = c.Int(nullable: false),
                        XuatSu = c.String(),
                        CREATED_BY = c.String(maxLength: 20),
                        CREATED_DATE = c.DateTime(),
                        UPDATED_BY = c.String(maxLength: 20),
                        UPDATED_DATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id_SanPham)
                .ForeignKey("dbo.KHUYENMAI", t => t.Id_KhuyenMai, cascadeDelete: false)
                .ForeignKey("dbo.LOAISANPHAM", t => t.Id_LoaiSanPham, cascadeDelete: false)
                .Index(t => t.Id_LoaiSanPham)
                .Index(t => t.Id_KhuyenMai);
            
            CreateTable(
                "dbo.DANHGIA",
                c => new
                    {
                        Id_KhachHang = c.Int(nullable: false),
                        Id_SanPham = c.Int(nullable: false),
                        Id_DanhGia = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        SoSao = c.Int(nullable: false),
                        NgayDanhGia = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id_DanhGia)
                .ForeignKey("dbo.KHACHHANG", t => t.Id_KhachHang, cascadeDelete: false)
                .ForeignKey("dbo.SANPHAM", t => t.Id_SanPham, cascadeDelete: false)
                .Index(t => t.Id_KhachHang)
                .Index(t => t.Id_SanPham);
            
            CreateTable(
                "dbo.KHACHHANG",
                c => new
                    {
                        Id_KhachHang = c.Int(nullable: false, identity: true),
                        TenKhachHang = c.String(),
                        DiaChi = c.String(),
                        NgaySinh = c.DateTime(),
                        SoDienThoai = c.String(),
                        Email = c.String(),
                        PassWord = c.String(),
                        DiemTichLuy = c.Double(),
                        TongChi = c.Double(),
                        TrangThai = c.Boolean(),
                        Id_LoaiKhachHang = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_KhachHang)
                .ForeignKey("dbo.LOAIKHACHHANG", t => t.Id_LoaiKhachHang, cascadeDelete: false)
                .Index(t => t.Id_LoaiKhachHang);
            
            CreateTable(
                "dbo.LOAIKHACHHANG",
                c => new
                    {
                        Id_LoaiKhachHang = c.Int(nullable: false, identity: true),
                        TenKhachHang = c.String(),
                    })
                .PrimaryKey(t => t.Id_LoaiKhachHang);
            
            CreateTable(
                "dbo.GIASANPHAM",
                c => new
                    {
                        Id_GiaSanPham = c.Int(nullable: false, identity: true),
                        NgayLap = c.DateTime(),
                        GiaBan = c.Double(nullable: false),
                        Id_SanPham = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_GiaSanPham)
                .ForeignKey("dbo.SANPHAM", t => t.Id_SanPham, cascadeDelete: false)
                .Index(t => t.Id_SanPham);
            
            CreateTable(
                "dbo.HINH",
                c => new
                    {
                        Id_Hinh = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                        Id_SanPham = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Hinh)
                .ForeignKey("dbo.SANPHAM", t => t.Id_SanPham, cascadeDelete: false)
                .Index(t => t.Id_SanPham);
            
            CreateTable(
                "dbo.KHUYENMAI",
                c => new
                    {
                        Id_KhuyenMai = c.Int(nullable: false, identity: true),
                        NgayBatDau = c.DateTime(),
                        NgayKetThuc = c.DateTime(),
                        GiaTriKhuyenMai = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id_KhuyenMai);
            
            CreateTable(
                "dbo.LOAISANPHAM",
                c => new
                    {
                        Id_LoaiSanPham = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(),
                        Id_KieuPhong = c.Int(nullable: false),
                        TrangThai = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id_LoaiSanPham)
                .ForeignKey("dbo.KIEUPHONG", t => t.Id_KieuPhong, cascadeDelete: false)
                .Index(t => t.Id_KieuPhong);
            
            CreateTable(
                "dbo.KIEUPHONG",
                c => new
                    {
                        Id_KieuPhong = c.Int(nullable: false, identity: true),
                        TenKieuPhong = c.String(),
                        HinhAnh = c.String(),
                        TrangThai = c.Boolean(),
                        Id_LoaiPhong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_KieuPhong)
                .ForeignKey("dbo.LOAIPHONG", t => t.Id_LoaiPhong, cascadeDelete: false)
                .Index(t => t.Id_LoaiPhong);
            
            CreateTable(
                "dbo.LOAIPHONG",
                c => new
                    {
                        Id_LoaiPhong = c.Int(nullable: false, identity: true),
                        TenLoaiPhong = c.String(),
                        HinhAnh = c.String(),
                        MoTa = c.String(),
                        TenNoiThat = c.String(),
                        TrangThai = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id_LoaiPhong);
            
            CreateTable(
                "dbo.TBL_GROUP_LOGIN",
                c => new
                    {
                        USERNAME = c.String(nullable: false, maxLength: 128),
                        ID_GROUP = c.Int(nullable: false),
                        ACTIVATE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.USERNAME, t.ID_GROUP })
                .ForeignKey("dbo.TBL_GROUP", t => t.ID_GROUP)
                .ForeignKey("dbo.TBL_LOGIN", t => t.USERNAME)
                .Index(t => t.USERNAME)
                .Index(t => t.ID_GROUP);
            
            CreateTable(
                "dbo.TBL_GROUP",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GROUPNAME = c.String(),
                        GROUPDESC = c.String(),
                        ACTIVATE = c.Boolean(nullable: false),
                        CREATED_BY = c.String(maxLength: 20),
                        CREATED_DATE = c.DateTime(),
                        UPDATED_BY = c.String(maxLength: 20),
                        UPDATED_DATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TBL_GROUP_ROLE",
                c => new
                    {
                        ID_GROUP = c.Int(nullable: false),
                        ID_ROLE = c.Int(nullable: false),
                        ACTIVATE = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.ID_GROUP, t.ID_ROLE })
                .ForeignKey("dbo.TBL_ROLE", t => t.ID_ROLE)
                .ForeignKey("dbo.TBL_GROUP", t => t.ID_GROUP)
                .Index(t => t.ID_GROUP)
                .Index(t => t.ID_ROLE);
            
            CreateTable(
                "dbo.TBL_ROLE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ROLE_NAME = c.String(maxLength: 200),
                        ROLE_DEF = c.String(maxLength: 500),
                        ROLE_LINK = c.String(),
                        ACTIVATE = c.Boolean(nullable: false),
                        CREATED_BY = c.String(maxLength: 20),
                        CREATED_DATE = c.DateTime(),
                        UPDATED_BY = c.String(maxLength: 20),
                        UPDATED_DATE = c.DateTime(),
                        Inactive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TBL_MENU",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MENU_NAME = c.String(),
                        MENU_SEQ = c.Int(),
                        MENU_PARENT = c.Int(),
                        ACTIVATE = c.Boolean(),
                        MENU_ICON = c.String(),
                        ID_ROLE = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TBL_ROLE", t => t.ID_ROLE)
                .Index(t => t.ID_ROLE);
            
            CreateTable(
                "dbo.TBL_LOGIN",
                c => new
                    {
                        USERNAME = c.String(nullable: false, maxLength: 128),
                        PASSWORD = c.String(),
                        EMAIL = c.String(maxLength: 500),
                        PHONE = c.String(maxLength: 20),
                        TYPE = c.Boolean(),
                        ISADMIN = c.Boolean(nullable: false),
                        ACTIVATE = c.Boolean(nullable: false),
                        CREATED_BY = c.String(maxLength: 20),
                        CREATED_DATE = c.DateTime(),
                        UPDATED_BY = c.String(maxLength: 20),
                        UPDATED_DATE = c.DateTime(),
                        Inactive = c.Boolean(),
                    })
                .PrimaryKey(t => t.USERNAME);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBL_GROUP_LOGIN", "USERNAME", "dbo.TBL_LOGIN");
            DropForeignKey("dbo.TBL_GROUP_ROLE", "ID_GROUP", "dbo.TBL_GROUP");
            DropForeignKey("dbo.TBL_MENU", "ID_ROLE", "dbo.TBL_ROLE");
            DropForeignKey("dbo.TBL_GROUP_ROLE", "ID_ROLE", "dbo.TBL_ROLE");
            DropForeignKey("dbo.TBL_GROUP_LOGIN", "ID_GROUP", "dbo.TBL_GROUP");
            DropForeignKey("dbo.HOADONGIAOHANG", "Id_DatHang", "dbo.DATHANG");
            DropForeignKey("dbo.SANPHAM", "Id_LoaiSanPham", "dbo.LOAISANPHAM");
            DropForeignKey("dbo.LOAISANPHAM", "Id_KieuPhong", "dbo.KIEUPHONG");
            DropForeignKey("dbo.KIEUPHONG", "Id_LoaiPhong", "dbo.LOAIPHONG");
            DropForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI");
            DropForeignKey("dbo.HINH", "Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.GIASANPHAM", "Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.DANHGIA", "Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.KHACHHANG", "Id_LoaiKhachHang", "dbo.LOAIKHACHHANG");
            DropForeignKey("dbo.HOADONGIAOHANG", "Id_KhachHang", "dbo.KHACHHANG");
            DropForeignKey("dbo.DATHANG", "Id_KhachHang", "dbo.KHACHHANG");
            DropForeignKey("dbo.DANHGIA", "Id_KhachHang", "dbo.KHACHHANG");
            DropForeignKey("dbo.CHITIETPHIEUGIAO", "Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.CHITIETDATHANG", "Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.CHITIETPHIEUGIAO", "Id_HOADONGIAOHANG", "dbo.HOADONGIAOHANG");
            DropForeignKey("dbo.CHITIETDATHANG", "Id_DatHang", "dbo.DATHANG");
            DropIndex("dbo.TBL_MENU", new[] { "ID_ROLE" });
            DropIndex("dbo.TBL_GROUP_ROLE", new[] { "ID_ROLE" });
            DropIndex("dbo.TBL_GROUP_ROLE", new[] { "ID_GROUP" });
            DropIndex("dbo.TBL_GROUP_LOGIN", new[] { "ID_GROUP" });
            DropIndex("dbo.TBL_GROUP_LOGIN", new[] { "USERNAME" });
            DropIndex("dbo.KIEUPHONG", new[] { "Id_LoaiPhong" });
            DropIndex("dbo.LOAISANPHAM", new[] { "Id_KieuPhong" });
            DropIndex("dbo.HINH", new[] { "Id_SanPham" });
            DropIndex("dbo.GIASANPHAM", new[] { "Id_SanPham" });
            DropIndex("dbo.KHACHHANG", new[] { "Id_LoaiKhachHang" });
            DropIndex("dbo.DANHGIA", new[] { "Id_SanPham" });
            DropIndex("dbo.DANHGIA", new[] { "Id_KhachHang" });
            DropIndex("dbo.SANPHAM", new[] { "Id_KhuyenMai" });
            DropIndex("dbo.SANPHAM", new[] { "Id_LoaiSanPham" });
            DropIndex("dbo.CHITIETPHIEUGIAO", new[] { "Id_SanPham" });
            DropIndex("dbo.CHITIETPHIEUGIAO", new[] { "Id_HOADONGIAOHANG" });
            DropIndex("dbo.HOADONGIAOHANG", new[] { "Id_KhachHang" });
            DropIndex("dbo.HOADONGIAOHANG", new[] { "Id_DatHang" });
            DropIndex("dbo.DATHANG", new[] { "Id_KhachHang" });
            DropIndex("dbo.CHITIETDATHANG", new[] { "Id_SanPham" });
            DropIndex("dbo.CHITIETDATHANG", new[] { "Id_DatHang" });
            DropTable("dbo.TBL_LOGIN");
            DropTable("dbo.TBL_MENU");
            DropTable("dbo.TBL_ROLE");
            DropTable("dbo.TBL_GROUP_ROLE");
            DropTable("dbo.TBL_GROUP");
            DropTable("dbo.TBL_GROUP_LOGIN");
            DropTable("dbo.LOAIPHONG");
            DropTable("dbo.KIEUPHONG");
            DropTable("dbo.LOAISANPHAM");
            DropTable("dbo.KHUYENMAI");
            DropTable("dbo.HINH");
            DropTable("dbo.GIASANPHAM");
            DropTable("dbo.LOAIKHACHHANG");
            DropTable("dbo.KHACHHANG");
            DropTable("dbo.DANHGIA");
            DropTable("dbo.SANPHAM");
            DropTable("dbo.CHITIETPHIEUGIAO");
            DropTable("dbo.HOADONGIAOHANG");
            DropTable("dbo.DATHANG");
            DropTable("dbo.CHITIETDATHANG");
        }
    }
}
