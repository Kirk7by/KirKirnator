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
            dgrid.ItemsSource = (new Repository().GetHeroesSource()).ToList();
        }

        private void buttonQuestions_Click(object sender, RoutedEventArgs e)
        {
            dgrid.ItemsSource= (new Repository().GetQuestionsSource()).ToList();
        }
    }
}
