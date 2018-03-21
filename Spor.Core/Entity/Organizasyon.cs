using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Core.Entity
{
    class Organizasyon
    {
        public int id { get; set; }
        public int KullaniciID { get; set; }
        public int SalonID { get; set; }

        public virtual Salon Salonlar { get; set; }
        public virtual Kullanici Kullanicilar { get; set; }

        //List<Salon> Salonlar { get; set; }
        //List<Kullanici> Kullanicilar { get; set; }
    }
}
