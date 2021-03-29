#define strings
//#define taken
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lab3
{
    static class Lab3
    {
        internal static void Run3Lab()
        {
            Random rnd = new Random();
            Stopwatch sw = new Stopwatch();
#if strings
            #region KMP
            Console.WriteLine("Введите строку для поиска или 0 чтобы перейти к следующему заданию");
            string str = Console.ReadLine();
            while (str != "0")
            {
                Console.WriteLine("Введите подстроку для поиска");
                string word = Console.ReadLine();
                //str = "aabaabaaaabaabaaab";
                //word = "aabaab";
                var start = DateTime.Now;
                int res = KMP(str, word);
                var end = DateTime.Now;
                Console.WriteLine($"Затраченное время на выполнение: {start - end}");
                Console.WriteLine($"Индекс начала искомой подстроки в строке: {res}");
                Console.WriteLine("Введите строку для поиска или 0 чтобы выйти");
                str = Console.ReadLine();
            }
            #endregion
            #region Boyer
            #endregion
#endif
#if taken
            //long max = (long)Math.Pow(10, 9);
            #region Taken
            Console.WriteLine("Введите любое значение кроме 0 чтобы начать игру или 0 чтобы выйти");
            var enter = int.Parse(Console.ReadLine());
            while (enter != 0)
            {
                var arr = new int[16];
                int count = 1;
                var avialible = new List<int>(16);
                for (int i = 0; i < avialible.Capacity; i++)
                {
                    avialible.Add(i);
                }
                Console.WriteLine($"Введите исходную последовательность неповторяющихся чисел от 0 до 15 включительно для игры в пятнашки или 20 для перехода к следующему заданию");
                while (count < 17)
                {
                    Console.WriteLine($"Введите {count} число");
                    arr[count - 1] = int.Parse(Console.ReadLine());
                    avialible.Remove(arr[count - 1]);
                    count++;
                    Console.Write($"Доступные числа для ввода: ");
                    foreach (var item in avialible)
                    {
                        Console.Write(item + " ");
                    }
                }
                var start = DateTime.Now;
                int[] res = Taken(arr);
                var end = DateTime.Now;
                Console.WriteLine($"Полученная последовательность перестановок: ");
                foreach (var item in res)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine($"Затраченное время на выполнение: {start - end}");
                Console.WriteLine("Введите любое значение кроме 0 чтобы начать игру или 0 чтобы выйти");
                enter = int.Parse(Console.ReadLine());
            }
            #endregion
#endif
        }

        static int KMP(string str, string word)
        {
            bool flag = true;
            var tmp = str + word;
            char ch = ' ';
            while (flag)
            {
                if (tmp.Contains(ch))
                {
                    ch++;
                }
                else
                {
                    flag = false;
                }
            }
            tmp = word + ch + str;
            var prefs = new int[tmp.Length];
            CalcPrefs(tmp);
            for (int i = word.Length; i < prefs.Length; i++)
            {
                if (prefs[i] == word.Length)
                {
                    return i - word.Length * 2;
                }
            }
            return -1;
            void CalcPrefs(string word)
            {
                for (int i = 0, j = 1; j < word.Length; j++, i = 0)
                {
                    string tempi = word[i].ToString(), tempj = word[j].ToString();
                    int ended = j;
                    while (tempi.Length <= j)
                    {
                        if (tempi.Equals(tempj) && tempj.Length > prefs[j])
                        {
                            prefs[j] = tempj.Length;
                        }
                        i++;
                        ended--;
                        tempi += word[i].ToString();
                        tempj = word[ended].ToString() + tempj;
                    }
                }
            }
        }

        static int[] Taken(int[] arr)
        {
            var res = new List<int>();
            var counter = 0;
            bool[] placed = new bool[16];
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] == 0)
                    {
                        counter += (i / 4) + 1;
                        continue;
                    }
                    if (arr[j] == 0)
                    {
                        continue;
                    }
                    if (arr[i] > arr[j])
                    {
                        counter++;
                    }
                }
            }
            if (counter % 2 == 1)//если нерешаемая
            {
                return null;
            }
            PlaceFirstRow(arr);
            return null;

            void PlaceFirstRow(int[] arr)
            {
                for (int i = 1; i < 5; i++)
                {
                    PlaceNum(arr, i);
                }
                void PlaceNum(int[] arr, int num)
                {
                    while (Array.IndexOf(arr, num) != num - 1)
                    {

                    }
                }
            }
        }

    }
}
