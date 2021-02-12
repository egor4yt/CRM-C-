using System;

namespace EErmakov.SoftwareDevelop.Domain
{
    public class Job
    {
        /// <summary>
        /// Конструктор класса Job.
        /// </summary>
        /// <param name="Title">Название работы.</param>
        /// <param name="StandartPrice">Стандартная цена за работу. -1, если отсутствует.</param>
        public Job(string Title, decimal StandartPrice = -1)
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
        /// Идентификатор работы. Автоматически задаётся при сохранении общего списка работ.
        /// </summary>
        public uint Id { get; set; }


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
                else throw new Exception("Название работы не может быть пустым или состоять из пробелов");

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
                if (value > 0 || value == -1)
                    _standartPrice = value;
                else throw new Exception("Значение стандартной цены должно быть больше 0, или равно -1");
            }
        }
    }
}
