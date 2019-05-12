namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredtenloai : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LOAISANPHAM", "TenLoai", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LOAISANPHAM", "TenLoai", c => c.String());
        }
    }
}
