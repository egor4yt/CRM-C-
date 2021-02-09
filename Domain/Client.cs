namespace EErmakov.SoftwareDevelop.Domain
{
    /// <summary>
    /// Класс клиент
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Клиент. Для инициализации обязательно указать Фамилию
        /// </summary>
        /// <param name="LastName">Фамилия</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="SecondName">Отчество</param>
        /// <param name="FNote">Примечание 1</param>
        /// <param name="SNote">Примечание 2</param>
        public Client(string LastName, string FirstName = "", string SecondName = "", string FNote = "", string SNote = "")
        {
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            FirstNote = FNote;
            SecondNote = SNote;
        }
        
        /// <summary>
        /// Фамилия
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Имя
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Отчество
        /// </summary>
        private string _secondName;

        /// <summary>
        /// Примечание 1
        /// </summary>
        public string FirstNote { get; set; }

        /// <summary>
        /// Примечание 2
        /// </summary>
        public string SecondNote { get; set; }

        /// <summary>
        /// Фамилия клиента. Состоит из букв, и необязательного знака "-"
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))//Значение может быть пустым. Доработать проверку: "Если не содержит цирф, пробелов и лишних символов"
                {
                    string result = "";
                    for (int i = 0; i < value.Length; i++)
                        result += i == 0 ? value[i].ToString().ToUpper() : value[i].ToString().ToLower();
                    _lastName = result;
                }
            }
        }
        
        /// <summary>
        /// Имя клиента. Состоит из букв, и необязательного знака "-"
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) // Доработать проверку: "Если не содержит цирф, пробелов и лишних символов"
                {
                    string result = "";
                    for (int i = 0; i < value.Length; i++)
                        result += i == 0 ? value[i].ToString().ToUpper() : value[i].ToString().ToLower();
                    _firstName = result;
                }
            }
        }
        
        /// <summary>
        /// Отчество клиента. Состоит из букв, и необязательного знака "-"
        /// </summary>
        public string SecondName 
        {
            get
            {
                return _secondName;
            }
            set 
            {
                if (true)//Значение может быть пустым. Доработать проверку: "Если не содержит цирф, пробелов и лишних символов"
                {
                    string result = "";
                    for (int i = 0; i < value.Length; i++)
                        result += i == 0 ? value[i].ToString().ToUpper() : value[i].ToString().ToLower();
                    _secondName = result;
                }
            }
        }
        
        /// <summary>
        /// Получение полного имени клиента
        /// </summary>
        /// <returns>Строка в формате "Фамилия Имя Отчество", если они есть</returns>
        public string GetFullName()
        {
            string result = LastName;

            if (!string.IsNullOrWhiteSpace(FirstName))
                result += $" {FirstName}";

            if (!string.IsNullOrWhiteSpace(SecondName))
                result += $" {SecondName}";

            return result;
        }
    }
}
