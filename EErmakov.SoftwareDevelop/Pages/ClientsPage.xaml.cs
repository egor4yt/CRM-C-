using EErmakov.SoftwareDevelop.Domain;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;
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
            Client CurrentClient = (sender as Button).DataContext as Client;

            if (MessageBox.Show($"Удалить клиента \"{CurrentClient.GetFullName()}\"?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
        }
        /// <summary>
        /// Обновление данных о клиентах в таблице.
        /// </summary>
        private void RefreshData()
        {
            tbFinder.Text = "";
            CSV.Load(out clients);
            dgListClients.ItemsSource = null;
            dgListClients.ItemsSource = clients;
            DataContext = null;
            tbLastName.Text = "";
            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbFirstNote.Text = "";
            tbSecondNote.Text = "";
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
