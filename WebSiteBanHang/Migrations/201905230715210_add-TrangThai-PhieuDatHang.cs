namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTrangThaiPhieuDatHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CT_PHIEUDATNCC", "TRANGTHAI", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CT_PHIEUDATNCC", "TRANGTHAI");
        }
    }
}
