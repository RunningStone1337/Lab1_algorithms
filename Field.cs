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


        /*/// <summary>
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
        }*/

        internal void PlaceFirstRow()
        {
            map.Print();
            PlaceNum(map.GetNum(1));
            PlaceNum(map.GetNum(2));
            PlaceNum(map.GetNum(3));
            var zero = map.GetNum(0);
            if (zero.Row == 0 && zero.Col == 3 && zero.Down.Value == 4)
            {
                map.MoveDown(zero);
            }
            else if (!(map.GetNum(4).Row == 0 && map.GetNum(4).Col == 3))
            {
                PlaceNum(map.GetNum(4));
                BlackMagic(map.GetNum(4));
            }
        }
        internal void PlaceFirstCol()
        {
            PlaceNum(map.GetNum(13));
        }
        private void BlackMagic(Cell four)
        {
            var zero = map.GetNum(0);
            switch (zero.GetDirection(four))
            {
                case 'l'://если 0 справа от цели
                    map.MoveDown(zero);
                    map.MoveLeft(zero);
                    goto case 'u';
                case 'u'://если 0 снизу цели
                    map.MoveLeft(zero);
                    map.MoveUp(zero);
                    goto case 'r';
                case 'r'://если 0 слева от цели
                    map.MoveLeft(zero);
                    map.MoveUp(zero);
                    map.MoveRight(zero);
                    map.MoveRight(zero);
                    map.MoveDown(zero);
                    map.MoveRight(zero);
                    map.MoveUp(zero);
                    map.MoveLeft(zero);
                    map.MoveLeft(zero);
                    map.MoveLeft(zero);
                    map.MoveDown(zero);
                    break;
                default:
                    break;
            }
        }

        private void PlaceNum(Cell actual)
        {
            var zero = map.GetNum(0);
            if (actual.Placed)
            {
                return;
            }
            while (!zero.IsNear(actual))
            {
                switch (zero.GetDirection(actual, true))
                {
                    case 'd'://если 0 сверху цели
                        map.MoveDown(zero);
                        break;
                    case 'l'://если 0 справа от цели
                        map.MoveLeft(zero);
                        break;
                    case 'u'://если 0 снизу цели
                        map.MoveUp(zero);
                        break;
                    case 'r'://если 0 слева от цели
                        map.MoveRight(zero);
                        break;
                    default:
                        break;
                }
            }
            while (!actual.Placed)//двигаем пока не поставим на своё место
            {
                if (actual.ColIsPlaced() && actual.NeedUp())//если в своём столбце и нужно поднимать
                {
                    switch (zero.GetDirection(actual, true))//определяем с какой стороны от целевой клетки 0
                    {
                        case 'd'://если 0 сверху цели
                            switch (actual.Row - actual.TargRow)//определяем число разделяющих рядов
                            {
                                case 1:
                                    map.MoveDown(zero);
                                    break;
                                case 2:
                                    map.MoveDown(zero);
                                    if (actual.Col != 3)
                                    {
                                        map.MoveGroup1(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup2(zero);
                                    }
                                    break;
                                case 3:
                                    map.MoveDown(zero);
                                    if (actual.Col != 3)
                                    {
                                        map.MoveGroup1(zero);
                                        map.MoveGroup1(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup2(zero);
                                        map.MoveGroup2(zero);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'l'://если 0 справа от цели
                            switch (actual.Row - actual.TargRow)//определяем число разделяющих рядов
                            {
                                case 1:
                                    map.MoveGroup3(zero);
                                    break;
                                case 2:
                                    map.MoveGroup3(zero);
                                    map.MoveGroup1(zero);
                                    break;
                                case 3:
                                    map.MoveGroup3(zero);
                                    map.MoveGroup1(zero);
                                    map.MoveGroup1(zero);
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case 'u'://если 0 снизу цели
                            if (actual.Col != 3)
                            {
                                map.MoveRight(zero);
                                map.MoveUp(zero);
                            }
                            else
                            {
                                map.MoveLeft(zero);
                                map.MoveUp(zero);
                            }
                            break;
                        case 'r'://если 0 слева от цели
                            if (actual.Col != 3 && actual.Row != 3)
                            {
                                map.MoveDown(zero);
                                map.MoveRight(zero);
                                map.MoveRight(zero);
                                map.MoveUp(zero);
                            }
                            else
                            {
                                map.MoveUp(zero);
                                map.MoveRight(zero);
                                map.MoveDown(zero);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (actual.NeedLeft())
                {
                    switch (zero.GetDirection(actual, true))
                    {
                        case 'd'://если 0 сверху цели
                            switch (actual.Col - actual.TargCol)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    if (actual.Col == 3 || actual.Row == 3)
                                    {
                                        map.MoveLeft(zero);
                                        map.MoveDown(zero);
                                        map.MoveRight(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup4(zero);
                                    }
                                    break;
                                case 2:
                                    if (actual.Col != 3 && actual.Row != 3)
                                    {
                                        map.MoveGroup4(zero);
                                        map.MoveGroup5(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup7(zero);
                                        map.MoveGroup6(zero);
                                    }
                                    break;
                                case 3:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveGroup8(zero);
                                        map.MoveGroup5(zero);
                                        map.MoveGroup5(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup8(zero);
                                        map.MoveGroup9(zero);
                                        map.MoveGroup9(zero);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'l'://если 0 справа от цели
                            switch (actual.Col - actual.TargCol)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveGroup5(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup9(zero);
                                    }
                                    break;
                                case 2:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveGroup5(zero);
                                        map.MoveGroup5(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup9(zero);
                                        map.MoveGroup9(zero);
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case 'u'://если 0 снизу цели
                            switch (actual.Col - actual.TargCol)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveGroup7(zero);
                                    break;
                                case 2:
                                    map.MoveGroup7(zero);
                                    map.MoveGroup5(zero);
                                    break;
                                case 3:
                                    map.MoveGroup7(zero);
                                    map.MoveGroup5(zero);
                                    map.MoveGroup5(zero);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'r'://если 0 слева от цели
                            switch (actual.Col - actual.TargCol)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveRight(zero);
                                    break;
                                case 2:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveRight(zero);
                                        map.MoveGroup5(zero);
                                    }
                                    else
                                    {
                                        map.MoveRight(zero);
                                        map.MoveGroup9(zero);
                                    }
                                    break;
                                case 3:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveRight(zero);
                                        map.MoveGroup5(zero);
                                        map.MoveGroup5(zero);
                                    }
                                    else
                                    {
                                        map.MoveRight(zero);
                                        map.MoveGroup9(zero);
                                        map.MoveGroup9(zero);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (actual.NeedRight())
                {
                    switch (zero.GetDirection(actual, true))
                    {
                        case 'd'://если 0 сверху цели
                            switch (actual.TargCol - actual.Col)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveGroup10(zero);
                                    break;
                                case 2:
                                    map.MoveGroup10(zero);
                                    map.MoveGroup11(zero);
                                    break;
                                case 3:
                                    map.MoveGroup10(zero);
                                    map.MoveGroup11(zero);
                                    map.MoveGroup11(zero);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'l'://если 0 справа от цели
                            switch (actual.TargCol - actual.Col)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveLeft(zero);
                                    break;
                                case 2:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveLeft(zero);
                                        map.MoveGroup12(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup11(zero);
                                    }
                                    break;
                                case 3:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveLeft(zero);
                                        map.MoveGroup12(zero);
                                        map.MoveGroup12(zero);
                                    }
                                    else
                                    {
                                        map.MoveLeft(zero);
                                        map.MoveGroup11(zero); ;
                                        map.MoveGroup11(zero); ;

                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case 'u'://если 0 снизу цели
                            switch (actual.TargCol - actual.Col)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveGroup13(zero);
                                    break;
                                case 2:
                                    map.MoveGroup13(zero);
                                    map.MoveGroup12(zero);
                                    break;
                                case 3:
                                    map.MoveGroup13(zero);
                                    map.MoveGroup12(zero);
                                    map.MoveGroup12(zero);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'r'://если 0 слева от цели
                            switch (actual.TargCol - actual.Col)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveGroup12(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup13(zero);
                                    }
                                    break;
                                case 2:
                                    if (actual.Row != 3)
                                    {
                                        map.MoveGroup12(zero);
                                        map.MoveGroup12(zero);
                                    }
                                    else
                                    {
                                        map.MoveGroup11(zero);
                                        map.MoveGroup11(zero);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (actual.ColIsPlaced() && actual.NeedDown())
                {
                    switch (zero.GetDirection(actual, true))
                    {
                        case 'd'://если 0 сверху цели
                            switch (actual.TargRow - actual.Row)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveGroup14(zero);
                                    break;
                                case 2:
                                    map.MoveGroup14(zero);
                                    map.MoveGroup14(zero);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'l'://если 0 справа от цели
                            switch (actual.TargRow - actual.Row)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveGroup15(zero);
                                    break;
                                case 2:
                                    map.MoveGroup15(zero);
                                    map.MoveGroup14(zero);
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case 'u'://если 0 снизу цели
                            switch (actual.TargRow - actual.Row)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveUp(zero);
                                    break;
                                case 2:
                                    if (actual.Col != 3)
                                    {
                                        map.MoveUp(zero);
                                        map.MoveGroup14(zero);
                                    }
                                    else
                                    {
                                        map.MoveUp(zero);
                                        map.MoveGroup15(zero);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'r'://если 0 слева от цели
                            switch (actual.TargRow - actual.Row)//определяем число разделяющих стоолбцов
                            {
                                case 1:
                                    map.MoveGroup16(zero);
                                    break;
                                case 2:
                                    map.MoveGroup16(zero);
                                    map.MoveGroup15(zero);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
