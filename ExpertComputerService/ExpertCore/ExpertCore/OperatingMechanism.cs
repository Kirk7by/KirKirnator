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
        static List<Heroes> lHeroes;
        static List<Questions> lQuestions = new List<Questions>();
        static List<Questions> Quest1 = new List<Questions>();
        static List<Questions> Quest2 = new List<Questions>();
        static int indexQuest2;
        private static int position = -1;
        private static List<Questions> StQestions;
        internal OperatingMechanism()
        {
            
            EntityStorage ent = new Repository().GetEntityStorage();
            lHeroes = ent.Heroes;
            lQuestions = ent.Qestion;
            GetSortListHero();
        }

        public string GetQuestion(int otv)
        {
            string Qestion="";
            position++;
            if(position==0)
            {
                Quest2 = lQuestions;

                Quest1.Add(Quest2[0]);
                foreach (var q in Quest1)
                {
                    q.OtvetSelected = otv;
                }
                Quest2.RemoveAt(0);

                return Quest1[0].NameQestion;
            }
            
            //угадай героя
            GetProbabilityProizvHero(Quest1);
            Quest2 = GetСonditionalEntropy(Quest2);

            double? MinEntropy=0;
            string QuestNAME="";
            int i=-1;
            foreach(var q2 in Quest2)
            {
                i++;
                if(MinEntropy<q2.ProbabilityQustion)
                {
                    indexQuest2 = i;
                    QuestNAME = q2.NameQestion;
                    MinEntropy = q2.ProbabilityQustion;
                }
            }
            Qestion = QuestNAME;
            string MaxHero="ничего";
            double? MaxheroProb=0.00000000000001;
            foreach(var hero in lHeroes)
            {
                if(hero.ProbabilityHero>=MaxheroProb)
                {
                    MaxheroProb = hero.ProbabilityHero;
                    MaxHero = hero.NameHeroes;
                }
                MaxheroProb = hero.ProbabilityAprioryHero;
            }
            

            if (Quest2[indexQuest2] == null)
                return "Угадан:" + Convert.ToString(MaxheroProb);

            Quest1.Add(Quest2[indexQuest2]);
            Quest2.RemoveAt(indexQuest2);

            return Qestion+ Convert.ToString(MaxheroProb)+MaxHero;
        }


        #region ВЫБОР ПЕРСОНАЖА ПО ЗАДАННЫМ ВОПРОСАМ
        //--------------------------input: NULL/Otput: ProbabilityAprioryHero
        private void GetSortListHero()    //Получение отсортированного списка Hero с априорной вероятностью
        {
            lHeroes.Sort((x, y) => Convert.ToInt32(x.WeigthHero).CompareTo(y.WeigthHero));
            foreach (var l in lHeroes)
            {
                l.ProbabilityAprioryHero = l.WeigthHero / lHeroes.Select(p => p.WeigthHero).Sum();
            }
            
            //TODO: Не забыть стереть РЕТУРН после тестов
        }
       
        //--------------------------input:OtvetSelected, List QuestionSelected /Otput: ProbabilityProizvHero
        private void GetProbabilityProizvHero(List<Questions> QuestionSelected)  //Получение списка героев с расчитанным произведением вероятностей за сессию
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
                    if ((string)qs.NameHeroes == (string)lH.NameHeroes)
                    {
                        lH.ProbabilityProizvHero = lH.ProbabilityProizvHero * tmpProbabilityOtvetSelected;
                    }
                }
                GetListHeroProbability();
            }
            //Логическое продолжение метода ниже
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





        #region Выбор вопроса

        public List<Questions> GetСonditionalEntropy(List<Questions> QuestionsNext) //Получение условной энтропии для каждого вопроса
        {
            double? tmpprobalityHero;
            List<Questions> EntropQuestions = GetQuestionDistinctList(QuestionsNext);    //создаём список без повторяющихся вопросов
            foreach (var entr in EntropQuestions)
            {
                entr.OtvetQuest1 = 0;
                entr.OtvetQuest2 = 0;
                entr.OtvetQuest3 = 0;
                entr.OtvetQuest4 = 0;
                entr.OtvetQuest5 = 0;

                foreach (var qe in lQuestions)
                {
                    if (entr.NameQestion == entr.NameQestion)
                    {
                        tmpprobalityHero = lHeroes.Find(item => item.NameHeroes == entr.NameHeroes).ProbabilityHero;
                        entr.OtvetQuest1 += qe.OtvetQuest1 * tmpprobalityHero;
                        entr.OtvetQuest2 += qe.OtvetQuest2 * tmpprobalityHero;
                        entr.OtvetQuest3 += qe.OtvetQuest3 * tmpprobalityHero;
                        entr.OtvetQuest4 += qe.OtvetQuest4 * tmpprobalityHero;
                        entr.OtvetQuest5 += qe.OtvetQuest5 * tmpprobalityHero;
                    }
                }
                //находим нашу условную энтропию
                entr.ProbabilityQustion = Math.Log(1 / (double)entr.OtvetQuest1) + Math.Log(1 / (double)entr.OtvetQuest2) + Math.Log(1 / (double)entr.OtvetQuest3) +
                    Math.Log(1 / (double)entr.OtvetQuest4) + Math.Log(1 / (double)entr.OtvetQuest5);
            }
            return EntropQuestions;
        }


        //--------------------------input: NULL/Otput: NULL
        private List<Questions> GetQuestionDistinctList(List<Questions> lQuestionsNext)   //Получение неповторяющегося списка вопросов
        {
            List<Questions> l = new List<Questions>();
            foreach (var lq in lQuestionsNext)
            {
                if (l.Find(item => item.NameQestion == lq.NameQestion) == null)
                    l.Add(lq);
            }
            return l;
        }

        #endregion
    }
}
