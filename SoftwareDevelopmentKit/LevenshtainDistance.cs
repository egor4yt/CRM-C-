using System;

namespace EErmakov.SoftwareDevelop.SoftwareDevelopmentKit
{
    public static class LevenshtainDistance
    {
        /// <summary>
        /// Высчитывает разницу между двумя строками без учета региста
        /// </summary>
        /// <param name="s1">Первая строка</param>
        /// <param name="s2">Вторая строка</param>
        /// <param name="MaxDistance">Допустимая разница в строках</param>
        /// <returns>Совпали строки, или нет</returns>
        public static bool Calculate(string s1, string s2, int MaxDistance)
        {
            s1 = s1.ToLower();
            s2 = s2.ToLower();
            int n = s1.Length + 1;
            int m = s2.Length + 1;
            int[,] table = new int[n, m];

            for (var i = 0; i < n; i++)
                table[i, 0] = i;

            for (var j = 0; j < m; j++)
                table[0, j] = j;

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    int cost = s1[i - 1] == s2[j - 1] ? 0 : 1; //цена замены

                    table[i, j] = Math.Min(table[i - 1, j] + 1,           // удаление
                                            Math.Min(table[i, j - 1] + 1, // вставка
                                            table[i - 1, j - 1] + cost)); // замена

                    if (i > 1 && j > 1
                        && s1[i - 1] == s2[j - 2]
                        && s1[i - 2] == s2[j - 1])
                    {
                        table[i, j] = Math.Min(table[i, j],
                                           table[i - 2, j - 2] + cost); // перестановка
                    }
                }
            }
            if (table[n - 1, m - 1] <= MaxDistance)
                return true;
            else return false;
        }
    }
}
