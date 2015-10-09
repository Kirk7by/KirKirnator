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
using NeuralCore;
using ExpertCore;
namespace ExpertComputerService
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        expertCore ExpIit = new expertCore();
        public Window2()
        {
            InitializeComponent();
         //   new expertCore().PlaySerialize();
            
         //   MessageBox.Show(Convert.ToString(new expertCore().a));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OutputsInputs(1);
        }
  
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            OutputsInputs(2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            OutputsInputs(3);
        }

        private void OutputsInputs(int Otv) 
        {
            ExpIit.PlaySerialize();
            label.Content = ExpIit.GetQuntit();
        }
    }
}
