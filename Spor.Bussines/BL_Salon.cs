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
    public class BL_Salon
    {
        MyDbContext db = new MyDbContext();
        public void _Ekle(Salon s) {

            db.Salonlar.Add(s);
            db.SaveChanges();

        }
        public List<Salon> _Liste(string User)
        {
                List<Salon> model = db.Salonlar.Where(x => x.KullaniciAdi == User).ToList();
                return model;
        }
        public void _Sil(string id)
        {
            int sid = Convert.ToInt32(id);
            using (MyDbContext db= new MyDbContext())
            {
                Salon model = db.Salonlar.Where(x => x.id == sid).SingleOrDefault();
                db.Salonlar.Remove(model);
                db.SaveChanges();
            }
        }
        public void _Guncelle(Salon s)
        {
            Salon model = db.Salonlar.Find(s.id);
            model.SalonAdi = s.SalonAdi;
            db.Entry(model).State = EntityState.Modified;

        }
        public Salon _GetirID(int id) { Salon model = db.Salonlar.Find(id); return model; }
    }
}
