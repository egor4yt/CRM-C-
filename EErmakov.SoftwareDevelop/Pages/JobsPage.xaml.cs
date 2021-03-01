using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;
using EErmakov.SoftwareDevelop.Domain;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EErmakov.SoftwareDevelop.WindowsApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для JobsPage.xaml
    /// </summary>
    public partial class JobsPage : Page
    {
        public JobsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
        private void RefreshData()
        {
            List<Job> jobs = new List<Job>();
            CSV.Load(out jobs);
            jobs.Add(new Job("Работа 1",500));
            dgListClients.ItemsSource = null;
            dgListClients.ItemsSource = jobs;
        }

        private void btnReloadData_Click(object sender, RoutedEventArgs e)
        {
            List<Job> jobs = new List<Job>();
            CSV.Load(out jobs);
            jobs.Add(new Job("Работа 2", 500));
        }
    }
}
