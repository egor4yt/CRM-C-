using NUnit.Framework;
using EErmakov.SoftwareDevelop.Domain;

namespace EErmakov.SoftwareDevelop.Tests
{
    public class OrderTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void OrderWithStandartPrice()
        {
            Client c = new Client("ермаков");
            Job j = new Job("Программирование", 150.50M);
            Order o = new Order(c, j);

            Assert.AreEqual(o.Price, 150.50);
        }
        [Test]
        public void OrderWithoutStandartPrice()
        {
            Client c = new Client("ермаков");
            Job j = new Job("Программирование");
            Order o = new Order(c, j);

            Assert.AreEqual(o.Price, 0, "Если стандартная цена у Job не указана, то цена работы должна быть 0");
        }
        [Test]
        public void OrderWithNewJob()
        {
            Client c = new Client("ермаков");
            Order o = new Order(c, "Программирование", 201.23M);
                Assert.IsTrue(o.Price == 201.23M && o.Job.Title == "Программирование" && o.Client.LastName == "Ермаков",
                    $"Цена работы была {o.Price}, должна быть 201.23\nНазвание работы \"{o.Job.Title}\", должно быть \"Программирование\"\nИмя клиента было \"{o.Client.LastName}\", должно быть \"Ермаков\"");
        }
    }
}
