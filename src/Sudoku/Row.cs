using System;
using System.Text;

namespace Sudoku
{
    public class Row
    {
        public Cell[] Cells { get; set; }

        public string ToActualString()
        {
            var sb = new StringBuilder();
            for (var index = Cells.GetLowerBound(0); index <= Cells.GetUpperBound(0); index++)
            {
                sb.Append(Cells[index].ToActualString());
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}