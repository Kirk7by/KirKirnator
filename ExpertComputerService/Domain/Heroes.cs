using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("Heroes")]
    public class Heroes
    {
        #region Поля сохраняющиеся в бд
        [Key]
        public string NameHeroes { get;  set; }  //название Hero, или по-простому "Условный идентификатор"

        public string TextHero { get;  set; }  //текст ответа, подробности о герое
        public int? WeigthHero { get;  set; }    //Количество угаданных раз этого Hero (вес)
        public virtual List<Questions> ParamsQusttype { get; set; }  //Привязанные вопросы к данному герою

        #endregion

        
       
    }
}
