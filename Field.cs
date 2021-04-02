using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Field
    {
        Cell[,] map = new Cell[4, 4];
        Queue<Cell> queue = new Queue<Cell>();
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
            map.FindNeighbours();
        }
        internal void PlaceFirstRow()
        {
            for (int i = 1; i < 5; i++)
            {
                PlaceNum(i);
            }
            void PlaceNum(int num)
            {
                Cell zero = map.GetNum(0);
                Cell actual = map.GetNum(num);
                while (!actual.Placed)
                {
                    if (actual.RowIsPlaced() && actual.ColIsPlaced())
                    {
                        actual.Placed = true;
                        break;
                    }

                    /*while (!zero.IsNear(actual))//пока 0 не окажется по соседству с целевой
                    {
                        switch (zero.GetDirection(actual))//двигаем по направлению к ней
                        {
                            case 'u'://если 0 снизу цели
                                MoveUp(zero);
                                break;
                            case 'r'://если 0 слева от цели
                                MoveRight(zero);
                                break;
                            case 'd'://если 0 сверху цели
                                MoveDown(zero);
                                break;
                            case 'l'://если 0 справа от цели
                                MoveLeft(zero);
                                break;
                            default:
                                break;
                        }
                    }
                    
                    
                    switch (zero.GetDirection(actual))//определяем, с какой стороны от целевой находится 0 
                    {
                        case 'u'://если 0 снизу цели
                            break;
                        case 'r'://если 0 слева от цели
                            break;
                        case 'd'://если 0 сверху цели

                            break;
                        case 'l'://если 0 справа от цели
                            break;
                        default:
                            break;
                    }*/
                }
            }
        }
        private void MoveLeft(Cell replaced)
        {
            Cell temp = replaced.Left;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Col += 1;
            replaced.Col -= 1;
            map.FindNeighbours();
        }
        private void MoveDown(Cell replaced)
        {
            Cell temp = replaced.Down;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Row -= 1;
            replaced.Row += 1;
            map.FindNeighbours();
        }

        private void MoveRight(Cell replaced)
        {
            Cell temp = replaced.Right;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Col -= 1;
            replaced.Col += 1;
            map.FindNeighbours();
        }

        /// <summary>
        /// Параметром передаётся ПЕРЕДВИГАЕМАЯ в указанном направлении ячейка
        /// </summary>
        /// <param name="replaced"></param>
        private void MoveUp(Cell replaced)
        {
            Cell temp = replaced.Up;
            map[temp.Row, temp.Col] = replaced;
            map[replaced.Row, replaced.Col] = temp;
            temp.Row += 1;
            replaced.Row -= 1;
            map.FindNeighbours();
        }
        /// <summary>
        /// Возвращает порядок перемещений для решения
        /// </summary>
        /// <returns>Лист с порядком перестановок</returns>
        internal List<int> GetOrder()
        {
            var res = new List<int>();
            for (int i = 1; i < 16; i++)
            {
                queue.Enqueue(map.GetNum(i));
            }
            for (int i = 0; i < queue.Count; i++)
            {
                res.AddRange(Place(map, queue.Dequeue()));
            }
            return res;
        }
        /// <summary>
        /// Возвращает список перемещений для конкретной ветки графа
        /// </summary>
        /// <param name="map">Начальная карта состояний</param>
        /// <param name="actual">Устанавливамый на своё место элемент</param>
        /// <returns>Список перемещений для ветки</returns>
        private List<int> Place(Cell[,] map, Cell actual)
        {
            if (actual.Placed)
            {
                return new List<int>();
            }
            var list = new List<int>[4];
            for (int i = 0; i < 4; i++)
            {
                list[i] = new List<int>();
            }
            var value = actual.Value;
            if (map.GetNum(0).Down != null)//если можем меняться вверх 
            {
                Cell[,] localmap_down = map.DeepClone();
                localmap_down.FindNeighbours();
                var target = localmap_down.GetNum(value);
                var zero = localmap_down.GetNum(0);
                MoveDown(zero);
                list[0].Add(zero.Up.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    list[0].AddRange(Place(localmap_down, target));
                }
            }
            if (map.GetNum(0).Left != null)//если можем меняться вправо 
            {
                Cell[,] localmap_left = map.DeepClone();
                localmap_left.FindNeighbours();
                var target = localmap_left.GetNum(value);
                var zero = localmap_left.GetNum(0);
                MoveLeft(zero);
                list[1].Add(zero.Up.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    list[1].AddRange(Place(localmap_left, target));
                }
            }
            if (map.GetNum(0).Up != null)//если можем меняться вниз 
            {
                Cell[,] localmap_up = map.DeepClone();
                localmap_up.FindNeighbours();
                var target = localmap_up.GetNum(value);
                var zero = localmap_up.GetNum(0);
                MoveUp(zero);
                list[2].Add(zero.Up.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    list[2].AddRange(Place(localmap_up, target));
                }
            }
            if (map.GetNum(0).Right != null)//если можем меняться влево 
            {
                Cell[,] localmap_right = map.DeepClone();
                localmap_right.FindNeighbours();
                var target = localmap_right.GetNum(value);
                var zero = localmap_right.GetNum(0);
                MoveRight(zero);
                list[3].Add(zero.Up.Value);
                if (target.ColIsPlaced() && target.RowIsPlaced())
                {
                    target.Placed = true;
                }
                else
                {
                    list[3].AddRange(Place(localmap_right, target));
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
