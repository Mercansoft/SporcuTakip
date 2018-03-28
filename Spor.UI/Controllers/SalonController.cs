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
    public class SalonController : Controller
    {
        BL_Salon _ClsSalon = new BL_Salon();
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Index()
        {
            //using (MyDbContext db = new MyDbContext())
            //{
            //    var model = db.Salonlar.Where(x => x.KullaniciAdi == User.Identity.Name).ToList();
            //    return View(model);
            //}
            return View(_ClsSalon._Liste(User.Identity.Name.ToString()));
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Sil(string id)
        {
            _ClsSalon._Sil(id);
            return RedirectToAction("Index", "Salon");
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Ekle()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Moderatör")]
        [HttpPost]
        public ActionResult Ekle(Salon s)
        {
            s.KullaniciAdi = User.Identity.Name.ToString();
            _ClsSalon._Ekle(s);
            return RedirectToAction("Index", "Salon");
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Duzenle(int id)
        {
            Salon s = _ClsSalon._GetirID(id);
            return View(s);
        }
        [Authorize(Roles = "Admin,Moderatör")]
        [HttpPost]
        public ActionResult Duzenle(Salon s)
        {
            _ClsSalon._Guncelle(s);
            return RedirectToAction("Index", "Salon");
        }
    }
}