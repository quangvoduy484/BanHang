namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedb : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.LOAIPHONG", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.LOAIPHONG", "Type");
        }
    }
}
