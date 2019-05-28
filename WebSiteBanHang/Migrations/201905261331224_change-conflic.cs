namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeconflic : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DATHANG", "SoDienThoai", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DATHANG", "SoDienThoai", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
