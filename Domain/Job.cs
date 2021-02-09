namespace EErmakov.SoftwareDevelop.Domain
{
    public class Job
    {
        /// <summary>
        /// Конструктор класса Job.
        /// </summary>
        /// <param name="Title">Название работы.</param>
        /// <param name="StandartPrice">Стандартная цена за работу. -1, если отсутствует.</param>
        public Job(string Title, decimal StandartPrice)
        {
            this.Title = Title;
            this.StandartPrice = StandartPrice;
        }

        /// <summary>
        /// Стандартная цена работы.
        /// </summary>
        private decimal _standartPrice;

        /// <summary>
        /// Название работы.
        /// </summary>
        private string _title;

        /// <summary>
        /// Название работы. Непустая строка.
        /// </summary>
        public string Title 
        {
            get { return _title; }
            set 
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _title = value;
            }
        }

        /// <summary>
        /// Стандартная цена работы. Значение должно быть больше 0. Если стандартной цены нет, или получено значение <= 0, то цена будет -1
        /// </summary>
        public decimal StandartPrice 
        {
            get { return _standartPrice; }
            set
            {
                if (value > 0)
                    _standartPrice = value;
                else _standartPrice = -1;
            }
        }
    }
}
