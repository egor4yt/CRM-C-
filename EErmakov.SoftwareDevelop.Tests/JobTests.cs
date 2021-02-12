using NUnit.Framework;
using EErmakov.SoftwareDevelop.Domain;

namespace EErmakov.SoftwareDevelop.Tests
{
    public class JobTests
    {
        [Test]
        public void Title()
        {
            // arrange
            Job j = new Job("Программирование", -1);

            // act
            try
            {
                j.Title = "   ";
                j.Title = "";
                Assert.Fail("Не было вызвано исключение для Title");
            }
            catch { }

            // assert
            Assert.AreEqual(j.Title, "Программирование");

            //Assert.AreEqual(j.Title, "Программирование");
        }
        [Test]
        public void StandartPriceMore0()
        {
            // arrange
            Job j = new Job("Программирование", 200M);

            // act
            j.StandartPrice = 10.25M;

            // assert
            Assert.AreEqual(j.StandartPrice, 10.25M);
        }
        [Test]
        public void InvalidStandartPrice()
        {
            // arrange
            Job j = new Job("Программирование", 200M);

            // act
            try
            {
                j.StandartPrice = 0;
                Assert.Fail("Не было вызвано исключение для StandartPrice");
            }
            catch { }

            // assert
            Assert.AreEqual(j.StandartPrice, 200M);
        }
    }
}
