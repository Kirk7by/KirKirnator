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
using Configurate;
using DataBase.Repository;
using Microsoft.Win32;

namespace ExpertComputerService
{
    /// <summary>
    /// Логика взаимодействия для ConfigurationSettings.xaml
    /// </summary>
    public partial class ConfigurationSettings : Window
    {
        public ConfigurationSettings()
        {
            InitializeComponent();
            InitializeFullscreanMode();
            uloadingBd();
            uploadingThemes();
            uploadingWindowMode();
            InitializeElementsInConfig();
        }

        private void InitializeElementsInConfig()
        {
            //база данных
            tbQuestionPriority.Text = ExpConfig.Default.PriorytyQuestions;
            tbMinProbabilityQuestion.Text = Convert.ToString(ExpConfig.Default.MinProbalityQuestion).Remove(0,2);
            TbListHeroMaxProbality.Text = Convert.ToString(ExpConfig.Default.MinGetQuestionMaxProbality);
            tbAttempts.Text = Convert.ToString(ExpConfig.Default.QuantityAttempt);
            if ((tbDBConnectionString.Text = ExpConfig.Default.ConnectionString) == "")
                btDefaultSettings.Background = Brushes.GreenYellow;

            //формы
            comboBoxThema.Text = ExpConfig.Default.Thema;
            comboBoxWindowMode.Text = Convert.ToString(ExpConfig.Default.FullscreanWinow);
        }
        private void InitializeFullscreanMode()
        {
            switch (ExpConfig.Default.FullscreanWinow)
            {
                case 1:
                    this.WindowState = WindowState.Normal;
                    break;
                case 2:
                    this.WindowState = WindowState.Maximized;
                    break;
                case 3:
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    break;
            }   //TODO: BUG STUDIA NO WORKING REFACTORING CRITICALL ERROR
        }

        private void uloadingBd()
        {
            try
            {
                tbQuestionPriority.ItemsSource = (from qs in new Repository().GetQuestionsSource().ToList()
                                                  select qs.NameQestion).Distinct();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void uploadingThemes()
        {
            comboBoxThema.ItemsSource = new string[] { "Dictionary1.xaml", "Dictionary2DarkViolet.xaml", "Dictionary3Blue.xaml", "Light.xaml" };
        }
        private void uploadingWindowMode()
        {
            comboBoxWindowMode.ItemsSource = new int[] { 1, 2, 3};
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try {
                //база данных
                ExpConfig.Default.PriorytyQuestions = tbQuestionPriority.Text;
                ExpConfig.Default.MinProbalityQuestion = Convert.ToDouble("0,"+tbMinProbabilityQuestion.Text);
                ExpConfig.Default.MinGetQuestionMaxProbality = Convert.ToInt32(TbListHeroMaxProbality.Text);
                ExpConfig.Default.QuantityAttempt = Convert.ToInt32(tbAttempts.Text);
                ExpConfig.Default.ConnectionString = tbDBConnectionString.Text;
                //темы
                ExpConfig.Default.Thema = comboBoxThema.Text;
                ExpConfig.Default.FullscreanWinow = int.Parse(comboBoxWindowMode.Text);

                InitializeFullscreanMode();
                ExpConfig.Default.Save();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void btDefaultSettings_Click(object sender, RoutedEventArgs e)
        {
            tbQuestionPriority.Text = "Random";
            tbMinProbabilityQuestion.Text = "90";
            TbListHeroMaxProbality.Text = "10";
            tbAttempts.Text = "5";
            tbDBConnectionString.Text = @"(LocalDb)\MSSQLLocalDB; initial catalog = ExpertHeros; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework";
        }
        private void SelectedBdFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mdf File|*.mdf|ALL Files|*. ";
            openFileDialog1.Title = "Select a Cursor File";



            if (openFileDialog1.ShowDialog() == true)
                tbDBConnectionString.Text = @"(LocalDB)\MSSQLLocalDB; AttachDbFilename =" + openFileDialog1.FileName+ ";Integrated Security = True; Connect Timeout = 30";
        }
        private void btRandomQuestion_Click(object sender, RoutedEventArgs e)
        {
            tbQuestionPriority.Text = "";
            btRandomQuestion.Background = Brushes.GreenYellow;
        }
        private void tbQuestionPriority_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            btRandomQuestion.Background = Brushes.White;
        }

        private void btThema_Click(object sender, RoutedEventArgs e)
        {
            switch(comboBoxThema.Text)
            {
                case "Стандартная(светлая)": MessageBox.Show("пока тут ничего не происходит");
                    break;
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
