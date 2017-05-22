using System;
using System.Collections.Generic;
using static System.Console;

namespace Bigsb.Sudoku.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new int[9, 9];

            ReadLine();
        }

        private char[] CalculatePossibleValues(int x, int y, int[,] matrix)
        {
            var result = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int columnIndex = 0; columnIndex < matrix.Length; columnIndex++)
            {
                if ()
            }
            return new char[0];
        }
    }
}
