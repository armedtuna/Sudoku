using System.Collections.Generic;

namespace Sudoku
{
    public class Cell
    {
        private byte _actual;
        private List<byte> _possibilities;

        public Block ParentBlock { get; set; }

        public Row ParentRow { get; set; }

        public Column ParentColumn { get; set; }

        public byte Actual { get { return _actual; } }

        public List<byte> Possibilities { get { return _possibilities; } }

        public byte Guess { get; set; }

        public Cell()
        {            
            _possibilities = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
 
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

            RemovePossibility(actual);
        }

        public void RemovePossibility(byte possibility)
        {
            if (ParentRow?.Cells != null)
            {
                for (var i = 0; i < ParentRow.Cells.Length; i++)
                {
                    ParentRow.Cells[i]._possibilities.Remove(possibility);
                }
            }

            if (ParentColumn?.Cells != null)
            {
                for (var i = 0; i < ParentColumn.Cells.Length; i++)
                {
                    ParentColumn.Cells[i]._possibilities.Remove(possibility);
                }
            }

            if (ParentBlock?.Cells != null)
            {
                for (var i = 0; i <= ParentBlock.Cells.GetUpperBound(0); i++)
                {
                    for (var j = 0; j <= ParentBlock.Cells.GetUpperBound(1); j++)
                    {
                        ParentBlock.Cells[i, j]._possibilities.Remove(possibility);
                    }
                }
            }

            _possibilities.Remove(possibility);
        }
   }
}
