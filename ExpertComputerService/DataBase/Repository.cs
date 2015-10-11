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
    public class Repository : IRepository
    {
        
        public Repository()
        {

        }
        public void ExecuteListHero()
        {
            Heroes h = new Heroes { NameHeroes = "Злыдень" };
            Model1 md = new Model1();
            md.heroes.Add(h);

        }

        public EntityStorage GetEntityStorage()
        {

            List<Heroes> hr = new List<Heroes>();
            List<Questions> qe = new List<Questions>();

            //локальная база EPTI BLYA
            hr.Add(new Heroes { NameHeroes = "УБЛЮДОК!" });
            hr.Add(new Heroes { NameHeroes = "ТОТ, ЧЬЯ СОВЕСТЬ НЕ ЧИСТА" });
            hr.Add(new Heroes { NameHeroes = "ну и просто 3 персонаж в базе " });

            EntityStorage ent = new EntityStorage(hr, qe);
            return ent;
        }
    }
}
