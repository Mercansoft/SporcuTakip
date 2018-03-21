using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Core.Entity
{
    public class Kullanici
    {
        public int id { get; set; }
        public string UserID { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string GizliSoru { get; set; }
        public string GizliCevap { get; set; }
        public DateTime Tarih { get; set; }
        public string Adres { get; set; }

        
        List<Salon> Salonlar { get; set; }
        List<Organizasyon> Organizasyonlar { get; set; }
    }
}
