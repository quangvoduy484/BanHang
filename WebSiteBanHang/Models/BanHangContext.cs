using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class BanHangContext : DbContext
    {
        public BanHangContext() : base("BanHangContext")
        {
        }

        public DbSet<TBL_LOGIN> TBL_LOGINs { get; set; }
        public DbSet<TBL_GROUP> TBL_GROUPs { get; set; }
        public DbSet<TBL_GROUP_LOGIN> TBL_GROUP_LOGINs { get; set; }
        public DbSet<TBL_MENU> TBL_MENUs { get; set; }
        public DbSet<TBL_ROLE> TBL_ROLEs { get; set; }
        public DbSet<TBL_GROUP_ROLE> TBL_GROUP_ROLEs { get; set; }
        public DbSet<CHITIETDATHANG> CHITIETDATHANGs { get; set; }
        public DbSet<CHITIETPHIEUGIAO> CHITIETPHIEUGIAOs { get; set; }
        public DbSet<DANHGIA> DANHGIAs { get; set; }
        public DbSet<DATHANG> DATHANGs { get; set; }
        public DbSet<GIASANPHAM> GIASANPHAMs { get; set; }
        public DbSet<HINH> HINHs { get; set; }
        public DbSet<HOADONGIAOHANG> HOADONGIAOHANGs { get; set; }
        public DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public DbSet<LOAIKHACHHANG> LOAIKHACHHANGs { get; set; }
        public DbSet<LOAIPHONG> LOAIPHONGs { get; set; }
        public DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public DbSet<SANPHAM> SANPHAMs { get; set; }
        public DbSet<MAUPHONG> MAUPHONGs { get; set; }
        public DbSet<CHITIETMAUPHONG> CHITIETMAUPHONGs { get; set; }
        public DbSet<NHACUNGCAP> NHACUNGCAPs { get;  set; }
        public DbSet<PHIEUDATHANG_NCC> PHIEUDATHANG_NCCs { get; set; }
        public DbSet<CT_PHIEUDATNCC> CT_PHIEUDATNCCs { get; set; }
        public DbSet<PHIEUNHAP_NCC> PHIEUNHAP_NCCs { get; set; }
        public DbSet<CTPHIEUNHAP_NCC> CTPHIEUNHAP_NCCs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<TBL_GROUP>()
              .HasMany(e => e.TBL_GROUP_ROLEs)
              .WithRequired(e => e.TBL_GROUP)
              .HasForeignKey(e => e.ID_GROUP)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_GROUP>()
                .HasMany(e => e.TBL_GROUP_LOGINs)
                .WithRequired(e => e.TBL_GROUP)
                .HasForeignKey(e => e.ID_GROUP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_LOGIN>()
                .HasMany(e => e.TBL_GROUP_LOGINs)
                .WithRequired(e => e.TBL_LOGIN)
                .HasForeignKey(e => e.USERNAME)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_ROLE>()
               .HasMany(e => e.TBL_GROUP_ROLEs)
               .WithRequired(e => e.TBL_ROLE)
               .HasForeignKey(e => e.ID_ROLE)
               .WillCascadeOnDelete(false);


        }
    }
}
