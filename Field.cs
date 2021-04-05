using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Field
    {
        internal static long counter = 0;
        internal static int delay = 50;
        internal static List<int> movelist;

        Cell[,] map = new Cell[4, 4];
        public Field(int?[] arr)
        {
            int size = 4;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = new Cell((int)arr[j + i * size], i, j);
                }
            }
            map.BindNeighbours();
            movelist = new List<int>();
        }
        internal void PlaceFirstRow()
        {
            map.Print();
            PlaceNum(map.GetNum(1));
            PlaceNum(map.GetNum(2));
            PlaceNum(map.GetNum(3));
            if (map.GetNum(4).CheckPlace())
            {
                return;
            }
            map.GetNum(4).TargRow = 1;
            map.GetNum(4).TargCol = 2;
            var zero = map.GetNum(0);
            if (zero.Row == 0 && zero.Col == 3 && zero.Down.Value == 4)
            {
                map.MoveDown(zero);
            }
            else if (!(map.GetNum(4).Row == 0 && map.GetNum(4).Col == 3))
            {
                PlaceNum(map.GetNum(4));
                BlackMagic1(map.GetNum(4));
            }
        }
        internal void PlaceFirstCol()
        {
            if (map.GetNum(5).CheckPlace() && map.GetNum(9).CheckPlace() && map.GetNum(13).CheckPlace())
            {
                return;
            }
            var zero = map.GetNum(0);
            PlaceNum(map.GetNum(13));
            map.GetNum(5).TargRow = 2;
            map.GetNum(9).TargRow = 2;
            map.GetNum(9).TargCol = 1;
            if (map.GetNum(5).Row == 3 && map.GetNum(5).Col == 1)
            {
                BlackMagic4(map.GetNum(5));
            }
            else
            {
                PlaceNum(map.GetNum(5));
            }
            if (map.GetNum(9).Row == 1 && map.GetNum(9).Col == 0)
            {
                BlackMagic2(map.GetNum(9));
            }
            else if (zero.Right.Value == 9 && zero.Down.Value == 5)
            {
                BlackMagic5(map.GetNum(9));
            }
            else
            {
                PlaceNum(map.GetNum(9));
                BlackMagic3(map.GetNum(9));
            }
            map.GetNum(5).TargRow = 1;
            map.GetNum(9).TargRow = 2;
            map.GetNum(9).TargCol = 0;
            map.GetNum(5).CheckPlace();
            map.GetNum(9).CheckPlace();
        }
        private void BlackMagic5(Cell nine)
        {
            var zero = map.GetNum(0);
            map.MoveGroup12(zero);
            map.MoveDown(zero);
            map.MoveRight(zero);
            map.MoveGroup9(zero);
            map.MoveUp(zero);
            map.MoveLeft(zero);
            map.MoveGroup12(zero);
            map.MoveGroup8(zero);
        }
        private void BlackMagic4(Cell five)
        {
            var zero = map.GetNum(0);
            while (!zero.IsNear(five))
            {
                switch (zero.GetDirection(five, true))
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
            switch (zero.GetDirection(five))
            {
                case 'd'://если 0 сверху цели
                    map.MoveDown(zero);
                    map.MoveRight(zero);
                    map.MoveUp(zero);
                    map.MoveUp(zero);
                    map.MoveLeft(zero);
                    map.MoveLeft(zero);
                    map.MoveDown(zero);
                    map.MoveRight(zero);
                    break;
                case 'l'://если 0 справа от цели
                    map.MoveUp(zero);
                    map.MoveLeft(zero);
                    goto case 'd';
                case 'u'://если 0 снизу от цели
                    map.MoveRight(zero);
                    map.MoveUp(zero);
                    goto case 'l';
                default:
                    break;
            }

        }
        internal void PlaceRest()
        {
            var zero = map.GetNum(0);
            var ten = map.GetNum(10);
            ten.TargRow = 3;
            ten.TargCol = 1;
            PlaceNum(ten);
            ten.CheckPlace();
            var fourteen = map.GetNum(14);
            fourteen.TargCol = 2;
            PlaceNum(fourteen);
            fourteen.CheckPlace();
            if (zero.Left == fourteen)
            {
                map.MoveUp(zero);
                map.MoveLeft(zero);
            }
            if (zero.Down == fourteen)
            {
                map.MoveLeft(zero);
            }
            if (zero.Down == ten)
            {
                map.MoveDown(zero);
                map.MoveRight(zero);
            }

            var eleven = map.GetNum(11);
            var twelve = map.GetNum(12);
            var fifteen = map.GetNum(15);
            while (!eleven.CheckPlace() || !twelve.CheckPlace() || !fifteen.CheckPlace())
            {
                RandomMove(zero);
                /*if (map.GetNum(10).Placed && map.GetNum(11).Placed && map.GetNum(12).Placed && map.GetNum(14).Right == map.GetNum(15))
                {
                    PlaceNum(map.GetNum(14));
                    PlaceNum(map.GetNum(15));
                    return;
                }
                while (!map.TenFourteenPlaced() && !map.TenElevenPlaced() && !map.TwelveFifteenPlaced())
                {
                    RandomMove(zero);
                }
                if (map.TenFourteenPlaced())
                {
                    ten.CheckPlace();
                    fourteen.CheckPlace();
                    while (!eleven.CheckPlace() || !twelve.CheckPlace() || !fifteen.CheckPlace())
                    {
                        RandomMove(zero);
                    }
                }
                else if (map.TenElevenPlaced())
                {
                    ten.Placed = true;
                    eleven.Placed = true;
                    int counter = 0;
                    //////////в некоторых случаях зависает на нерешаемой комбинации, предусмотреть альтернативный способ выхода
                    while ((twelve.Row != 2 || twelve.Col != 2 || fifteen.Row != 3 || fifteen.Col != 3 || fourteen.Col != 2 || fourteen.Row != 3) /*&& counter < 4)
                    {
                        RandomMove(zero);
                        counter++;
                    }
                    if (twelve.Row == 2 || twelve.Col == 2 || fifteen.Row == 3 || fifteen.Col == 3 || fourteen.Col == 2 || fourteen.Row == 3)
                    {
                        map.MoveLeft(zero);
                        map.MoveLeft(zero);
                        map.MoveDown(zero);
                        map.MoveRight(zero);
                        map.MoveRight(zero);
                    }
                    else
                    {
                        ten.Placed = false;
                        eleven.Placed = false;
                        RandomMove(zero);
                    }
                }
                else//////////в некоторых случаях зависает на нерешаемой комбинации, предусмотреть альтернативный способ выхода
                {
                    twelve.CheckPlace();
                    fifteen.Placed = true;
                    while (!ten.CheckPlace() || !eleven.CheckPlace())
                    {
                        RandomMove(zero);
                    }
                    fifteen.Placed = false;
                    while (!fifteen.CheckPlace())
                    {
                        map.MoveRight(zero);
                    }
                }*/
            }
        }
        private void RandomMove(Cell zero)
        {
            Random rand = new Random();
            int next = rand.Next(0, 4);
            var flag = false;
            while (!flag)
            {
                switch (next)
                {
                    case 0:
                        if (zero.CanUp() && zero.Previous != "down")
                        {
                            map.MoveUp(zero);
                            zero.Previous = "up";
                            flag = true;
                        }
                        else
                        {
                            next = rand.Next(0, 4);
                        }
                        break;
                    case 1:
                        if (zero.CanRight() && zero.Previous != "left")
                        {
                            map.MoveRight(zero);
                            zero.Previous = "right";
                            flag = true;
                        }
                        else
                        {
                            next = rand.Next(0, 4);
                        }
                        break;
                    case 2:
                        if (zero.CanDown() && zero.Previous != "up")
                        {
                            map.MoveDown(zero);
                            zero.Previous = "down";
                            flag = true;
                        }
                        else
                        {
                            next = rand.Next(0, 4);
                        }
                        break;
                    case 3:
                        if (zero.CanLeft() && zero.Previous != "right")
                        {
                            map.MoveLeft(zero);
                            zero.Previous = "left";
                            flag = true;
                        }
                        else
                        {
                            next = rand.Next(0, 4);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        internal void PlaceSecondRow()
        {
            PlaceNum(map.GetNum(6));
            PlaceNum(map.GetNum(7));
            if (map.GetNum(8).CheckPlace())
            {
                return;
            }
            map.GetNum(8).TargRow = 2;
            map.GetNum(8).TargCol = 2;
            var zero = map.GetNum(0);
            if (zero.Down.Value == 8 && map.GetNum(8).Row == 2 && map.GetNum(8).Col == 3)
            {
                map.MoveDown(zero);
            }
            else if (!(map.GetNum(8).Row == 1 && map.GetNum(8).Col == 3))//если 8 не на месте
            {
                PlaceNum(map.GetNum(8));
                BlackMagic6(map.GetNum(8));
            }
            map.GetNum(8).TargCol = 3;
            map.GetNum(8).TargRow = 1;
            map.GetNum(6).CheckPlace();
            map.GetNum(7).CheckPlace();
            map.GetNum(8).CheckPlace();
        }
        private void BlackMagic6(Cell eight)
        {
            var zero = map.GetNum(0);
            while (!zero.IsNear(eight))
            {
                switch (zero.GetDirection(eight, true))
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
            switch (zero.GetDirection(eight))
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
                    map.MoveUp(zero);
                    map.MoveRight(zero);
                    map.MoveDown(zero);
                    map.MoveRight(zero);
                    map.MoveUp(zero);
                    map.MoveLeft(zero);
                    map.MoveLeft(zero);
                    map.MoveDown(zero);
                    break;
                default:
                    break;
            }
        }
        private void BlackMagic3(Cell nine)
        {
            var zero = map.GetNum(0);
            while (!zero.IsNear(nine))
            {
                switch (zero.GetDirection(nine, true))
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
            switch (zero.GetDirection(nine))
            {
                case 'd'://если 0 сверху цели
                    map.MoveGroup17(zero);
                    break;
                case 'l'://если 0 справа от цели
                    map.MoveUp(zero);
                    map.MoveLeft(zero);
                    goto case 'd';
                case 'u'://если 0 снизу цели
                    map.MoveRight(zero);
                    map.MoveUp(zero);
                    goto case 'l';
                default:
                    break;
            }

        }
        private void BlackMagic2(Cell nine)
        {
            var zero = map.GetNum(0);
            while (!zero.IsNear(nine))
            {
                switch (zero.GetDirection(nine, true))
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
            map.MoveGroup17(zero);
            map.MoveRight(zero);
            map.MoveUp(zero);
            map.MoveGroup17(zero);
            map.MoveGroup9(zero);
            map.MoveUp(zero);
            map.MoveGroup17(zero);
            map.MoveRight(zero);
            map.MoveGroup9(zero);
        }
        private void BlackMagic1(Cell four)
        {
            var zero = map.GetNum(0);
            while (!zero.IsNear(four))
            {
                switch (zero.GetDirection(four, true))
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
            actual.CheckPlace();
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
                                    if (actual.Col == 3 || actual.Row == 3 || (map.GetNum(13).Placed && actual.Value == 5))
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
                                    if ((actual.Col != 3 && actual.Row != 3) && (actual.Value != 5))
                                    {
                                        map.MoveGroup4(zero);
                                        map.MoveGroup5(zero);
                                    }
                                    else if (actual.Value == 5)
                                    {
                                        map.MoveGroup8(zero);
                                        map.MoveGroup3(zero);
                                        map.MoveGroup18(zero);

                                    }
                                    else
                                    {
                                        map.MoveGroup8(zero);
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
                                    if ((actual.Row != 3 && !map.GetNum(13).Placed) || (actual.Row == 1 && (actual.Value == 9 || actual.Value == 5 || actual.Value == 6 || actual.Value == 10)))
                                    {
                                        map.MoveRight(zero);
                                        map.MoveGroup5(zero);
                                    }
                                    else if (map.GetNum(13).Placed && actual.Value == 5 && actual.Row == 3)
                                    {
                                        map.MoveRight(zero);
                                        map.MoveGroup3(zero);
                                        map.MoveRight(zero);
                                        map.MoveUp(zero);
                                        map.MoveUp(zero);
                                        map.MoveLeft(zero);
                                        map.MoveLeft(zero);
                                        map.MoveDown(zero);
                                        map.MoveRight(zero);
                                        //map.MoveGroup9(zero);
                                        //map.MoveGroup18(zero);
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
                                    else if (map.GetNum(13).Placed && actual.Value == 5)
                                    {
                                        map.MoveRight(zero);
                                        map.MoveGroup9(zero);
                                        map.MoveGroup3(zero);
                                        map.MoveGroup18(zero);
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
                                        map.MoveLeft(zero);
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
                actual.CheckPlace();
            }
        }
    }
}
