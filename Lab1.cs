using System;
using System.Diagnostics;
using static Lab2.Lab2;
using static Lab2.Problems;
using static Lab3.Lab3;
using static Lab4.Lab4;
namespace Lab1
{
    class Lab1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер лабораторной работы:\n1. Алгоритмы сортировки\n2. Методы поиска, хеширование и ферзи\n" +
                "3. Поиск подстроки в строке\n4. Реализация стека/дека\n5. [ДАННЫЕ УДАЛЕНЫ]\n6. Дополнительные задания");
            int num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 1:
                    Run1Lab();
                    break;
                case 2:
                    Run2Lab();
                    break;
                case 3:
                    Run3Lab();
                    break;
                case 4:
                    Run4Lab();
                    break;
                case 5:
                    //Run5Lab();
                    break;
                case 6:
                    RunProblems();
                    break;
                default:
                    break;
            }
        }
        static void Run1Lab()
        {
            while (true)
            {
                Console.WriteLine("Введите размерность матрицы:");
                int size = int.Parse(Console.ReadLine());
                Random rnd = new Random();
                int[] matrix = new int[size * size];
                for (int i = 0; i < size * size; i++)
                {
                    matrix[i] = rnd.Next(-1000, 1001);
                }
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var sorted = Insertion_sort(matrix);
                sw.Stop();
                Console.WriteLine("Время выполения сортировки вставками - " + sw.Elapsed);
                sw.Reset();
                int[] intern = (int[])matrix.Clone();
                sw.Start();
                Array.Sort(intern);
                sw.Stop();
                Console.WriteLine("Время выполения встроенной сортировки - " + sw.Elapsed);
                sw.Reset();
                int[] quick = (int[])matrix.Clone();
                sw.Start();
                Quicksort(quick, 0, quick.Length - 1);
                sw.Stop();
                Console.WriteLine("Время выполнения быстрой сортировки - " + sw.Elapsed);
                sw.Reset();
                Console.WriteLine("Нажмите любую кнопку для повторения");
                Console.ReadLine();
            }
        }
        //сортировка вставками
        static int[] Insertion_sort(int[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var key = array[i];
                var j = i;
                while ((j > 1) && (array[j - 1] > key))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }
                array[j] = key;
            }
            return array;
        }
        static void Swap(ref int first, ref int second)
        {
            var temp = first;
            first = second;
            second = temp;
        }

        //сортировка Хоара

        static void Quicksort(int[] arr, int start, int end)
        {
            {
                int i = start;
                int j = end;
                int x = arr[(start + end) / 2];
                while (i <= j)
                {
                    while (arr[i] < x) i++;
                    while (arr[j] > x) j--;
                    if (i <= j)
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                        i++;
                        j--;
                    }
                }
                if (start < j) Quicksort(arr, start, j);
                if (i < end) Quicksort(arr, i, end);
            }

        }
    }
}
