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

namespace TaskCompletionSourceTest
{
    /// <summary>
    /// ChooseWindow1.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseWindow1 : Window
    {
        public ChooseWindow1()
        {
            InitializeComponent();
        }
        public int Result { get; private set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = 1;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Result = 2;
            Close();
        }
    }
}
