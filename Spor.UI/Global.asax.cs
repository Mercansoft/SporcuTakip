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
                    if (!Roles.RoleExists("Admin"))
                    {
                        Roles.CreateRole("Admin");
                    }
                    if (Membership.GetUser("Admin") == null)
                    {
                        Membership.CreateUser("Admin", "@q1w2e3Aa");
                        Roles.AddUserToRole("Admin", "Admin");
                    }
                    MembershipUser mu = Membership.GetUser("Admin");
                    Kullanici Kullanicis = new Kullanici();
                    Kullanicis.KullaniciAdi = mu.UserName;
                    Kullanicis.Sifre = mu.GetPassword();
                    Kullanicis.Email = "admin@admin.com";
                    Kullanicis.AdminName = mu.UserName;
                    db.Kullanicilar.Add(Kullanicis);
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
