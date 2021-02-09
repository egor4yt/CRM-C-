using NUnit.Framework;
using EErmakov.SoftwareDevelop.Domain;

namespace EErmakov.SoftwareDevelop.Tests
{
    public class JobTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Title()
        {
            // arrange
            Job j = new Job("Программирование", -1);

            // act
            j.Title = "   ";

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
        public void StandartPriceLess0()
        {
            // arrange
            Job j = new Job("Программирование", 200M);

            // act
            j.StandartPrice = 0;

            // assert
            Assert.AreEqual(j.StandartPrice, -1);
        }
    }
}
