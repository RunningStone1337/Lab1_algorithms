using System;
using System.Collections.Generic;

namespace Lab3
{
    /// <summary>
    /// Представляет поле 4х4, состоящее из клеток
    /// </summary>
    class Field
    {
        internal static long counter = 0;
        internal static int delay = 300;
        internal static List<int> movelist;
        Cell[,] map = new Cell[4, 4];
        /// <summary>
        /// Клнструктор
        /// </summary>
        /// <param name="arr"></param>
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
        /// <summary>
        /// Метод для размещения первого ряда
        /// </summary>
        internal void PlaceFirstRow()
        {
            map.Print();
            PlaceNum(map.GetNum(1));
            PlaceNum(map.GetNum(2));
            PlaceNum(map.GetNum(3));
            var four = map.GetNum(4);
            if (four.CheckPlace())
            {
                return;
            }
            four.TargRow = 1;
            four.TargCol = 2;
            var zero = map.GetNum(0);
            if (zero.Row == 0 && zero.Col == 3 && zero.Down == four)
            {
                four.Actual = true;
                map.MoveDown(zero);
                four.Actual = false;
            }
            else if (!(four.Row == 0 && four.Col == 3))
            {
                PlaceNum(four);
                BlackMagic1(four);
            }
        }
        /// <summary>
        /// Мтод размещения первой колонки
        /// </summary>
        internal void PlaceFirstCol()
        {
            var five = map.GetNum(5);
            var nine = map.GetNum(9);
            var thirteen = map.GetNum(13);
            if (five.CheckPlace() && nine.CheckPlace() && thirteen.CheckPlace())
            {
                return;
            }
            var zero = map.GetNum(0);
            PlaceNum(thirteen);
            if (!nine.CheckPlace())
            {
                PlaceNum(nine);
            }
            if (zero.Right == five && zero.Down == nine)
            {
                five.Actual = true;
                map.MoveRight(zero);
                five.CheckPlace();
                five.Actual = false;
            }
            if (!five.CheckPlace())
            {
                five.TargCol = 1;
                five.TargRow = 2;
                PlaceNum(five);
                BlackMagic2(five);
            }
        }
        /// <summary>
        /// Метод размещения последних трёх клеток поля для одной из конечных конфигураций
        /// </summary>
        /// <param name="fourteen"></param>
        private void BlackMagic5(Cell fourteen)
        {
            fourteen.Actual = true;
            var zero = map.GetNum(0);
            while (!zero.IsNear(fourteen))
            {
                switch (zero.GetDirectionToMove(fourteen))
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
            map.MoveLeft(zero);
            map.MoveDown(zero);
            map.MoveRight(zero);
            map.MoveRight(zero);
            map.MoveUp(zero);
            map.MoveLeft(zero);
            map.MoveDown(zero);
            map.MoveLeft(zero);
            map.MoveUp(zero);
            map.MoveRight(zero);
            map.MoveRight(zero);
            map.MoveDown(zero);
            map.MoveLeft(zero);
            map.MoveUp(zero);
            fourteen.Actual = false;
        }
        /// <summary>
        /// Метод размещения последних трёх клеток поля для одной из конечных конфигураций
        /// </summary>
        /// <param name="fourteen"></param>
        private void BlackMagic4(Cell fourteen)
        {
            fourteen.Actual = true;
            var zero = map.GetNum(0);
            while (!zero.IsNear(fourteen))
            {
                switch (zero.GetDirectionToMove(fourteen))
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
            switch (zero.GetDirection(fourteen))
            {
                case Cell.Direction.Left://если 0 справа от цели
                    map.MoveUp(zero);
                    map.MoveLeft(zero);
                    goto case Cell.Direction.Down;
                case Cell.Direction.Down://если 0 сверху от цели
                    map.MoveLeft(zero);
                    map.MoveDown(zero);
                    map.MoveRight(zero);
                    break;
                default:
                    break;
            }
            fourteen.Actual = false;

        }
        /// <summary>
        /// Метод размещения последних трёх клеток поля
        /// </summary>
        internal void PlaceRest()
        {
            var zero = map.GetNum(0);
            var ten = map.GetNum(10);
            var eleven = map.GetNum(11);
            var twelve = map.GetNum(12);
            var fourteen = map.GetNum(14);
            var fifteen = map.GetNum(15);
            if (ten.CheckPlace() && eleven.CheckPlace() && twelve.CheckPlace())
            {
                PlaceNum(fourteen);
                PlaceNum(fifteen);
                return;
            }
            else
            {
                ten.TargRow = 3;
                PlaceNum(ten);
                if (ten.Up == fourteen)
                {
                    BlackMagic5(fourteen);
                }
                if (zero.Down == ten && zero.Right == fourteen) //если требуется перемешать
                {
                    fourteen.Actual = true;
                    map.MoveDown(zero);
                    map.MoveRight(zero);
                    map.MoveUp(zero);
                    map.MoveRight(zero);
                    map.MoveDown(zero);
                    map.MoveLeft(zero);
                    map.MoveLeft(zero);
                    map.MoveUp(zero);
                    map.MoveRight(zero);
                    fourteen.Actual = false;
                }
                fourteen.TargCol = 2;
                PlaceNum(fourteen);
                BlackMagic4(fourteen);
                PlaceNum(eleven);
                PlaceNum(twelve);
                PlaceNum(fifteen);
            }
        }
       /// <summary>
       /// Метод размещения второго ряда
       /// </summary>
        internal void PlaceSecondRow()
        {
            var zero = map.GetNum(0);
            var six = map.GetNum(6);
            var seven = map.GetNum(7);
            var eight = map.GetNum(8);
            if (six.CheckPlace() && seven.CheckPlace() && eight.CheckPlace())
            {
                return;
            }
            PlaceNum(six);
            PlaceNum(seven);
            if (eight.Col == 3 && eight.Row == 2 && eight.Up == zero)
            {
                PlaceNum(eight);
            }
            if (!eight.CheckPlace())
            {
                eight.TargCol = 2;
                eight.TargRow = 2;
                PlaceNum(eight);
                BlackMagic3(eight);
            }
        }
        /// <summary>
        /// Метод размещения одной из конечных конфигураций для второго ряда
        /// </summary>
        /// <param name="eight"></param>
        private void BlackMagic3(Cell eight)
        {
            eight.Actual = true;
            var zero = map.GetNum(0);
            while (!zero.IsNear(eight))
            {
                switch (zero.GetDirectionToMove(eight))
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
            switch (zero.GetDirection(eight))
            {
                case Cell.Direction.Left://если 0 справа от цели
                    map.MoveDown(zero);
                    map.MoveLeft(zero);
                    goto case Cell.Direction.Up;
                case Cell.Direction.Up://если 0 снизу цели
                    map.MoveLeft(zero);
                    map.MoveUp(zero);
                    goto case Cell.Direction.Right;
                case Cell.Direction.Right://если 0 слева от цели
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
            eight.Actual = false;
        }
        /// <summary>
        /// Метод размещения первого столбца поля для одной из конечных конфигураций
        /// </summary>
        /// <param name="five"></param>
        private void BlackMagic2(Cell five)
        {
            five.Actual = true;
            var zero = map.GetNum(0);
            while (!zero.IsNear(five))
            {
                switch (zero.GetDirectionToMove(five))
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
            switch (zero.GetDirection(five))
            {
                case Cell.Direction.Left://если 0 справа от цели
                    map.MoveDown(zero);
                    map.MoveLeft(zero);
                    goto case Cell.Direction.Up;
                case Cell.Direction.Up://если 0 снизу цели
                    map.MoveLeft(zero);
                    map.MoveUp(zero);
                    map.MoveRight(zero);
                    map.MoveUp(zero);
                    map.MoveLeft(zero);
                    map.MoveDown(zero);
                    map.MoveDown(zero);
                    map.MoveRight(zero);
                    break;
                case Cell.Direction.Down://если 0 сверху от цели
                    map.MoveRight(zero);
                    map.MoveDown(zero);
                    goto case Cell.Direction.Left;
                default:
                    break;
            }
            five.Actual = false;
        }
        private void BlackMagic1(Cell four)
        {
            var zero = map.GetNum(0);
            four.Actual = true;
            while (!zero.IsNear(four))
            {
                switch (zero.GetDirectionToMove(four))
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
            switch (zero.GetDirection(four))
            {
                case Cell.Direction.Left://если 0 справа от цели
                    map.MoveDown(zero);
                    map.MoveLeft(zero);
                    goto case Cell.Direction.Up;
                case Cell.Direction.Up://если 0 снизу цели
                    map.MoveLeft(zero);
                    map.MoveUp(zero);
                    goto case Cell.Direction.Right;
                case Cell.Direction.Right://если 0 слева от цели
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
            four.Actual = false;
        }
        /// <summary>
        /// Метод размещения элементарной клетки на поле
        /// </summary>
        /// <param name="target"></param>
        private void PlaceNum(Cell target)
        {
            var zero = map.GetNum(0);
            target.Actual = true;
            target.CheckPlace();
            if (target.Placed)
            {
                target.Actual = false;
                return;
            }
            while (!target.Placed)//двигаем пока не поставим на своё место
            {
                map.MoveToNeighbourhood(target);//пока не будем рядом с целью
                Cell.Direction destination_side = target.GetDirectionToMove();
                /*
                 определяем, в каком направлении двигаться
                приоритет: лево-право, вверх-вниз.
                определяем кратчайший для передвижения с учётом уже размещённых
                 */
                switch (zero.GetDirection(target))//определяем, с какой стороны граничит с целью 0
                {
                    case Cell.Direction.Up://0 снизу
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                map.MoveRight(zero);
                                map.MoveUp(zero);
                                break;
                            case Cell.Direction.Right://надо вправо
                                map.MoveRight(zero);
                                map.MoveUp(zero);
                                break;
                            case Cell.Direction.Down://надо вниз
                                map.MoveUp(zero);
                                break;
                            case Cell.Direction.Left://надо влево
                                if (zero.CanLeft())
                                {
                                    map.MoveLeft(zero);
                                    map.MoveUp(zero);
                                }
                                else
                                {
                                    map.MoveRight(zero);
                                    map.MoveUp(zero);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Cell.Direction.Right://0 слева
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                if (zero.CanUp())
                                {
                                    map.MoveUp(zero);
                                    map.MoveRight(zero);
                                }
                                else
                                {
                                    map.MoveDown(zero);
                                    map.MoveRight(zero);
                                }
                                break;
                            case Cell.Direction.Right://надо вправо
                                if (zero.CanUp())
                                {
                                    map.MoveUp(zero);
                                    map.MoveRight(zero);
                                }
                                else
                                {
                                    map.MoveDown(zero);
                                    map.MoveRight(zero);
                                }
                                break;
                            case Cell.Direction.Down://надо вниз
                                if (zero.CanDown())
                                {
                                    map.MoveDown(zero);
                                    map.MoveRight(zero);
                                }
                                else if (zero.CanUp())
                                {
                                    map.MoveUp(zero);
                                    map.MoveRight(zero);
                                }
                                break;
                            case Cell.Direction.Left://надо влево
                                map.MoveRight(zero);
                                break;
                            default:
                                break;
                        }
                        break;
                    case Cell.Direction.Down://0 сверху
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                map.MoveDown(zero);
                                break;
                            case Cell.Direction.Right://надо вправо
                                map.MoveRight(zero);
                                map.MoveDown(zero);
                                break;
                            case Cell.Direction.Down://надо вниз
                                map.MoveRight(zero);
                                map.MoveDown(zero);
                                break;
                            case Cell.Direction.Left://надо влево
                                if (zero.CanLeft() && target.CanLeft())//и слева не установленная клетка и целевая может идти влево
                                {
                                    map.MoveLeft(zero);
                                    map.MoveDown(zero);
                                }
                                else
                                {
                                    map.MoveRight(zero);
                                    map.MoveDown(zero);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Cell.Direction.Left://0 справа
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                map.MoveUp(zero);
                                map.MoveLeft(zero);
                                break;
                            case Cell.Direction.Right://надо вправо
                                map.MoveLeft(zero);
                                break;
                            case Cell.Direction.Down://надо вниз
                                map.MoveDown(zero);
                                map.MoveLeft(zero);
                                break;
                            case Cell.Direction.Left://надо влево
                                if ((zero.CanDown() && zero.Previous != "up") || target.Row == 0)
                                {
                                    map.MoveDown(zero);
                                    map.MoveLeft(zero);
                                }
                                else
                                {
                                    map.MoveUp(zero);
                                    map.MoveLeft(zero);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                target.CheckPlace();
            }
            target.Actual = false;

        }
    }
}
