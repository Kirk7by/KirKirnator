using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EntityStorage
    {
        //храним сущности всего 2
        public Heroes[] Heroes { get; private set; }
        public Questions[] Qestion { get; private set; }


        public EntityStorage(IEnumerable<Heroes> heroes, IEnumerable<Questions> qestion)
        {
            Heroes = heroes.ToArray<Heroes>();
            Qestion = qestion.ToArray<Questions>();
        }
    }
}
