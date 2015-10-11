using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ExpertCore;
using DataBase.Repository;
using Domain;
using DataBase;

namespace ExpertComputerService
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        expertCore ExpIit = new expertCore();
        
        event EventHandler Fin; //будет событием о завершении поиска
        event EventHandler Start; //событие начать сначала
        public Window2()
        {
            InitializeComponent();
            ExpIit.Fin += Fin;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OutputsInputs(1);
        }
  
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            OutputsInputs(2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            OutputsInputs(3);
        }


        //взаимодействие с ядром
        private void OutputsInputs(int Otv) 
        {
            //   DataBase.Repository.Repository rep = new DataBase.Repository.Repository();
            List<Heroes> hr = new List<Heroes>();
            List<Questions> qe = new List<Questions>();

            Domain.EntityStorage ent = new Domain.EntityStorage(hr,qe);
        //    ent = rep.GetEntityStorage();

            foreach (var s in ent.Heroes)
                MessageBox.Show(Convert.ToString(s.NameHeroes));
            ExpIit.PlaySerialize();
            label.Content = ExpIit.GetQuntit();

           

            //локальная база EPTI BLYA
            hr.Add(new Heroes { NameHeroes = "УБЛЮДОК!" });
            hr.Add(new Heroes { NameHeroes = "ТОТ, ЧЬЯ СОВЕСТЬ НЕ ЧИСТА" });
            hr.Add(new Heroes { NameHeroes = "ну и просто 3 персонаж в базе " });

            Repository rp = new Repository();
            rp.ExecuteListHero();
            
/*
            using (Model1 rep = new Model1())
            {
                var hero = new Heroes { NameHeroes = "UBLUDICH" };
                rep.heroes.Add(hero);
                rep.SaveChanges();
            }*/
        }
    }
}
