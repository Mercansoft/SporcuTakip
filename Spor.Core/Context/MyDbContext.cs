using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Spor.Core.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() {
            //Database.SetInitializer<MyDbContext>(new DropCreateDatabaseIfModelChanges<MyDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<MyDbContext>());
            //Database.Initialize(true);

        }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Organizasyon> Organizasyonlar { get; set; }
        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Ayar> Ayarlar { get; set; }
        public DbSet<Grup> Gruplar { get; set; }
    }
}
