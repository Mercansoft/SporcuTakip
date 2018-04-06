using Spor.Core.Context;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Spor.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    MembershipCreateStatus status;
                    if (!Roles.RoleExists("Admin"))
                    {
                        Roles.CreateRole("Admin");
                        Roles.CreateRole("Moderatör");
                        Roles.CreateRole("Kullanıcı");
                    }
                    if (Membership.GetUser("Admin") == null)
                    {
                        Membership.CreateUser("Admin", "123456Aa","admin@admin.com","adminsoru","admincevap",true,out status);
                        Roles.AddUserToRole("Admin", "Admin");
                    }
                    MembershipUser mu = Membership.GetUser("Admin");
                    Kullanici Kullanicis = new Kullanici();
                    int say = db.Kullanicilar.ToList().Count;
                    if (say==0)
                    {
                        Kullanicis.KullaniciAdi = mu.UserName;
                        Kullanicis.Sifre = "123456Aa";
                        Kullanicis.Email = mu.Email;
                        Kullanicis.AdminName = mu.UserName;
                        Kullanicis.AdSoyad = mu.UserName;
                        Kullanicis.GizliSoru = mu.PasswordQuestion;
                        Kullanicis.GizliCevap = "admincevap";
                        Kullanicis.Onay = true;
                        Kullanicis.Tarih = Convert.ToDateTime(DateTime.Now);
                        Kullanicis.Adres = "adres";
                        Kullanicis.DogumTarihi = Convert.ToDateTime(DateTime.Now);
                        Kullanicis.Resim = "yok";
                        db.Kullanicilar.Add(Kullanicis);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

            }
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    Ayar a = db.Ayarlar.SingleOrDefault();
                    if (a==null)
                    {
                        Ayar model = new Ayar();
                        model.Email = "deneme@deneme.com";
                        model.Port = 587;
                        model.Sifre = "deneme";
                        model.SmtpSunucu = "smtp.deneme.com";
                        model.Telefon = "05343716661";
                        model.Title = "SporMAX Sporcu Yönetim Sistemi";
                        db.Ayarlar.Add(model);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
