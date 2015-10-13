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
            HeroeS.Add(new Heroes { NameHeroes = "Hero11" , WeigthHero =4 });
            HeroeS.Add(new Heroes { NameHeroes = "Hero22", WeigthHero = 5 });
            //............
            //
            return HeroeS;
        }
        private List<Questions> getQuestions()
        {
            List<Questions> QuestionS = new List<Questions>();
            QuestionS.Add(new Questions { NameQestion = "Quest1", NameHeroes="Hero11"});
            QuestionS.Add(new Questions { NameQestion = "Quest2", NameHeroes = "Hero11" });
            //............
            //
            return QuestionS;
        }

    }
}
