using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Core.Entity
{
    public class Salon
    {
        public int id { get; set; }
        public string SalonAdi { get; set; }
        public string KullaniciAdi { get; set; }

        public virtual List<Organizasyon> Organizasyonlar { get; set; }
    }
}
