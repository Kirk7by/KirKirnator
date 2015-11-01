using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore
{
    public class Expertcore  //обёртка над логикой
    {
        
        public event EventHandler QuestionReturnAnswer;
        public event EventHandler Fin;

        public Expertcore()
        {

        }

        public string StartMechanism()
        {
            return new OperatingMechanism().NewStarting();
        }


        public string GetQuestion(int otv, string nameHero=null, string nameQuestion=null)
        {
            if (nameHero != null && nameQuestion != null)
            {
                return new OperatingMechanism().shippingNewHeroAndQuestion(nameHero, nameQuestion);
            }
            else
            {  
                return new OperatingMechanism().GetQuestion(otv);
            }
        }
    }
}
