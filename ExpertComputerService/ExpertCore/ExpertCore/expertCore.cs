using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore
{
    public class Expertcore  //обёртка над логикой
    {
     

        public Expertcore()
        {
            OperatingMechanism op = new OperatingMechanism();
                        
        }
        //
        //
        //
        public event EventHandler Fin;

        public string GetQuestion(int otv, string nameHero=null, string nameQuestion=null)
        {
            if (nameHero != null && nameQuestion != null)
            {
                 //иницилизируем начало запуска//только 1 раз
                
                return new OperatingMechanism().shippingNewHeroAndQuestion(nameHero, nameQuestion);
            }
            else
            {
                return new OperatingMechanism().NewStarting();
                return new OperatingMechanism().GetQuestion(otv);
            }
        }
    }
}
