using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Core.Entity
{
    public class Organizasyon
    {
        public int id { get; set; }
        public string KullaniciAdi { get; set; }
        public int SalonID { get; set; }
        public int GrupID { get; set; }
        public string OrganizasyonAdi { get; set; }
        public DateTime Tarih { get; set; }
        public string Saat { get; set; }
        public string Yer { get; set; }
        public string Adres { get; set; }
        public bool Durum { get; set; }
        public virtual Salon Salonlar { get; set; }
        public virtual Grup Gruplar { get; set; }
    }
}
