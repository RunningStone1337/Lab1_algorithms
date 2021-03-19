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
            long max = (long)Math.Pow(10, 9);
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
                void PlaceNum(int[] arr,int num)
                {
                    while (Array.IndexOf(arr, num) != num-1)
                    {

                    }
                }
            }
        }

    }
}
