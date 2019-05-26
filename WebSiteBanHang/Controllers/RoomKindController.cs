using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Controllers
{
    public class RoomKindController : Controller
    {
        private RoomKindService roomKindService = new RoomKindService();
        // GET: RoomKind
        public ActionResult Index()
        {
            return View();
        }
        //get
        public ActionResult LoadByRoomKind(int id, int? loaiSP)
        {
            var result = roomKindService.GetRoomKind(id, loaiSP);

            return View(result);
        }

    }
}
