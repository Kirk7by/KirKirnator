using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Repository;
using System.Collections;
using Configurate;

namespace ExpertCore
{
    //Вся работа происходит ТУТ
    internal class OperatingMechanism : IMechanism
    {
        internal event EventHandler QuestionEnter;   //событие, максимальное количество попыток выбора исчерпано, необходимо добавить новый вопрос, *либо выбрать из списка подобный*
        internal event Action<string,string> GetMessageHero; //событие, Нужно вывести предположительный ответ


        internal event Func<string> QuestionStart; //событие, Процесс запущен// предварительно сброшен
        internal event EventHandler QuestionStop; //событие, Процесс остановлен//данные сброшены // из-за ошибок, либо других причин

        static int testint1, testint2, testint3;


        static List<Heroes> lHeroes;   //список героев
        static List<Questions> lQuestions;  //список вопросов
        static List<Questions> Quest1;  //добавляем
        static List<Questions> Quest2;  //убираем

        string test = ""; //TODO: ТЕСТИТЬ
        static int indexQuest2; //индексатор выбранных вопросов

        internal OperatingMechanism()
        {
            QuestionStart += NewStarting;
        }

        internal string NewStarting()   //начать заново
        {
            EntityStorage ent = new Repository().GetEntityStorage();
            lHeroes = ent.Heroes.ToList();
            lQuestions = ent.Qestion.ToList();
            GetSortListHero();  //сортируем список героев

            Quest2 = GetQuestionDistinctList(lQuestions.ToList()).ToList(); //возвращаем quest2 список без повторяющихся вопросов
            Quest1 = new List<Questions>();
            indexQuest2 = -1;

            Random rd = new Random();
            foreach (var t in Quest2)
            {
                indexQuest2++;
                if (t.NameQestion == ExpConfig.Default.PriorytyQuestions)
                {
                    return Quest2[indexQuest2].NameQestion;
                }
            }
            indexQuest2 = rd.Next(0, Quest2.Count);
            return Quest2[indexQuest2].NameQestion; //TODO: EARST
        }
        public string GetQuestion(int otv)
        {
            string Qestion = "";
            
            if (Quest2.Count == 0)
            {
                GetProbabilityProizvHero(Quest1.ToList());
                string str = "";
                foreach (var s in lHeroes)
                {
                    str = str + ") " + s.NameHeroes + ": (" + s.ProbabilityHero + ": " + s.ProbabilityProizvHero + ": " + s.ProbabilityProizvHero;
                }
                //       return "The End";
                this.QuestionEnter(this, EventArgs.Empty);
                return "Не угадал .Вот уж не задача. РЕЗУЛЬТАты: +\n" + str + "\n Событие добавить новый вопрос..";
            }

            Quest2[indexQuest2].OtvetSelected = otv;    //TODO: НУЖНЫ ТЕСТЫ именно этой части кода
            Quest1.Add(Quest2[indexQuest2]);
            Quest2.RemoveAt(indexQuest2);

            //получение списка вероятностей для каждого героя
            GetProbabilityProizvHero(Quest1.ToList());

            //поиск из оставшихся вопросов вопроса с минимальной энтропией, только если есть в хоть один вопрос в списке вопросов
            double? MinEntropy = 0;
            if (Quest2.Count != 0)
            {
                testint1 = Quest2.Count;
                Quest2 = GetСonditionalEntropy(Quest2.ToList()).ToList();
                MinEntropy = Quest2[0].ProbabilityQustion;
                int i = -1;
                indexQuest2 = 0;
                testint2 = Quest2.Count;
                foreach (var q2 in Quest2)
                {
                    i++;
                    if (MinEntropy >= q2.ProbabilityQustion)
                    {
                        indexQuest2 = i;
                        Qestion = q2.NameQestion;
                        MinEntropy = q2.ProbabilityQustion;
                    }
                }
            }


            //получение героя с самой максимальной вероятностью
            string MaxprobalityHero = "ничего";
            double? MaxheroProb = 0;
            foreach (var hero in lHeroes)
            {
                if (hero.ProbabilityHero >= MaxheroProb)
                {
                    MaxheroProb = hero.ProbabilityHero;
                    MaxprobalityHero = hero.NameHeroes;
                }
            }


            // //временное условие
            if (MaxheroProb >= ExpConfig.Default.MinProbalityQuestion)
            {
                if (Quest2.Count != 0)
                {
                    GetMessageHero(MaxprobalityHero, Qestion + " (" + Quest2.Count + ")");
                    return null;
                }
                else
                {
                    GetProbabilityProizvHero(Quest1.ToList());
                    this.QuestionEnter(this, EventArgs.Empty);
                }
            }

            string str1 = "", str2 = "";
            foreach (var qs in Quest2)
            {
                str2 = str2 + qs.NameQestion + " (" + qs.ProbabilityQustion + ")|";
            }
            foreach (var s in lHeroes)
            {
                str1 = str1 + s.NameHeroes + " Вероятность:(" + s.ProbabilityHero + ") Промежуточная вероятность:(" + s.ProbabilityProizvHero + ")  \n";
            }
            return str1 + "Сумма промежуточных вероятностей: " + test + "\n\n Вопрос: " + Qestion + "\n Энтропия этого вопроса:(" + Convert.ToString(MinEntropy) + ")\n" + " Максимальная вероятность:(" + Convert.ToString(MaxheroProb) + ")(" + MaxprobalityHero + ")\n\n" + str2;
        }   //получение вопроса

        #region отправка нового героя и вопроса на сервер
        public Exception shippingNewHeroAndQuestion(string nameHero, string nameQuestion)
        {
            return new Repository().AddHeroesAndQuestion(nameHero, nameQuestion, Quest1);
        }

        #endregion
        #region ВЫБОР ПЕРСОНАЖА ПО ЗАДАННЫМ ВОПРОСАМ
        //--------------------------input: NULL/Otput: ProbabilityAprioryHero
        private void GetSortListHero()    //Получение отсортированного списка Hero с априорной вероятностью
        {
            lHeroes.Sort((x, y) => Convert.ToInt32(x.WeigthHero).CompareTo(y.WeigthHero));
            foreach (var l in lHeroes)
            {
                l.ProbabilityAprioryHero = (double)l.WeigthHero / lHeroes.Select(p => p.WeigthHero).Sum();
            }

            //   lHeroes[0].ProbabilityAprioryHero =(double?)lHeroes[0].WeigthHero / lHeroes.Select(p => p.WeigthHero).Sum();
            //ВАЖНОЕ УТОЧНЕНИЕ: деление осуществлять только с (double)
            //TODO: Не забыть стереть РЕТУРН после тестов
        }

        //--------------------------input:OtvetSelected, List QuestionSelected /Otput: ProbabilityProizvHero
        private void GetProbabilityProizvHero(IEnumerable<Questions> QuestionSelected)  //Получение списка героев с расчитанным произведением вероятностей за сессию
        {                                               //Где List<Questions> QuestionSelected - Список выбранных вопросов
            double? tmpProbabilityOtvetSelected = 0;
            double tmp = 1;
            foreach (var lH in lHeroes)
            {
                tmpProbabilityOtvetSelected = 0;
                lH.ProbabilityProizvHero = 1;       //ибо умножение
                tmp = 1;
                foreach (var qs in QuestionSelected)
                {
                    foreach (var qall in lQuestions)
                    {
                        if ((qs.NameQestion == qall.NameQestion) && (qall.NameHeroes == lH.NameHeroes))
                        {
                            switch (qs.OtvetSelected)
                            {
                                case 1:
                                    tmpProbabilityOtvetSelected = qall.OtvetQuest1;
                                    break;
                                case 2:
                                    tmpProbabilityOtvetSelected = qall.OtvetQuest2;
                                    break;
                                case 3:
                                    tmpProbabilityOtvetSelected = qall.OtvetQuest3;
                                    break;
                                case 4:
                                    tmpProbabilityOtvetSelected = qall.OtvetQuest4;
                                    break;
                                case 5:
                                    tmpProbabilityOtvetSelected = qall.OtvetQuest5;
                                    break;      //больше 5 невозможно
                            }
                            tmp = tmp * (double)tmpProbabilityOtvetSelected;
                        }
                    }
                }
                lH.ProbabilityProizvHero = tmp * (double)lH.ProbabilityAprioryHero;
            }

            GetListHeroProbability();
            //Логическое продолжение метода ниже
        }
        //--------------------------input: ProbabilityProizvHero/Otput: ProbabilityHero
        private void GetListHeroProbability()    //Получение списка с ответов с расчитанной вероятностью
        {

            double? SumProbabilityProizvHero = 0;

            foreach (var l in lHeroes)
            {
                SumProbabilityProizvHero = SumProbabilityProizvHero + l.ProbabilityProizvHero;
            }
            test = Convert.ToString(SumProbabilityProizvHero);
            foreach (var l in lHeroes)
            {
                l.ProbabilityHero = (l.ProbabilityProizvHero) / SumProbabilityProizvHero;
                //априоритиВероятность * произведение вероятности всех отвеченных вопросов по данному персонажу l / сумма всех произведений вероятностий всех вопросов
                //ходим по всем отвеченным вопросам и находим вероятность для каждого персонажа
            }
        }
        #endregion

        #region Выбор вопроса

        private IEnumerable<Questions> GetСonditionalEntropy(IEnumerable<Questions> QuestionsNext) //Получение условной энтропии для каждого вопроса
        {
            double? tmpprobalityHero;
            // List<Questions> EntropQuestions = GetQuestionDistinctList(QuestionsNext);    //создаём список без повторяющихся вопросов

            double? q1, q2, q3, q4, q5;
            foreach (var entr in QuestionsNext)
            {
                q1 = 0;
                q2 = 0;
                q3 = 0;
                q4 = 0;
                q5 = 0;
                foreach (var qe in lQuestions)
                {
                    if (entr.NameQestion == qe.NameQestion)
                    {
                        tmpprobalityHero = lHeroes.Find(item => (string)item.NameHeroes == (string)entr.NameHeroes).ProbabilityHero;

                        q1 += qe.OtvetQuest1 * tmpprobalityHero;
                        q2 += qe.OtvetQuest2 * tmpprobalityHero;
                        q3 += qe.OtvetQuest3 * tmpprobalityHero;
                        q4 += qe.OtvetQuest4 * tmpprobalityHero;
                        q5 += qe.OtvetQuest5 * tmpprobalityHero;
                    }
                }
                //находим нашу условную энтропию
                entr.ProbabilityQustion = Math.Abs(Math.Log(1 / (double)q1) + Math.Log(1 / (double)q2) + Math.Log(1 / (double)q3) +
                    Math.Log(1 / (double)q4) + Math.Log(1 / (double)q5));
            }
            return QuestionsNext.ToList();
        }


        //--------------------------input: NULL/Otput: NULL
        private IEnumerable<Questions> GetQuestionDistinctList(IEnumerable<Questions> lQuestionsNext)   //Получение неповторяющегося списка вопросов
        {
            List<Questions> l = new List<Questions>();
            foreach (var lq in lQuestionsNext)
            {
                if (l.Find(item => item.NameQestion == lq.NameQestion) == null)
                    l.Add(lq);
            }
            return l.ToList();
        }

        #endregion
    }
}
