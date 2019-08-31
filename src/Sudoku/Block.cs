using System;
using System.Text;

namespace Sudoku
{
    public class Block
    {
        public Cell[,] Cells { get; set; }

        public string ToActualString()
        {
            var sb = new StringBuilder();
            for (var rowIndex = Cells.GetLowerBound(0); rowIndex <= Cells.GetUpperBound(0); rowIndex++)
            {
                for (var columnIndex = Cells.GetLowerBound(1); columnIndex <= Cells.GetUpperBound(1); columnIndex++)
                {
                    sb.Append(Cells[rowIndex, columnIndex].ToActualString());
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}