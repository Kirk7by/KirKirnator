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
        public SumbitCancelWindow(string d = null)
        {
            InitializeComponent();
            if (d!=null)
            {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Media/smileCat.jpg"));
                TittleLabel.Text = "Нам было приятно с вами играть, теперь ваш "+d+"будет присутствовать в моей угадывательной памяти";
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
            Application.Current.Shutdown();
        }
    }
}
