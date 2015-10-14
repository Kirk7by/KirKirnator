using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Repository;
using System.Collections;

namespace ExpertCore
{
    //Вся работа происходит ТУТ
    internal class OperatingMechanism : IMechanism
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



            switch (otv)
            {
                case 1:
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

            position++;

            string Qestion;

            if (GetQuestionDistinctList().Count > position)
                Qestion = GetQuestionDistinctList()[position].NameQestion;
            else
                Qestion = "The End";
            return Qestion;
        }




        #region ВЫБОР ПЕРСОНАЖА ПО ЗАДАННЫМ ВОПРОСАМ
        //--------------------------input: NULL/Otput: ProbabilityAprioryHero
        private List<Heroes> GetSortListHero()    //Получение отсортированного списка ответов с априорной вероятностью
        {
            lHeroes.Sort((x, y) => Convert.ToInt32(x.WeigthHero).CompareTo(y.WeigthHero));
            foreach (var l in lHeroes)
            {
                l.ProbabilityAprioryHero = l.WeigthHero / lHeroes.Select(p => p.WeigthHero).Sum();
            }
            return lHeroes;
            //TODO: Не забыть стереть РЕТУРН после тестов
        }
        //--------------------------input: NULL/Otput: NULL
        private List<Questions> GetQuestionDistinctList()   //Получение неповторяющегося списка вопросов
        {
            List<Questions> l = new List<Questions>();
            foreach (var lq in lQuestions)
            {

                if (l.Find(item => item.NameQestion == lq.NameQestion) == null)
                    l.Add(lq);
            }
            return l;
        }
        //--------------------------input:OtvetSelected, List QuestionSelected /Otput: ProbabilityProizvHero
        private List<Heroes> GetProbabilityProizvHero(List<Questions> QuestionSelected)  //Получение списка героев с расчитанным произведением вероятностей за сессию
        {                                               //Где List<Questions> QuestionSelected - Список выбранных вопросов
            double? tmpProbabilityOtvetSelected=0;
            foreach (var lH in lHeroes)
            {
                lH.ProbabilityProizvHero = 1;       //ибо умножение
                foreach (var qs in QuestionSelected)
                {
                    switch (qs.OtvetSelected)
                    {
                        case 1:
                            tmpProbabilityOtvetSelected = qs.OtvetQuest1;
                            break;
                        case 2:
                            tmpProbabilityOtvetSelected = qs.OtvetQuest2;
                            break;
                        case 3:
                            tmpProbabilityOtvetSelected = qs.OtvetQuest3;
                            break;
                        case 4:
                            tmpProbabilityOtvetSelected = qs.OtvetQuest4;
                            break;
                        case 5:
                            tmpProbabilityOtvetSelected = qs.OtvetQuest5;
                            break;      //больше 5 невозможно
                    }
                    if (qs.NameHeroes == lH.NameHeroes)
                    {
                        lH.ProbabilityProizvHero = lH.ProbabilityProizvHero * tmpProbabilityOtvetSelected;
                    }
                } 
            }
            return lHeroes;
            //TODO: стереть ретурн после тестов
        }
        //--------------------------input: ProbabilityProizvHero/Otput: ProbabilityHero
        public void GetListHeroProbability()    //Получение списка с ответов с расчитанной вероятностью
        {
            double? SumProbabilityProizvHero = 0;

            foreach (var l in lHeroes)
            {
                SumProbabilityProizvHero = SumProbabilityProizvHero + l.ProbabilityProizvHero;
            }
            foreach (var l in lHeroes)
            {
                l.ProbabilityHero = (l.ProbabilityAprioryHero * l.ProbabilityProizvHero) / SumProbabilityProizvHero;

                //априоритиВероятность * произведение вероятности всех отвеченных вопросов по данному персонажу l / сумма всех произведений вероятностий всех вопросов
                //ходим по всем отвеченным вопросам и находим вероятность для каждого персонажа
            }
        }
        #endregion
    }
}
