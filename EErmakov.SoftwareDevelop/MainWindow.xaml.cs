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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMenuOrders_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void btnMenuClients_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnMenuJobs_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnMenuExport_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnMenuSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
