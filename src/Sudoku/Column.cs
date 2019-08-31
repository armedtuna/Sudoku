using System;
using System.Text;

namespace Sudoku
{
    public class Column
    {
        public Cell[] Cells { get; set; }

        public string ToActualString()
        {
            var sb = new StringBuilder();
            for (var index = Cells.GetLowerBound(0); index <= Cells.GetUpperBound(0); index++)
            {
                sb.Append(Cells[index].ToActualString() + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}