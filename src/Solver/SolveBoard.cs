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

            var loop = 100;
            var proceed = true;
            while (proceed && (loop-- > 0))
            {
                proceed = !SolvePossibilities();
                Console.WriteLine(_board.ToActualString());
            }
            Console.WriteLine();
        }

        private bool Solve()
        {
            // todo-at: solve by row, column, and block
            return SolvePossibilities();
        }

        private bool SolvePossibilities()
        {
            var cells = _board.Cells;

            var rowIndexLowerBound = cells.GetLowerBound(0);
            var rowIndexUpperBound = cells.GetUpperBound(0);
            var columnIndexLowerBound = cells.GetLowerBound(1);
            var columnIndexUpperBound = cells.GetUpperBound(1);
            var cellsLeft = 0;
            for (var rowIndex = rowIndexLowerBound; rowIndex <= rowIndexUpperBound; rowIndex++)
            {
                for (var columnIndex = columnIndexLowerBound; columnIndex <= columnIndexUpperBound; columnIndex++)
                {
                    var cell = cells[rowIndex, columnIndex];
                    if (cell.Possibilities.Count == 1)
                    {
                        Console.WriteLine($"Setting cell actual: {cell.Possibilities[0]}");
                        cell.SetActual(cell.Possibilities[0]);
                    }
                    else if (cell.Possibilities.Count > 1)
                    {
                        cellsLeft++;
                    }
                }
            }
            Console.WriteLine($"{cellsLeft} cells left.");

            return cellsLeft == 0;
        }
    }
}