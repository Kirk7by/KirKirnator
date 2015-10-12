using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("Qestions")]
    public class Questions //таблица Вопросы
    {

        #region Поля полученные из базы данных

        
        [Key]
        [Column(Order = 0)]
        public string NameHeroes { get; set; }
        [Key]
        [Column(Order = 1)]
        public string NameQestion { get; set; } //название вопроса //                   PK
        public virtual Heroes Heroes { get; set; }

        public string TextQustion { get; set; } //текст вопроса //                      
        public string OtvetQuest1 { get; set; }  //Вероятность 1 ответа //да                
        public double? OtvetQuest2 { get; set; } //Вероятность 2 ответа  //нет
        public double? OtvetQuest3 { get; set; } //Вероятность 3 ответа  //скорее да
        public double? OtvetQuest4 { get; set; } //Вероятность 4 ответа  //скорее нет
        public double? OtvetQuest5 { get; set; } //Вероятность 5 ответа  //не знаю
        #endregion
        //
        ///
        //
        #region Поля полученные через код
        public bool? QuestSelected { get; set; } //Индекс выбранного вопроса, {true or false}-выбран/нет/// надеюсь пригодится
        public int? OtvetSelected { get; set; } //Индекс выбранного ответа,/// надеюсь пригодится
        public string ProbabilityQustion { get; set; }  //вероятность появления этого вопроса по Байесу() с минимальной условной энтропией
        //Расчитывается очень трудоёмким путём
        #endregion
    }
}
