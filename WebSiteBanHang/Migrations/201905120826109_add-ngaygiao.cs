namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addngaygiao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DATHANG", "NgayGiao", c => c.DateTime());
            AlterColumn("dbo.DATHANG", "DiaChiGiao", c => c.String(nullable: false));
            AlterColumn("dbo.DATHANG", "SoDienThoai", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DATHANG", "SoDienThoai", c => c.String());
            AlterColumn("dbo.DATHANG", "DiaChiGiao", c => c.String());
            DropColumn("dbo.DATHANG", "NgayGiao");
        }
    }
}
