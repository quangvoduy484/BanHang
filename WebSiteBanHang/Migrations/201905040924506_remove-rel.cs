namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LOAISANPHAM", "Id_MauPhong", "dbo.MAUPHONG");
            DropIndex("dbo.LOAISANPHAM", new[] { "Id_MauPhong" });
            AddColumn("dbo.LOAISANPHAM", "Id_LoaiPhong", c => c.Int(nullable: false));
            CreateIndex("dbo.LOAISANPHAM", "Id_LoaiPhong");
            AddForeignKey("dbo.LOAISANPHAM", "Id_LoaiPhong", "dbo.LOAIPHONG", "Id_LoaiPhong", cascadeDelete: true);
            DropColumn("dbo.LOAISANPHAM", "Id_MauPhong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LOAISANPHAM", "Id_MauPhong", c => c.Int(nullable: false));
            DropForeignKey("dbo.LOAISANPHAM", "Id_LoaiPhong", "dbo.LOAIPHONG");
            DropIndex("dbo.LOAISANPHAM", new[] { "Id_LoaiPhong" });
            DropColumn("dbo.LOAISANPHAM", "Id_LoaiPhong");
            CreateIndex("dbo.LOAISANPHAM", "Id_MauPhong");
            AddForeignKey("dbo.LOAISANPHAM", "Id_MauPhong", "dbo.MAUPHONG", "Id_MauPhong", cascadeDelete: true);
        }
    }
}
