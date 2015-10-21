using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    //реализация через синглтун
    //отлично подходит для этой бд
    public class bdTestsClass
    {
        private static bdTestsClass bdClass;
        private bdTestsClass()
        {
            
        }
        internal static bdTestsClass GetInstance()
        {
            if (bdClass == null)
            {
                bdClass= new bdTestsClass();
            }
            return bdClass;
        }
        internal EntityStorage GetListsBDHeroesAndQuestions()
        {
            List<Heroes> HeroeS = getHeroes();
            List<Questions> QuestionS = getQuestions();
            return new EntityStorage(HeroeS, QuestionS);
        }

        //
        //
        //
        //
        //
        //База данных, закидываемая в SQL
        private List<Heroes> getHeroes()
        {
            List<Heroes> HeroeS = new List<Heroes>();
            
            HeroeS.Add(new Heroes { NameHeroes = "Монитор", WeigthHero = 10 });
            HeroeS.Add(new Heroes { NameHeroes = "Принтер", WeigthHero = 10 });
            HeroeS.Add(new Heroes { NameHeroes = "Видеокарта", WeigthHero = 10 });
            //............
            //
            return HeroeS;
        }
        private List<Questions> getQuestions()
        {
            List<Questions> QuestionS = new List<Questions>();
            


            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с отображением информации на мониторе?", NameHeroes = "Монитор", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с отображением информации на мониторе?", NameHeroes = "Принтер", OtvetQuest1 = 0.01, OtvetQuest2 = 0.9, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с отображением информации на мониторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });

            QuestionS.Add(new Questions { NameQestion = "У вас работает монитор со встроенной видеокартой?", NameHeroes = "Монитор", OtvetQuest1 = 0.01, OtvetQuest2 = 0.9, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "У вас работает монитор со встроенной видеокартой?", NameHeroes = "Принтер", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "У вас работает монитор со встроенной видеокартой?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });

            QuestionS.Add(new Questions { NameQestion = "Работает ли ваш монитор в другом пк?", NameHeroes = "Монитор", OtvetQuest1 = 0.01, OtvetQuest2 = 0.9, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "Работает ли ваш монитор в другом пк?", NameHeroes = "Принтер", OtvetQuest1 = 0.01, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "Работает ли ваш монитор в другом пк?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });

            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с бумагой?", NameHeroes = "Принтер", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с бумагой?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.01, OtvetQuest2 = 0.9, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с бумагой?", NameHeroes = "Монитор", OtvetQuest1 = 0.01, OtvetQuest2 = 0.9, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });

            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Принтер", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Монитор", OtvetQuest1 = 0.9, OtvetQuest2 = 0.9, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });
            
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.9, OtvetQuest2 = 0.01, OtvetQuest3 = 0.096, OtvetQuest4 = 0.01, OtvetQuest5 = 0.01 });


           
           
            /*        QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Монитор" });
                    QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Принтер" });


                    QuestionS.Add(new Questions { NameQestion = "У вас нет изображения на мониторе?", NameHeroes = "Монитор" });
                    QuestionS.Add(new Questions { NameQestion = "У вас нет изображения на мониторе?", NameHeroes = "Принтер" });


                    QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Монитор" });

                    QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Принтер" });

                    QuestionS.Add(new Questions { NameQestion = "Горячий ли системник?", NameHeroes = "Монитор" });
                    QuestionS.Add(new Questions { NameQestion = "Горячий ли системник?", NameHeroes = "Принтер" });



                    QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Принтер" });
                    QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Монитор" });



                    foreach (var qe in QuestionS)
                    {
                        qe.OtvetQuest1 = 0.1;
                        qe.OtvetQuest2 = 0.3;
                        qe.OtvetQuest3 = 0.2;
                        qe.OtvetQuest4 = 0.2;
                        qe.OtvetQuest5 = 0.2;
                    }
                    QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Принтер", OtvetQuest1 = 0.01, OtvetQuest2 = 0.7, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });

                    QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Монитор", OtvetQuest1 = 0.01, OtvetQuest2 = 0.7, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });

                    QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
                    QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
                    QuestionS.Add(new Questions { NameQestion = "Горячий ли системник?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
                    QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
                    QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
                    QuestionS.Add(new Questions { NameQestion = "У вас нет изображения на мониторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
                    //           QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Видеокарта" ,OtvetQuest1=0.2, OtvetQuest2 =0.2, OtvetQuest3 = 0.2, OtvetQuest4 = 0.2, OtvetQuest5 = 0.2 });
                    //         QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.2, OtvetQuest2 = 0.2, OtvetQuest3 = 0.2, OtvetQuest4 = 0.2, OtvetQuest5 = 0.2 });
                    //       QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Принтер", OtvetQuest1 = 0.2, OtvetQuest2 = 0.2, OtvetQuest3 = 0.2, OtvetQuest4 = 0.2, OtvetQuest5 = 0.2 });
                    //............
                    //*/
            return QuestionS;
        }

    }
}
