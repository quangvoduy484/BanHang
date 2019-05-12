using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Helper
{
    [NotMapped]
    public class User
    {
        public string name;
    }
    public class SessionUser
    {
        public static void SetSession(User user)
        {
            HttpContext.Current.Session["name"] = user;
        }
                
        public static User GetSession()
        {
            var session = HttpContext.Current.Session["name"];
            if (session == null)
            {
                return null;
            }
            return session as User;
            
        }
    }
}