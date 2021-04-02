using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Field
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
                    while (!zero.IsNear(actual))//пока 0 не окажется по соседству с целевой
                    {
                        switch (zero.GetDirection(actual))//двигаем по направлению к ней
                        {
                            case 'u':
                                MoveUp(zero);
                                break;
                            case 'r':
                                MoveRight(zero);
                                break;
                            case 'd':
                                MoveDown(zero);
                                break;
                            case 'l':
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
                    }
                    zero.Quad();
                    //zero.Move(actual);
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
    }
}
