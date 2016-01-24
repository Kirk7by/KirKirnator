using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertComputerService.LearnSystemsMaterials
{
    internal class PQuestionsDominations
    {
        public string NameQestion { get; set; }
        public double? OtvetQuest1 { get; set; }  //Вероятность 1 ответа //да                
        public double? OtvetQuest2 { get; set; } //Вероятность 2 ответа  //нет
        public double? OtvetQuest3 { get; set; } //Вероятность 3 ответа  //скорее да
        public double? OtvetQuest4 { get; set; } //Вероятность 4 ответа  //скорее нет
        public double? OtvetQuest5 { get; set; } //Вероятность 5 ответа  //не знаю

    }
}
