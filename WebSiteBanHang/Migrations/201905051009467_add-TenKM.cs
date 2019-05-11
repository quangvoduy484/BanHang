namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTenKM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KHUYENMAI", "TenKhuyenMai", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KHUYENMAI", "TenKhuyenMai");
        }
    }
}
