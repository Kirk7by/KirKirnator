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
        public Action<string,string> GetMessageHero;
        public event EventHandler QuestionEnter;    //+
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
        } //ЗАПУСК МЕХАНИЗМА 
        public string GetQuestion(int otv)  //получение вопроса
        {
            return Mech.GetQuestion(otv); 
        }
        public Exception OutputNewHero(string heroName, string questName)
        {
            return Mech.shippingNewHeroAndQuestion(heroName, questName);
        }   //отправка нового героя и вопроса


        public Exception ShippingСonfirmQuestionProbability(string Hname=null)   //отправка подтверждения угадывания
        {
            if (Hname == null)
                return Mech.ShippingСonfirmQuestionProbability();
            else
                return Mech.ShippingСonfirmQuestionProbability(Hname);
        }
        public Exception ShippingNoConfirmQuestionProbability()
        {
            return Mech.ShippingNoConfirmQuestionProbability();
        } //отправка неподтверждения угадывания


        public IEnumerable<Heroes> GetPriorityListHero()
        {
            return Mech.GetPriorityListHero();
        }

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
