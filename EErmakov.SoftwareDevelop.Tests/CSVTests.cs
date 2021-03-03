using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using EErmakov.SoftwareDevelop.Domain;
using EErmakov.SoftwareDevelop.SoftwareDevelopmentKit;

namespace EErmakov.SoftwareDevelop.Tests
{
    class CSVTests
    {
        [Test]
        public void SaveAndLoadClientData()
        {
            // arrange
            List<Client> NewClients = new List<Client>();
            NewClients.Add(new Client("ермаков", "егор", "Дмитриевич", "любит чай с лимоном", "+79233410310"));
            NewClients.Add(new Client("пупкин", "Анатолий"));
            NewClients.Add(new Client("человекоВ", "", "виКторовИч"));

            // act
            CSV.Save(ref NewClients);
            List<Client> LoadedClients = new List<Client>();
            CSV.Load(out LoadedClients);


            // assert
            for (int i = 0; i < LoadedClients.Count; i++)
            {
                foreach (Client client in LoadedClients)
                    if (LoadedClients.Where(c => c.Id == client.Id).ToList().Count != 1) Assert.Fail("В таблице есть объекты с одинаковым идентификатором");
                if (LoadedClients[i].FirstName != NewClients[i].FirstName) Assert.Fail($"не сошлось имя у клиентов {i}");
                if (LoadedClients[i].SecondName != NewClients[i].SecondName) Assert.Fail($"не сошлось отчество у клиентов {i}");
                if (LoadedClients[i].LastName != NewClients[i].LastName) Assert.Fail($"не сошлось фамилия у клиентов {i}");
                if (LoadedClients[i].FirstNote != NewClients[i].FirstNote) Assert.Fail($"не сошлось примечание1 у клиентов {i}");
                if (LoadedClients[i].SecondNote != NewClients[i].SecondNote) Assert.Fail($"не сошлось примечание2 у клиентов {i}");
            }
            Assert.AreEqual(LoadedClients.Count, NewClients.Count);
        }
        [Test]
        public void SaveAndLoadJobData()
        {
            // arrange
            List<Job> NewJobs = new List<Job>();
            NewJobs.Add(new Job("Программирование", 200.12M));
            NewJobs.Add(new Job("Дизайн"));
            NewJobs.Add(new Job("Программирование", 110));

            // act
            CSV.Save(ref NewJobs);
            List<Job> LoadedJobs = new List<Job>();
            CSV.Load(out LoadedJobs);


            // assert
            for (int i = 0; i < LoadedJobs.Count; i++)
            {
                foreach (Job job in LoadedJobs)
                    if (LoadedJobs.Where(j => j.Id == job.Id).ToList().Count != 1) Assert.Fail($"В таблице есть объекты с одинаковым идентификатором. Штук: {LoadedJobs.Where(j => j.Id == job.Id).ToList().Count}, ид: {job.Id}");
                if (LoadedJobs[i].Title != NewJobs[i].Title) Assert.Fail($"не сошлось название у работ {i}");
                if (LoadedJobs[i].StandartPrice != NewJobs[i].StandartPrice) Assert.Fail($"не сошлась цена у работ [{i}]");
            }
            Assert.AreEqual(LoadedJobs.Count, NewJobs.Count);
        }
        [Test]
        public void SaveAndLoadOrderData()
        {
            // arrange
            List<Job> NewJobs = new List<Job>();
            NewJobs.Add(new Job("Дизайн"));
            List<Client> NewClients = new List<Client>();
            NewClients.Add(new Client("ермаков", "егор", "Дмитриевич", "любит чай с лимоном", "+79233410310"));
            NewClients.Add(new Client("пупкин", "Анатолий"));
            NewClients.Add(new Client("человекоВ", "", "виКторовИч"));
            List<Order> NewOrders = new List<Order>();
            List<Order> LoadedOrders = new List<Order>();

            // act
            CSV.Save(ref NewClients);
            NewOrders.Add(new Order(NewClients[1], "Дизайн", 200));
            NewOrders.Add(new Order(NewClients[0], "Программирование", 100.50m));
            NewOrders.Add(new Order(NewClients[2], NewJobs[0].Title, 115));
            NewOrders[1].State = State.InProgress;
            NewOrders[2].Payed=true;
            CSV.Save(ref NewOrders);
            CSV.Load(out LoadedOrders);

            // assert
            for (int i = 0; i < LoadedOrders.Count; i++)
            {
                foreach (Order order in LoadedOrders)
                    if (LoadedOrders.Where(o => o.Id == order.Id).ToList().Count != 1) Assert.Fail($"В таблице есть объекты с одинаковым идентификатором");

                if (LoadedOrders[i].Client.Id != NewOrders[i].Client.Id) Assert.Fail($"не сошелся идентификатор клиента у заказов {i}");
                if (LoadedOrders[i].JobTitle != NewOrders[i].JobTitle) Assert.Fail($"не сошлось название работы у заказов [{i}]");
                if (LoadedOrders[i].Price != NewOrders[i].Price) Assert.Fail($"не сошлась цена у заказов [{i}]");
                if (LoadedOrders[i].Payed != NewOrders[i].Payed) Assert.Fail($"не сошелся статус оплаты у заказов [{i}]");
                if (LoadedOrders[i].State != NewOrders[i].State) Assert.Fail($"не сошелся статус оплаты у заказов [{i}]");
            }
            Assert.AreEqual(LoadedOrders.Count, NewOrders.Count);
        }
    }
}
