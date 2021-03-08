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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private List<Client> clients = new List<Client>();
        public ClientsPage()
        {
            InitializeComponent();
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            try { (DataContext as Client).LastName = tbLastName.Text; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { (DataContext as Client).SecondName = tbSecondName.Text; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { (DataContext as Client).FirstName = tbFirstName.Text; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { (DataContext as Client).FirstNote = tbFirstNote.Text; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            try { (DataContext as Client).SecondNote = tbSecondNote.Text; }
            catch (Exception ex) { errors.AppendLine(ex.Message); }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!clients.Contains(DataContext as Client)) clients.Add((DataContext as Client));
            CSV.Save(ref clients);
            RefreshData();
            DataContext = null;
            gListClients.Visibility = Visibility.Visible;
            gEditClient.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
            gListClients.Visibility = Visibility.Visible;
            gEditClient.Visibility = Visibility.Collapsed;
        }

        private void btnDeleteElement_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            Client CurrentClient = (sender as Button).DataContext as Client;
            List<Order> orders = new List<Order>();
            CSV.Load(out orders);

            if (orders.Where(o => o.Client.Id == CurrentClient.Id).Count() > 0) errors.AppendLine("Этот клиент участвует в Заказе");

            if (errors.Length > 0) { MessageBox.Show($"{errors}", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error); return; }


            if (MessageBox.Show($"Удалить клиента \"{CurrentClient.GetFullName}\"?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                clients.Remove(CurrentClient);
                CSV.Save(ref clients);
                RefreshData();
            }
        }

        private void btnEditElement_Click(object sender, RoutedEventArgs e)
        {
            gListClients.Visibility = Visibility.Collapsed;
            gEditClient.Visibility = Visibility.Visible;
            DataContext = null;
            DataContext = (sender as Button).DataContext as Client;

            if (DataContext != null)
            {
                Client CurrentClient = (DataContext as Client);
                tbLastName.Text = CurrentClient.LastName;
                tbFirstName.Text = CurrentClient.FirstName;
                tbSecondName.Text = CurrentClient.SecondName;
                tbFirstNote.Text = CurrentClient.FirstNote;
                tbSecondNote.Text = CurrentClient.SecondNote;
            }
            else DataContext = new Client("Фамилия");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
            dgHeaderNote1.Header = ApplicationSettings.FirstNoteClient;
            dgHeaderNote2.Header = ApplicationSettings.SecondNoteClient;
            tbNote1.Text = ApplicationSettings.FirstNoteClient + ": ";
            tbNote2.Text = ApplicationSettings.SecondNoteClient + ": ";
        }
        /// <summary>
        /// Обновление данных о клиентах в таблице.
        /// </summary>
        private void RefreshData()
        {
            CSV.Load(out clients);
            dgListClients.ItemsSource = null;
            dgListClients.ItemsSource = clients.OrderBy(c => c.LastName).ToList();
            DataContext = null;
            tbLastName.Text = "";
            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbFirstNote.Text = "";
            tbSecondNote.Text = "";
            tbFinder.Text = "";
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Client> finded = clients.Where(c => LevenshtainDistance.Calculate(c.LastName, tbFinder.Text, 1)).ToList();
            dgListClients.ItemsSource = null;
            dgListClients.ItemsSource = finded;
            if (tbFinder.Text.Length == 0) RefreshData();
        }
    }
}
