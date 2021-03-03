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
            Client c = new Client("�������", "����", "����������", "��� �������� ����������");

            // act
            c.LastName = "��������";
            c.FirstName = "";
            try
            {
                c.LastName = "   ";
                Assert.Fail("�� ���� ������� ���������� ��� LastName");
            }
            catch {}

            // assert
            Assert.AreEqual(c.LastName, "��������");
        }
        [Test]
        public void FirstName()
        {
            // arrange
            Client c = new Client("�������", "����", "����������", "��� �������� ����������");

            // act
            c.FirstName = "�������";

            // assert
            Assert.AreEqual(c.FirstName, "�������");
        }
        [Test]
        public void SecondName()
        {
            // arrange
            Client c = new Client("�������", "����", "����������", "��� �������� ����������");

            // act
            c.SecondName = "����������";

            // assert
            Assert.AreEqual(c.SecondName, "����������");
        }
        [Test]
        public void FullName()
        {
            // arrange
            Client c = new Client("�������", "����", "����������", "��� �������� ����������");

            c.LastName = "��������";
            c.FirstName = "�����";
            c.SecondName = "����������";

            // assert
            Assert.AreEqual(c.GetFullName, "�������� ����� ����������");
        }
        [Test]
        public void InitializeClient()
        {
            // arrange
            Client c = new Client("�������", "����", "����������", "��� �������� ����������");

            // act
            c.FirstName = "";

            // assert
            Assert.AreEqual(c.GetFullName, "������� ����������");
        }
    }
}