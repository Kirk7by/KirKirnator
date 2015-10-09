using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore.elements
{
    public class  Questions
    {
        public string NameQestion { get; set; } //название вопроса
        public string OtvetQuest { get; set; }
        public double OtvetQuest2 { get; set; }
        public double OtvetQuest3 { get; set; }
        public double OtvetQuest4 { get; set; }
        public string ProbabilityQustion { get; set; }  //вероятность появления этого вопроса по Байесу() с минимальной условной энтропией
        public string TextQustion { get; set; } //текст вопроса
    }
}
