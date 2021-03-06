namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNCC : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI");
            DropIndex("dbo.SANPHAM", new[] { "Id_KhuyenMai" });
            CreateTable(
                "dbo.CTPHIEUNHAP_NCC",
                c => new
                    {
                        MACTPN = c.Int(nullable: false, identity: true),
                        MAPHIEUNHAP = c.Int(nullable: false),
                        MASANPHAM = c.Int(nullable: false),
                        SOLUONGNHAP = c.Int(),
                        GIANHAP = c.Decimal(precision: 18, scale: 2),
                        THANHTIEN = c.Decimal(precision: 18, scale: 2),
                        NGUOINHAP = c.String(),
                        SANPHAM_Id_SanPham = c.Int(),
                        TBL_LOGIN_USERNAME = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.MACTPN)
                .ForeignKey("dbo.PHIEUNHAP_NCC", t => t.MAPHIEUNHAP, cascadeDelete: true)
                .ForeignKey("dbo.SANPHAM", t => t.SANPHAM_Id_SanPham)
                .ForeignKey("dbo.TBL_LOGIN", t => t.TBL_LOGIN_USERNAME)
                .Index(t => t.MAPHIEUNHAP)
                .Index(t => t.SANPHAM_Id_SanPham)
                .Index(t => t.TBL_LOGIN_USERNAME);
            
            CreateTable(
                "dbo.PHIEUNHAP_NCC",
                c => new
                    {
                        MAPHIEUNHAP = c.Int(nullable: false, identity: true),
                        MAPHIEUDAT = c.Int(nullable: false),
                        MANHANVIEN = c.Int(nullable: false),
                        NGAYNHAP = c.DateTime(),
                        TONGTIEN = c.Decimal(precision: 18, scale: 2),
                        GHICHU = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MAPHIEUNHAP)
                .ForeignKey("dbo.PHIEUDATHANG_NCC", t => t.MAPHIEUDAT, cascadeDelete: true)
                .Index(t => t.MAPHIEUDAT);
            
            CreateTable(
                "dbo.PHIEUDATHANG_NCC",
                c => new
                    {
                        MAPHIEUDAT = c.Int(nullable: false, identity: true),
                        MANCC = c.Int(nullable: false),
                        NGAYDAT = c.DateTime(),
                        GHICHU = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MAPHIEUDAT)
                .ForeignKey("dbo.NHACUNGCAP", t => t.MANCC, cascadeDelete: true)
                .Index(t => t.MANCC);
            
            CreateTable(
                "dbo.CT_PHIEUDATNCC",
                c => new
                    {
                        MACTPD = c.Int(nullable: false, identity: true),
                        MAPHIEUDAT = c.Int(nullable: false),
                        MASANPHAM = c.Int(nullable: false),
                        SOLUONG = c.Int(),
                        NGUOIDAT = c.String(),
                        SANPHAM_Id_SanPham = c.Int(),
                        TBL_LOGIN_USERNAME = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.MACTPD)
                .ForeignKey("dbo.PHIEUDATHANG_NCC", t => t.MAPHIEUDAT, cascadeDelete: true)
                .ForeignKey("dbo.SANPHAM", t => t.SANPHAM_Id_SanPham)
                .ForeignKey("dbo.TBL_LOGIN", t => t.TBL_LOGIN_USERNAME)
                .Index(t => t.MAPHIEUDAT)
                .Index(t => t.SANPHAM_Id_SanPham)
                .Index(t => t.TBL_LOGIN_USERNAME);
            
            CreateTable(
                "dbo.NHACUNGCAP",
                c => new
                    {
                        MANCC = c.Int(nullable: false, identity: true),
                        TENNCC = c.String(nullable: false, maxLength: 100),
                        DIACHI = c.String(maxLength: 100),
                        SODIENTHOAI = c.String(maxLength: 10),
                        TRANGTHAI = c.Boolean(),
                    })
                .PrimaryKey(t => t.MANCC);
            
            AlterColumn("dbo.SANPHAM", "Id_KhuyenMai", c => c.Int(nullable: true));
            AlterColumn("dbo.SANPHAM", "SoLuongTon", c => c.Int(nullable: true));
            CreateIndex("dbo.SANPHAM", "Id_KhuyenMai");
            AddForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI", "Id_KhuyenMai", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI");
            DropForeignKey("dbo.CTPHIEUNHAP_NCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN");
            DropForeignKey("dbo.CTPHIEUNHAP_NCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.PHIEUNHAP_NCC", "MAPHIEUDAT", "dbo.PHIEUDATHANG_NCC");
            DropForeignKey("dbo.PHIEUDATHANG_NCC", "MANCC", "dbo.NHACUNGCAP");
            DropForeignKey("dbo.CT_PHIEUDATNCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN");
            DropForeignKey("dbo.CT_PHIEUDATNCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.CT_PHIEUDATNCC", "MAPHIEUDAT", "dbo.PHIEUDATHANG_NCC");
            DropForeignKey("dbo.CTPHIEUNHAP_NCC", "MAPHIEUNHAP", "dbo.PHIEUNHAP_NCC");
            DropIndex("dbo.CT_PHIEUDATNCC", new[] { "TBL_LOGIN_USERNAME" });
            DropIndex("dbo.CT_PHIEUDATNCC", new[] { "SANPHAM_Id_SanPham" });
            DropIndex("dbo.CT_PHIEUDATNCC", new[] { "MAPHIEUDAT" });
            DropIndex("dbo.PHIEUDATHANG_NCC", new[] { "MANCC" });
            DropIndex("dbo.PHIEUNHAP_NCC", new[] { "MAPHIEUDAT" });
            DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "TBL_LOGIN_USERNAME" });
            DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "SANPHAM_Id_SanPham" });
            DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "MAPHIEUNHAP" });
            DropIndex("dbo.SANPHAM", new[] { "Id_KhuyenMai" });
            AlterColumn("dbo.SANPHAM", "SoLuongTon", c => c.Int());
            AlterColumn("dbo.SANPHAM", "Id_KhuyenMai", c => c.Int());
            DropTable("dbo.NHACUNGCAP");
            DropTable("dbo.CT_PHIEUDATNCC");
            DropTable("dbo.PHIEUDATHANG_NCC");
            DropTable("dbo.PHIEUNHAP_NCC");
            DropTable("dbo.CTPHIEUNHAP_NCC");
            CreateIndex("dbo.SANPHAM", "Id_KhuyenMai");
            AddForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI", "Id_KhuyenMai");
        }
    }
}
