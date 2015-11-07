using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore
{
    public class Expertcore  //обёртка над логикой
    {
        public event Action<string,string> GetMessageHero; //событие, Нужно вывести предположительный ответ
        public event EventHandler QuestionEnter; //событие, Попытки угадывания кончились

        private OperatingMechanism Mech;

        public Expertcore()
        {
            Mech = new OperatingMechanism();
            Mech.QuestionEnter += QuestionEnterMethod;
            Mech.GetMessageHero += GetMessageHeroMethod;
        }

        public string StartMechanism()
        {
            return Mech.NewStarting();
        } //ЗАПУСК МЕХАНИЗМА  //С получением первого вопроса
        public string GetQuestion(int otv)
        {
            return Mech.GetQuestion(otv);
        }  //получение вопроса //NEXT Question

        public Exception OutputNewHero(string heroName, string questName)
        {
            return Mech.shippingNewHeroAndQuestion(heroName, questName);
        }  //отправка нового героя и вопроса
        public Exception ShippingСonfirmQuestionProbability(string Hname=null)
        {
            if (Hname == null)
                return Mech.ShippingСonfirmQuestionProbability();
            else
                return Mech.ShippingСonfirmQuestionProbability(Hname);
        }  //отправка подтверждения угадывания
        public Exception ShippingNoConfirmQuestionProbability()
        {
            return Mech.ShippingNoConfirmQuestionProbability();
        }  //отправка неподтверждения угадывания
        public IEnumerable<Heroes> GetPriorityListHero()
        {
            return Mech.GetPriorityListHero();
        }   //Получение списка наиболее вероятных героев



        private void GetMessageHeroMethod(string Hero, string Quest)  //обработчик получения предположительного ответа
        {
            GetMessageHero(Hero, Quest);
        }
        private void QuestionEnterMethod(object sender, EventArgs e)
        {
            QuestionEnter(this, EventArgs.Empty);
        }   //обработчик отправки нового вопроса
    }
}
