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
using Configurate;
using Microsoft.Win32;
using System.IO;

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

            InitializeComponent();
            ConfigWindow();
            expCore = new Expertcore();
            var HeroNamesList = from lh in expCore.GetPriorityListHero().ToList()
                                select lh.NameHeroes;
            comboBox.ItemsSource = HeroNamesList;
            listBox.ItemsSource = HeroNamesList;
        }

        private void ConfigWindow()
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
            if (tbHeroName.Text != "" && tbQuestName.Text != "" && tbHeroName_Detayl.Text!="")
            {
                string HeroName = tbHeroName.Text + "(" + tbHeroName_Detayl.Text + ")";

                if (ImageSave(HeroName))
                    return;

                Exception ex = expCore.OutputNewHero(HeroName, tbQuestName.Text);
                if (ex != null)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                
                new SumbitCancelWindow("Нам было приятно с вами играть, теперь ваш "+ tbHeroName.Text+" будет присутствовать в моей угадывательной памяти!").Show();
                this.Close();
            }
            else
            {
                TittleLabel2.Content = "Вводите только корректные значения!";
                TittleLabel2.Background = Brushes.DarkRed;
            }
        }

        #region intialyze Image
        string patchImage = null;
        private bool ImageSave(string HeroName)
        {
            bool errors = false;
            if (patchImage != null)
            {
                try
                {
                    if (!Directory.Exists(ExpConfig.Default.patchImages))
                    {
                        Directory.CreateDirectory(ExpConfig.Default.patchImages);
                    }
                    System.IO.File.Copy(patchImage, ExpConfig.Default.patchImages + HeroName + patchImage.Substring(patchImage.Length - 4, 4), true);
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show("Не удалось сохранить картинку. Продолжить отправку?: \n" + ex.Message,"Ошибка сохранения фото",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                        errors = true;
                }
            }
            return errors;
        }
        private void addimagebut_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Файлы изображений|*.png;*.jpg;*.gif|Все файлы(*.*)|*.*";
            openFileDialog1.Title = "Выберите картинку героя";

            if (openFileDialog1.ShowDialog() == true)
            {
                patchImage = openFileDialog1.FileName;
                image.Source = new BitmapImage(new Uri(openFileDialog1.FileName));
            }
        }
        #endregion




        private void Sumbit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            new SumbitCancelWindow().Show();
            this.Close();
        }

        
    }
}
