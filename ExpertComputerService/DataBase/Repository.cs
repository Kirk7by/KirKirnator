using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;
//DataBase.Repository
namespace DataBase.Repository
{
    public class Repository : DbContext, IKirk7byRepository
    {
        public Repository():base("BaseHeroesAndQestions")
        {

        }
        private DbSet<Heroes> hers { get; set; }
        private DbSet<Questions> qest { get; set; }
        public EntityStorage GetEntityStorage()
        {



            List<Heroes> hr = new List<Heroes>();
            List<Questions> qe = new List<Questions>();

            //локальная база EPTI BLYA
            hr.Add(new Heroes { NameHeroes = "УБЛЮДОК!" });
            hr.Add(new Heroes { NameHeroes = "ТОТ, ЧЬЯ СОВЕСТЬ НЕ ЧИСТА" });
            hr.Add(new Heroes { NameHeroes = "ну и просто 3 персонаж в базе " });


            //   Heroes[] hero = null;


            //       hl = hers.ToList();
            // hero = hers;
            //   Questions[] Qest = null;
            // добавить обновление данных из бдююю

            EntityStorage ent = new EntityStorage(hr, qe);
            return ent;
        }
    }
}
