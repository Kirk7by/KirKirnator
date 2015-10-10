using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Heroes
    {
        #region Поля сохраняющиеся в бд
        public string NameHeroes { get;  set; }  //название Hero, или по-простому "Условный идентификатор"
        public string TextHero { get; private set; }  //текст ответа, подробности о герое
        public double WeigthHero { get; private set; }    //Количество угаданных раз этого Hero (вес)
        public virtual List<Questions> ParamsQusttype { get;private set; }  //Привязанные вопросы к данному герою

        public double QantityAnswer { get; private set; } //Количество полученных раз за этот вопрос
        #endregion

        #region Поля, появляющиеся программным путём
        public double ProbabilityAprioryHero { get; private set; } //априорная вероятность, расчитывается программным путём
        public double ProbabilityHero { get; set; } //промежуточная вероятность //понадобиться в расчетах или вероятность, что это именно этот персонаж
        public double ProbabilityProizvHero { get; set; } //произведение вероятностей всех ответов, отвеченных за сессию //промежуточная величина
        public List<string> ParamQust { get; set; }  //вероятностные связи с вопросами или проще говоря, база знаний для наших ответов 
        #endregion
    }
}
