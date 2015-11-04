using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;
using System.Collections;


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
            EntityStorage ent = null;
            try
            {
                using (var dbContext = new MyModelContext())
                {
                    ent = new EntityStorage(dbContext.heroes.ToList(), dbContext.qestions.ToList());
                }
            }
            catch
            { return null; }
            
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
            return hs.ToList();
        }

        public IEnumerable<Questions> GetQuestionsSource()
        {
            using (MyModelContext _context = new MyModelContext())
            {
                return _context.qestions.ToList();
            }
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
                                OtvetSelected = 1,
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
                                OtvetSelected = 1,
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
                            OtvetSelected = 1,
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
                        OtvetSelected = 1,
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
        public Exception UpdateEndGamePobability(IEnumerable<Questions> ListQuestToHero, string HeroName)
        {
            try
            {
                using (MyModelContext _context = new MyModelContext())
                {

                    var listQuest = (from que in _context.qestions.ToList()
                                    where (que.NameHeroes == HeroName && ListQuestToHero.ToList().Find((items) => items.NameQestion == que.NameQestion) != null)
                                    select que).ToList();

                    var ListQuest = ListQuestToHero.ToList();
                    foreach(var lstq in listQuest)
                    {
                        // находим условное количество по ответов по каждому варианту
                        double? l1 = lstq.OtvetSelected * lstq.OtvetQuest1;
                        double? l2 = lstq.OtvetSelected * lstq.OtvetQuest2;
                        double? l3 = lstq.OtvetSelected * lstq.OtvetQuest3;
                        double? l4 = lstq.OtvetSelected * lstq.OtvetQuest4;
                        double? l5 = lstq.OtvetSelected * lstq.OtvetQuest5;
                        //

                        switch(ListQuestToHero.ToList().Find((item)=>item.NameQestion==lstq.NameQestion).OtvetSelected)
                        {
                            case 1:
                                l1++;
                                break;
                            case 2:
                                l2++;
                                break;
                            case 3:
                                l3++;
                                break;
                            case 4:
                                l4++;
                                break;
                            case 5:
                                l5++;
                                break;
                        }
                        lstq.OtvetSelected++;

                        lstq.OtvetQuest1 = l1 / lstq.OtvetSelected;
                        lstq.OtvetQuest2 = l2 / lstq.OtvetSelected;
                        lstq.OtvetQuest3 = l3 / lstq.OtvetSelected;
                        lstq.OtvetQuest4 = l4 / lstq.OtvetSelected;
                        lstq.OtvetQuest5 = l5 / lstq.OtvetSelected;

                        var result = _context.qestions.SingleOrDefault((item) => item.NameHeroes == HeroName && item.NameQestion == lstq.NameQestion);
                        if(result!=null)
                        {
                            result.OtvetSelected = lstq.OtvetSelected;
                            result.OtvetQuest1 = lstq.OtvetQuest1;
                            result.OtvetQuest2 = lstq.OtvetQuest2;
                            result.OtvetQuest3 = lstq.OtvetQuest3;
                            result.OtvetQuest4 = lstq.OtvetQuest4;
                            result.OtvetQuest5 = lstq.OtvetQuest5;
                        }
                    }
                    _context.SaveChanges();    
                }
            }
            catch(Exception ex)
            {
                return ex;
            }
            return null;
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
