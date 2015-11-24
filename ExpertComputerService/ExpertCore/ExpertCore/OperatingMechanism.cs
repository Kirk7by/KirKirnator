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

        static string heroName; //последний вероятный герой

        static List<HeroesProgramm> lHeroes;   //список героев
        static List<Questions> lQuestions;  //список вопросов
        static List<Questions> Quest1;  //добавляем
        static List<Questions> Quest2;  //убираем

        static int indexQuest2; //индексатор выбранных вопросов
        static int IterAttempst; //Итератор вывода предположительного героя
        static int BackQuestIter;
        internal OperatingMechanism()
        {
        }


        #region Основные методы
        public string NewStarting()   //начать заново, или первый старт 
        {
            EntityStorage ent = new Repository().GetEntityStorage();
            //   lHeroes = ent.Heroes.ToList();

             lHeroes = new List<HeroesProgramm>();
          
            lHeroes.Clear();
            foreach (var prHero in ent.Heroes.ToList())
            {
                lHeroes.Add(new HeroesProgramm { NameHeroes = prHero.NameHeroes, TextHero = prHero.TextHero, WeigthHero = prHero.WeigthHero });
            }
            lQuestions = ent.Qestion.ToList();
            GetSortListHero();  //сортируем список героев

            Quest2 = GetQuestionDistinctList(lQuestions.ToList()).ToList(); //возвращаем quest2 список без повторяющихся вопросов
            Quest1 = new List<Questions>();

            indexQuest2 = -1;
            IterAttempst = 0;
            BackQuestIter = 0;

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


            if (Quest2.Count == 0)
            {
                GetProbabilityProizvHero(Quest1.ToList());
                this.QuestionEnter(this, EventArgs.Empty);
                return "Количество вопросов исчерпано";
            }

            Quest2[indexQuest2].OtvetSelected = otv;    //TODO: НУЖНЫ ТЕСТЫ именно этой части кода
            Quest1.Add(Quest2[indexQuest2]);
            Quest2.RemoveAt(indexQuest2);

            //получение списка вероятностей для каждого героя
            GetProbabilityProizvHero(Quest1.ToList());

            //поиск из оставшихся вопросов вопроса с минимальной энтропией, только если есть в хоть один вопрос в списке вопросов

            string Qestion = "не прошел";
            string MaxprobalityHero;
            double? MaxheroProb;
            Qestion = GetMixEntropyGetMaxProbalityHero(Qestion, out MaxprobalityHero, out MaxheroProb);

            //проверка на попадание героя под выбранную максимальную вероятность и количество предположительных ответов
            if (MaxheroProb >= ExpConfig.Default.MinProbalityQuestion && Quest2.Count != 0)
            {
                if (IterAttempst <= ExpConfig.Default.QuantityAttempt)
                {
                    GetMessageHero(MaxprobalityHero, Qestion);
                    IterAttempst++;
                }
                else
                {
                    QuestionEnter(this, EventArgs.Empty);
                }
                return null; // TODO:ОСТАНОВИСЬ
            }

            if (Quest2.Count == 0)
            {
                QuestionEnter(this, EventArgs.Empty);
                return "Questions null";
            }

            return Qestion;
        }   //получение вопроса

        private string GetMixEntropyGetMaxProbalityHero(string Qestion, out string MaxprobalityHero, out double? MaxheroProb)
        {
            double MinEntropy = 0;
            if (Quest2.Count != 0)
            {
                Quest2 = GetСonditionalEntropy(Quest2.ToList()).ToList();
                MinEntropy = (double)Quest2[0].ProbabilityQustion;
                int i = -1;
                indexQuest2 = 0;
                foreach (var q2 in Quest2)
                {
                    i++;
                    if (MinEntropy >= (double)q2.ProbabilityQustion)
                    {
                        indexQuest2 = i;
                        Qestion = q2.NameQestion;
                        MinEntropy = (double)q2.ProbabilityQustion;
                    }
                }
            }

            //получение героя с самой максимальной вероятностью
            MaxprobalityHero = "ничего";
            MaxheroProb = 0;
            foreach (var hero in lHeroes)
            {
                if (hero.ProbabilityHero >= MaxheroProb)
                {
                    MaxheroProb = hero.ProbabilityHero;
                    MaxprobalityHero = hero.NameHeroes;
                    heroName = MaxprobalityHero;

                }
            }

            return Qestion;
        }

        public string GetQuestion()  // Микширует все вопросы по вероятностям, при этом не проводит отбора// Безболезненный метод
        {           
            if(Quest1.Count==0)
            {
                indexQuest2 = Quest2.Count-1;
                return Quest2[Quest2.Count - 1].NameQestion;
            }
            GetProbabilityProizvHero(Quest1.ToList());
            if (Quest2.Count == 0)
            {
                GetProbabilityProizvHero(Quest1.ToList());
                this.QuestionEnter(this, EventArgs.Empty);
                return null;
            }
            Quest2 = GetСonditionalEntropy(Quest2.ToList()).ToList();

            string Qestion = "не прошел";
            string MaxprobalityHero;
            double? MaxheroProb;
            Qestion = GetMixEntropyGetMaxProbalityHero(Qestion, out MaxprobalityHero, out MaxheroProb);
            return Qestion;
        }
        #endregion


        #region Событийные методы
        public Exception ShippingСonfirmQuestionProbability(string HName=null)
        {
            if (HName == null)
                return new Repository().UpdateEndGamePobability(Quest1, heroName);
            else
                return new Repository().UpdateEndGamePobability(Quest1, HName);
        } //Отправка изменений вероятностей для всех героев
        public Exception ShippingNoConfirmQuestionProbability() 
        {
            try
            {
                if (lHeroes.Count >= 2)
                {
                    lHeroes.RemoveAll((item) => item.NameHeroes == heroName);
            //        Quest2.RemoveAll((item) => item.NameHeroes == heroName); //TODO: проводим чистку всех вопросов для этого героя
           //         Quest1.RemoveAll((item) => item.NameHeroes == heroName);
                    lQuestions.RemoveAll((item) => item.NameHeroes == heroName);
                    GetProbabilityProizvHero(Quest1.ToList());//
                    BackQuestIter = Quest1.Count;
                }
            }
            catch(Exception ex)
            {
                return ex;
            }
            return null;
        } //удаление героя который не прокатил
        public IEnumerable<Heroes> GetPriorityListHero()
        {
            lHeroes.Sort((x, y) => ((double)x.ProbabilityHero).CompareTo(y.ProbabilityHero));   //сортируем по максимальной вероятности
            List<Heroes> heros = new List<Heroes>();

            int i = 0;
            foreach(var lh in lHeroes)
            {
                i++;
                heros.Add(new Heroes { NameHeroes=lh.NameHeroes, TextHero=lh.TextHero, WeigthHero=lh.WeigthHero, ParamsQusttype=lh.ParamsQusttype }); //TODO: ParamsQusttype скорее можно убрать
                if (i == ExpConfig.Default.MinGetQuestionMaxProbality)
                    break;
            }
            return heros.ToList();
        }   //Получение списка наиболее вероятных героев
        public Exception shippingNewHeroAndQuestion(string nameHero, string nameQuestion)
        {
            return new Repository().AddHeroesAndQuestion(nameHero, nameQuestion, Quest1);
        } //Отправка нового героя и вопроса на сервер

        public bool BackQuestion()  
        //тупо возвращает на 1 вопрос назад, но нужно получить ответ! //Примечание!!! после него обычно вызывают GetQuestion
        {
            if (Quest1.Count > BackQuestIter)
            {
                Quest2.Add(Quest1[Quest1.Count - 1]);
                Quest1.RemoveAt(Quest1.Count - 1);
                return true;
            }
            return false;
        }
        //возвращает булевское значение, сумели ли мы вернуть вопрос назад
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
                    if (entr.NameQestion == qe.NameQestion)     //  if (entr.NameQestion == qe.NameQestion) //TODO:SWAP
                    {
                        tmpprobalityHero = lHeroes.Find(item => (string)item.NameHeroes == (string)qe.NameHeroes).ProbabilityHero;        //  tmpprobalityHero = lHeroes.Find(item => (string)item.NameHeroes == (string)entr.NameHeroes).ProbabilityHero; 

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
