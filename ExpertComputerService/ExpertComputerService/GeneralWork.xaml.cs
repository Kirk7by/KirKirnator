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
        Repository rp = new Repository();
        public GeneralWork()
        {
            InitializeComponent();

       //     rp.ClearBdData();
      //      rp.FillBdData();
            LabelWrap.Text = new Expertcore().GetQuestion(0);
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
            string TextQuestions="";

            TextQuestions = new Expertcore().GetQuestion(otv);
            if (TextQuestions=="The End") //сигнализирует о конце списка с вопросами и ожидание события приёма сообщения с ответом
            {
                LabelWrap.Text = "Конец, выводим предположительный ответ...";
            }
            else
            {
                LabelWrap.Text = TextQuestions;
            }
        }
    }
}
