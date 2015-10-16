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
            HeroeS.Add(new Heroes { NameHeroes = "Принтер", WeigthHero = 30 });
            HeroeS.Add(new Heroes { NameHeroes = "Видеокарта", WeigthHero = 10 });
            //............
            //
            return HeroeS;
        }
        private List<Questions> getQuestions()
        {
            List<Questions> QuestionS = new List<Questions>();
            
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Монитор" });
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Принтер" });
            QuestionS.Add(new Questions { NameQestion = "Ваша поломка связана с компьютером?", NameHeroes = "Видеокарта" });
            
            QuestionS.Add(new Questions { NameQestion = "У вас нет изображения на мониторе?", NameHeroes = "Монитор" });
            QuestionS.Add(new Questions { NameQestion = "У вас нет изображения на мониторе?", NameHeroes = "Принтер" });
            
            
            QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Монитор" });
            QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Видеокарта" });
            QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Принтер" });

            QuestionS.Add(new Questions { NameQestion = "Горячий ли системник?", NameHeroes = "Монитор" });
            QuestionS.Add(new Questions { NameQestion = "Горячий ли системник?", NameHeroes = "Принтер" });
            QuestionS.Add(new Questions { NameQestion = "Горячий ли системник?", NameHeroes = "Видеокарта" });


            QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Принтер" });
            QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Монитор" });
            

            QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Принтер" });
            
            QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Монитор" });
            foreach (var qe in QuestionS)
            {
                qe.OtvetQuest1 = 0.2;
                qe.OtvetQuest2 = 0.2;
                qe.OtvetQuest3 = 0.2;
                qe.OtvetQuest4 = 0.2;
                qe.OtvetQuest5 = 0.2;
            }
            QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
            QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
            QuestionS.Add(new Questions { NameQestion = "У вас нет изображения на мониторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.5, OtvetQuest2 = 0.1, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1, OtvetQuest5 = 0.1 });
            //           QuestionS.Add(new Questions { NameQestion = "Есть ли белые полосы на мониторе?", NameHeroes = "Видеокарта" ,OtvetQuest1=0.2, OtvetQuest2 =0.2, OtvetQuest3 = 0.2, OtvetQuest4 = 0.2, OtvetQuest5 = 0.2 });
            //         QuestionS.Add(new Questions { NameQestion = "Есть черные точки на миниторе?", NameHeroes = "Видеокарта", OtvetQuest1 = 0.2, OtvetQuest2 = 0.2, OtvetQuest3 = 0.2, OtvetQuest4 = 0.2, OtvetQuest5 = 0.2 });
            //       QuestionS.Add(new Questions { NameQestion = "Не включается компьютер?", NameHeroes = "Принтер", OtvetQuest1 = 0.2, OtvetQuest2 = 0.2, OtvetQuest3 = 0.2, OtvetQuest4 = 0.2, OtvetQuest5 = 0.2 });
            //............
            //
            return QuestionS;
        }

    }
}
