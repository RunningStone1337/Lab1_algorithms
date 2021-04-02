using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Field
    {
        internal static long counter = 0;
        Cell[,] map = new Cell[4, 4];
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
            map.BindNeighbours();
        }
        
        
        /// <summary>
        /// Возвращает порядок перемещений для решения
        /// </summary>
        /// <returns>Лист с порядком перестановок</returns>
        internal List<int> GetOrder()
        {
            return Place(map);
        }
        /// <summary>
        /// Возвращает список перемещений для конкретной ветки графа
        /// </summary>
        /// <param name="map">Начальная карта состояний</param>
        /// <param name="actual">Устанавливамый на своё место элемент</param>
        /// <returns>Список перемещений для ветки</returns>
        private List<int> Place(Cell[,] map)
        {
            counter++;
            map.Print();
            if (map.AllPlaced())
            {
                return new List<int>();
            }
            var list = new List<int>[4];
            for (int i = 0; i < 4; i++)
            {
                list[i] = new List<int>();
            }
            if (map.GetNum(0).Down != null)//если можем передвинуть вверх 
            {
                Cell[,] localmap_down = map.DeepClone();
                localmap_down.BindNeighbours();
                var zero = localmap_down.GetNum(0);
                var target = localmap_down.GetNum(zero.Down.Value);
                localmap_down.MoveDown(zero);
                list[0].Add(zero.Up.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    target.Placed = false;
                    list[0].AddRange(Place(localmap_down));
                }
            }
            if (map.GetNum(0).Left != null)//если можем передвинуть вправо 
            {
                Cell[,] localmap_left = map.DeepClone();
                localmap_left.BindNeighbours();
                var zero = localmap_left.GetNum(0);
                var target = localmap_left.GetNum(zero.Left.Value);
                localmap_left.MoveLeft(zero);
                list[1].Add(zero.Right.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    target.Placed = false;
                    list[1].AddRange(Place(localmap_left));
                }
            }
            if (map.GetNum(0).Up != null)//если можем передвинуть вниз 
            {
                Cell[,] localmap_up = map.DeepClone();
                localmap_up.BindNeighbours();
                var zero = localmap_up.GetNum(0);
                var target = localmap_up.GetNum(zero.Up.Value);
                localmap_up.MoveUp(zero);
                list[2].Add(zero.Down.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    target.Placed = false;
                    list[2].AddRange(Place(localmap_up));
                }
            }
            if (map.GetNum(0).Right != null)//если можем передвинуть влево 
            {
                Cell[,] localmap_right = map.DeepClone();
                localmap_right.BindNeighbours();
                var zero = localmap_right.GetNum(0);
                var target = localmap_right.GetNum(zero.Left.Value);
                localmap_right.MoveRight(zero);
                list[3].Add(zero.Left.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    target.Placed = false;
                    list[3].AddRange(Place(localmap_right));
                }
            }
            long min = long.MaxValue;
            List<int> result = null;
            for (int i = 0; i < 4; i++)
            {
                if (list[i].Count < min)
                {
                    result = list[i];
                }
            }
            return result;
        }
    }
}
