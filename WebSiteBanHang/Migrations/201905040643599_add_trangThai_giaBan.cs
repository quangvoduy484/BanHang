namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_trangThai_giaBan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GIASANPHAM", "TrangThai", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GIASANPHAM", "TrangThai");
        }
    }
}
