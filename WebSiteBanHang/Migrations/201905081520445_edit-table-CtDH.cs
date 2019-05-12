namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittableCtDH : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CHITIETDATHANG", "ThanhTien", c => c.Double(nullable: false));
            AddColumn("dbo.DATHANG", "DiaChiGiao", c => c.String());
            AddColumn("dbo.DATHANG", "SoDienThoai", c => c.String());
            DropColumn("dbo.CHITIETDATHANG", "DiaChiGiao");
            DropColumn("dbo.CHITIETDATHANG", "SoDienThoai");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CHITIETDATHANG", "SoDienThoai", c => c.String());
            AddColumn("dbo.CHITIETDATHANG", "DiaChiGiao", c => c.String());
            DropColumn("dbo.DATHANG", "SoDienThoai");
            DropColumn("dbo.DATHANG", "DiaChiGiao");
            DropColumn("dbo.CHITIETDATHANG", "ThanhTien");
        }
    }
}
