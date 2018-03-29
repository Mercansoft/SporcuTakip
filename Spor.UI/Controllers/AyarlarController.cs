using Spor.Bussines;
using Spor.Core.Context;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spor.UI.Controllers
{
    [Authorize]
    public class AyarlarController : Controller
    {
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            MyDbContext db = new MyDbContext();
            Ayar ay = db.Ayarlar.Find(1);
            return View(ay);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Guncelle(Ayar a)
        {
            BL_Ayar _clsAyar = new BL_Ayar();
            _clsAyar._Guncelle(a);
            return RedirectToAction("Index","Ayarlar");
        }
    }
}