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
    public partial class GeneralWork : Window
    {
        Expertcore ExpCore = new Expertcore();
        public GeneralWork()
        {
            InitializeComponent();
            ExpCore.QuestionEnter += openWindowAddHero;
            ExpCore.GetMessageHero += outputHeroMessage;
            LabelWrap.Text = ExpCore.StartMechanism();

        }
        private void outputHeroMessage(string Hero, string Question)
        {
            LabelWrap.Text = "Вы загадали : \n"+Hero+"\n Если желате продолжить, то ответте на вопрос: \n"+Question;
        }
        private void openWindowAddHero(object sender, EventArgs e)
        {
            new GenerateWork_AddHeroAndQuestion().Show();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CoreLink(1);
        }
  
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CoreLink(2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            CoreLink(3);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            CoreLink(4);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            CoreLink(5);
        }


        private void CoreLink(int otv)
        {
            string quest = ExpCore.GetQuestion(otv);
            if(quest!=null)
                LabelWrap.Text = quest;
        }
    }
}
