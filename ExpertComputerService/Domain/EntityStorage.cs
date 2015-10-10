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
        public List<Heroes> Heroes { get; private set; }
        public List<Questions> Qestion { get; private set; }


        public EntityStorage(IEnumerable<Heroes> heroes, IEnumerable<Questions> qestion)
        {
            Heroes = heroes.ToList<Heroes>();
            Qestion = qestion.ToList<Questions>();
        }
    }
}
