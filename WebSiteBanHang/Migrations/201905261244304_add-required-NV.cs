namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredNV : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBL_LOGIN", "PASSWORD", c => c.String(nullable: false));
            AlterColumn("dbo.TBL_LOGIN", "EMAIL", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.TBL_LOGIN", "PHONE", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBL_LOGIN", "PHONE", c => c.String(maxLength: 20));
            AlterColumn("dbo.TBL_LOGIN", "EMAIL", c => c.String(maxLength: 500));
            AlterColumn("dbo.TBL_LOGIN", "PASSWORD", c => c.String());
        }
    }
}
