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

        public IEnumerable<Heroes> GetHeroesSource()
        {
            List<Heroes> hs = new List<Heroes>();
            using (MyModelContext _context = new MyModelContext())
            {
                hs.AddRange(_context.heroes.ToList());
            }
            return hs;
        }

        public IEnumerable<Questions> GetQuestionsSource()
        {
            List<Questions> qe = new List<Questions>();
            using (MyModelContext _context = new MyModelContext())
            {
                qe.AddRange(_context.qestions.ToList());
            }
            return qe;
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

        //Добавление нового героя со своим вопросом // происходит, если мы не можем угадать героя
        public Exception AddHeroesAndQuestion(string nameHero,string nameQuestion,List<Questions> QustSelected)   //QustSelected c передачей параметра выбранного вопроса
        {
            Exception ex=null;
            try
            {
                using (var dbContext = new MyModelContext())
                {
                    List<Questions> QuestSELECTED = new List<Questions>();


                    foreach (var Q in QustSelected)  //иницилизируем список выбранных вопросов
                    {
                        QuestSELECTED.Add(Q);
                    }

                    dbContext.heroes.Add(new Heroes { NameHeroes = nameHero, WeigthHero = 1 }); //добавляем героя
                //    dbContext.SaveChanges();



                    List<Questions> QuestionDistinct = new List<Questions>();  //получаем список неповторяющихся вопросов
                    foreach (var lq in dbContext.qestions.ToList())
                    {
                        if (QuestionDistinct.Find(item => item.NameQestion == lq.NameQestion) == null)
                            QuestionDistinct.Add(lq);
                    }


                    foreach (var Qdistinct in QuestionDistinct)     //проходим по всем вопросам добавляя вероятность по умолчанию и добавляя выбранным вопросам максимальную вероятность ответов
                    {
                       
                        double otv1 = 0.1, otv2 = 0.1, otv3 = 0.1, otv4 = 0.1, otv5 = 0.1;

                        var result = QuestSELECTED.SingleOrDefault(item=>item.NameQestion==Qdistinct.NameQestion);   //TODO: ТУТУ ЧТО-ТО ЕСТЬ
                        if (result != null)
                        {
                            switch (result.OtvetSelected)
                            {
                                case 1:
                                    otv1 = 0.6;
                                    break;
                                case 2:
                                    otv2 = 0.6;
                                    break;
                                case 3:
                                    otv3 = 0.6;
                                    break;
                                case 4:
                                    otv4 = 0.6;
                                    break;
                                case 5:
                                    otv5 = 0.6;
                                    break;
                            }
                            dbContext.qestions.Add(new Questions
                            {
                                NameQestion = Qdistinct.NameQestion,
                                NameHeroes=nameHero,
                                OtvetQuest1 = otv1,
                                OtvetQuest2 = otv2,
                                OtvetQuest3 = otv3,
                                OtvetQuest4 = otv4,
                                OtvetQuest5 = otv5
                            });

                        }
                        else
                        {
                            dbContext.qestions.Add(new Questions
                            {
                                NameQestion = Qdistinct.NameQestion,
                                NameHeroes = nameHero,
                                OtvetQuest1 = 0.2,
                                OtvetQuest2 = 0.2,
                                OtvetQuest3 = 0.2,
                                OtvetQuest4 = 0.2,
                                OtvetQuest5 = 0.2
                            });
                        }
                    }

                    //            dbContext.SaveChanges();
                    //          dbContext.Dispose();dbContext.SaveChanges();
                    


                    foreach (var HERO in dbContext.heroes.ToList())     //добавляем и иницилизируем вопрос для всех героев
                    {
                        dbContext.qestions.Add(new Questions
                        {
                            NameQestion = nameQuestion,
                            NameHeroes = HERO.NameHeroes,
                            OtvetQuest1 = 0,
                            OtvetQuest2 = 0.8,
                            OtvetQuest3 = 0,
                            OtvetQuest4 = 0.15,
                            OtvetQuest5 = 0.05,
                        });
                    }
                    dbContext.qestions.Add(new Questions        //добавляем вопрос для нашего героя
                    {
                        NameQestion = nameQuestion,
                        NameHeroes = nameHero,
                        OtvetQuest1 = 0.8,
                        OtvetQuest2 = 0,
                        OtvetQuest3 = 0.15,
                        OtvetQuest4 = 0,
                        OtvetQuest5 = 0.05,
                    });


                    dbContext.SaveChanges();
                }
            }
            catch(Exception EX)
            {
                ex = EX;
            }
            return ex;
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
        public Exception UpdateEndGamePobability()
        {
            throw new NotImplementedException();
        }
        public Exception UpdateHeroes()
        {
            throw new NotImplementedException();
        }
        public Exception UpdateQuestion()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Удаление данных
        public Exception ClearBdData()
        {
            Exception ex = null;
            try
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
            catch (Exception ERROR)
            {
                ex = ERROR;
            }
            return ex;
        }
        public Exception RemoveHeroes(string NameHeroes)
        {
            Exception ex=null;

            try
            {
                using (MyModelContext _context = new MyModelContext())
                {
                    var listRemoveQuest = from qe in _context.qestions
                                          where qe.NameHeroes == NameHeroes
                                          select qe;
                    if(listRemoveQuest!=null)
                        _context.qestions.RemoveRange(listRemoveQuest);
                   
                    var hero = _context.heroes.SingleOrDefault(item => item.NameHeroes == NameHeroes);
                    if(hero!=null)
                        _context.heroes.Remove(hero);
                    _context.SaveChanges();  
                }
            }
            catch(Exception ERROR)
            {
                ex = ERROR;
            }
            return ex;
        }
        public Exception RemoveQuestion(string NameQuestion)
        {
            Exception ex = null;

            try
            {
                using (MyModelContext _context = new MyModelContext())
                {
                    var listRemoveQuest = from qe in _context.qestions
                                          where qe.NameQestion == NameQuestion
                                          select qe;
                    if (listRemoveQuest != null)
                        _context.qestions.RemoveRange(listRemoveQuest);

                    _context.SaveChanges();
                }
            }
            catch (Exception ERROR)
            {
                ex = ERROR;
            }
            return ex;
        }
        #endregion

    }
}
