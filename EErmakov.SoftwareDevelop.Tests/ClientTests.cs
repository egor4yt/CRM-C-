using EErmakov.SoftwareDevelop.Domain;
using NUnit.Framework;

namespace EErmakov.SoftwareDevelop.Tests
{
    public class ClientTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void LastName()
        {
            // arrange
            Client c = new Client("ермаков", "еГоР", "дмитриеВич", "Это тестовое примечание");

            // act
            c.LastName = "никоЛаеВ";
            c.FirstName = "";
            try
            {
                c.LastName = "   ";
                Assert.Fail("Не было вызвано исключение для LastName");
            }
            catch {}

            // assert
            Assert.AreEqual(c.LastName, "Николаев");
        }
        [Test]
        public void FirstName()
        {
            // arrange
            Client c = new Client("ермаков", "еГоР", "дмитриеВич", "Это тестовое примечание");

            // act
            c.FirstName = "алексЕй";

            // assert
            Assert.AreEqual(c.FirstName, "Алексей");
        }
        [Test]
        public void SecondName()
        {
            // arrange
            Client c = new Client("ермаков", "еГоР", "дмитриеВич", "Это тестовое примечание");

            // act
            c.SecondName = "вИкторовиЧ";

            // assert
            Assert.AreEqual(c.SecondName, "Викторович");
        }
        [Test]
        public void FullName()
        {
            // arrange
            Client c = new Client("ермаков", "еГоР", "дмитриеВич", "Это тестовое примечание");

            c.LastName = "николаев";
            c.FirstName = "антон";
            c.SecondName = "викторович";

            // assert
            Assert.AreEqual(c.GetFullName, "Николаев Антон Викторович");
        }
        [Test]
        public void InitializeClient()
        {
            // arrange
            Client c = new Client("ермаков", "еГоР", "дмитриеВич", "Это тестовое примечание");

            // act
            c.FirstName = "";

            // assert
            Assert.AreEqual(c.GetFullName, "Ермаков Дмитриевич");
        }
    }
}