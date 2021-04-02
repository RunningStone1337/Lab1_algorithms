﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class Cell
    {
        bool placed;
        internal bool Placed
        {
            get { return placed; }
            set { placed = value; }
        }
        internal int Value { private set; get; }
        internal int Row { set; get; }
        internal int Col { set; get; }
        internal int TargPosRow { private set; get; }
        internal int TargPosCol { private set; get; }

        internal Cell Up { set; get; }
        internal Cell Right { set; get; }
        internal Cell Down { set; get; }
        internal Cell Left { set; get; }
        public Cell(int val, int r, int c)
        {
            if (val == 0)
            {
                TargPosCol = 3;
                TargPosRow = 3;
            }
            else
            {
                TargPosCol = (val - 1) % 4;
                TargPosRow = (val - 1) / 4;
            }
            Value = val;
            Row = r;
            Col = c;
            if (ColIsPlaced() && RowIsPlaced())
            {
                placed = true;
            }
        }
        public Cell(Cell father)
        {
            TargPosCol = father.TargPosCol;
            TargPosRow = father.TargPosRow;
            Value = father.Value;
            Row = father.Row;
            Col = father.Col;
            Placed = father.Placed;
        }
        internal Cell Clone()
        {
            return new Cell(this);
        }
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
            return Row == TargPosRow;
        }
        internal bool ColIsPlaced()
        {
            return Col == TargPosCol;
        }

        /// <summary>
        /// Определяет в каком направлении находится цель относительно вызывающей
        /// </summary>
        /// <param name="actual">Целевая клетка</param>
        /// <returns></returns>
        internal char GetDirection(Cell actual)
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
}
