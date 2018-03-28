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
    public class TasarimController : Controller
    {
        MyDbContext db = new MyDbContext();
        public ActionResult _Layout()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public PartialViewResult _Menu() { return PartialView(); }
        public PartialViewResult _Profil() {
            MembershipUser mu = Membership.GetUser(User.Identity.Name);
            Kullanici k = db.Kullanicilar.Where(x => x.KullaniciAdi == mu.UserName).FirstOrDefault();
            return PartialView(k); }
        public PartialViewResult _ProfilMenu()
        {
            MembershipUser mu = Membership.GetUser(User.Identity.Name);
            Kullanici k = db.Kullanicilar.Where(x => x.KullaniciAdi == mu.UserName).FirstOrDefault();
            return PartialView(k);
        }
        [Authorize(Roles = "Moderatör")]
        public PartialViewResult _MenuMod() { return PartialView(); }
        [Authorize(Roles = "Kullanıcı")]
        public PartialViewResult _MenuUser() { return PartialView(); }
    }
}