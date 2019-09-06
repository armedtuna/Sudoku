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
            ParentRow = new Row { Cells = new Cell[] { } };
            ParentColumn = new Column { Cells = new Cell[] { } };
            ParentBlock = new Block { Cells = new Cell[,] { } };
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

        // todo-at: test?
        // todo-at: should this be in the model?
        public void SetActual(byte actual)
        {
            _actual = actual;

            // when calling this from the data parser, the rows, cells, and blocks are not set up yet
            // when calling this after the board has been set up, the possibilities need to be updated,
            // so this needs to be done here
            // todo-at: consider the above and see if there is a better way to set this up
            RemovePossibility(actual, true);
        }

        public void RemovePossibility(byte possibility, bool updateConnectedCells)
        {
            if (updateConnectedCells)
            {
                for (var i = 0; i < ParentRow.Cells.Length; i++)
                {
                    ParentRow.Cells[i]._possibilities.Remove(possibility);
                }

                for (var i = 0; i < ParentColumn.Cells.Length; i++)
                {
                    ParentColumn.Cells[i]._possibilities.Remove(possibility);
                }

                for (var i = 0; i <= ParentBlock.Cells.GetUpperBound(0); i++)
                {
                    for (var j = 0; j <= ParentBlock.Cells.GetUpperBound(1); j++)
                    {
                        ParentBlock.Cells[i, j]._possibilities.Remove(possibility);
                    }
                }
            }
            else
            {
                _possibilities.Remove(possibility);
            }
        }
   }
}
