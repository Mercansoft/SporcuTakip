using Spor.Core.Context;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Bussines
{
    public class BL_Organizasyon
    {
        MyDbContext db = new MyDbContext();
        public void _Ekle(Organizasyon o)
        {
            db.Organizasyonlar.Add(o);
            db.SaveChanges();
        }
        public object _Liste(string User)
        {
            var model = db.Organizasyonlar.Where(x => x.KullaniciAdi == User).ToList();
            return model;
        }
        public void _Sil(string id)
        {
            int sid = Convert.ToInt32(id);
            using (MyDbContext db = new MyDbContext())
            {
                Organizasyon model = db.Organizasyonlar.Where(x => x.id == sid).SingleOrDefault();
                db.Organizasyonlar.Remove(model);
                db.SaveChanges();
            }
        }
        public void _Guncelle(Organizasyon o)
        {
            Organizasyon model = db.Organizasyonlar.Find(o.id);
            model.Adres = o.Adres;
            model.OrganizasyonAdi = o.OrganizasyonAdi;
            model.Saat = o.Saat;
            model.SalonID = o.SalonID;
            model.Tarih = o.Tarih;
            model.Yer = o.Yer;
            db.Entry(model).State = EntityState.Modified;

        }
        public Organizasyon _GetirID(int id) { Organizasyon model = db.Organizasyonlar.Find(id); return model; }
    }
}
