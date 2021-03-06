﻿using System;
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
        public double? OtvetQuest1 { get; set; }  //Вероятность 1 ответа //да                
        public double? OtvetQuest2 { get; set; } //Вероятность 2 ответа  //нет
        public double? OtvetQuest3 { get; set; } //Вероятность 3 ответа  //скорее да
        public double? OtvetQuest4 { get; set; } //Вероятность 4 ответа  //скорее нет
        public double? OtvetQuest5 { get; set; } //Вероятность 5 ответа  //не знаю
        #endregion

        /*перегрузка сравнения// сравнение по названию вопроса Only */
        /*
        public override bool Equals(object obj)
        {
            
            return this.NameQestion.Equals(((Questions)obj).NameQestion);
        }
        */
        //
        ///
        //
        #region Поля полученные через код
        public bool? QuestSelected { get; set; } //Индекс выбранного вопроса, {true or false}-выбран/нет/// надеюсь пригодится
        public int? OtvetSelected { get; set; } //Индекс выбранного ответа,//  ТАКЖЕ ОДНОВРЕМЕННО ЯВЛЯЕТСЯ ВЕСОМ ВОПРОСА ДЛЯ КОНКРЕТНОГО ГЕРОЯ
        public double? ProbabilityQustion { get; set; }  //величина УСЛОВНОЙ ЭНТРОПИИ, усовершенствованный Байесовсий подход//
        //Расчитывается очень трудоёмким путём
        #endregion
    }
}
