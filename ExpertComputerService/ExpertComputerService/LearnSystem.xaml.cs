using DataBase.Repository;
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
using Domain;

namespace ExpertComputerService
{
    /// <summary>
    /// Логика взаимодействия для LearnSystem.xaml
    /// </summary>
    public partial class LearnSystem : Window
    {
        public LearnSystem()
        {
            InitializeComponent();
        }
        private void buttonHeroes_Click(object sender, RoutedEventArgs e)
        {
            dgrid.ItemsSource = (new Repository().GetHeroesSource()).Select(d => new { d.NameHeroes, d.TextHero, d.WeigthHero }).ToList(); 
        }
        private void buttonQuestions_Click(object sender, RoutedEventArgs e)
        {
            dgrid.ItemsSource=(new Repository().GetQuestionsSource()).Select(d => new{ d.NameQestion } ).Distinct().ToList();
        }
        private void buttonDominations_Click(object sender, RoutedEventArgs e)
        {
            dgrid.ItemsSource = (new Repository().GetQuestionsSource()).Select(d => new
            {
                d.NameQestion,
                d.NameHeroes,
                d.OtvetQuest1,
                d.OtvetQuest2,
                d.OtvetQuest3,
                d.OtvetQuest4,
                d.OtvetQuest5,
                d.OtvetSelected
            }).ToList();
        }
        

        private void deletebut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Exception ERep=null;
                var deletelst1 = dgrid.SelectedItem as Questions;
                if (deletelst1 != null)
                {
                    ERep = new Repository().RemoveQuestion(deletelst1.NameQestion);
                    if (ERep != null)
                        MessageBox.Show(ERep.Message);
                    ProgressSuccessLabel.Visibility = Visibility.Visible;
                    return;
                }

                var deletelst2 = dgrid.SelectedItem as Heroes;
                if (deletelst2 != null)
                {
                    ERep = new Repository().RemoveHeroes(deletelst2.NameHeroes);
                    if (ERep != null)
                        MessageBox.Show(ERep.Message);
                    ProgressSuccessLabel.Visibility = Visibility.Visible;
                    return;
                }
                else
                    MessageBox.Show("Всё плохо! Удалить данные невозможно!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (ProgressSuccessLabel.IsVisible == true)
                ProgressSuccessLabel.Visibility = Visibility.Collapsed;
        }

        private void BackMenu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void buttonAddQuest_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxAddQuestion.Text!="")
                try {  new Repository().AddQuestion(textBoxAddQuestion.Text); ProgressSuccessLabel.Visibility = Visibility.Visible;  }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
