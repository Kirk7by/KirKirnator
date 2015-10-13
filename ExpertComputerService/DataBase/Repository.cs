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
        #region Выборка данных

        //получение всех данных из бд
        public EntityStorage GetEntityStorage()
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

        //получить героя по ID_Name
        public Heroes GetHero(string heroname)
        {
            var dbContext = new MyModelContext();
            var Heros = dbContext.heroes.First(p => p.NameHeroes == heroname);
            return Heros;
        }

        #endregion
        #region Добавление данных

        //Заполнение бд целиком
        public void FillBdData()
        {
            //      bdTestsClass bdSingleton = bdTestsClass.GetInstance();
            EntityStorage MyDataBase = bdTestsClass.GetInstance().GetListsBDHeroesAndQuestions(); //использован в синглтуне
            //      List<Heroes> heL = new List<Heroes>();
            //      List<Questions> qeL = new List<Questions>();
            //      heL = MyDataBase.Heroes;
            //      qeL = MyDataBase.Qestion;

            using (var dbContext = new MyModelContext())
            {
                foreach (var h in MyDataBase.Heroes)
                {
                    dbContext.heroes.Add(h);
                }

                foreach (var q in MyDataBase.Qestion)
                {
                    dbContext.qestions.Add(q);
                }
                dbContext.SaveChanges();
            }

        }

        //Добавление нового героя
        public void AddHeroes(Heroes hero)
        {
            using (var dbContext = new MyModelContext())
            {
                dbContext.heroes.Add(hero);
                dbContext.SaveChanges();
            }
        }

        //Добавление нового вопроса
        public void AddQuestion(Questions que)
        {
            using (var dbContext = new MyModelContext())
            {
                dbContext.qestions.Add(que);
                dbContext.SaveChanges();
            }
        }

        #endregion
        #region Обновление данных
        #endregion
        #region Удаление данных

        //Чистка всей базы
        public void ClearBdData()
        {

            using (var ctx = new MyModelContext())
            {
                foreach (var u in ctx.qestions)
                {
                    ctx.qestions.Remove(u);
                }
                foreach (var u in ctx.heroes)
                {
                    ctx.heroes.Remove(u);
                }
                ctx.SaveChanges();
            }

        }

        #endregion
    }
}
