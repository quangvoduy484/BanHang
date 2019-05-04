namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SANPHAM", "BaoHanh", c => c.String());
            AlterColumn("dbo.SANPHAM", "DonViTinh", c => c.String());
            AlterColumn("dbo.SANPHAM", "KichThuoc", c => c.String());
            AlterColumn("dbo.SANPHAM", "TrangThai", c => c.Boolean());
            DropColumn("dbo.SANPHAM", "NgayBatDauBaoHanh");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SANPHAM", "NgayBatDauBaoHanh", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SANPHAM", "TrangThai", c => c.Int(nullable: false));
            AlterColumn("dbo.SANPHAM", "KichThuoc", c => c.Double());
            AlterColumn("dbo.SANPHAM", "DonViTinh", c => c.Double());
            DropColumn("dbo.SANPHAM", "BaoHanh");
        }
    }
}
