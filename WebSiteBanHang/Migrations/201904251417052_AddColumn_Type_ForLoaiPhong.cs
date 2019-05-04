namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Type_ForLoaiPhong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LOAIPHONG", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LOAIPHONG", "Type");
        }
    }
}
