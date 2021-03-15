using System;
using System.Diagnostics;

namespace Lab1
{
    class Lab1
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите размерность матрицы");
                int size = int.Parse(Console.ReadLine());
                Random rnd = new Random();
                int[] matrix = new int[size * size];
                for (int i = 0; i < size*size; i++)
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

        static void Swap(ref int first, ref int second)
        {
            var temp = first;
            first = second;
            second = temp;
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
