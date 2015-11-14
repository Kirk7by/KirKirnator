using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertComputerService.LearnSystemsMaterials
{
    internal class PHero
    {
        public string NameHeroes { get; set; }  //название Hero, или по-простому "Условный идентификатор"

        public string TextHero { get; set; }  //текст ответа, подробности о герое
        public int? WeigthHero { get; set; }    //Количество угаданных раз этого Hero (вес)
    }
}
