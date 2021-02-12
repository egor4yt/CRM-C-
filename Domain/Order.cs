using System;

namespace EErmakov.SoftwareDevelop.Domain
{
    public class Order
    {
        /// <summary>
        /// Консруктор класса Order
        /// </summary>
        /// <param name="Client">Экзепляр клиента</param>
        /// <param name="JobTitle">Название работы</param>
        /// <param name="Price">Цена за работу</param>
        public Order(Client Client, string JobTitle, decimal Price)
        {
            ClientId = Client.Id;
            this.JobTitle = JobTitle;
            this.Price = Price;
        }

        /// <summary>
        /// Конструктор класса Order
        /// </summary>
        /// <param name="Client">Экземпляр класса Client</param>
        /// <param name="Job">Экземпляр класса Job, имеющий значение стандартной цены больше 0</param>
        public Order(Client Client, Job Job)
        {
            ClientId = Client.Id;
            JobTitle = Job.Title;
            if (Job.StandartPrice > 0)
                Price = Job.StandartPrice;
            else throw new Exception("При создании заказа был передан объект Job с недопустимым значением стандартной цены");
        }

        /// <summary>
        /// Цена за работу
        /// </summary>
        private decimal _price;
        private string _jobTitle;

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public uint ClientId { get; set; }
        /// <summary>
        /// Идентификатор заказа. Автоматически задаётся при сохранении
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Работа, выполняемая в заказе
        /// </summary>
        public string JobTitle
        {
            get{ return _jobTitle; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _jobTitle = value;
                else throw new Exception("Название работы не может быть пустым");
            }
        }

        /// <summary>
        /// Цена за работу. больше 0
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                    _price = value;
                else throw new Exception("Цена должна быть больше 0");
            }
        }
    }
}
