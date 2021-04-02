using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public static class MatrixExtension
    {
        public static void MoveLeft(this Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.Left;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Col += 1;
            replaced.Col -= 1;
            map.BindNeighbours();
        }
        public static void MoveDown(this Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.Down;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Row -= 1;
            replaced.Row += 1;
            map.BindNeighbours();
        }

        public static void MoveRight(this Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.Right;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Col -= 1;
            replaced.Col += 1;
            map.BindNeighbours();
        }

        /// <summary>
        /// Параметром передаётся ПЕРЕДВИГАЕМАЯ в указанном направлении ячейка
        /// </summary>
        /// <param name="replaced"></param>
        public static void MoveUp(this Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.Up;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Row += 1;
            replaced.Row -= 1;
            map.BindNeighbours();
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
        public static void Print(this Cell[,] map)
        {
            var size = 4;
            Console.WriteLine(Field.counter);
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j].Value.ToString().Length == 1)
                    {
                        Console.Write(" "+map[i, j].Value + "|");
                    }
                    else
                    {
                        Console.Write(map[i, j].Value + "|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static bool AllPlaced(this Cell[,] map)
        {
            int size = 4;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!map[i, j].Placed)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static Cell[,] DeepClone(this Cell[,] map)////////проверено
        {
            var size = 4;
            var res = new Cell[4, 4];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    res[i, j] = map[i, j].Clone();
                }
            }
            return res;
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
