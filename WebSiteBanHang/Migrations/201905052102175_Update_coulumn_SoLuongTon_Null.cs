namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_coulumn_SoLuongTon_Null : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SANPHAM", "SoLuongTon", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SANPHAM", "SoLuongTon", c => c.Int(nullable: false));
        }
    }
}
