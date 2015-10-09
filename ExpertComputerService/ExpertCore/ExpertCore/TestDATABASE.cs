using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertCore.elements;

namespace ExpertCore
{
    public class TestDATABASE
    {
        public List<Answers> getAnswer() //возвращает список героев и процент ответов на их вопросы
        {
            List<Questions> qeЕнштейОтветы = new List<Questions>();
            List<Answers> ans = new List<Answers>();

            //да нет неЗнаю
            qeЕнштейОтветы.Add(new Questions { NameQestion = "Относительность", OtvetQuest2 = 0.90, OtvetQuest3 = 0.5, OtvetQuest4 = 0.5 });
            qeЕнштейОтветы.Add(new Questions { NameQestion = "Сверхспособности", OtvetQuest2 = 0.5, OtvetQuest3 = 0.2, OtvetQuest4 = 0.3 });
            qeЕнштейОтветы.Add(new Questions { NameQestion = "Анимешность", OtvetQuest2 = 0.1, OtvetQuest3 = 0.8, OtvetQuest4 = 0.1 });

            ans.Add(new Answers{NameAnswer="Ейнштейн", TextAnswer="герой такой умный", QantityAnswer=30,ParamsQusttype=qeЕнштейОтветы });

            List<Questions> qeБетменОтветы = new List<Questions>();
            qeБетменОтветы.Add(new Questions { NameQestion = "Относительность", OtvetQuest2 = 0.10, OtvetQuest3 = 0.5, OtvetQuest4 = 0.4 });
            qeБетменОтветы.Add(new Questions { NameQestion = "Сверхспособности", OtvetQuest2 = 0.8, OtvetQuest3 = 0.1, OtvetQuest4 = 0.1 });
            qeБетменОтветы.Add(new Questions { NameQestion = "Анимешность", OtvetQuest2 = 0.10, OtvetQuest3 = 0.80, OtvetQuest4 = 0.10 });

            ans.Add(new Answers { NameAnswer = "Бетмен", TextAnswer = "герой такой черный", QantityAnswer = 200, ParamsQusttype = qeБетменОтветы });

            List<Questions> qeГульОтветы = new List<Questions>();
            qeГульОтветы.Add(new Questions { NameQestion = "Относительность", OtvetQuest2 = 0.10, OtvetQuest3 = 0.80, OtvetQuest4 = 0.1 });
            qeГульОтветы.Add(new Questions { NameQestion = "Сверхспособности", OtvetQuest2 = 0.9, OtvetQuest3 = 0.0, OtvetQuest4 = 0.1 });
            qeГульОтветы.Add(new Questions { NameQestion = "Анимешность", OtvetQuest2 = 0.9, OtvetQuest3 = 50, OtvetQuest4 = 0.05 });

            ans.Add(new Answers { NameAnswer = "гуль", TextAnswer = "герой такой анимешный", QantityAnswer = 49, ParamsQusttype = qeГульОтветы });
            return ans;
        }
    }
}
