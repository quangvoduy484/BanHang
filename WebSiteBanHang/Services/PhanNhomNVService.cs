﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using System.Data.Entity;

namespace WebSiteBanHang.Services
{
    public class PhanNhomNVService
    {
        private BanHangContext context = null;
        public PhanNhomNVService()
        {
            context = new BanHangContext();

        }
        // Lấy danh sách nhóm 
        public List<TBL_GROUP> GetNhom()
        {
            return context.TBL_GROUPs.Where(t => t.ACTIVATE == true).ToList().Select(t => new TBL_GROUP()
            {
                ID = t.ID,
                GROUPNAME = t.GROUPNAME,

            }).ToList();
        }


        //Lấy danh sách nhân viên theo nhóm
        public List<TBL_LOGIN> GetNV(int idgroup)
        {
            return context.TBL_LOGINs.Include(t => t.TBL_GROUP_LOGINs)
                .Where(t => t.ACTIVATE != false)
                .Where(t => t.TBL_GROUP_LOGINs.All(x => x.ID_GROUP != idgroup && x.ACTIVATE != false)).ToList();
        }

        //Thêm group
        public void AddGroup(TBL_GROUP model)
        {
            model.ACTIVATE = true;

            context.TBL_GROUPs.Add(model);
            context.SaveChanges();
        }




        // Thêm account mới vào nhóm -final
        public bool Add(int id, List<string> userNames)
        {
            foreach (var username in userNames)
            {
                var grouplogin = new TBL_GROUP_LOGIN
                {
                    ID_GROUP = id,
                    USERNAME = username,
                    ACTIVATE = true
                };
                context.TBL_GROUP_LOGINs.Add(grouplogin);
            }
            context.SaveChanges();
            return true;
        }

        //Thêm account mới
        public void AddAccount(TBL_LOGIN model)
        {

            model.ISADMIN = false;
            model.ACTIVATE = true;
            model.CREATED_DATE = DateTime.Now;
            model.CREATED_BY = HttpContext.Current.User.Identity.Name;
            context.TBL_LOGINs.Add(model);
            context.SaveChanges();

        }
        public List<QuyenDropDownViewModel> GetAllDropDownList(string search, int idgroup)
        {

            var Quyens = context.TBL_ROLEs
                 .Where(t => t.ACTIVATE != false)
                 .Where(t => t.TBL_GROUP_ROLEs.All(x => x.ID_GROUP != idgroup && x.ACTIVATE != false));

            if (!string.IsNullOrWhiteSpace(search))
            {
                Quyens = Quyens.Where(t => t.ROLE_NAME.Contains(search));

            }
            var result = Quyens.OrderBy(t => t.ROLE_NAME)
                .Take(30)
                .Select(t => new QuyenDropDownViewModel()
                {
                    id = t.ID,
                    text = t.ROLE_NAME,
                }).ToList();

            return result;
        }

        public bool Delete(int maNhom)
        {
            TBL_GROUP groupExist = context.TBL_GROUPs.Find(maNhom);
            if (groupExist == null)
            {
                return false;
            }
            context.TBL_GROUPs.Remove(groupExist);
            context.SaveChanges();
            return true;
        }
    }
}