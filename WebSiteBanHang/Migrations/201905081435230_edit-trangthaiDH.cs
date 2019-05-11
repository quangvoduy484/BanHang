namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittrangthaiDH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DATHANG", "TrangThai", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DATHANG", "TrangThai", c => c.Int());
        }
    }
}
