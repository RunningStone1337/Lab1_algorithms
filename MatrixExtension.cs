using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public static class MatrixExtension
    {
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
        public static Cell[,] DeepClone(this Cell[,] map)
        {
            var size = 4;
            var res = new Cell[4,4];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    res[i, j] = map[i,j].Clone();
                }
            }
            return res;
        }
        public static void FindNeighbours(this Cell[,] map)
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
