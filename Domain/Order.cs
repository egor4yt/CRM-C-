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
            this.Job = new Job(JobTitle);
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
            this.Job = Job;
            this.Price = Job.StandartPrice > 0 ? Job.StandartPrice : 0;
        }

        /// <summary>
        /// Цена за работу
        /// </summary>
        private decimal _price;

        /// <summary>
        /// Клиент
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Работа, выполняемая в заказе
        /// </summary>
        public Job Job { get; set; }

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
            }
        }
    }
}
