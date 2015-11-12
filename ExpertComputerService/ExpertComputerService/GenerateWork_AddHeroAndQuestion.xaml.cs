﻿using System;
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
using Configurate;

namespace ExpertComputerService
{
    /// <summary>
    /// Логика взаимодействия для GenerateWork_AddHeroAndQuestion.xaml
    /// </summary>
    public partial class GenerateWork_AddHeroAndQuestion : Window
    {
        Expertcore expCore;
        public GenerateWork_AddHeroAndQuestion()
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
            }
            InitializeComponent();
            expCore = new Expertcore();
            var HeroNamesList= from lh in expCore.GetPriorityListHero().ToList()
                          select lh.NameHeroes;
            comboBox.ItemsSource = HeroNamesList;
            listBox.ItemsSource = HeroNamesList;
        }

        

        private void buttonExist_Click(object sender, RoutedEventArgs e)
        {
            
            Exception ex = expCore.ShippingСonfirmQuestionProbability(comboBox.Text);
            if (ex != null)
            {
                MessageBox.Show(ex.Message); 
            }
            else
            {
                MessageBox.Show("Success!!!");
                new SumbitCancelWindow("Мне было очень приятно с вами играть, теперь "+comboBox.Text+" будет всё чаще угадываться!").Show();
                this.Close();
            }
        }


        private void buttonNoExist_Click(object sender, RoutedEventArgs e)
        {
            GridSelectedHero.Visibility = Visibility.Collapsed;
            vievbox2.Visibility = Visibility.Visible;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBox.Text = listBox.SelectedItem as string;
        }

        private void Sumbit_Click(object sender, RoutedEventArgs e)
        {
            if (tbHeroName.Text != "" && tbQuestName.Text != "")
            {
                Exception ex = expCore.OutputNewHero(tbHeroName.Text, tbQuestName.Text);
                if (ex != null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show("successfully added question");

                new SumbitCancelWindow("Нам было приятно с вами играть, теперь ваш "+ tbHeroName.Text+" будет присутствовать в моей угадывательной памяти!").Show();
                this.Close();
            }
            else
            {
                TittleLabel2.Content = "Вводите только корректные значения!";
                TittleLabel2.Background = Brushes.DarkRed;
            }
        }
        private void Sumbit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            new SumbitCancelWindow().Show();
            this.Close();
        }
    }
}
