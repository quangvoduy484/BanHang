using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Helper
{
   
    public class SessionUser
    {
        public static void SetSession(User user)
        {
            
            HttpContext.Current.Session["user"] = user;
            
        }
                
        public static User GetSession()
        {
            var session = HttpContext.Current.Session["user"];
            if (session == null)
            {
                return null;
            }
            return session as User;
            
        }
    }
}