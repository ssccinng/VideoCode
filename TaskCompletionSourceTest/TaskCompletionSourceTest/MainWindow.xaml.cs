using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskCompletionSourceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread;
        public MainWindow()
        {
            InitializeComponent();

            thread = new Thread(Loop);
            thread.Start();
        }

        async void Loop()
        {

            while (true)
            {
                Thread.Sleep(5000);

                TaskCompletionSource<int> tcs = new ();

                Dispatcher.Invoke(() =>
                {
                    ChooseWindow1 chooseWindow1 = new ChooseWindow1();
                    chooseWindow1.ShowDialog();

                    tcs.SetResult(chooseWindow1.Result);

                });
                   
                MessageBox.Show("You chose: " + await tcs.Task);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChooseWindow1 chooseWindow1 = new ChooseWindow1();
            chooseWindow1.ShowDialog();
            MessageBox.Show("You chose: " + chooseWindow1.Result);
        }
    }
}