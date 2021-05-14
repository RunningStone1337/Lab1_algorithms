using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            #region IntervalsProblem
            start = DateTime.Now;
            int[][] res2 = IntervalsProblem(new int[][] { new int[] { 1, 2 }, new int[] { 2, 4 }, new int[] { 0, 5 }, new int[] { 0, 2 }, new int[] { 4, 7 } });
            end = DateTime.Now;
            Console.WriteLine($"Число отрезков: {ress}");
            Console.WriteLine($"Затраченное время на выполнение: {start - end}");
            #endregion
            #region LongestPalindrome
            start = DateTime.Now;
            var res3 = LongestPalindrome("Спортивный клуб «Ротор».");
            end = DateTime.Now;
            Console.WriteLine($"Самый длинный палиндром: {res3}");
            Console.WriteLine($"Затраченное время на выполнение: {start - end}");
            #endregion
            #region CoolestString
            start = DateTime.Now;
            var res4 = CoolestString("abe", "acd");
            end = DateTime.Now;
            Console.WriteLine($"Строка 1 может победить строку 2: {res4}");
            Console.WriteLine($"Затраченное время на выполнение: {start - end}");
            #endregion
        }

        static string MaxDigit(int[] arr)
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
                    if (left.Equals(right) && !subs.Contains(left))
                    {
                        subs.Add(left);
                        counter++;
                    }
                    j++;
                }
            }
            return counter;
        }
        static string LongestPalindrome(string input)
        {
            input = input.ToLower();
            string res = string.Empty;
            for (int i = 0, j = i - 1, k = i + 1; i < input.Length; i++)
            {
                j = i - 1;
                k = i + 1;
                while (j >= 0 && k < input.Length)
                {
                    string temp = input.Substring(j, k - j + 1);
                    if (IsPalindrome(temp) && temp.Length > res.Length)
                    {
                        res = temp;
                        j--;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                j = i - 1;
                k = i + 2;
                while (j >= 0 && k < input.Length)
                {
                    string temp = input.Substring(j, k - j + 1);
                    if (IsPalindrome(temp) && temp.Length > res.Length)
                    {
                        res = temp;
                        j--;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return res;
            bool IsPalindrome(string str)
            {
                if (str.Length % 2 == 0)
                {
                    int index = str.Length / 2;
                    string left = str.Substring(0, index);
                    var right = new string(str.Substring(index).ToCharArray().Reverse().ToArray());
                    for (int i = 0; i < left.Length; i++)
                    {
                        if (left[i] != right[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    int index = str.Length / 2;
                    string left = str.Substring(0, index);
                    var right = new string(str.Substring(index + 1).ToCharArray().Reverse().ToArray());
                    for (int i = 0; i < left.Length; i++)
                    {
                        if (left[i] != right[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        static bool CoolestString(string first, string second)
        {
            if (first.Length != second.Length)//если начальные условия не выполняются
            {
                return false;
            }
            string tempf = first;
            var listf = new List<string>();
            string temps = second;
            var lists = new List<string>();
            do//в этом блоке циклов получаем всевозможные варианты перестановок символов в первом слове
            {
                var innerf = tempf;
                do
                {
                    tempf = new string(tempf.Append(tempf[0]).ToArray()).Remove(0, 1);
                    listf.Add(tempf);
                } while (tempf != new string(innerf[innerf.Length - 1] + innerf.Remove(innerf.Length - 1, 1)));
                var fch = tempf[0];
                var lch = tempf[tempf.Length - 1];
                tempf = tempf.Substring(1, tempf.Length - 2);
                tempf = lch + tempf + fch;
                listf.Add(tempf);
            } while (tempf != first);
            do//то же самое со вторым словом
            {
                var inners = temps;
                do
                {
                    temps = new string(temps.Append(temps[0]).ToArray()).Remove(0, 1);
                    lists.Add(temps);
                } while (temps != new string(inners[inners.Length - 1] + inners.Remove(inners.Length - 1, 1)));
                var fch = temps[0];
                var lch = temps[temps.Length - 1];
                temps = temps.Substring(1, temps.Length - 2);
                temps = lch + temps + fch;
                lists.Add(temps);
            } while (temps != second);
            foreach (var fs in listf)//проверяем, могут ли  какие-то из перестановок в первом слове победить какие-то перестановки во втором
            {
                foreach (var ss in lists)
                {
                    int counter = 0;
                    for (int i = 0; i < fs.Length; i++)
                    {
                        if (fs[i] >= ss[i])
                        {
                            counter++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (counter == ss.Length)
                    {
                        return true;
                    }
                }
            }
            foreach (var ss in lists)//то же самое наоборот
            {
                foreach (var fs in listf)
                {
                    int counter = 0;
                    for (int i = 0; i < fs.Length; i++)
                    {
                        if (ss[i] >= fs[i])
                        {
                            counter++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (counter == ss.Length)
                    {
                        return true;
                    }
                }
            }
            return false;//если ничего не нашлось
        }
        static int[][] IntervalsProblem(int[][] input)
        {
            List<int[]> list = new List<int[]>();
            foreach (var item in input)
            {
                list.Add(item);
            }
            list.Sort((f, s) =>
            {
                var ff = f[0];
                var sf = s[0];
                return ff <= sf ? -1 : 1;
            });
            int counter = 0;
            do
            {
                counter = 0;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    int j = i + 1;
                    if (list[i][0] <= list[j][0])//если 1 точка 1 отрезка левее или совпадает
                    {
                        if (list[i][1] <= list[j][1])//если 2 точка 1 отрезка левее или совпадает 
                        {
                            counter++;
                            list.Add(new int[] { list[i][0], list[j][1] });
                            list.RemoveAt(j);
                            list.RemoveAt(i);
                            list.Sort((f, s) =>
                            {
                                var ff = f[0];
                                var sf = s[0];
                                return ff <= sf ? -1 : 1;
                            });
                        }
                    }
                }
            } while (counter != 0);
            int[][] res = list.ToArray();
            return res;
        }
    }
}
