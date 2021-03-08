using EErmakov.SoftwareDevelop.Domain;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;

namespace EErmakov.SoftwareDevelop.WindowsApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void btnSaveAppSettings_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbCompanyName.Text)) errors.AppendLine("Название кампании не должно быть пустым");
            if (string.IsNullOrWhiteSpace(tbFirstNoteClient.Text) || string.IsNullOrWhiteSpace(tbSecondNoteClient.Text)) errors.AppendLine("Название заметок не должно быть пустым");

            if (errors.Length > 0) { System.Windows.MessageBox.Show(errors.ToString(), "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            ApplicationSettings.CompanyName = tbCompanyName.Text;
            ApplicationSettings.FirstNoteClient = tbFirstNoteClient.Text;
            ApplicationSettings.SecondNoteClient = tbSecondNoteClient.Text;

            CSV.SaveApplicationData();

            System.Windows.MessageBox.Show("Чтобы изменения вступили в силу, необходимо перезапустить приложение!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void btnSaveAppData_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Выберите папку в которую сохранятся данные таблиц";


            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string CurrentPath = folderBrowser.SelectedPath + @"\BackupCrm";
                if (Directory.Exists(CurrentPath))
                    errors.AppendLine("В этой папке уже существуют файлы резервной копии!");

                if (errors.Length > 0)
                {
                    System.Windows.MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                foreach (string pathFile in Directory.GetFiles(CSV.PathDataFolder))
                {
                    string fileName = "";

                    for (int i = 0; i < pathFile.Length; i++)
                    {
                        if (pathFile[i] != '\\')
                            fileName += pathFile[i];
                        else fileName = "";
                    }
                    Directory.CreateDirectory(CurrentPath);
                    File.Copy(pathFile, CurrentPath + $@"\{fileName}");
                }
            }

        }

        private void btnLoadAppData_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Выберите папку с файлами резервного копирования \"BackupCrm\"";


            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string CurrentPath = folderBrowser.SelectedPath;
                if (Directory.Exists(CurrentPath + @"\BackupCrm"))
                    CurrentPath += @"\BackupCrm";

                if (Directory.GetFiles(CurrentPath, "*.fcd").Count() != 4) errors.AppendLine("В этой папке нет каталога \"BackupCrm\" или 4 файлов резервной копии!");
                if (errors.Length > 0)
                {
                    System.Windows.MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Directory.GetFiles(CSV.PathDataFolder).Count() != 1 && System.Windows.MessageBox.Show("При загрузке данных из файлов резервного копирования старые данные будут стёрты\nУбедитесь, что вы сделали резервную копию текущих данных, иначе можете их потерять!\nЗагрузить данные из файлов?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;

                foreach (string pathFile in Directory.GetFiles(CurrentPath, "*.fcd")) //поиск файлов free crm data
                {
                    string fileName = "";

                    for (int i = 0; i < pathFile.Length; i++)
                    {
                        if (pathFile[i] != '\\')
                            fileName += pathFile[i];
                        else fileName = "";
                    }
                    if (!Directory.Exists(CSV.PathDataFolder)) Directory.CreateDirectory(CSV.PathDataFolder);
                    if (File.Exists(CSV.PathDataFolder + $@"\{fileName}")) File.Delete(CSV.PathDataFolder + $@"\{fileName}");
                    File.Copy(pathFile, CSV.PathDataFolder + $@"\{fileName}");
                }
            }

        }

        private void btnAuthorInfo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/egor4yt");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbCompanyName.Text = ApplicationSettings.CompanyName;
            tbFirstNoteClient.Text = ApplicationSettings.FirstNoteClient;
            tbSecondNoteClient.Text = ApplicationSettings.SecondNoteClient;
        }
    }
}
