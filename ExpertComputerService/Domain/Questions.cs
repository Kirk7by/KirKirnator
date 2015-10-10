using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Questions //таблица Вопросы
    {
        #region Поля полученные из базы данных
        public virtual string NameQestion { get; set; } //название вопроса //                   PK
        public string TextQustion { get; set; } //текст вопроса //                      
        public string OtvetQuest1 { get; set; }  //Вероятность 1 ответа //да                
        public double OtvetQuest2 { get; set; } //Вероятность 2 ответа  //нет
        public double OtvetQuest3 { get; set; } //Вероятность 3 ответа  //скорее да
        public double OtvetQuest4 { get; set; } //Вероятность 4 ответа  //скорее нет
        public double OtvetQuest5 { get; set; } //Вероятность 5 ответа  //не знаю
        #endregion
        //
        ///
        //
        #region Поля полученные через код
        public int OtvetSelected { get; set; } //Индекс выбранного ответа, надеюсь пригодится
        public string ProbabilityQustion { get; set; }  //вероятность появления этого вопроса по Байесу() с минимальной условной энтропией
        #endregion
    }
}
