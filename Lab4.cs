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
            filename = "StackData.txt";
            Plate plate6 = new Plate(6);
            Plate plate5 = new Plate(5);
            Plate plate4 = new Plate(4);
            Plate plate3 = new Plate(3);
            Plate plate2 = new Plate(2);
            Plate plate1 = new Plate(1);
            Plate[] plates = new Plate[] { plate5, plate4, plate3, plate2, plate1 };
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(dir + Path.DirectorySeparatorChar + filename, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, plates);
            }
            using (FileStream fs = new FileStream(dir + Path.DirectorySeparatorChar + filename, FileMode.OpenOrCreate))
            {
                plates = bf.Deserialize(fs) as Plate[];
            }
            var t3res = Task3(plates);
            #endregion
            #region Task4
            filename = "Task4.txt";
            string[] strings;
            strings = File.ReadAllLines(dir + Path.DirectorySeparatorChar + filename);
            var t4res = Task4(strings);
            #endregion
            #region Task5

            #endregion
            filename = "Task5.txt";
            string[] strings2;
            strings2 = File.ReadAllLines(dir + Path.DirectorySeparatorChar + filename);
            var t5res = Task5(strings2);
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
            foreach (var item in input)
            {
                stacks[0].Push(item);
            }
            PlacePlate(stacks[0], stacks[2], stacks[1], stacks[0].Count);
            foreach (var item in stacks[2])
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
        /*
        * Дан текстовый файл с программой на алгоритмическом языке. За один просмотр
        * файла проверить баланс круглых скобок в тексте, используя стек.
         */
        public static bool Task4(string[] text)
        {
            var stack = new MyStack<char>();
            foreach (var str in text)
            {
                foreach (var ch in str)
                {
                    if (ch == '(')
                    {
                        stack.Push(ch);
                    }
                    if (ch == ')' && stack.Count != 0)
                    {
                        stack.Pop();
                    }
                    else if (ch == ')' && stack.Count == 0)
                    {
                        return false;
                    }
                }
            }
            if (stack.IsEmpty())
            {
                return true;
            }
            return false;
        }
        /*
         * Дан текстовый файл с программой на алгоритмическом языке. За один просмотр
         * файла проверить баланс квадратных скобок в тексте, используя дек.
         */
        public static bool Task5(string[] text)
        {
            var deq = new MyDeque<char>();
            foreach (var str in text)
            {
                foreach (var ch in str)
                {
                    if (ch == '[')
                    {
                        deq.Dequeue(ch);
                    }
                    if (ch == ']')
                    {
                        deq.Enqueue(ch);
                    }

                }
            }
            while (deq.Count > 1)
            {
                if (deq.PopHead() == '[' && deq.PopTail() == ']')
                {
                    deq.RemoveFirst();
                    deq.RemoveLast();
                }
                else
                {
                    return false;
                }
            }
            if (deq.IsEmpty())
            {
                return true;
            }
            return false;
        }
    }
}
