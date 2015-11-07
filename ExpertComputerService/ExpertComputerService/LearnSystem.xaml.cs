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
        List<Heroes> Hero= new List<Heroes>();
        List<Questions> Quest = new List<Questions>();
        private void buttonHeroes_Click(object sender, RoutedEventArgs e)
        {
            Hero= (new Repository().GetHeroesSource()).ToList();
            dgrid.ItemsSource = Hero.ToList();
        }

        private void buttonQuestions_Click(object sender, RoutedEventArgs e)
        {
            Quest= (new Repository().GetQuestionsSource()).ToList();
            dgrid.ItemsSource = Quest;
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
    }
}
