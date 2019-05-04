namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Table_SanPham : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI");
            DropIndex("dbo.SANPHAM", new[] { "Id_KhuyenMai" });
            AlterColumn("dbo.SANPHAM", "Id_KhuyenMai", c => c.Int());
            AlterColumn("dbo.SANPHAM", "DonViTinh", c => c.String());
            AlterColumn("dbo.SANPHAM", "KichThuoc", c => c.String());
            CreateIndex("dbo.SANPHAM", "Id_KhuyenMai");
            AddForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI", "Id_KhuyenMai");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI");
            DropIndex("dbo.SANPHAM", new[] { "Id_KhuyenMai" });
            AlterColumn("dbo.SANPHAM", "KichThuoc", c => c.Double());
            AlterColumn("dbo.SANPHAM", "DonViTinh", c => c.Double());
            AlterColumn("dbo.SANPHAM", "Id_KhuyenMai", c => c.Int(nullable: false));
            CreateIndex("dbo.SANPHAM", "Id_KhuyenMai");
            AddForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI", "Id_KhuyenMai", cascadeDelete: true);
        }
    }
}
