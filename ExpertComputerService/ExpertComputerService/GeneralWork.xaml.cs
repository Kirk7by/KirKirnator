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
    public partial class GeneralWork : Window
    {
        Expertcore ExpCore = new Expertcore();
        public GeneralWork()
        {
            InitializeComponent();
            ExpCore.QuestionEnter += openWindowAddHero;
            ExpCore.GetMessageHero += outputHeroMessage;
            LabelWrap.Text = ExpCore.StartMechanism();

            DevainedGrid.Visibility = Visibility.Collapsed;
        }

        #region General buttons
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
        #endregion

        private void CoreLink(int otv)
        {
            string quest = ExpCore.GetQuestion(otv);
            if(quest!=null)
                LabelWrap.Text = quest;
        }

        private void outputHeroMessage(string Hero, string Question)
        {
            LabelWrap.Text = Question;
            thinkWrap.Text = Hero;
            DevainedGrid.Visibility = Visibility.Visible;
            GridSelectedAnswers.Visibility = Visibility.Collapsed;
            GridWrapAnswer.Visibility = Visibility.Collapsed;
        }
        private void button_No_Click(object sender, RoutedEventArgs e)
        {
            Exception ex = ExpCore.ShippingNoConfirmQuestionProbability();
            if (ex != null)
                MessageBox.Show(ex.Message);
            else
                MessageBox.Show("удалили");
            DevainedGrid.Visibility = Visibility.Collapsed;
            GridSelectedAnswers.Visibility = Visibility.Visible;
            GridWrapAnswer.Visibility = Visibility.Visible;
        }
        private void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            Exception ex = ExpCore.ShippingСonfirmQuestionProbability();
            if (ex != null)
                MessageBox.Show(ex.Message);
            else
                MessageBox.Show("SUCCESS SHIPPING UPDATE HERO");
            new SumbitCancelWindow("Мне было очень приятно играть с вами.").Show();
            this.Close();
        }

        private void openWindowAddHero(object sender, EventArgs e)
        {
            new GenerateWork_AddHeroAndQuestion().Show();
            this.Close();
        }

        private void btBackMenu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
