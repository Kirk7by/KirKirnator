using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Repository;

namespace ExpertCore
{
    //Вся работа происходит ТУТ
    internal class OperatingMechanism:IMechanism
    {
        List<Heroes> lHeroes = new List<Heroes>();
        List<Questions> lQuestions = new List<Questions>();
        private static int position = -1;

        internal OperatingMechanism()
        {
            EntityStorage ent = new Repository().GetEntityStorage();
            lHeroes = ent.Heroes;
            lQuestions = ent.Qestion;
        }


        public string GetQuestion(int otv)
        {
            
            position++;

            switch (otv)
            {
                case 1:
                    //выбран вопрос// умножение вероятности
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
            //пока ничего не происходит при выборе ответа
            string hero;
            if (GetSortListHero().Count > position)
                hero = GetSortListHero()[1].NameHeroes;
            else
                hero = "The End";
            
            return hero;
        }
        //1 действие
        private List<Heroes> GetSortListHero()    //Получение отсортированного списка ответов с априорной вероятностью
        {
            //    List<Answers> lstOut = new List<Answers>();
            lHeroes.Sort((x, y) => Convert.ToInt32(x.WeigthHero).CompareTo(y.WeigthHero));
            foreach (var l in lHeroes)
            {
                l.ProbabilityAprioryHero = l.WeigthHero / lHeroes.Select(p => p.WeigthHero).Sum();
            }
            //
            return lHeroes;
            //Не забыть стереть РЕТУРН после тестов
        }
        //2 действие
        public void GetListHeroProbability()    //Получение списка с ответов с расчитанной вероятностью
        {
            double? SumProbabilityHero = 0;

            foreach (var l in lHeroes)
            {
                SumProbabilityHero = SumProbabilityHero + l.ProbabilityProizvHero;
            }
            foreach (var l in lHeroes)
            {
                l.ProbabilityHero = (l.ProbabilityAprioryHero * l.ProbabilityProizvHero) / SumProbabilityHero;

                //априоритиВероятность * произведение вероятности всех отвеченных вопросов по данному персонажу l / сумма всех произведений вероятностий всех вопросов
                //ходим по всем отвеченным вопросам и находим вероятность для каждого персонажа
            }
        }




    }
}
