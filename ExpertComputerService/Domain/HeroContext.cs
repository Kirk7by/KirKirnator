using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class HeroContext:DbContext
    {
        public DbSet<Heroes> HeroeS { get; set; }
        public DbSet<Questions> QestionS { get; set; }
    }
}
