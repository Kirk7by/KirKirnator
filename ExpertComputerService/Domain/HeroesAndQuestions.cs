using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class HeroesAndQuestions     //класс представляющий 2 других класса для чтения
    {
        public Heroes[] Heroes { get; private set; }
        public Questions[] Qestion { get; private set; }

        
        public HeroesAndQuestions(IEnumerable<Heroes> heroes, IEnumerable<Questions> qestion)
        {
            Heroes = heroes.ToArray<Heroes>();
            Qestion = qestion.ToArray<Questions>();
        }
    }
}
