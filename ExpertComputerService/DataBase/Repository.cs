using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;


//DataBase.Repository//http://habrahabr.ru/post/203214/ 
//
//

namespace DataBase.Repository
{
    public class Repository : IRepository
    {
        
        public Repository()
        {

        }




        public void ExecuteListHero()
        {
            Heroes h = new Heroes { NameHeroes = "Злыдень3" };
            MyModelContext md = new MyModelContext();
            md.heroes.Add(h);
            md.SaveChanges();

            //пример удаления всех данных
            using (var ctx = new MyModelContext())
            {
                foreach (var u in ctx.heroes)
                {
                    ctx.heroes.Remove(u);
                }
               

                //   md.heroes.Remove(new Heroes { NameHeroes = "Злыдень" });
                ctx.SaveChanges();
            }

        }
        #region others
        public Heroes GetHero(string heroname)//получить героя по ID_Name
        {
            var dbContext = new MyModelContext();
            var Heros = dbContext.heroes.First(p => p.NameHeroes == heroname);
            return Heros;
        }
        //       var Heros = dbContext.heroes.First(p => p.NameHeroes !=""); //лямбда выражение всё что "" будет выходным условием
        //      var Qests = dbContext.qestions.First(p => p.NameQestion != "");
        #endregion

        public EntityStorage GetEntityStorage() //получение всех данных из бд
        {
            List<Heroes> heL = new List<Heroes>();
            List<Questions> qeL = new List<Questions>();
            using (var dbContext = new MyModelContext())
            {
                foreach (var t in dbContext.heroes)
                {
                    heL.Add(t);
                }
                foreach (var t in dbContext.qestions)
                {
                    qeL.Add(t);
                }
            }
            EntityStorage ent = new EntityStorage(heL, qeL);
            return ent;
        }
    }
}
