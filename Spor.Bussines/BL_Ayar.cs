using Spor.Core.Context;
using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Bussines
{
    public class BL_Ayar
    {
        MyDbContext db = new MyDbContext();
        public void _Guncelle(Ayar a)
        {
            try
            {
                Ayar model = db.Ayarlar.Find(1);
                model.Email = a.Email;
                model.Port = a.Port;
                model.Sifre = a.Sifre;
                model.SmtpSunucu = a.SmtpSunucu;
                model.Telefon = a.Telefon;
                model.Title = a.Title;
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}
