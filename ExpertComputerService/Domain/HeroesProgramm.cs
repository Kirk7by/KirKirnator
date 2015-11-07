using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class HeroesProgramm  // TODO: ЗАТЕСТИРОВАТЬ, ПОЧЕМУ ПРИ НАСЛЕДОВАНИИИ базового HEROES В БД ПОЯВЛЯЮТСЯ ДАННЫЕ И О КЛАССЕ НАСЛЕДНИКЕ. А не только о ONLY HEROES
    {
        #region Поля, появляющиеся программным путём

        public string NameHeroes { get; set; }  //название Hero, или по-простому "Условный идентификатор"

        public string TextHero { get; set; }  //текст ответа, подробности о герое
        public int? WeigthHero { get; set; }    //Количество угаданных раз этого Hero (вес)
        public virtual List<Questions> ParamsQusttype { get; set; }  //Привязанные вопросы к данному герою*/

        public  double? ProbabilityAprioryHero { get; set; } //априорная вероятность, расчитывается программным путём      
        public  double? ProbabilityProizvHero { get; set; } //произведение вероятностей всех ответов, отвеченных за сессию //промежуточная величина
        public  double? ProbabilityHero { get; set; } //промежуточная вероятность //понадобиться в расчетах ответа на вопрос или вероятность, что это именно этот персонаж

        #endregion
    }
}
