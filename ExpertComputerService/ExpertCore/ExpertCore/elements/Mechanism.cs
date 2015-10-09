using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCore.elements
{
    class Mechanism
    {
        public void GetDeserialyze()
        {
            Questions quest = new Questions();
            Answers Answer = new Answers();
            List<Answers> Lstr = new List<Answers>();
            List<string> srrR = new List<string>();
            Answer.ParamQust = new List<string>();
            srrR.Add("pussy");
            srrR.Add("dick");
            srrR.Add("cleetor");
            List<Questions> que1 = new List<Questions>();
            que1.Add(new Questions { NameQestion = "ключ1", ProbabilityQustion = "вероятность1", TextQustion = "Whera a You" });
            que1.Add(new Questions { NameQestion = "ключ2", ProbabilityQustion = "вероятность2", TextQustion = "Whera a You" });
            Lstr.Add(new Answers() {NameAnswer="больной ублюдок", WeigthAnswer="ублюдки", QantityAnswer=200, TextAnswer="ну да", ParamQust=srrR, ParamsQusttype=que1});

            new Serialyze.Serialyzable().SerializableCollections(new TestDATABASE().getAnswer());

           // SqlDataAdapter da = sele
      //      set.Answer.
            //  set.Answers.ReadXml("test.xml");
            /*   set.Answers.Rows.Add(new Object[] { "petr", "Smith",20 });
               set.Answers.Rows.Add(new Object[] { "petr2", "Smith3", 20 });*/
       //     set.WriteXml("test3.xml");
        }

        public void Shells()
        {

        }
    }
}
