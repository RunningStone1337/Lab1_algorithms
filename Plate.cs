using System;
using System.Diagnostics.CodeAnalysis;

namespace Lab4
{
    [Serializable]
    public class Plate
    {
        public int Size { get; set; }
        public string Visual { get; set; }
        public Plate(int size)
        {
            Size = size;
            Visual = "-";
            for (int i = 1; i < size; i++)
            {
                Visual += "--";
            }
        }
    }
}
