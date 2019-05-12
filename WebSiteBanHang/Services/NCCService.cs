using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class NCCService
    {
        private BanHangContext context = null;
        public NCCService()
        {
            context = new BanHangContext();

        }
        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.NHACUNGCAPs.Where(t => t.TRANGTHAI != false).AsQueryable(); //lấy nhà cung cấp (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.TENNCC.Contains(search));
            }
            var totalRecord = model.Count();
            // Sorting
            switch (sortBy)
            {
                case "TenNhaCungCap":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TENNCC)
                            : model.OrderBy(t => t.TENNCC);
                    break;
                case "DC":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.DIACHI)
                            : model.OrderBy(t => t.DIACHI);
                    break;
                default:
                    model = model.OrderBy(t => t.TENNCC);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.Select(t => new NhaCungCapViewModel()
            {
                MaNCC = t.MANCC,
                TenNCC = t.TENNCC,
                DC = t.DIACHI,
                SDT = t.SODIENTHOAI,

            }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }
    
        public List<NHACUNGCAP> GetAllNcc()
        {
            return context.NHACUNGCAPs.Where(t => t.TRANGTHAI != false).ToList();
        }
        public void Add(NHACUNGCAP NCC)
        {
            context.NHACUNGCAPs.Add(NCC);
            context.SaveChanges();
        }
        public bool Update(NHACUNGCAP NCC)
        {

            NHACUNGCAP NCCExist = context.NHACUNGCAPs.Find(NCC.MANCC);
            if (NCCExist == null)
            {
                return false;
            }
            NCCExist.TENNCC = NCC.TENNCC;
            NCCExist.SODIENTHOAI = NCC.SODIENTHOAI;
            NCCExist.DIACHI = NCC.DIACHI;

            context.SaveChanges();
            return true;
        }

        public NHACUNGCAP FindById(int? id)
        {
            return context.NHACUNGCAPs.Find(id);
        }
        public bool Delete(int maNCC)
        {
            NHACUNGCAP NCCExist = context.NHACUNGCAPs.FirstOrDefault(t => t.TRANGTHAI != false && t.MANCC == maNCC);
            if (NCCExist == null)
            {
                return false;
            }
            NCCExist.TRANGTHAI = false;
            context.SaveChanges();
            return true;
        }

    }
}