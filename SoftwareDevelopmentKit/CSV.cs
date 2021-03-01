using EErmakov.SoftwareDevelop.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EErmakov.SoftwareDevelop.SoftwareDevelopmentKit
{
    public static class CSV
    {
        /// <summary>
        /// Путь к каталогу, в котором хранятся данные
        /// </summary>
#if DEBUG
        public readonly static string PathDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BuisnesCalculatorTests";
#else
        public readonly static string PathFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BuisnesCalculator";
#endif
        /// <summary>
        /// Сохранение списка клиентов и присваивание идентификаторов
        /// </summary>
        /// <param name="clients">Список клиентов для сохранения</param>
        public static void Save(ref List<Client> clients)
        {
            if (!Directory.Exists(PathDataFolder))
                Directory.CreateDirectory(PathDataFolder);

#if DEBUG
            string FilePath = PathDataFolder + @"\ClientsTest.csv";
#else
            string FilePath = PathDataFolder + @"\Clients.csv";
#endif


            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate)) { }

            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.UTF8))
            {
                uint lastId = 0;
                foreach (var c in clients)
                {
                    c.Id = lastId;
                    sw.WriteLine($"{c.Id};{c.FirstName};{c.SecondName};{c.LastName};{c.FirstNote};{c.SecondNote}");
                    lastId++;
                }
                sw.Close();
            }

            for (int i = 0; i < clients.Count; i++)
                foreach (Client client in clients)
                    if (clients.Where(c => c.Id == client.Id).ToList().Count != 1)
                        throw new Exception("При сохранении были созданы объекты с одинаковым идентификатором");
        }
        /// <summary>
        /// Загрузка сохранённых клиентов в передаваемый список
        /// </summary>
        /// <param name="loadedClients">Список в который загрузятся клиенты</param>
        public static void Load(out List<Client> loadedClients)
        {
            loadedClients = new List<Client>();

#if DEBUG
            string FilePath = PathDataFolder + @"\ClientsTest.csv";
#else
            string FilePath = PathDataFolder + @"\Clients.csv";
#endif

            StringBuilder data = new StringBuilder();

            if (File.Exists(FilePath))
                using (StreamReader sr = new StreamReader(FilePath, Encoding.UTF8))
                    data.Append(sr.ReadToEnd());

            int i = 0;
            while (i < data.Length - 1)
            {
                string lastName = "";
                Client c = new Client("фамилия");

                string id = "";
                while (data[i] != ';')
                {
                    id += data[i];
                    i++;
                }
                i++;
                uint clientid = 0;
                if (uint.TryParse(id, out clientid))
                    c.Id = clientid;
                else throw new Exception("Идентификатор клиента не удалось преобразовать в число");


                while (data[i] != ';')
                {
                    c.FirstName += data[i] != '\n' ? data[i].ToString() : ""; ;
                    i++;
                }
                i++;


                while (data[i] != ';')
                {
                    c.SecondName += data[i] != '\n' ? data[i].ToString() : ""; ;
                    i++;
                }
                i++;

                while (data[i] != ';')
                {
                    lastName += data[i] != '\n' ? data[i].ToString() : ""; ;
                    i++;
                }
                c.LastName = lastName;
                i++;

                while (data[i] != ';')
                {
                    c.FirstNote += data[i] != '\n' ? data[i].ToString() : ""; ;
                    i++;
                }
                i++;

                while (data[i] != '\r' && data[i] != '\n')
                {
                    c.SecondNote += data[i] != '\n' ? data[i].ToString() : ""; ;
                    i++;
                }
                i++;

                loadedClients.Add(c);

            }
        }

        /// <summary>
        /// Сохранение списка работ
        /// </summary>
        /// <param name="jobs">Список работ для сохранения</param>
        public static void Save(ref List<Job> jobs)
        {
            if (!Directory.Exists(PathDataFolder))
                Directory.CreateDirectory(PathDataFolder);

#if DEBUG
            string FilePath = PathDataFolder + @"\JobsTest.csv";
#else
            string FilePath = PathDataFolder + @"\Jobs.csv";
#endif
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate)) { }

            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.UTF8))
            {
                uint lastid = 0;
                foreach (var j in jobs)
                {
                    j.Id = lastid;
                    sw.WriteLine($"{j.Id};{j.Title};{j.StandartPrice}");
                    lastid++;
                }
                sw.Close();
            }

            for (int i = 0; i < jobs.Count; i++)
                foreach (Job job in jobs)
                    if (jobs.Where(j => j.Id == job.Id).ToList().Count != 1)
                        throw new Exception("При сохранении были созданы объекты с одинаковым идентификатором");
        }
        /// <summary>
        /// Загрузка сохранённых работ в передаваемый список
        /// </summary>
        /// <param name="loadedJobs">Список в который загрузятся работы</param>
        public static void Load(out List<Job> loadedJobs)
        {
            loadedJobs = new List<Job>();

#if DEBUG
            string FilePath = PathDataFolder + @"\JobsTest.csv";
#else
            string FilePath = PathDataFolder + @"\Jobs.csv";

#endif

            StringBuilder data = new StringBuilder();

            if (File.Exists(FilePath))
                using (StreamReader sr = new StreamReader(FilePath, Encoding.UTF8))
                    data.Append(sr.ReadToEnd());

            int i = 0;
            while (i < data.Length - 1)
            {
                string title = "";
                string standartPrice = "";
                string id = "";

                while (data[i] != ';')
                {
                    id += data[i] != '\n' ? data[i].ToString() : "";
                    i++;
                }
                i++;

                while (data[i] != ';')
                {
                    title += data[i] != '\n' ? data[i].ToString() : "";
                    i++;
                }
                i++;

                while (data[i] != '\r' && data[i] != '\n')
                {
                    standartPrice += data[i];
                    i++;
                }
                i++;

                decimal sp; // standart price 
                if (!decimal.TryParse(standartPrice, out sp))
                    throw new Exception("Не удалось преобразовать стандартную цену в число");


                uint jid; // job id
                if (!uint.TryParse(id, out jid))
                    throw new Exception("Не удалось преобразовать идентификатор в число");

                Job newjob = new Job(title, sp);
                newjob.Id = jid;
                loadedJobs.Add(newjob);
            }
        }
        /// <summary>
        /// Сохранения списка заказов и присваивание идентификаторов
        /// </summary>
        /// <param name="orders">Список заказов для сохранения</param>
        public static void Save(ref List<Order> orders)
        {
            if (!Directory.Exists(PathDataFolder))
                Directory.CreateDirectory(PathDataFolder);

#if DEBUG
            string FilePath = PathDataFolder + @"\OrdersTest.csv";
#else
            string FilePath = PathDataFolder + @"\Orders.csv";
#endif
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate)) { }

            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.UTF8))
            {
                uint lastid = 0;
                foreach (var o in orders)
                {
                    o.Id = lastid;
                    sw.WriteLine($"{o.Id};{o.ClientId};{o.JobTitle};{o.Price}");
                    lastid++;
                }
                sw.Close();
            }

            for (int i = 0; i < orders.Count; i++)
                foreach (Order order in orders)
                    if (orders.Where(o => o.Id == order.Id).ToList().Count != 1)
                        throw new Exception("При сохранении были созданы объекты с одинаковым идентификатором");
        }
        /// <summary>
        /// Загрузка сохранённых заказов в передаваемый список
        /// </summary>
        /// <param name="loadedOrders">Список в который загрузятся заказы</param>
        public static void Load(out List<Order> loadedOrders)
        {
            loadedOrders = new List<Order>();

#if DEBUG
            string FilePath = PathDataFolder + @"\OrdersTest.csv";
#else
            string FilePath = PathDataFolder + @"\Orders.csv";
#endif

            StringBuilder data = new StringBuilder();

            if (File.Exists(FilePath))
                using (StreamReader sr = new StreamReader(FilePath, Encoding.UTF8))
                    data.Append(sr.ReadToEnd());


            List<Client> clients = new List<Client>();
            Load(out clients);
            int i = 0;
            while (i < data.Length - 1)
            {
                string param = "";// переменная для парсинга нетекстовых типов
                string jobtitle = "";

                while (data[i] != ';')
                {
                    param += data[i] != '\n' ? data[i].ToString() : "";
                    i++;
                }
                uint id;
                if (!uint.TryParse(param, out id))
                    throw new Exception("не удалось преобразовать идентификатор заказа в числовой тип");
                i++;
                param = "";

                while (data[i] != ';')
                {
                    param += data[i] != '\n' ? data[i].ToString() : "";
                    i++;
                }
                uint clientid;
                if (!uint.TryParse(param, out clientid))
                    throw new Exception("не удалось преобразовать идентификатор клиента в числовой тип");
                i++;
                param = "";

                while (data[i] != ';')
                {
                    jobtitle += data[i] != '\n' ? data[i].ToString() : "";
                    i++;
                }
                i++;

                while (data[i] != '\r' && data[i] != '\n')
                {
                    param += data[i] != '\n' ? data[i].ToString() : "";
                    i++;
                }
                i++;
                decimal price;
                if (!decimal.TryParse(param, out price))
                    throw new Exception($"не удалось преобразовать цену заказа в числовой тип \"{param}\"");

                Order neworder = new Order(clients.Where(c => c.Id == clientid).FirstOrDefault(), jobtitle, price);

                neworder.Id = id;
                param = "";
                jobtitle = "";

                loadedOrders.Add(neworder);
            }
        }
    }
}
