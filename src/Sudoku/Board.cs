using System;
using System.Text;

namespace Sudoku
{
    public class Board
    {
        public Block[,] Blocks { get; set; }

        public Row[] Rows { get; set; }

        public Column[] Columns { get; set; }

        public Cell[,] Cells { get; set; } 

        public string ToActualString()
        {
            var sb = new StringBuilder();
            sb.Append("+---+---+---+" + Environment.NewLine);
            for (var rowIndex = Cells.GetLowerBound(0); rowIndex <= Cells.GetUpperBound(0); rowIndex++)
            {
                sb.Append('|');
                for (var columnIndex = Cells.GetLowerBound(1); columnIndex <= Cells.GetUpperBound(1); columnIndex++)
                {
                    sb.Append(Cells[rowIndex, columnIndex].ToActualString());
                    if (columnIndex % 3 == 2)
                    {
                        sb.Append('|');
                    }
                }
                sb.Append(Environment.NewLine);
                if (rowIndex % 3 == 2)
                {
                    sb.Append("+---+---+---+" + Environment.NewLine);
                }
            }

            return sb.ToString();
        }
    }
}