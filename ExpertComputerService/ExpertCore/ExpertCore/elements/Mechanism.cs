using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore.elements
{
    class Mechanism
    {
        List<Answers> Lstat = new List<Answers>();
        private static int position = -1;
        public void GetDeserialyze()
        {
            Questions quest = new Questions();
            Answers Answer = new Answers();
            List<Answers> Lstr = new List<Answers>();
            List<string> srrR = new List<string>();
            Answer.ParamQust = new List<string>();
            srrR.Add("pussy");
            srrR.Add("dick");
            srrR.Add("cleetor");
            List<Questions> que1 = new List<Questions>();
            que1.Add(new Questions { NameQestion = "ключ1", ProbabilityQustion = "вероятность1", TextQustion = "Whera a You" });
            que1.Add(new Questions { NameQestion = "ключ2", ProbabilityQustion = "вероятность2", TextQustion = "Whera a You" });
            Lstr.Add(new Answers() {NameAnswer="больной ублюдок", WeigthAnswer="ублюдки", QantityAnswer=200, TextAnswer="ну да", ParamQust=srrR, ParamsQusttype=que1});

            List<Answers>  lstAn = new TestDATABASE().getAnswer();
            lstAn.Sort((x, y) => y.QantityAnswer.CompareTo(x.QantityAnswer));
            new Serialyze.Serialyzable().SerializableCollections(GetListAnswer());


           // SqlDataAdapter da = sele
      //      set.Answer.
            //  set.Answers.ReadXml("test.xml");
            /*   set.Answers.Rows.Add(new Object[] { "petr", "Smith",20 });
               set.Answers.Rows.Add(new Object[] { "petr2", "Smith3", 20 });*/
       //     set.WriteXml("test3.xml");
        }

        //Первое действие
        public List<Answers> GetListAnswer()    //Получение отсортированного списка ответов с априорной вероятностью
        {
            
        //    List<Answers> lstOut = new List<Answers>();
            Lstat = new TestDATABASE().getAnswer();
            Lstat.Sort((x, y) => x.QantityAnswer.CompareTo(y.QantityAnswer));
            foreach (var l in Lstat)
            {
                l.ProbabilityAprioryHero = l.QantityAnswer/ Lstat.Capacity;
            }
            return Lstat;
        }

        //Второе действие
        public void GetListHeroProbability()    //Получение списка с ответов с расчитанной вероятностью
        {
            double SumProbabilityHero=0;

            foreach(var l in Lstat )
            {
                SumProbabilityHero = SumProbabilityHero+l.ProbabilityProizvHero;
            }                       
            foreach(var l in Lstat)
            {
                l.ProbabilityHero = (l.ProbabilityAprioryHero * l.ProbabilityProizvHero) / SumProbabilityHero;
                    
                    //априоритиВероятность * произведение вероятности всех отвеченных вопросов по данному персонажу l / сумма всех произведений вероятностий всех вопросов
                    //ходим по всем отвеченным вопросам и находим вероятность для каждого персонажа
            }
        }
        
        //Третье действие
        public void GetListQuantityProbability()    //Получение упорядоченного списка всех вопросов по сортировке по полученной вероятности// или угадайка вопросов
        {
            //
            //
            //определение вероятности каждого варианта ответа по каждому вопросу :/ Создаем цикл Foreach по всем Ответам
            //Foreach( var Ответ....){ (Берём "GetListHeroProbability()" - список расчитанных вероятностей по ответам для выбранного Hero. 
            //делаем Foreach // (Хиро.ProbabilityHero /*/ перемножаем с  Ответ для данного хиро)) После всего суммируем все вероятности для этих хиро }
            //
            //делаем прогон по Вопросам
            //Сравниваем/// смотрим у кого меньше условная энтропия, тот вопрос и победил
            //
            //Сохранив и отсортировав по минимальной энтропие все вопросы, отправляем этот список дальше лесом
            //
        }


        public string GetQuntity()  //взаимодействие с формой // вывод текста вопроса
        {
            string StrQa;
            
            position++;
            StrQa=GetListAnswer()[1].ParamsQusttype[position].NameQestion;
          //  Lstat.

            return StrQa;   
        }

        public void Shells()
        {

        }
    }
}
