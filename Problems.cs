using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lab2
{
    static class Problems
    {
        static internal void RunProblems()
        {
            Random rnd = new Random();
            Stopwatch sw = new Stopwatch();
            long max = (long)Math.Pow(10, 9);
            #region MaxDigit
            Console.WriteLine($"Введите размерность генерируемого массива в диапазоне [1;100] или 0 для перехода к следующему заданию");
            var size = int.Parse(Console.ReadLine());
            while (size > 0 && size <= 100)
            {
                var arr = new int[size];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = rnd.Next(0, (int)max + 1);
                }
                var start1 = DateTime.Now;
                string res = MaxDigit(arr);
                var end1 = DateTime.Now;
                Console.WriteLine($"Составленное число: {res}");
                Console.WriteLine($"Затраченное время на выполнение: {start1 - end1}");
                Console.WriteLine($"Введите размерность генерируемого массива в диапазоне [1;100] или 0 для перехода к следующему заданию");
                size = int.Parse(Console.ReadLine());
            }
            #endregion
            #region ConcatSum
            Console.WriteLine($"Введите строку для поиска или 0 для перехода к следующему заданию");
            var str = Console.ReadLine();
            var start = DateTime.Now;
            int ress = ConcatSum(str);
            var end = DateTime.Now;
            Console.WriteLine($"Найденное число подстрок в строке: {ress}");
            Console.WriteLine($"Затраченное время на выполнение: {start - end}");
            #endregion

        }

        private static string MaxDigit(int[] arr)
        {
            List<int> list = new List<int>(arr.Length);
            foreach (var item in arr)
            {
                list.Add(item);
            }
            string res = "";
            List<string> newList = list.ConvertAll<string>(i => i.ToString());
            newList.Sort(MyCompare);
            for (int i = 0; i < list.Count; i++)
            {
                res += newList[i];
            }
            if (res[0] == '0' && res.Length > 1)
            {
                return "0";
            }
            return res;

            static int MyCompare(string first, string second)
            {
                string f = first + second;
                string s = second + first;
                return f.CompareTo(s) > 0 ? -1 : 1;
            }
        }
        static int ConcatSum(string str)
        {
            int counter = 0;
            var subs = new List<string>();
            for (int i = 0; i < str.Length - 1; i++)
            {
                int j = i + 1;
                while (j <= str.Length)
                {
                    string left = str.Substring(i, j - i);
                    string right;
                    if (left.Length > (str.Length - j))
                    {
                        right = str.Substring(j);
                    }
                    else
                    {
                        right = str.Substring(j, left.Length);
                    }
                    if (left.Equals(right)&&!subs.Contains(left))
                    {
                        subs.Add(left);
                        counter++;
                    }
                    j++;
                }
            }
            return counter;
        }
    }
}
