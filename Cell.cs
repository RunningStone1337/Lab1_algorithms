using System;

namespace Lab3
{
    public class Cell
    {
        internal bool Placed { get; set; }
        internal bool Actual { get; set; }
        internal int Value { private set; get; }
        internal int Row { set; get; }
        internal int Col { set; get; }
        internal int TargRow { set; get; }
        internal int TargCol { set; get; }
        /// <summary>
        /// Проверяет, на своём ли месте клетка
        /// </summary>
        internal bool CheckPlace()
        {
            if (ColIsPlaced() && RowIsPlaced())
            {
                Placed = true;
                return true;
            }
            else
            {
                Placed = false;
                return false;
            }
        }
        internal Cell Up { set; get; }
        internal Cell Right { set; get; }
        internal Cell Down { set; get; }
        internal Cell Left { set; get; }
        internal string Previous { get; set; }

        public Cell(int val, int r, int c)
        {
            if (val == 0)
            {
                TargCol = 3;
                TargRow = 3;
            }
            else
            {
                TargCol = (val - 1) % 4;
                TargRow = (val - 1) / 4;
            }
            Value = val;
            Row = r;
            Col = c;
        }

        /// <summary>
        /// Проверяет на соседство вызывающей клетки с параметром
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        internal bool IsNear(Cell actual)
        {
            if (Up == actual || Right == actual || Left == actual || Down == actual)
            {
                return true;
            }
            return false;
        }
        internal bool RowIsPlaced()
        {
            return Row == TargRow;
        }
        internal bool ColIsPlaced()
        {
            return Col == TargCol;
        }
        /// <summary>
        /// Определяет в каком направлении находится по отношению к цели с учётом приоритета
        /// </summary>
        /// <param name="actual">Целевая клетка</param>
        /// <returns></returns>
        internal Direction GetDirectionToMove(Cell actual)
        {
            if (Row > actual.Row && !Up.Placed)//если 0 снизу цели и над 0 неустановленная клетка
            {
                return Direction.Up;
            }
            if (Row < actual.Row && !Down.Placed)//если 0 сверху цели и под 0 неустановленная
            {
                return Direction.Down;
            }
            if (Col < actual.Col && !Right.Placed)//если 0 слева от цели и справа от 0 неустановленная
            {
                return Direction.Right;
            }
            else//если 0 справа от цели
            {
                return Direction.Left;
            }
        }
        /// <summary>
        /// Определяет с какой стороны от вызывающей находится переданная в параметр. Работает корректно если они действительно граничат.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        internal Direction GetDirection(Cell actual)
        {
            if (Up == actual)//если 0 снизу цели 
            {
                return Direction.Up;
            }
            if (Down == actual)//если 0 сверху цели
            {
                return Direction.Down;
            }
            if (Right == actual)//если 0 слева от цели
            {
                return Direction.Right;
            }
            else//если 0 справа от цели
            {
                return Direction.Left;
            }
        }
        internal bool NeedUp()
        {
            if (Row > TargRow)
            {
                return true;
            }
            return false;
        }

        internal bool NeedLeft()
        {
            if (Col > TargCol)
            {
                return true;
            }
            return false;
        }

        internal bool NeedRight()
        {
            if (Col < TargCol)
            {
                return true;
            }
            return false;
        }

        internal bool CanUp()
        {
            if (Up != null && !Up.Placed)
            {
                return true;
            }
            return false;
        }

        internal bool CanRight()
        {
            if (Right != null && !Right.Placed)
            {
                return true;
            }
            return false;
        }

        internal bool CanDown()
        {
            if (Down != null && !Down.Placed)
            {
                return true;
            }
            return false;
        }

        internal bool CanLeft()
        {
            if (Left != null && !Left.Placed)
            {
                return true;
            }
            return false;
        }
        public enum Direction
        {
            Up = 0, Right = 1, Down = 2, Left = 3
        }



        /// <summary>
        /// Возвращает предпочтительное направление движения перемещаемой клетки
        /// </summary>
        /// <returns></returns>
        internal Direction GetDirectionToMove()
        {
            if (NeedLeft() && !Left.Placed)
            {
                return Direction.Left;
            }
            if (NeedRight() && !Right.Placed)
            {
                return Direction.Right;
            }
            if (NeedUp() && !Up.Placed)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.Down;
            }
        }
    }
}
