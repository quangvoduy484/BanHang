namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCTMauPhong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KIEUPHONG", "Id_LoaiPhong", "dbo.LOAIPHONG");
            DropForeignKey("dbo.LOAISANPHAM", "Id_KieuPhong", "dbo.KIEUPHONG");
            DropForeignKey("dbo.TBL_GROUP_LOGIN", "USERNAME", "dbo.TBL_LOGIN");
            DropIndex("dbo.LOAISANPHAM", new[] { "Id_KieuPhong" });
            DropIndex("dbo.KIEUPHONG", new[] { "Id_LoaiPhong" });
            DropIndex("dbo.TBL_GROUP_LOGIN", new[] { "USERNAME" });
            DropPrimaryKey("dbo.TBL_GROUP_LOGIN");
            DropPrimaryKey("dbo.TBL_LOGIN");
            CreateTable(
                "dbo.CHITIETMAUPHONG",
                c => new
                    {
                        Id_ChiTietMauPhong = c.Int(nullable: false, identity: true),
                        Id_SanPham = c.Int(nullable: false),
                        Id_MauPhong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_ChiTietMauPhong)
                .ForeignKey("dbo.MAUPHONG", t => t.Id_MauPhong, cascadeDelete: true)
                .ForeignKey("dbo.SANPHAM", t => t.Id_SanPham, cascadeDelete: true)
                .Index(t => t.Id_SanPham)
                .Index(t => t.Id_MauPhong);
            
            CreateTable(
                "dbo.MAUPHONG",
                c => new
                    {
                        Id_MauPhong = c.Int(nullable: false, identity: true),
                        TenMauPhong = c.String(),
                        HinhAnh = c.String(),
                        TrangThai = c.Boolean(),
                        Id_LoaiPhong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_MauPhong)
                .ForeignKey("dbo.LOAIPHONG", t => t.Id_LoaiPhong, cascadeDelete: true)
                .Index(t => t.Id_LoaiPhong);
            
            AddColumn("dbo.SANPHAM", "XuatXu", c => c.String());
            AddColumn("dbo.LOAISANPHAM", "Id_MauPhong", c => c.Int(nullable: false));
            AlterColumn("dbo.TBL_GROUP_LOGIN", "USERNAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.TBL_LOGIN", "USERNAME", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.TBL_GROUP_LOGIN", new[] { "USERNAME", "ID_GROUP" });
            AddPrimaryKey("dbo.TBL_LOGIN", "USERNAME");
            CreateIndex("dbo.LOAISANPHAM", "Id_MauPhong");
            CreateIndex("dbo.TBL_GROUP_LOGIN", "USERNAME");
            AddForeignKey("dbo.LOAISANPHAM", "Id_MauPhong", "dbo.MAUPHONG", "Id_MauPhong", cascadeDelete: true);
            AddForeignKey("dbo.TBL_GROUP_LOGIN", "USERNAME", "dbo.TBL_LOGIN", "USERNAME");
            DropColumn("dbo.SANPHAM", "XuatSu");
            DropColumn("dbo.LOAISANPHAM", "Id_KieuPhong");
            DropTable("dbo.KIEUPHONG");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id_KieuPhong);
            
            AddColumn("dbo.LOAISANPHAM", "Id_KieuPhong", c => c.Int(nullable: false));
            AddColumn("dbo.SANPHAM", "XuatSu", c => c.String());
            DropForeignKey("dbo.TBL_GROUP_LOGIN", "USERNAME", "dbo.TBL_LOGIN");
            DropForeignKey("dbo.LOAISANPHAM", "Id_MauPhong", "dbo.MAUPHONG");
            DropForeignKey("dbo.CHITIETMAUPHONG", "Id_SanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.MAUPHONG", "Id_LoaiPhong", "dbo.LOAIPHONG");
            DropForeignKey("dbo.CHITIETMAUPHONG", "Id_MauPhong", "dbo.MAUPHONG");
            DropIndex("dbo.TBL_GROUP_LOGIN", new[] { "USERNAME" });
            DropIndex("dbo.LOAISANPHAM", new[] { "Id_MauPhong" });
            DropIndex("dbo.MAUPHONG", new[] { "Id_LoaiPhong" });
            DropIndex("dbo.CHITIETMAUPHONG", new[] { "Id_MauPhong" });
            DropIndex("dbo.CHITIETMAUPHONG", new[] { "Id_SanPham" });
            DropPrimaryKey("dbo.TBL_LOGIN");
            DropPrimaryKey("dbo.TBL_GROUP_LOGIN");
            AlterColumn("dbo.TBL_LOGIN", "USERNAME", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBL_GROUP_LOGIN", "USERNAME", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.LOAISANPHAM", "Id_MauPhong");
            DropColumn("dbo.SANPHAM", "XuatXu");
            DropTable("dbo.MAUPHONG");
            DropTable("dbo.CHITIETMAUPHONG");
            AddPrimaryKey("dbo.TBL_LOGIN", "USERNAME");
            AddPrimaryKey("dbo.TBL_GROUP_LOGIN", new[] { "USERNAME", "ID_GROUP" });
            CreateIndex("dbo.TBL_GROUP_LOGIN", "USERNAME");
            CreateIndex("dbo.KIEUPHONG", "Id_LoaiPhong");
            CreateIndex("dbo.LOAISANPHAM", "Id_KieuPhong");
            AddForeignKey("dbo.TBL_GROUP_LOGIN", "USERNAME", "dbo.TBL_LOGIN", "USERNAME");
            AddForeignKey("dbo.LOAISANPHAM", "Id_KieuPhong", "dbo.KIEUPHONG", "Id_KieuPhong", cascadeDelete: true);
            AddForeignKey("dbo.KIEUPHONG", "Id_LoaiPhong", "dbo.LOAIPHONG", "Id_LoaiPhong", cascadeDelete: true);
        }
    }
}
