//#define strings
#define taken
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            Console.WriteLine("Введите строку для поиска или 0 чтобы перейти к следующему заданию");
            string str_bo = Console.ReadLine();
            while (str_bo != "0")
            {
                Console.WriteLine("Введите подстроку для поиска");
                string word = Console.ReadLine();
                //str = "Hoola-hoola girls like hooligans";
                //word = "h";
                var start = DateTime.Now;
                int res = BoyerMoor(str, word);
                var end = DateTime.Now;
                Console.WriteLine($"Затраченное время на выполнение: {start - end}");
                Console.WriteLine($"Индекс начала искомой подстроки в строке: {res}");
                Console.WriteLine("Введите строку для поиска или 0 чтобы выйти");
                str = Console.ReadLine();
            }
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
                /*Console.WriteLine($"Введите исходную последовательность неповторяющихся чисел от 0 до 15 включительно для игры в пятнашки или 20 для перехода к следующему заданию");
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
                }*/
                var start = DateTime.Now;
                int[] res = Taken(/*arr*/new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 });
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

        private static int BoyerMoor(string str, string word)
        {
            var alphabet = new int[256];
            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabet[i] = word.Length;
            }
            string reversed = new string(word.ToCharArray().Reverse().ToArray());
            var chars_cost = new Dictionary<char, int>();
            for (int i = 0; i < reversed.Length; i++)
            {
                if (!chars_cost.ContainsKey(reversed[i]))
                {
                    chars_cost.Add(reversed[i], i);
                    alphabet[reversed[i]] = i;
                }
            }
            int l = word.Length - 1;
            while (l <= str.Length)
            {
                if (word[word.Length - 1] != str[l])//если последний символ не == текущему
                {
                    l += alphabet[str[l]];
                }
                else//если равен
                {
                    var flag = false;
                    for (int i = l - 1, j = word.Length - 2; j >= 0; i--, j--)
                    {
                        if (str[i] != word[j])
                        {
                            l += alphabet[str[i]];
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return l - word.Length + 1;
                    }
                }
            }
            return -1;
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
            var field = new Field(arr);
            field.PlaceFirstRow();
            return null;

        }

    }
    public class Cell
    {
        internal int Value { private set; get; }
        internal int Row { private set; get; }
        internal int Col { private set; get; }
        internal int TargPosRow { private set; get; }
        internal int TargPosCol { private set; get; }

        internal Cell Up { set; get; }
        internal Cell Right { set; get; } 
        internal Cell Down { set; get; } 
        internal Cell Left { set; get; } 
        public Cell(int val, int r, int c)
        {
            if (val == 0)
            {
                TargPosCol = 3;
                TargPosRow = 3;
            }
            else
            {
                TargPosCol = (val - 1) % 4;
                TargPosRow = val - 1 / 4;
            }
            Value = val;
            Row = r;
            Col = c;
        }

        internal bool IsPlaced()
        {
            return (Row == TargPosRow && Col == TargPosCol);
        }
    }
    internal class Field
    {
        internal Cell[,] map = new Cell[4, 4];
        public Field(int[] arr)
        {
            int size = 4;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = new Cell(arr[j + i * size], i, j);
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map.FindNeighbours(map[i, j]);
                }
            }
        }
        internal void PlaceFirstRow()
        {
            for (int i = 1; i < 5; i++)
            {
                PlaceNum(i);
            }
            void PlaceNum(int num)
            {
                Cell zero = map.FindNum(0);
                Cell actual = map.FindNum(num);
                while (!actual.IsPlaced())
                {

                }
            }
        }
    }
    public static class IntExtension
    {
        /// <summary>
        /// Возвращает клетку поля с заданным значением
        /// </summary>
        /// <param name="map"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Cell FindNum(this Cell[,] map, int val)
        {
            int size = 4;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j].Value == val)
                    {
                        return map[i, j];
                    }
                }
            }
            return null;
        }
        public static void FindNeighbours(this Cell[,] map, Cell target)
        {
            var col = target.Col;
            var row = target.Row;
            if (target.Row != 0)
            {
                target.Up = map[row - 1, col];
            }
            else
            {
                target.Up = null;
            }
            if (target.Col != 3)
            {
                target.Right = map[row, col + 1];
            }
            else
            {
                target.Right = null;
            }
            if (target.Row != 3)
            {
                target.Down = map[row + 1, col];
            }
            else
            {
                target.Down = null;
            }
            if (target.Col != 0)
            {
                target.Left = map[row, col - 1];
            }
            else
            {
                target.Left = null;
            }
        }
    }
}
