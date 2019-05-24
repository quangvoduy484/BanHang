namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Coulumn_TrangThai_For_DIACHI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DIACHI", "TrangThai", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DIACHI", "TrangThai");
        }
    }
}
