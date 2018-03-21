using Spor.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spor.Core.Context
{
    class MyDbContext
    {
        DbSet<Kullanici> Kullanicis { get; set; }
        DbSet<Organizasyon> Organizasyons { get; set; }
        DbSet<Salon> Salons { get; set; }
    }
}
