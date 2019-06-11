namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Twocolumn_Giamgia_And_Diemtichluy_For_DatHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DATHANG", "TongTienSauGiamGia", c => c.Double());
            AddColumn("dbo.DATHANG", "DiemTichLuy", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DATHANG", "DiemTichLuy");
            DropColumn("dbo.DATHANG", "TongTienSauGiamGia");
        }
    }
}
