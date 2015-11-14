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
using System.Data;
using Configurate;
using ExpertComputerService.LearnSystemsMaterials;

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
            }
        }
        enum selectedButtons
        {
            HeroesBut,
            QuestionsBut,
            DominatingBut
        }
        selectedButtons SelectBut;

       


        private void buttonHeroes_Click(object sender, RoutedEventArgs e)
        {
         //   var a = (new Repository().GetHeroesSource()).Select(d => new { d.NameHeroes, d.TextHero, d.WeigthHero }).ToList();
            dgrid.ItemsSource = (new Repository().GetHeroesSource()).Select(d => new { d.NameHeroes, d.TextHero, d.WeigthHero }).ToList();
            SelectBut = selectedButtons.HeroesBut; 
        }
        private void buttonQuestions_Click(object sender, RoutedEventArgs e)
        {
            dgrid.ItemsSource=(new Repository().GetQuestionsSource()).Select(d => new{ d.NameQestion } ).Distinct().ToList();
            SelectBut = selectedButtons.QuestionsBut;
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
            SelectBut = selectedButtons.DominatingBut;
        }
        

        private void deletebut_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (dgrid.SelectedItem != null)
                {
                    PHero a = dgrid.SelectedItem as PHero;
                    var NameQuest = dgrid.SelectedCells[0].Item.ToString();
               
                
               // MessageBox.Show(NameQuest.Substring("{"));
                }
                Exception ERep=null;

                switch(SelectBut)
                {
                    case selectedButtons.QuestionsBut:
                        
                        ERep = new Repository().RemoveQuestion(textBoxAddQuestion.Text);
                        break;
                                   
               }

            


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
