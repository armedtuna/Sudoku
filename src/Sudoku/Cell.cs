using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Cell
    {
        private byte _actual;

        public Block ParentBlock { get; set; }

        public Row ParentRow { get; set; }

        public Column ParentColumn { get; set; }

        public byte Actual { get { return _actual; } }

        // todo-at: change to array
        public List<byte> Possibilities { get { return GetPossibilities().ToList(); } }

        public byte Guess { get; set; }
 
        public string ToActualString()
        {
            var c = Actual.ToString()[0];
            if (c == '0')
            {
                c = ' ';
            }

            return c.ToString();
        }

        public void SetActual(byte actual)
        {
            _actual = actual;
        }

        public byte[] GetPossibilities()
        {
            return new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                .Except(ParentBlock.GetActualValues())
                .Except(ParentRow.GetActualValues())
                .Except(ParentColumn.GetActualValues())
                .ToArray();
        }
   }
}
