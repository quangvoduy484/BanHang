namespace WebSiteBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedvb : DbMigration
    {
        public override void Up()
        {

        //    DropForeignKey("dbo.CTPHIEUNHAP_NCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN");
        //    DropForeignKey("dbo.CT_PHIEUDATNCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN");
        //    DropForeignKey("dbo.CT_PHIEUDATNCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM");
        //    DropForeignKey("dbo.CTPHIEUNHAP_NCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM");
        //    DropIndex("dbo.CT_PHIEUDATNCC", new[] { "TBL_LOGIN_USERNAME" });
        //    DropIndex("dbo.CT_PHIEUDATNCC", new[] { "SANPHAM_Id_SanPham" });
        //    DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "SANPHAM_Id_SanPham" });
        //    DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "TBL_LOGIN_USERNAME" });
        //    DropColumn("dbo.CT_PHIEUDATNCC", "MASANPHAM");
        //    DropColumn("dbo.CTPHIEUNHAP_NCC", "MASANPHAM");
        //    RenameColumn(table: "dbo.CT_PHIEUDATNCC", name: "SANPHAM_Id_SanPham", newName: "MASANPHAM");
        //    RenameColumn(table: "dbo.CTPHIEUNHAP_NCC", name: "SANPHAM_Id_SanPham", newName: "MASANPHAM");
        //    AddColumn("dbo.CT_PHIEUDATNCC", "GIANHAP", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        //    AddColumn("dbo.CT_PHIEUDATNCC", "THANHTIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        //    AddColumn("dbo.PHIEUDATHANG_NCC", "TRANGTHAI", c => c.Int(nullable: false));
        //    AddColumn("dbo.PHIEUDATHANG_NCC", "NGUOIDAT", c => c.String(maxLength: 20));
        //    AddColumn("dbo.PHIEUDATHANG_NCC", "TONGTIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        //    AddColumn("dbo.PHIEUNHAP_NCC", "TRANGTHAI", c => c.Int(nullable: false));
        //    AddColumn("dbo.PHIEUNHAP_NCC", "NGUOINHAP", c => c.String(maxLength: 20));
        //    AlterColumn("dbo.CT_PHIEUDATNCC", "SOLUONG", c => c.Int(nullable: false));
        //    AlterColumn("dbo.CT_PHIEUDATNCC", "MASANPHAM", c => c.Int(nullable: false));
        //    AlterColumn("dbo.PHIEUNHAP_NCC", "TONGTIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        //    AlterColumn("dbo.CTPHIEUNHAP_NCC", "MASANPHAM", c => c.Int(nullable: false));
        //    CreateIndex("dbo.CT_PHIEUDATNCC", "MASANPHAM");
        //    CreateIndex("dbo.PHIEUDATHANG_NCC", "NGUOIDAT");
        //    CreateIndex("dbo.PHIEUNHAP_NCC", "NGUOINHAP");
        //    CreateIndex("dbo.CTPHIEUNHAP_NCC", "MASANPHAM");
        //    AddForeignKey("dbo.PHIEUDATHANG_NCC", "NGUOIDAT", "dbo.TBL_LOGIN", "USERNAME");
        //    AddForeignKey("dbo.PHIEUNHAP_NCC", "NGUOINHAP", "dbo.TBL_LOGIN", "USERNAME");
        //    AddForeignKey("dbo.CT_PHIEUDATNCC", "MASANPHAM", "dbo.SANPHAM", "Id_SanPham", cascadeDelete: true);
        //    AddForeignKey("dbo.CTPHIEUNHAP_NCC", "MASANPHAM", "dbo.SANPHAM", "Id_SanPham", cascadeDelete: true);
        //    DropColumn("dbo.CT_PHIEUDATNCC", "NGUOIDAT");
        //    DropColumn("dbo.CT_PHIEUDATNCC", "TBL_LOGIN_USERNAME");
        //    DropColumn("dbo.PHIEUDATHANG_NCC", "GHICHU");
        //    DropColumn("dbo.PHIEUNHAP_NCC", "MANHANVIEN");
        //    DropColumn("dbo.PHIEUNHAP_NCC", "GHICHU");
        //    DropColumn("dbo.CTPHIEUNHAP_NCC", "NGUOINHAP");
        //    DropColumn("dbo.CTPHIEUNHAP_NCC", "TBL_LOGIN_USERNAME");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CTPHIEUNHAP_NCC", "TBL_LOGIN_USERNAME", c => c.String(maxLength: 20));
            AddColumn("dbo.CTPHIEUNHAP_NCC", "NGUOINHAP", c => c.String());
            AddColumn("dbo.PHIEUNHAP_NCC", "GHICHU", c => c.String(maxLength: 50));
            AddColumn("dbo.PHIEUNHAP_NCC", "MANHANVIEN", c => c.Int(nullable: false));
            AddColumn("dbo.PHIEUDATHANG_NCC", "GHICHU", c => c.String(maxLength: 50));
            AddColumn("dbo.CT_PHIEUDATNCC", "TBL_LOGIN_USERNAME", c => c.String(maxLength: 20));
            AddColumn("dbo.CT_PHIEUDATNCC", "NGUOIDAT", c => c.String());
            DropForeignKey("dbo.CTPHIEUNHAP_NCC", "MASANPHAM", "dbo.SANPHAM");
            DropForeignKey("dbo.CT_PHIEUDATNCC", "MASANPHAM", "dbo.SANPHAM");
            DropForeignKey("dbo.PHIEUNHAP_NCC", "NGUOINHAP", "dbo.TBL_LOGIN");
            DropForeignKey("dbo.PHIEUDATHANG_NCC", "NGUOIDAT", "dbo.TBL_LOGIN");
            DropIndex("dbo.CTPHIEUNHAP_NCC", new[] { "MASANPHAM" });
            DropIndex("dbo.PHIEUNHAP_NCC", new[] { "NGUOINHAP" });
            DropIndex("dbo.PHIEUDATHANG_NCC", new[] { "NGUOIDAT" });
            DropIndex("dbo.CT_PHIEUDATNCC", new[] { "MASANPHAM" });
            AlterColumn("dbo.CTPHIEUNHAP_NCC", "MASANPHAM", c => c.Int());
            AlterColumn("dbo.PHIEUNHAP_NCC", "TONGTIEN", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CT_PHIEUDATNCC", "MASANPHAM", c => c.Int());
            AlterColumn("dbo.CT_PHIEUDATNCC", "SOLUONG", c => c.Int());
            DropColumn("dbo.PHIEUNHAP_NCC", "NGUOINHAP");
            DropColumn("dbo.PHIEUNHAP_NCC", "TRANGTHAI");
            DropColumn("dbo.PHIEUDATHANG_NCC", "TONGTIEN");
            DropColumn("dbo.PHIEUDATHANG_NCC", "NGUOIDAT");
            DropColumn("dbo.PHIEUDATHANG_NCC", "TRANGTHAI");
            DropColumn("dbo.CT_PHIEUDATNCC", "THANHTIEN");
            DropColumn("dbo.CT_PHIEUDATNCC", "GIANHAP");
            RenameColumn(table: "dbo.CTPHIEUNHAP_NCC", name: "MASANPHAM", newName: "SANPHAM_Id_SanPham");
            RenameColumn(table: "dbo.CT_PHIEUDATNCC", name: "MASANPHAM", newName: "SANPHAM_Id_SanPham");
            AddColumn("dbo.CTPHIEUNHAP_NCC", "MASANPHAM", c => c.Int(nullable: false));
            AddColumn("dbo.CT_PHIEUDATNCC", "MASANPHAM", c => c.Int(nullable: false));
            CreateIndex("dbo.CTPHIEUNHAP_NCC", "TBL_LOGIN_USERNAME");
            CreateIndex("dbo.CTPHIEUNHAP_NCC", "SANPHAM_Id_SanPham");
            CreateIndex("dbo.CT_PHIEUDATNCC", "SANPHAM_Id_SanPham");
            CreateIndex("dbo.CT_PHIEUDATNCC", "TBL_LOGIN_USERNAME");
            AddForeignKey("dbo.CTPHIEUNHAP_NCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM", "Id_SanPham");
            AddForeignKey("dbo.CT_PHIEUDATNCC", "SANPHAM_Id_SanPham", "dbo.SANPHAM", "Id_SanPham");
            AddForeignKey("dbo.CT_PHIEUDATNCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN", "USERNAME");
            AddForeignKey("dbo.CTPHIEUNHAP_NCC", "TBL_LOGIN_USERNAME", "dbo.TBL_LOGIN", "USERNAME");
        }
    }
}
