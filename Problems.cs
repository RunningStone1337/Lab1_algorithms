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
            #region BiggestTriangle
            int[] tr = { 12, 6, 2, 5 };
            Console.WriteLine("Полученный периметр периметры:" + BiggestTriangle(tr));
            #endregion
            #region MatrixSort
            int[,] mx = { { 3, 3, 1, 1 }, { 2, 2, 1, 2 }, { 1, 1, 1, 2 } };
            Console.WriteLine("Полученная матрица:");
            var resm = MatrixSort(mx);
            for (int i = 0; i < resm.GetLength(0); i++)
            {
                for (int j = 0; j < resm.GetLength(1); j++)
                {
                    Console.Write(resm[i, j] + " ");
                }
                Console.WriteLine();
            }
            #endregion
            #region RightAngle
            int angles = 5;
            Console.WriteLine($"Наименьший подмногоугольник имеет {RightAngle(58)} вершин");
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
            if (input == null || input.Length == 0) return new int[0][];
            Array.Sort(input, 0, input.Length, new IntervalComparer());
            var stack = new Stack<int[]>();
            var curr = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                var next = input[i];
                if (curr[1] >= next[0])
                    curr = new int[] { curr[0], Math.Max(curr[1], next[1]) };
                else
                {
                    stack.Push(curr);
                    curr = next;
                }
            }
            stack.Push(curr);
            var res = new int[stack.Count][];
            for (int i = res.Length - 1; i >= 0; i--)
                res[i] = stack.Pop();
            return res;
        }
        class IntervalComparer : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                return x[0].CompareTo(y[0]);
            }
        }
        static int BiggestTriangle(int[] mas)
        {
            List<int> arr = new List<int>(mas);
            arr.Sort();
            arr.Reverse();
            while (arr[0] >= arr[1] + arr[2])
            {
                if (arr.Count > 3)
                    arr.RemoveAt(0);
                else return 0;
            }
            return arr[0] + arr[1] + arr[2];
        }
        static int?[,] MatrixSort(int[,] input)
        {
            var list = new List<int>();
            foreach (var val in input)
            {
                list.Add(val);
            }
            list.Sort();
            var res = new int?[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (res[i, j] == null)
                    {
                        res[i, j] = list[0];
                        list.RemoveAt(0);
                    }
                }
                for (int k = 0; k < input.GetLength(0); k++)
                {
                    if (k > input.GetLength(0))
                    {
                        break;
                    }
                    if (res[k, i] == null)
                    {
                        res[k, i] = list[0];
                        list.RemoveAt(0);
                    }
                }
            }
            return res;
        }
        static int RightAngle(int input)
        {
            if (input % 3 == 0)
            {
                return 3;
            }
            else if (input % 2 == 1)
            {
                int sqrt = (int)Math.Sqrt(input);
                if (sqrt<3)
                {
                    return input;
                }
                int res = sqrt;
                for (int j = sqrt; j > 2; j--)
                {
                    if (input % j == 0 && IsSimple(j))
                    {
                        res = j;
                    }
                }
                return res;
            }
            else if (input % 2 == 0 && input % 4 != 0)
            {
                int sqrt = (int)Math.Sqrt(input);
                if (sqrt < 3)
                {
                    return input;
                }
                int res = sqrt;
                for (int j = sqrt; j > 2; j--)
                {
                    if (input % j == 1 && IsSimple(j))
                    {
                        res = j;
                    }
                }
                if (res == sqrt)
                {
                    return input / 2;
                }
                return res;
            }
            else
            {
                return 4;
            }

            bool IsSimple(int num)
            {
                int counter = 0;
                for (int i = 1; i <= num; i++)
                {
                    if (num % i == 0)
                    {
                        counter++;
                    }
                    if (counter > 2)
                    {
                        return false;
                    }
                }
                if (counter != 2)
                {
                    return false;
                }
                return true;
            }
        }
    }

}
