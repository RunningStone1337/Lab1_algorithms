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
                var start = DateTime.Now;
                string res = MaxDigit(arr);
                var end = DateTime.Now;
                Console.WriteLine($"Составленное число: {res}");
                Console.WriteLine($"Затраченное время на выполнение: {start - end}");
                Console.WriteLine($"Введите размерность генерируемого массива в диапазоне [1;100] или 0 для перехода к следующему заданию");
                size = int.Parse(Console.ReadLine());
            }
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
    }
}
