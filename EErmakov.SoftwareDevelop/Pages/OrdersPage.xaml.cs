using EErmakov.SoftwareDevelop.Domain;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private List<Order> orders = new List<Order>();
        public OrdersPage()
        {
            InitializeComponent();
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            Order CurrentOrder = DataContext as Order;
            List<Client> clients = new List<Client>();
            CSV.Load(out clients);
            decimal Price = -1;

            if (tbPrice.Text.Length > 0 && !decimal.TryParse(tbPrice.Text.Replace('.', ','), out Price))
                errors.AppendLine("В качестве значения цены было задано не число");

            try { CurrentOrder.Client = cbClientsList.SelectedItem as Client; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { CurrentOrder.Price = Price; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { CurrentOrder.JobTitle = tbJobTitle.Text; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { CurrentOrder.State = (State)cbStatusWork.SelectedIndex; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!orders.Contains(DataContext as Order)) orders.Add((DataContext as Order));
            CSV.Save(ref orders);
            RefreshData();
            DataContext = null;
            gListOrders.Visibility = Visibility.Visible;
            gEditOrder.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
            gListOrders.Visibility = Visibility.Visible;
            gEditOrder.Visibility = Visibility.Collapsed;
        }

        private void btnDeleteElement_Click(object sender, RoutedEventArgs e)
        {
            Order CurrentOrder = (sender as Button).DataContext as Order;

            if (MessageBox.Show($"Удалить заказ \"{CurrentOrder.JobTitle}\"?", "Удаление заказа", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                orders.Remove(CurrentOrder);
                CSV.Save(ref orders);
                RefreshData();
            }
        }

        private void btnEditElement_Click(object sender, RoutedEventArgs e)
        {
            gListOrders.Visibility = Visibility.Collapsed;
            gEditOrder.Visibility = Visibility.Visible;
            DataContext = null;
            DataContext = (sender as Button).DataContext as Order;
            List<Client> clients = new List<Client>();
            List<Job> jobs = new List<Job>();
            CSV.Load(out jobs);
            CSV.Load(out clients);
            cbClientsList.ItemsSource = clients;
            cbJobsList.ItemsSource = jobs;

            if (DataContext != null)
            {
                Order CurrentOrder = (DataContext as Order);
                tbJobTitle.Text = CurrentOrder.JobTitle;
                tbPrice.Text = CurrentOrder.Price.ToString();
                Client client = clients.Where(c => c.Id == CurrentOrder.Client.Id).FirstOrDefault();
                cbClientsList.SelectedIndex = clients.IndexOf(client);
            }
            else DataContext = new Order(new Client("Фамилия"), "Работа", 1);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
        /// <summary>
        /// Обновление данных о заказах в таблице.
        /// </summary>
        private void RefreshData()
        {
            tbFinder.Text = "";
            CSV.Load(out orders);
            dgListOrders.ItemsSource = null;
            dgListOrders.ItemsSource = orders;
            DataContext = null;
            tbJobTitle.Text = "";
            tbPrice.Text = "";
            cbStatusWork.SelectedIndex = 0;
            cbJobsList.SelectedIndex = -1;
            cbClientsList.SelectedIndex = -1;
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Order> finded = orders;
            if (tbFinder.Text.Length == 0 && tbShowOnlyIncomplete.IsChecked.HasValue && !tbShowOnlyIncomplete.IsChecked.Value) { RefreshData(); return; }
            else if (tbFinder.Text.Length > 0) finded = orders.Where(o => LevenshtainDistance.Calculate(o.JobTitle, tbFinder.Text, 1) || LevenshtainDistance.Calculate(o.Client.LastName, tbFinder.Text, 1)).ToList();
            if (tbShowOnlyIncomplete.IsChecked.HasValue && tbShowOnlyIncomplete.IsChecked.Value)
                finded = finded.Where(f => f.State != State.Done).ToList();

            dgListOrders.ItemsSource = null;
            dgListOrders.ItemsSource = finded;
        }

        private void cbJobsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Job job = (cbJobsList.SelectedItem as Job) != null ? (cbJobsList.SelectedItem as Job) : new Job("Работа", 1);
            tbJobTitle.Text = job.Title;
            tbPrice.Text = job.StandartPrice.ToString();
        }

        private void tbShowOnlyIncomplete_Checked(object sender, RoutedEventArgs e)
        {
            List<Order> orders = (dgListOrders.ItemsSource as List<Order>).Where(o => o.State != State.Done).ToList();
            dgListOrders.ItemsSource = null;
            dgListOrders.ItemsSource = orders;
        }

        private void tbShowOnlyIncomplete_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tbFinder.Text.Length == 0) { RefreshData(); return; }
            List<Order> finded = orders.Where(o => LevenshtainDistance.Calculate(o.JobTitle, tbFinder.Text, 1) || LevenshtainDistance.Calculate(o.Client.LastName, tbFinder.Text, 1)).ToList();
            dgListOrders.ItemsSource = null;
            dgListOrders.ItemsSource = finded;
        }
    }
}
