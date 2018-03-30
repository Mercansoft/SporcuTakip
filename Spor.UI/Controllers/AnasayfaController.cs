using Spor.Bussines;
using Spor.Core.Context;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Spor.UI.Controllers
{
    [Authorize]
    public class AnasayfaController : Controller
    {
        MyDbContext db = new MyDbContext();
        [Authorize(Roles ="Admin,Moderatör")]
        public ActionResult Index()
        {
            BL_Kullanici _cLsKullanici = new BL_Kullanici();
            BL_Salon _cLsSalon = new BL_Salon();
            BL_Organizasyon _cLsOrganizasyon = new BL_Organizasyon();
            ViewBag.ToplamSporcu = _cLsKullanici._ToplamSporcuSayisi(User.Identity.Name);
            ViewBag.ToplamSalon = _cLsSalon._ToplamSalonSayisi(User.Identity.Name);
            ViewBag.ToplamOrganizasyon = _cLsOrganizasyon._ToplamOrganizasyonSayisi(User.Identity.Name);
            try
            {
                string[] klupler = Roles.GetUsersInRole("Moderatör");
                ViewBag.ToplamKlup = klupler.Length.ToString();
            }
            catch (Exception)
            {

            }
            return View();
        }
        public PartialViewResult _SonEklenenSporcular()
        {
            var sporcular = db.Kullanicilar.Where(o=>o.AdminName==User.Identity.Name).OrderByDescending(x => x.id).Take(5).ToList();
            return PartialView(sporcular);
        }
        public PartialViewResult _SonEklenenSalonlar()
        {
            var salonlar = db.Salonlar.Where(o => o.KullaniciAdi == User.Identity.Name).OrderByDescending(x => x.id).Take(5).ToList();
            return PartialView(salonlar);
        }
        public PartialViewResult _SonEklenenOrganizasyonlar()
        {
            var org = db.Organizasyonlar.Where(o => o.KullaniciAdi == User.Identity.Name).OrderByDescending(x => x.id).Take(5).ToList();
            return PartialView(org);
        }
    }
}