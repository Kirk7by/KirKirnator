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
            uloadingBd();
            InitializeElementsInConfig();
        }

        private void InitializeElementsInConfig()
        {
            tbQuestionPriority.Text = ExpConfig.Default.PriorytyQuestions;
            tbMinProbabilityQuestion.Text = Convert.ToString(ExpConfig.Default.MinProbalityQuestion).Remove(0,2);
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



        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try {
                ExpConfig.Default.PriorytyQuestions = tbQuestionPriority.Text;
                ExpConfig.Default.MinProbalityQuestion = Convert.ToDouble("0,"+tbMinProbabilityQuestion.Text);
                ExpConfig.Default.Save();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btDefaultSettings_Click(object sender, RoutedEventArgs e)
        {
            tbQuestionPriority.Text = "Random";
            tbMinProbabilityQuestion.Text = "90";    
        }
    }
}
