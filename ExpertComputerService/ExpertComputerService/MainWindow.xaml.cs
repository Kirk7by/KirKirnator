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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpertCore;
using DataBase.Repository;
namespace ExpertComputerService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            new Repository().ClearBdData();
            new Repository().FillBdData();
            //*/
         
        }

        private void diagnostyc_Click(object sender, RoutedEventArgs e)
        {
            new GeneralWork().Show();
            this.Close();
        }

        private void lern_Click(object sender, RoutedEventArgs e)
        {
            new LearnSystem().Show();
          //  this.Close();
        }

        private void configurations_Click(object sender, RoutedEventArgs e)
        {
            new ConfigurationSettings().Show();
        }
    }
}
