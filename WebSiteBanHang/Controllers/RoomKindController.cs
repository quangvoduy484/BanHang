using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteBanHang.Controllers
{
  public class RoomKindController : Controller
  {
    // GET: RoomKind
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult LoadByRoomKind(int id,int type)
    {
      return View();
    }

  }
}
