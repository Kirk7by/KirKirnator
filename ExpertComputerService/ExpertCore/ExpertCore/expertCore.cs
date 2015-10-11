using ExpertCore.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore
{
    public class expertCore //обёртка над логикой
    {
        public event EventHandler Fin;
        public void PlaySerialize()
        {
            new Mechanism().GetDeserialyze();//добро победит зло

        }
        public string GetQuntit()
        {
            return new Mechanism().GetQuntity();
        }

        public void GetSetDateWindow(int Otv)
        {

        }
    }
}
