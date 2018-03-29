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
    public class OrganizasyonController : Controller
    {
        BL_Organizasyon _ClsOrganizasyon = new BL_Organizasyon();
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Index()
        {
            return View(_ClsOrganizasyon._Liste(User.Identity.Name.ToString()));
        }
        [Authorize(Roles = "Admin,Moderatör,Kullanıcı")]
        public ActionResult Liste()
        {
            using (MyDbContext db = new MyDbContext())
            {
                Kullanici k = db.Kullanicilar.Where(x => x.KullaniciAdi == User.Identity.Name.ToString()).SingleOrDefault();
            return View(_ClsOrganizasyon._Liste(k.AdminName.ToString()));
            }
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Sil(string id)
        {
            _ClsOrganizasyon._Sil(id);
            return RedirectToAction("Index", "Organizasyon");
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Ekle()
        {
            MyDbContext db = new MyDbContext();
            var query = db.Salonlar.Where(x => x.KullaniciAdi==User.Identity.Name).Select(c => new { c.id, c.SalonAdi });
            ViewBag.Salon = new SelectList(query.AsEnumerable(), "id", "SalonAdi");

            return View();
        }
        [Authorize(Roles = "Admin,Moderatör")]
        [HttpPost]
        public ActionResult Ekle(Organizasyon o)
        {
            o.KullaniciAdi = User.Identity.Name.ToString();
            o.Durum = true;
            _ClsOrganizasyon._Ekle(o);
            return RedirectToAction("Index", "Organizasyon");
        }
        [Authorize(Roles = "Admin,Moderatör")]
        public ActionResult Duzenle(int id)
        {
            MyDbContext db = new MyDbContext();
            var query = db.Salonlar.Select(c => new { c.id, c.SalonAdi });
            ViewBag.Salon = new SelectList(query.AsEnumerable(), "id", "SalonAdi");

            Organizasyon o = _ClsOrganizasyon._GetirID(id);
            return View(o);
        }
        [Authorize(Roles = "Admin,Moderatör")]
        [HttpPost]
        public ActionResult Duzenle(Organizasyon o)
        {
            string durum= _ClsOrganizasyon._Guncelle(o);
            ViewData["Durum"] = "<script>alert('" + durum + "');</script>";
           // Session["Durum"]= "<script>alert('"+ durum + "');</script>";

            return Redirect("/Organizasyon/Duzenle/"+o.id);
        }
    }
}