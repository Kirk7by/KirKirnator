using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore
{
    internal interface IMechanism
    {
        string NewStarting();
        string GetQuestion(int otv);
        Exception ShippingСonfirmQuestionProbability(string HName = null);
        Exception ShippingNoConfirmQuestionProbability();
        IEnumerable<Heroes> GetPriorityListHero();
        Exception shippingNewHeroAndQuestion(string nameHero, string nameQuestion);
    }
}
