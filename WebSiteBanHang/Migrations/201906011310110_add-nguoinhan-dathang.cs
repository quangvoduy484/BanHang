namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnguoinhandathang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DATHANG", "TenNguoiNhan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DATHANG", "TenNguoiNhan");
        }
    }
}
