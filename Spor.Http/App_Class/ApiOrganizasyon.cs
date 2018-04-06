using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spor.Http.App_Class
{
    public class ApiOrganizasyon
    {
        public string KullaniciAdi { get; set; }
        public int SalonID { get; set; }
        public int GrupID { get; set; }
        public string OrganizasyonAdi { get; set; }
        public DateTime Tarih { get; set; }
        public string Saat { get; set; }
        public string Yer { get; set; }
        public string Adres { get; set; }
        public bool Durum { get; set; }
    }
}