using System.Windows;
using System.Windows.Input;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;
using EErmakov.SoftwareDevelop.WindowsApplication.Pages;

namespace EErmakov.SoftwareDevelop.WindowsApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PageManager.MainFrame = MainFrame;
            CSV.LoadApplicationData();
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMenuOrders_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new OrdersPage());
        }

        private void btnMenuClients_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ClientsPage());
        }

        private void btnMenuJobs_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new JobsPage());
        }

        private void btnMenuExport_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ExportPage());
        }

        private void btnMenuSettings_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new SettingsPage());
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
