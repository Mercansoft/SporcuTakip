using Spor.Bussines;
using Spor.Core.Context;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Spor.UI.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {

        [AllowAnonymous]
        public ActionResult Giris()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Giris(Kullanici k ,string Hatirla)
        {
           // MembershipProvider provider = Membership.Providers["AspNetSqlMembershipProvider"];
            if (Membership.ValidateUser(k.KullaniciAdi, k.Sifre))
            {
                if (Hatirla=="on")
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                    
                }
               string[] rols= Roles.GetRolesForUser(k.KullaniciAdi);
                if (rols[0].ToString()=="Kullanıcı")
                {
                    return RedirectToAction("Liste", "Organizasyon");
                }
                else
                {
                    return RedirectToAction("Index", "Anasayfa");
                }                
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adınız veya Şifreniz Hatalı";
                return RedirectToAction("Giris", "Kullanici");
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            MembershipUserCollection user = Membership.GetAllUsers();
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Ekle()
        {
            List<string> roller = Roles.GetAllRoles().ToList();
            return View(roller);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Ekle(Kullanici k, string RolAdi, HttpPostedFileBase resim)
        {
            BL_Kullanici m = new BL_Kullanici();
            if (resim != null)
            {
                WebImage img = new WebImage(resim.InputStream);
                FileInfo fotoinfo = new FileInfo(resim.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(800, 350);
                string yol = Server.MapPath("/Uploads/Kullanici/" + newfoto);
                img.Save(yol);
                k.Resim = "/Uploads/Kullanici/" + newfoto;
            }

            ViewBag.Durum = m._Ekle(k, RolAdi, LoginID());
            List<string> roller = Roles.GetAllRoles().ToList();
            return View(roller);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Duzenle(string id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Kullanici k = db.Kullanicilar.Where(z => z.KullaniciAdi == id).SingleOrDefault();
                ViewBag.Rol = Roles.GetAllRoles().ToList();
                return View(k);
            }

        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult Duzenle(Kullanici k, string RolAdi, string Onay, HttpPostedFileBase resim)
        {
            BL_Kullanici m = new BL_Kullanici();
            if (resim != null)
            {
                WebImage img = new WebImage(resim.InputStream);
                FileInfo fotoinfo = new FileInfo(resim.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(800, 350);
                string yol = Server.MapPath("/Uploads/Kullanici/" + newfoto);
                img.Save(yol);
                k.Resim = "/Uploads/Kullanici/" + newfoto;
            }
            if (Onay=="on")
            {
                k.Onay = true;
            }
           string[] Rolu= Roles.GetRolesForUser(k.KullaniciAdi);
            ViewBag.Durum = m._Duzenle(k, RolAdi,Rolu[0].ToString());
            return RedirectToAction("Index","Kullanici");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Roller()
        {
            List<string> roller = Roles.GetAllRoles().ToList();
            return View(roller);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RolEkle()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolEkle(string RolAdi)
        {
            Roles.CreateRole(RolAdi);
            return View();
        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Giris");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Sil(string id)
        {
            if (Membership.DeleteUser(id))
            {
                try
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        Kullanici model = db.Kullanicilar.Where(x => x.KullaniciAdi == id).SingleOrDefault();
                        db.Kullanicilar.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {

                }
                ViewBag.Durum = "Başrıyla Silindi.";
            }
            return RedirectToAction("Index","Kullanici");
        }
        [AllowAnonymous]
        public ActionResult ParolamiUnuttum() {

            return View(); }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ParolamiUnuttum(Kullanici k)
        {
            MembershipUser mu = Membership.GetUser(k.KullaniciAdi);
            if (mu.PasswordQuestion==k.GizliSoru)
            {
                try
                {
                    string pw = mu.ResetPassword(k.GizliCevap);
                    mu.ChangePassword(pw, k.Sifre);
                    //mu.ChangePassword(mu.ResetPassword(), k.Sifre);

                    //string pw = mu.GetPassword(k.GizliCevap);
                    //mu.ChangePassword(pw, k.Sifre);
                    return RedirectToAction("Giris");
                }
                catch (Exception)
                {
                    ViewBag.Mesaj = "Gizli Cevabınz Hatalı.";
                    return View();
                }
            }
            else
            {
                ViewBag.Mesaj = "Gizli Sorunuz Hatalı.";
                return View();
            }
        }
        [Authorize(Roles = "Moderatör")]
        public ActionResult Sporcular()
        {
            MyDbContext db = new MyDbContext();
            var sporcu = db.Kullanicilar.Where(x => x.AdminName == User.Identity.Name).ToList();
            return View(sporcu);
        }
        [Authorize(Roles = "Moderatör")]
        public ActionResult SporcuEkle()
        {
            MyDbContext db = new MyDbContext();
            var query = db.Gruplar.Where(x => x.KullaniciAdi == User.Identity.Name).Select(c => new { c.id, c.GrupAdi });
            ViewBag.Grup = new SelectList(query.AsEnumerable(), "id", "GrupAdi");
            return View();
        }
        [Authorize(Roles = "Moderatör")]
        [HttpPost]
        public ActionResult SporcuEkle(Kullanici k, HttpPostedFileBase resim)
        {
            BL_Kullanici m = new BL_Kullanici();
            if (resim != null)
            {
                WebImage img = new WebImage(resim.InputStream);
                FileInfo fotoinfo = new FileInfo(resim.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(800, 350);
                string yol = Server.MapPath("/Uploads/Kullanici/" + newfoto);
                img.Save(yol);
                k.Resim = "/Uploads/Kullanici/" + newfoto;
            }
            
            ViewBag.Durum = m._Ekle(k, "Kullanıcı", LoginID());
            return RedirectToAction("Sporcular","Kullanici");
        }
        [Authorize(Roles = "Moderatör")]
        public ActionResult SporcuDuzenle(string id)
        {
            MyDbContext db = new MyDbContext();

            var query = db.Gruplar.Where(x => x.KullaniciAdi == User.Identity.Name).Select(c => new { c.id, c.GrupAdi });
            ViewBag.Grup = new SelectList(query.AsEnumerable(), "id", "GrupAdi");

            
            Kullanici k = db.Kullanicilar.Where(z => z.KullaniciAdi == id).SingleOrDefault();
            ViewBag.Rol = Roles.GetAllRoles().ToList();
            return View(k);
        }
        [Authorize(Roles = "Moderatör")]
        [HttpPost]
        public ActionResult SporcuDuzenle(Kullanici k,string Onay, HttpPostedFileBase resim)
        {
            BL_Kullanici m = new BL_Kullanici();
            if (resim != null)
            {
                WebImage img = new WebImage(resim.InputStream);
                FileInfo fotoinfo = new FileInfo(resim.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(800, 350);
                string yol = Server.MapPath("/Uploads/Kullanici/" + newfoto);
                img.Save(yol);
                k.Resim = "/Uploads/Kullanici/" + newfoto;
            }
            if (Onay == "on")
            {
                k.Onay = true;
            }
            string[] Rolu = Roles.GetRolesForUser(k.KullaniciAdi);
            ViewBag.Durum = m._Duzenle(k);
            return RedirectToAction("Sporcular", "Kullanici");
        }
        public string LoginID()
        {
            string LoginUserName = User.Identity.Name;
           // MembershipUser mu = Membership.GetUser(LoginUserName);
            //string LoginID = mu.ProviderUserKey.ToString();
            return LoginUserName;
        }
        public ActionResult Profil()
        {
            MyDbContext db = new MyDbContext();
            try
            {
                

                Kullanici k = db.Kullanicilar.Where(z => z.KullaniciAdi == User.Identity.Name).SingleOrDefault();
                return View(k);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Profil(Kullanici k, HttpPostedFileBase resim)
        {
            BL_Kullanici m = new BL_Kullanici();
            if (resim != null)
            {
                WebImage img = new WebImage(resim.InputStream);
                FileInfo fotoinfo = new FileInfo(resim.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(800, 350);
                string yol = Server.MapPath("/Uploads/Kullanici/" + newfoto);
                img.Save(yol);
                k.Resim = "/Uploads/Kullanici/" + newfoto;
            }
            k.Onay = true;
            ViewBag.Durum = m._Duzenle(k);
            return Redirect("/Kullanici/Profil/"+k.KullaniciAdi);
        }
    }
}