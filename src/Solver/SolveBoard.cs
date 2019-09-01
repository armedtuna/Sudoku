using System;
using System.Collections.Generic;
using Sudoku;

namespace Solver
{
    public class SolveBoard
    {
        private Board _board;

        public void Solve(Board board)
        {
            _board = board;
            EnsurePossibilities();
        }

        private void EnsurePossibilities()
        {
            var cells = _board.Cells;

            var rowIndexLowerBound = cells.GetLowerBound(0);
            var rowIndexUpperBound = cells.GetUpperBound(0);
            var columnIndexLowerBound = cells.GetLowerBound(1);
            var columnIndexUpperBound = cells.GetUpperBound(1);
            for (var rowIndex = rowIndexLowerBound; rowIndex <= rowIndexUpperBound; rowIndex++)
            {
                for (var columnIndex = columnIndexLowerBound; columnIndex <= columnIndexUpperBound; columnIndex++)
                {
                    var cell = cells[rowIndex, columnIndex];
                    if (cell.Possibilities == null)
                    {
                        cell.Possibilities = new List<byte>();
                    }
                }
            }
        }

        // todo-at: test?
        // todo-at: is method in the correct place? this can also be used by someone solving the puzzle
        private void SetActual(Cell cell, byte actual)
        {
            if (!cell.Possibilities.Contains(actual))
            {
                var possiblitiesString = string.Join(", ", cell.Possibilities);
                // todo-at: if used by someone playing the puzzle then this exception shouldn't be thrown
                throw new Exception($"Cell with possiblities {possiblitiesString} doesn't contain {actual}.");
            }

            cell.Actual = actual;

            var parentRowCells = cell.ParentRow.Cells;
            for (var i = 0; i < parentRowCells.Length; i++)
            {
                parentRowCells[i].Possibilities.Remove(actual);
            }

            var parentColumnCells = cell.ParentColumn.Cells;
            for (var i = 0; i < parentColumnCells.Length; i++)
            {
                parentColumnCells[i].Possibilities.Remove(actual);
            }

            var parentBlockCells = cell.ParentBlock.Cells;
            for (var i = 0; i <= parentBlockCells.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= parentBlockCells.GetUpperBound(1); j++)
                {
                    parentBlockCells[i, j].Possibilities.Remove(actual);
                }
            }
        }
    }
}