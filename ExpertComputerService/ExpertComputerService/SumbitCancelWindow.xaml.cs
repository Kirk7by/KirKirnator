using Configurate;
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
    /// Логика взаимодействия для SumbitCancelWindow.xaml
    /// </summary>
    public partial class SumbitCancelWindow : Window
    {
        public SumbitCancelWindow(string TittleStr = null)
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

            if (TittleStr!=null)
            {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Media/smileCat.jpg"));
                TittleLabel.Text = TittleStr;
                TittleLabel.Background = Brushes.Green;
                TittleLabel1.Background = Brushes.Green;

                InitializeComponent();
            }
        }
        private void butRetryGame_Click(object sender, RoutedEventArgs e)
        {
            new GeneralWork().Show();
            this.Close();
        }

        private void butExitGame_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void addQuestact_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (textBoxquest.Text == "") throw new System.ArgumentException("не есть хорошо");
                new Repository().AddQuestion(textBoxquest.Text);
                label_success.Visibility = Visibility.Visible;
                label_error.Visibility = Visibility.Collapsed;
                Log(textBlockConsole, "Добавлен вопрос:" + textBoxquest.Text + "\r", Brushes.LightBlue);
                //         textBlockConsole.Text=("Добавлен вопрос:"+ textBoxquest.Text);

            }
            catch(Exception ex) {
                label_success.Visibility = Visibility.Collapsed;
                label_error.Visibility = Visibility.Visible;
            //    textBlockConsole.Text = ("Ошибка: " + ex.Message);
                Log(textBlockConsole, "Ошибка..." +ex.Message +"\r", Brushes.Red);
            }
        }
        public void Log(RichTextBox box1, string msg, object color)//Здесь у обжект будем передавать цвет Brushes.Color!!!
        {
            box1.Dispatcher.Invoke(new Action(() =>
            {
                TextRange range = new TextRange(box1.Document.ContentEnd, box1.Document.ContentEnd);
                range.Text = msg;
                range.ApplyPropertyValue(TextElement.ForegroundProperty, color);
                box1.ScrollToEnd();// функция Autoscroll
            }));
        }
        private void butaddQuests_Click(object sender, RoutedEventArgs e)
        {
            gridAddQuest.Visibility = Visibility.Visible;
            textBoxquest.Text = "";
            label_error.Visibility = Visibility.Collapsed;
            label_success.Visibility = Visibility.Collapsed;
        }
    }
}
