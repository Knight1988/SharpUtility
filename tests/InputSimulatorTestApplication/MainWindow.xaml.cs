using System.Windows;
using System.Windows.Input;

namespace InputSimulatorTestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CenterButton_OnClick(object sender, RoutedEventArgs e)
        {
            CenterButton.Visibility = Visibility.Hidden;
        }

        private void MainWindow_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            lblPosition.Content = p.ToString();
        }
    }
}
