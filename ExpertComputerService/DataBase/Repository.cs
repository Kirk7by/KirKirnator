using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace DataBase
{
    public class Repository : IKirk7byRepository
    {
        public EntityStorage GetEntityStorage()
        {
            Heroes[] hero = null;
            Questions[] Qest = null;
            // добавить обновление данных из бдююю
            ERRORS_METKA
            EntityStorage ent = new EntityStorage(hero, Qest);
            return ent;
        }
    }
}
