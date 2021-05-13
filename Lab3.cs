#define strings
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
            #region Taken
            Console.WriteLine("Введите 1 чтобы ввести массив вручную, 2 сгенерировать случайный массив или 0 чтобы выйти");
            var arr = new int?[16];
            var enter = int.Parse(Console.ReadLine());
            while (enter != 0)
            {
                if (enter == 1)
                {
                    arr = new int?[16];
                    int count = 1;
                    var avialible = new List<int>(16);
                    for (int i = 0; i < avialible.Capacity; i++)
                    {
                        avialible.Add(i);
                    }
                    Console.WriteLine($"Введите исходную последовательность неповторяющихся чисел от 0 до 15 включительно для игры в пятнашки или 20 для перехода к следующему заданию");
                    while (count < 17)
                    {
                        int entered;
                        Console.WriteLine($"Введите {count} число");
                        try
                        {
                            entered = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Ошибка преобразования типов. Повторите ввод");
                            continue;
                        }
                        if (!arr.Contains(entered) && entered < 16 && entered >= 0)
                        {
                            arr[count - 1] = entered;
                            avialible.Remove(entered);
                            count++;
                        }
                        else
                        {
                            Console.WriteLine($"Введённое число уже есть в массиве или не входит в заданный диапазон! Введите другое число.");
                        }
                        Console.Write($"Доступные числа для ввода: ");
                        foreach (var item in avialible)
                        {
                            Console.Write(item + " ");
                        }
                    }
                }
                else if (enter == 2)
                {
                    arr = new int?[16];
                    int count = 1;
                    var avialible = new List<int>(16);
                    for (int i = 0; i < avialible.Capacity; i++)
                    {
                        avialible.Add(i);
                    }
                    Random rand = new Random();
                    for (int i = 0; i < 16;)
                    {
                        var next = rand.Next(0, 16);
                        if (!arr.Contains(next))
                        {
                            arr[i] = next;
                            i++;
                        }
                    }
                }
                var start = DateTime.Now;
                int[] res = Taken(arr /*new int?[] {1,2,3,4,5,6,7,8,9,11,0,14,13,15,10,12}*/);//закомментирована просто одна из допустимых начальных комбинаций
                var end = DateTime.Now;
                Console.WriteLine($"Полученная последовательность перестановок: ");
                if (res != null)
                {
                    foreach (var item in res)
                    {
                        Console.Write(item + " ");
                    }
                }
                else
                {
                    Console.WriteLine("Нерешаемая комбинация.");
                }
                Console.WriteLine();
                Console.WriteLine($"Затраченное время на выполнение: {start - end}");
                Console.WriteLine("1: Ввести новый массив вручную\n2: Cгенерировать случайный массив\n0: Выйти\nЛюбое другое число: - повторить");
                enter = int.Parse(Console.ReadLine());
            }
            #endregion
#endif
        }
        /// <summary>
        /// Алгоритм Боуера-Мура. Принцип поиска основан на смещении относительно текущего символа на
        /// определённое "стоимостью" этого символа расстояния, которое определяется позицией этого символа в подстроке.
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <param name="word">Образ</param>
        /// <returns>Индекс начала подстроки</returns>
        private static int BoyerMoor(string str, string word)
        {
            var alphabet = new int[256];
            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabet[i] = word.Length;
            }
            string reversed = new string(word.ToCharArray().Reverse().ToArray());//переворачиваем исходный образ чтобы было удобнее манипулировать
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
        /// <summary>
        /// Алгоритм КНута-Морриса-Прата. Принцип основан на подсчёте префикс-функции для исходной строки и образа
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <param name="word">Образ</param>
        /// <returns>Индекс начала подстроки</returns>
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
            ///функция подсчёта префикса для строки
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
        /// <summary>
        /// Пятнашки. Суть кратко: на входе одномерный массив [0;15], 0 символизирует пустую клетку, остальные числа свои клетки
        /// На основе массива составляется игровое поле 4х4, если комбинация решаемая, каждый элемент последовательно ставится на своё место. Иначе возвращается null
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        static int[] Taken(int?[] arr)
        {
            var counter = 0;
            ///сначала определяем, решаема ли комбинация
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] == 0)
                {
                    continue;
                }
                for (int j = i + 1; j < arr.Length; j++)
                {
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
            counter += (Array.IndexOf(arr, 0) / 4) + 1;
            if (counter % 2 == 1)//если нерешаемая
            {
                return null;
            }
            ///иначе создаём поле  на основе массива
            var field = new Field(arr);
            field.PlaceFirstRow();//размещаем первый ряд
            field.PlaceFirstCol();//размещаем первую колонку
            field.PlaceSecondRow();//второй ряд
            field.PlaceRest();//оставшиеся 3 клетки
            Field.counter = 0;
            return Field.movelist.ToArray();//возвращаем результат в виде массива
        }
    }
}
