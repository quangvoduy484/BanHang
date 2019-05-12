using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebSiteBanHang.Helper
{
    public class GenHash
    {
        //Ma hoa
        public static string GenSHA1(string input)
        {
            return string.Join("", new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input)).Select(x => x.ToString("X2")).ToArray());
        }
    }
}