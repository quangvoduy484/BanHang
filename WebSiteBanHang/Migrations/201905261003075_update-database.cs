namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CTPHIEUNHAP_NCC", "SOLUONGNHAP", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CTPHIEUNHAP_NCC", "SOLUONGNHAP", c => c.Int());
        }
    }
}
