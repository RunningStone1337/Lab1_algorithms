#define stack
#define deque

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab4
{
    public static class Lab4
    {
        internal static void Run4Lab()
        {
            Random rnd = new Random();
            Stopwatch sw = new Stopwatch();
            string filename = "Books.txt";
            string dir = Directory.GetCurrentDirectory();
            while (!File.Exists(dir + Path.DirectorySeparatorChar + filename))
            {
                dir = dir.Substring(0, dir.LastIndexOf('\\'));
            }
#if stack
            #region Stack
            MyStack<int> stack = new MyStack<int>();
            stack.Push(0);
            stack.Push(3);
            var peek = stack.Peek();
            stack.Pop();
            stack.Pop();
            #endregion
#endif
#if deque
            #region Deque
            MyDeque<int> deque = new MyDeque<int>();
            deque.Enqueue(5);
            deque.Dequeue(4);
            var head = deque.PopHead();
            var tail = deque.PopTail();
            var first = deque.RemoveFirst();
            var last = deque.RemoveLast();
            #endregion
#endif
            #region Tasks
            #region Task1
            MyDeque<string> deq1 = new MyDeque<string>();
            using (StreamReader reader = new StreamReader(dir + Path.DirectorySeparatorChar + filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    deq1.Enqueue(line);
                }
            }
            var t1res = Task1(deq1);
            foreach (var item in t1res)
            {
                Console.WriteLine(item);
            }
            #endregion
            #region Task2

            #endregion
            #region Task3
            Plate plate6 = new Plate(6);
            Plate plate5 = new Plate(5);
            Plate plate4 = new Plate(4);
            Plate plate3 = new Plate(3);
            Plate plate2 = new Plate(2);
            Plate plate1 = new Plate(1);
            Plate[] plates = new Plate[] { plate5, plate4, plate3, plate2, plate1 };
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(dir + Path.DirectorySeparatorChar + "StackData.txt", FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, plates);
            }
            using (FileStream fs = new FileStream(dir + Path.DirectorySeparatorChar + "StackData.txt", FileMode.OpenOrCreate))
            {
                plates = bf.Deserialize(fs) as Plate[];
            }
            var t3res = Task3(plates);
            #endregion
            #region Task4

            #endregion
            #region Task5

            #endregion
            #region Task6

            #endregion
            #region Task7

            #endregion
            #region Task8

            #endregion
            #region Task9

            #endregion
            #region Task10

            #endregion
            #region Task11

            #endregion
            #endregion
        }
        /*
         Отсортировать строки файла, содержащие названия книг, в алфавитном порядке с
        использованием двух деков.
         */
        public static MyDeque<T> Task1<T>(MyDeque<T> input) where T : IComparable<T>
        {
            var first = new MyDeque<T>();
            var second = new MyDeque<T>();
            first.Dequeue(input.RemoveFirst());
            while (input.Count > 0)
            {
                var temp = input.RemoveFirst();
                if (temp.CompareTo(first.PopHead()) <= 0)//если temp должен быть перед первым
                {
                    first.Dequeue(temp);//ставим в первый дек в начало
                }
                else if (temp.CompareTo(first.PopTail()) < 0)//если temp должен быть перед последним
                {
                    while (temp.CompareTo(first.PopTail()) <= 0)//пока temp не будет >= first.Tail
                    {
                        second.Enqueue(first.RemoveLast());
                    }
                    first.Enqueue(temp);
                    while (second.Count > 0)
                    {
                        first.Enqueue(second.RemoveLast());
                    }
                }
                else
                {
                    first.Enqueue(temp);
                }
            }
            return first;
        }
        /*
         * Дек содержит последовательность символов для шифровки сообщений. Дан
         * текстовый файл, содержащий зашифрованное сообщение. Пользуясь деком,
         * расшифровать текст. Известно, что при шифровке каждый символ сообщения
         * заменялся следующим за ним в деке по часовой стрелке через один.
         */
        public static string Task2(MyDeque<string> input)
        {
            return null;
        }
        /*
         Даны три стержня и n дисков различного размера. Диски можно надевать на
        cтержни, образуя из них башни. Перенести n дисков со стержня А на стержень С,
        сохранив их первоначальный порядок. При переносе дисков необходимо соблюдать
        следующие правила:- на каждом шаге со стержня на стержень переносить только один диск;
        - диск нельзя помещать на диск меньшего размера;
        - для промежуточного хранения можно использовать стержень В.
        Реализовать алгоритм, используя три стека вместо стержней А, В, С. Информация о дисках хранится в исходном файле.*/
        public static MyDeque<Plate> Task3(Plate[] input)
        {
            var res = new MyDeque<Plate>();
            var stacks = new MyStack<Plate>[3] { new MyStack<Plate>(), new MyStack<Plate>(), new MyStack<Plate>() };
            var first = stacks[0];
            var second = stacks[1];
            var third = stacks[2];
            foreach (var item in input)
            {
                first.Push(item);
            }
            PlacePlate(first, third, second, first.Count);
            foreach (var item in third)
            {
                res.Enqueue(item);
            }
            return res;
            void PlacePlate(MyStack<Plate> first, MyStack<Plate> third, MyStack<Plate> second, int count)
            {
                if (count != 0)
                {
                    PlacePlate(first, second, third, count - 1);
                    third.Push(first.Pop());
                    PlacePlate(second, third, first, count - 1);
                }
            }
        }

    }
}
