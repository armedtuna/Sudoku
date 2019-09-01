using System.Collections.Generic;

namespace Sudoku
{
    public class Cell
    {
        public Block ParentBlock { get; set; }

        public Row ParentRow { get; set; }

        public Column ParentColumn { get; set; }

        public byte Actual { get; set; }

        public List<byte> Possibilities { get; set; }

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
   }
}