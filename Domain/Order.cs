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
            this.Client = Client;
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
            this.Client = Client;
            JobTitle = Job.Title;
            if (Job.StandartPrice > 0)
                Price = Job.StandartPrice;
            else throw new Exception("При создании заказа был передан объект Job с недопустимым значением стандартной цены");
        }

        /// <summary>
        /// Цена за работу
        /// </summary>
        private decimal _price;
        /// <summary>
        /// Название работы
        /// </summary>
        private string _jobTitle;
        /// <summary>
        /// Клиент
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Идентификатор заказа. Автоматически задаётся при сохранении
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// Состояние заказа
        /// </summary>
        public State State { get; set; }
        /// <summary>
        /// Статус выполнения заказа
        /// </summary>
        public string StatusWork
        {
            get
            {
                switch (State)
                {
                    case Domain.State.Queue:
                        return "В очереди";
                    case Domain.State.InProgress:
                        return "Выполняется";
                    case Domain.State.Done:
                        return "Выполнено";
                    default:
                        return "Неизвестно";
                }
            }
        }
        /// <summary>
        /// Состояние оплаты заказа
        /// </summary>
        public bool Payed { get; set; }
        /// <summary>
        /// Статус оплаты заказа
        /// </summary>
        public string StatusPay { get { return Payed ? "Оплачено" : "Не оплачено"; } }
        /// <summary>
        /// Работа, выполняемая в заказе
        /// </summary>
        public string JobTitle
        {
            get { return _jobTitle; }
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
    public enum State
    {
        Queue,
        InProgress,
        Done
    }
}
