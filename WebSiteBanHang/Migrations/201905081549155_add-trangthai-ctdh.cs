namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtrangthaictdh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CHITIETDATHANG", "TrangThai", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CHITIETDATHANG", "TrangThai");
        }
    }
}
