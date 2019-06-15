using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Component
{
    [NotMapped]
    public class Ngay
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    [NotMapped]
    public class Nam
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class DayMonthYear
    {
        public static List<Ngay> GetNgays()
        {
            List<Ngay> Ngays = new List<Ngay>();
            Ngay Ngay = new Ngay();
            for (int i = 1; i <= 31; i++)
            {
                Ngay = new Ngay
                {
                    Text = i.ToString(),
                    Value = i
                };
                Ngays.Add(Ngay);
            }
            return Ngays;
        }

        public static List<Nam> GetNams()
        {
            List<Nam> Nams = new List<Nam>();
            Nam Nam = new Nam();
            for (int i = 1900; i <= Convert.ToInt32(DateTime.Now.Year.ToString()); i++)
            {
                Nam = new Nam
                {

                    Text = i.ToString(),
                    Value = i
                };
                Nams.Add(Nam);
            }
            return Nams;

        }

    }
}