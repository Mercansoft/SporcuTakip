using Spor.Core.Context;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Spor.Http.App_Class;

namespace Spor.Http.Controllers
{

    public class bildirimController : ApiController
    {
        MyDbContext db = new MyDbContext();
        public HttpResponseMessage GetAll(string isim)
        {
            Organizasyon model = new Organizasyon();
            try
            {
                Kullanici users = db.Kullanicilar.Where(x => x.KullaniciAdi == isim).FirstOrDefault();
                model = db.Organizasyonlar.Where(x => x.GrupID == users.GrupID && x.Durum == true).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Found, "Böyle Bir KAyıt Bulunamadı.");
            }
        }
        //public List<Organizasyon> Get()
        //{
        //    // Kullanici users = db.Kullanicilar.Where(x => x.KullaniciAdi == isim).FirstOrDefault();
        //    using (MyDbContext data = new MyDbContext())
        //    {
        //        var model = data.Organizasyonlar.ToList();
        //        return model;
        //    }

        //}
        public List<Organizasyon> Get()
        {
            //List<ApiOrganizasyon> listOfUsers = new List<ApiOrganizasyon>();
            //ApiOrganizasyon userModel = new ApiOrganizasyon();
            //foreach (var user in db.Organizasyonlar)
            //{
            //    userModel.OrganizasyonAdi = user.OrganizasyonAdi;
            //    userModel.Saat = user.Saat;
            //    userModel.Tarih = user.Tarih;
            //    listOfUsers.Add(userModel);
            //}
            var users = db.Organizasyonlar.ToList();
            return users;

        }
        //public HttpResponseMessage Get(string isim)
        //{
        //    Organizasyon model = new Organizasyon();
        //    ApiOrganizasyon model2 = new ApiOrganizasyon();
        //    try
        //    {
        //        Kullanici users = db.Kullanicilar.Where(x => x.KullaniciAdi == isim).FirstOrDefault();
        //        model = db.Organizasyonlar.Where(x => x.GrupID == users.GrupID && x.Durum == true).FirstOrDefault();
        //        model2.OrganizasyonAdi = model.OrganizasyonAdi;
        //        model2.Saat = model.Saat;
        //        model2.Yer = model.Yer;
        //        return Request.CreateResponse(HttpStatusCode.OK, model2);
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Found, "Böyle Bir KAyıt Bulunamadı.");
        //    }
        //}
        //public List<Organizasyon> GetAll()
        //{
        //    using (MyDbContext db = new MyDbContext())
        //    {
        //        List<ApiOrganizasyon> listOfUsers = new List<ApiOrganizasyon>();
        //        ApiOrganizasyon userModel = new ApiOrganizasyon();
        //        foreach (var user in db.Organizasyonlar)
        //        {
        //            userModel.OrganizasyonAdi= user.OrganizasyonAdi;
        //            userModel.Saat = user.Saat;
        //            userModel.Tarih = user.Tarih;
        //            listOfUsers.Add(userModel);
        //        }
        //        var users = listOfUsers.ToList();
        //        return users;

        //    }

        //}
        //public HttpResponseMessage Get()
        //{
        //    try
        //    {

        //        return Request.CreateResponse(HttpStatusCode.OK, "MINA KOYİM");
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Found, "Böyle Bir KAyıt Bulunamadı.");
        //    }
        //}
    }
}
