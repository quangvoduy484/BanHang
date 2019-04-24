using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.ViewModel;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Helper
{
 

    public  class UserRole
    {
  
        public static  List<MenuRole> GetMenuByUser(string username)
        {

                using (BanHangContext db = new BanHangContext())
                {
                    var aRoleMenu = (from l in db.TBL_LOGINs
                                     join lg in db.TBL_GROUP_LOGINs on l.USERNAME equals lg.USERNAME
                                     join g in db.TBL_GROUPs on lg.ID_GROUP equals g.ID
                                     join rg in db.TBL_GROUP_ROLEs on g.ID equals rg.ID_GROUP
                                     join r in db.TBL_ROLEs on rg.ID_ROLE equals r.ID
                                     join m in db.TBL_MENUs on r.ID equals m.ID_ROLE
                                     where l.ACTIVATE == true && l.USERNAME == username && lg.ACTIVATE == true && g.ACTIVATE == true && rg.ACTIVATE == true && r.ACTIVATE == true && m.ACTIVATE == true
                                     select new { m, r }).Distinct();

                    var aUserMenus = new List<MenuRole>();
                    foreach(var aMenu in aRoleMenu)
                    {
                        var aMN = new MenuRole();
                        aMN.ID = aMenu.m.ID;
                        aMN.ID_ROLE = aMenu.m.ID_ROLE;
                        aMN.MENU_NAME = aMenu.m.MENU_NAME;
                        aMN.MENU_PARENT = aMenu.m.MENU_PARENT;
                        aMN.MENU_SEQ = aMenu.m.MENU_SEQ;
                        aMN.RouterLink = aMenu.r.ROLE_LINK;
                        aMN.MENU_ICON = aMenu.m.MENU_ICON;  

                        if (aUserMenus.Count(x => x.ID == aMN.ID) == 0)
                        {
                            aUserMenus.Add(aMN);
                        }


                    }
                    return aUserMenus;
                }
        

        }

    }
}