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
            // arrange
            Client c = new Client("ермаков");

            // act
            Job j = new Job("Программирование", 150.50M);
            Order o = new Order(c, j);

            // assert
            Assert.AreEqual(o.Price, 150.50);
        }
        [Test]
        public void OrderWithoutStandartPrice()
        {
            // arrange
            Client c = new Client("ермаков");

            // act
            Job j = new Job("Программирование");
            try
            {
                Order o = new Order(c, j);
                Assert.Fail("Не обработано исключение при создании Order с недопустимым значение стандартной цены");
            }
            catch { }

            // assert
            Assert.Pass();
        }
        [Test]
        public void OrderWithNewJob()
        {
            // arrange
            Client c = new Client("ермаков");

            // act
            Order o = new Order(c, "Программирование", 201.23M);

            // assert
            Assert.IsTrue(o.Price == 201.23M && o.JobTitle == "Программирование",
                $"Цена работы была {o.Price}, должна быть 201.23\nНазвание работы \"{o.JobTitle}\", должно быть \"Программирование\"\n");
        }
    }
}