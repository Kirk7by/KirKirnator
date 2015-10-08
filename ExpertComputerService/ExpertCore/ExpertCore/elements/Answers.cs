using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore.elements
{
    public class Answers  //ответы
    {
        public Answers()
        {
         //   Questions que = new Questions();
        }
        public string NameAnswer { get; set; }  //название ответа, или по-простому "Условный идентификатор"
      //  public string Answer { get; set; }
        public string WeigthAnswer { get; set; }    //вероятностный статистический вес ответа (как часто мы будем это отгадывать)
        public string TextAnswer { get; set; }  //текст ответа
        public string QantityAnswer { get; set; } //Количество полученных раз за этот вопрос    
        public List<string> ParamQust { get; set; }  //вероятностные связи с вопросами или проще говоря, база знаний для наших ответов

        public List<Questions> ParamsQusttype { get; set; }

    }
}
