namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyconflic : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.DIACHI",
            //    c => new
            //        {
            //            Id_DiaChi = c.Int(nullable: false, identity: true),
            //            TenKhachHang = c.String(),
            //            DiaChi = c.String(),
            //            SoDienThoai = c.String(),
            //            TrangThai = c.Boolean(nullable: false),
            //            Id_KhachHang = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id_DiaChi)
            //    .ForeignKey("dbo.KHACHHANG", t => t.Id_KhachHang, cascadeDelete: true)
            //    .Index(t => t.Id_KhachHang);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DIACHI", "Id_KhachHang", "dbo.KHACHHANG");
            DropIndex("dbo.DIACHI", new[] { "Id_KhachHang" });
            DropTable("dbo.DIACHI");
        }
    }
}
