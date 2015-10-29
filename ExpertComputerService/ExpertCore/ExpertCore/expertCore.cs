using ExpertCore.elements;
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

        }
        //
        //
        //
        public event EventHandler Fin;
        public void PlaySerialize()
        {
            new Mechanism().GetDeserialyze();//добро победит зло

        }
        public string GetQuntit()
        {
            return new Mechanism().GetQuntity();
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
