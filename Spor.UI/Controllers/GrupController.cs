using Spor.Bussines;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spor.UI.Controllers
{
    [Authorize]
    public class GrupController : Controller
    {
        BL_Grup _ClsSalon = new BL_Grup();
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Index()
        {
            return View(_ClsSalon._Liste(User.Identity.Name.ToString()));
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Sil(string id)
        {
            _ClsSalon._Sil(id);
            return RedirectToAction("Index", "Grup");
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Ekle()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Moderatör")]
        [HttpPost]
        public ActionResult Ekle(Grup s)
        {
            s.KullaniciAdi = User.Identity.Name.ToString();
            _ClsSalon._Ekle(s);
            return RedirectToAction("Index", "Grup");
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Duzenle(int id)
        {
            Grup s = _ClsSalon._GetirID(id);
            return View(s);
        }
        [Authorize(Roles = "Admin,Moderatör")]
        [HttpPost]
        public ActionResult Duzenle(Grup s)
        {
            _ClsSalon._Guncelle(s);
            return RedirectToAction("Index", "Grup");
        }
    }
}