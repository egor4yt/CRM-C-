using EErmakov.SoftwareDevelop.Domain;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace EErmakov.SoftwareDevelop.WindowsApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для JobsPage.xaml
    /// </summary>
    public partial class JobsPage : Page
    {
        private List<Job> jobs = new List<Job>();
        public JobsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
        /// <summary>
        /// Обновление данных о работах в таблице.
        /// </summary>
        private void RefreshData()
        {
            tbFinder.Text = "";
            CSV.Load(out jobs);
            dgListJobs.ItemsSource = null;
            dgListJobs.ItemsSource = jobs;
            DataContext = null;
            tbTitle.Text = "";
            tbStandartPrice.Text = "";
        }

        private void btnDeleteElement_Click(object sender, RoutedEventArgs e)
        {
            Job CurrentJob = (sender as Button).DataContext as Job;
            if (MessageBox.Show($"Удалить работу \"{CurrentJob.Title}\"?", "Удаление работы", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                jobs.Remove(CurrentJob);
                CSV.Save(ref jobs);
                RefreshData();
            }
        }

        private void btnEditElement_Click(object sender, RoutedEventArgs e)
        {
            gListOrders.Visibility = Visibility.Collapsed;
            gEditJob.Visibility = Visibility.Visible;
            DataContext = null;
            DataContext = (sender as Button).DataContext as Job;

            if (DataContext != null)
            {
                tbTitle.Text = (DataContext as Job).Title;
                tbStandartPrice.Text = (DataContext as Job).StandartPrice.ToString();
            }
            else DataContext = new Job("Название");
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            decimal StandartPrice = -1;

            if (tbStandartPrice.Text.Length > 0 && !decimal.TryParse(tbStandartPrice.Text, out StandartPrice))
                errors.AppendLine("В качестве значения цены было задано не число");

            try { (DataContext as Job).Title = tbTitle.Text; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { (DataContext as Job).StandartPrice = StandartPrice; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!jobs.Contains(DataContext as Job)) jobs.Add((DataContext as Job));
            CSV.Save(ref jobs);
            RefreshData();
            DataContext = null;
            gListOrders.Visibility = Visibility.Visible;
            gEditJob.Visibility = Visibility.Collapsed;
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Job> finded = jobs.Where(j => LevenshtainDistance.Calculate(j.Title, tbFinder.Text, 1)).ToList();
            dgListJobs.ItemsSource = null;
            dgListJobs.ItemsSource = finded;
            if (tbFinder.Text.Length == 0) RefreshData();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
            gListOrders.Visibility = Visibility.Visible;
            gEditJob.Visibility = Visibility.Collapsed;
        }
    }
}
