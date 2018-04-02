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
    public class BL_Grup
    {
        MyDbContext db = new MyDbContext();
        public void _Ekle(Grup s)
        {
            db.Gruplar.Add(s);
            db.SaveChanges();
        }
        public List<Grup> _Liste(string User)
        {
            List<Grup> model = db.Gruplar.Where(x => x.KullaniciAdi == User).ToList();
            return model;
        }
        public void _Sil(string id)
        {
            int sid = Convert.ToInt32(id);
            using (MyDbContext db = new MyDbContext())
            {
                Grup model = db.Gruplar.Where(x => x.id == sid).SingleOrDefault();
                db.Gruplar.Remove(model);
                db.SaveChanges();
            }
        }
        public void _Guncelle(Grup s)
        {
            Grup model = db.Gruplar.Find(s.id);
            model.GrupAdi = s.GrupAdi;
            db.SaveChanges();
          //  db.Entry(model).State = EntityState.Modified;

        }
        public Grup _GetirID(int id) { Grup model = db.Gruplar.Find(id); return model; }
        public string _ToplamGrupSayisi(string Klup)
        {
            using (MyDbContext db = new MyDbContext())
            {
                int model = db.Gruplar.ToList().Count;
                return model.ToString();
            }
        }
    }
}
