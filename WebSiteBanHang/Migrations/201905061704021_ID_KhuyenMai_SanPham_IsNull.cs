namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ID_KhuyenMai_SanPham_IsNull : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI");
            //DropIndex("dbo.SANPHAM", new[] { "Id_KhuyenMai" });
            //AlterColumn("dbo.SANPHAM", "Id_KhuyenMai", c => c.Int());
            //CreateIndex("dbo.SANPHAM", "Id_KhuyenMai");
            //AddForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI", "Id_KhuyenMai");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI");
            //DropIndex("dbo.SANPHAM", new[] { "Id_KhuyenMai" });
            //AlterColumn("dbo.SANPHAM", "Id_KhuyenMai", c => c.Int(nullable: false));
            //CreateIndex("dbo.SANPHAM", "Id_KhuyenMai");
            //AddForeignKey("dbo.SANPHAM", "Id_KhuyenMai", "dbo.KHUYENMAI", "Id_KhuyenMai", cascadeDelete: true);
        }
    }
}
