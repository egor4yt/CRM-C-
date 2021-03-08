using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using EErmakov.SoftwareDevelop.Domain;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;
using Word = Microsoft.Office.Interop.Word;

namespace EErmakov.SoftwareDevelop.WindowsApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExportPage.xaml
    /// </summary>
    public partial class ExportPage : System.Windows.Controls.Page
    {
        public ExportPage()
        {
            InitializeComponent();
        }

        private void btnExportClients_Click(object sender, RoutedEventArgs e)
        {
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            List<Client> clients = new List<Client>();
            List<Order> orders = new List<Order>();
            CSV.Load(out clients);
            CSV.Load(out orders);
            foreach (Client c in clients)
            {
                List<Order> ordersClient = orders.Where(o => o.Client.Id == c.Id).ToList();//поиск всех заказов, в которых участвует текущий клиент
                if (ordersClient.Count() < 1) continue;
                Word.Paragraph clientInfoParagraph = document.Paragraphs.Add();
                Word.Range clientInfoRange = clientInfoParagraph.Range;
                clientInfoRange.Text = $"Код: {c.Id}, ФИО: {c.GetFullName}";
                clientInfoRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                clientInfoRange.InsertParagraphAfter();

                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table ordersTable = document.Tables.Add(tableRange, ordersClient.Count() + 1, 4);
                ordersTable.Borders.InsideLineStyle = ordersTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                ordersTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                cellRange = ordersTable.Cell(1, 1).Range;
                cellRange.Text = "Работа";
                cellRange = ordersTable.Cell(1, 2).Range;
                cellRange.Text = "Цена";
                cellRange = ordersTable.Cell(1, 3).Range;
                cellRange.Text = "Ход работы";
                cellRange = ordersTable.Cell(1, 4).Range;
                cellRange.Text = "Статус оплаты";

                decimal totalSum = 0;
                decimal totalSumReal = 0;
                for (int i = 0; i < ordersClient.Count(); i++)
                {
                    cellRange = ordersTable.Cell(i + 2, 1).Range;
                    cellRange.Text = ordersClient[i].JobTitle;
                    cellRange = ordersTable.Cell(i + 2, 2).Range;
                    cellRange.Text = ordersClient[i].Price.ToString();
                    cellRange = ordersTable.Cell(i + 2, 3).Range;
                    cellRange.Text = ordersClient[i].StatusWork;
                    cellRange = ordersTable.Cell(i + 2, 4).Range;
                    cellRange.Text = ordersClient[i].StatusPay;
                    totalSum += ordersClient[i].Price;
                    totalSumReal += ordersClient[i].Payed ? ordersClient[i].Price : 0;
                }

                Word.Paragraph clientPayParagraph = document.Paragraphs.Add();
                Word.Range clientPayRange = clientPayParagraph.Range;
                if (totalSum != totalSumReal)
                    clientPayRange.Text = $"Общий вклад (потенциально): {totalSum}\nОбщий вклад (Реально): {totalSumReal}\n";
                else clientPayRange.Text = $"Общий вклад: {totalSum}\n";
                clientPayRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                clientPayRange.InsertParagraphAfter();

                if (tbSetInsertBreak.IsChecked.HasValue && tbSetInsertBreak.IsChecked.Value && c != clients.LastOrDefault())
                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
            }

            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\FreeCRM\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path += $@"{DateTime.Now.ToString().Replace(':', '-')}\";
            Directory.CreateDirectory(path);

            document.SaveAs2(path + "Отчет по клиентам.docx");
            document.SaveAs2(path + "Отчет по клиентам.pdf", Word.WdExportFormat.wdExportFormatPDF);
            document.Close();

            MessageBox.Show($"Отчеты успешно сохранены по следующему пути:\n{path}", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
