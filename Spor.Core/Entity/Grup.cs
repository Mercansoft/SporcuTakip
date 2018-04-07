using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Core.Entity
{
    public class Grup
    {
        public int id { get; set; }
        public string GrupAdi { get; set; }
        public string KullaniciAdi { get; set; }

        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
        public virtual ICollection<Organizasyon> Organizasyonlar { get; set; }
    }
}
