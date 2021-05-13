using System;
using System.Threading;
namespace Lab3
{
    /// <summary>
    /// Класс расширений для двумерного массива, в основном в нём все взаимодействия с игровым полем
    /// </summary>
    public static class MatrixExtension
    {
        /// <summary>
        /// Перемещает заданную клетку влево
        /// </summary>
        /// <param name="map"></param>
        /// <param name="replaced">Перемещаемая клетка</param>
        public static void MoveLeft(this Cell[,] map, Cell replaced)
        {
            Field.counter++;
            Cell temp = replaced.Left;
            Field.movelist.Add(temp.Value);
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Col += 1;
            replaced.Col -= 1;
            replaced.Previous = "left";
            temp.Previous = "right";
            map.BindNeighbours();
            map.Print();
            Thread.Sleep(Field.delay);
        }
        public static void MoveDown(this Cell[,] map, Cell replaced)
        {
            Field.counter++;
            Cell temp = replaced.Down;
            Field.movelist.Add(temp.Value);
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Row -= 1;
            replaced.Row += 1;
            replaced.Previous = "down";
            temp.Previous = "up";
            map.BindNeighbours();
            map.Print();
            Thread.Sleep(Field.delay);
        }

        /// <summary>
        /// Параметром передаётся ПЕРЕДВИГАЕМАЯ в указанном направлении ячейка
        /// </summary>
        /// <param name="replaced"></param>
        public static void MoveUp(this Cell[,] map, Cell replaced)
        {
            Field.counter++;
            Cell temp = replaced.Up;
            Field.movelist.Add(temp.Value);
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Row += 1;
            replaced.Row -= 1;
            replaced.Previous = "up";
            temp.Previous = "down";
            map.BindNeighbours();
            map.Print();
            Thread.Sleep(Field.delay);
        }
        public static void MoveRight(this Cell[,] map, Cell replaced)
        {
            Field.counter++;
            Cell temp = replaced.Right;
            Field.movelist.Add(temp.Value);
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Col -= 1;
            replaced.Col += 1;
            replaced.Previous = "right";
            temp.Previous = "left";
            map.BindNeighbours();
            map.Print();
            Thread.Sleep(Field.delay);
        }

        /// <summary>
        /// Возвращает клетку поля с заданным значением
        /// </summary>
        /// <param name="map"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Cell GetNum(this Cell[,] map, int val)
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
        /// <summary>
        /// Двигает 0 по направлению к цели, пока не станет его соседом
        /// </summary>
        /// <param name="map"></param>
        /// <param name="actual"></param>
        public static void MoveToNeighbourhood(this Cell[,] map, Cell actual)
        {
            var zero = map.GetNum(0);
            while (!zero.IsNear(actual))//двигаем 0 к цели пока не будем рядом
            {
                switch (zero.GetDirectionToMove(actual))
                {
                    case Cell.Direction.Down://если 0 сверху цели
                        map.MoveDown(zero);
                        break;
                    case Cell.Direction.Left://если 0 справа от цели
                        map.MoveLeft(zero);
                        break;
                    case Cell.Direction.Up://если 0 снизу цели
                        map.MoveUp(zero);
                        break;
                    case Cell.Direction.Right://если 0 слева от цели
                        map.MoveRight(zero);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Вывод карты на консоль на текущее состояние поля
        /// </summary>
        /// <param name="map"></param>
        public static void Print(this Cell[,] map)
        {
            var size = 4;
            Console.WriteLine(Field.counter);
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j].Actual)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (map[i, j].Value.ToString().Length == 1)
                    {
                        if (map[i, j].Value == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write(" " + map[i, j].Value + "|");

                    }
                    else
                    {
                        Console.Write(map[i, j].Value + "|");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Привязывает соседей со всех сторон для каждой клетки поля
        /// </summary>
        /// <param name="map"></param>
        public static void BindNeighbours(this Cell[,] map)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var temp = map[i, j];
                    var col = temp.Col;
                    var row = temp.Row;
                    if (row != 0)
                    {
                        temp.Up = map[row - 1, col];
                    }
                    else
                    {
                        temp.Up = null;
                    }
                    if (col != 3)
                    {
                        temp.Right = map[row, col + 1];
                    }
                    else
                    {
                        temp.Right = null;
                    }
                    if (row != 3)
                    {
                        temp.Down = map[row + 1, col];
                    }
                    else
                    {
                        temp.Down = null;
                    }
                    if (col != 0)
                    {
                        temp.Left = map[row, col - 1];
                    }
                    else
                    {
                        temp.Left = null;
                    }
                }
            }
        }
    }
}
