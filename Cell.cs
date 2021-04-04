using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class Cell
    {
        internal bool Placed { get; set; }
        internal int Value { private set; get; }
        internal int Row { set; get; }
        internal int Col { set; get; }
        internal int TargRow {  set; get; }
        internal int TargCol {  set; get; }
        /// <summary>
        /// Устанавливает true или false для placed
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
        public Cell(Cell father)
        {
            TargCol = father.TargCol;
            TargRow = father.TargRow;
            Value = father.Value;
            Row = father.Row;
            Col = father.Col;
            Placed = father.Placed;
        }
        internal Cell Clone()
        {
            return new Cell(this);
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
        internal char GetDirection(Cell actual, bool accurate = false)
        {
            if (accurate)
            {
                if (Row > actual.Row && !Up.Placed)//если 0 снизу цели и над 0 неустановленная клетка
                {
                    return 'u';
                }
                if (Row < actual.Row && !Down.Placed)//если 0 сверху цели
                {
                    return 'd';
                }
                if (Col < actual.Col)//если 0 слева от цели
                {
                    return 'r';
                }
                else//если 0 справа от цели
                {
                    return 'l';
                }
            }
            else
            {
                if (Row < actual.Row)//если 0 сверху цели
                {
                    return 'd';
                }

                if (Row > actual.Row)//если 0 снизу цели 
                {
                    return 'u';
                }
                if (Col < actual.Col)//если 0 слева от цели
                {
                    return 'r';
                }
                else//если 0 справа от цели
                {
                    return 'l';
                }
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

        internal bool NeedDown()
        {
            if (Row < TargRow)
            {
                return true;
            }
            return false;
        }

        internal bool CanUp()
        {
            if (Up!=null && !Up.Placed)
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
    }
}
