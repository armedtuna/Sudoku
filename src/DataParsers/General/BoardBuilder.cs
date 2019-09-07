using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku;

namespace DataParsers.General
{
    public class BoardBuilder
    {
        public Board BuildBoard(Cell[,] cells)
        {
            var slicer = new ArraySlicer<Cell>(cells);
            var maxRows = slicer.RowUpperBound - slicer.RowLowerBound + 1;
            var maxColumns = slicer.ColumnUpperBound - slicer.ColumnLowerBound + 1;
            var maxBlockRows = maxRows / 3;
            var maxBlockColumns = maxColumns / 3;

            var board = new Board();
            board.Cells = cells;
            board.Rows = BuildRows(maxRows, slicer);
            board.Columns = BuildColumns(maxColumns, slicer);
            board.Blocks = BuildBlocks(maxBlockRows, maxBlockColumns, slicer);

            // since the rows, columns, and blocks were not set up until now, the possibilities could not be calculated
            RefreshPossibilities(board);

            return board;
        }

        private Row[] BuildRows(int maxRows, ArraySlicer<Cell> slicer)
        {
            var rows = new Row[maxRows];
            for (var rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                var row = new Row
                {
                    Cells = slicer.GetRowSlice(rowIndex)
                };
                for (var i = 0; i < row.Cells.Length; i++)
                {
                    row.Cells[i].ParentRow = row;
                }
                rows[rowIndex] = row;
            }

            return rows;
        }

        private Column[] BuildColumns(int maxColumns, ArraySlicer<Cell> slicer)
        {
            var columns = new Column[maxColumns];
            for (var columnIndex = 0; columnIndex < maxColumns; columnIndex++)
            {
                var column = new Column
                {
                    Cells = slicer.GetColumnSlice(columnIndex)
                };
                for (var i = 0; i < column.Cells.Length; i++)
                {
                    column.Cells[i].ParentColumn = column;
                }
                columns[columnIndex] = column;
            }

            return columns;
        }

        private Block[,] BuildBlocks(int maxBlockRows, int maxBlockColumns, ArraySlicer<Cell> slicer)
        {
            var blocks = new Block[maxBlockRows, maxBlockColumns];
            for (var rowIndex = 0; rowIndex < maxBlockRows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < maxBlockColumns; columnIndex++)
                {
                    var r1 = rowIndex * maxBlockRows;
                    var c1 = columnIndex * maxBlockColumns;
                    var r2 = r1 + maxBlockRows - 1;
                    var c2 = c1 + maxBlockColumns - 1;
                    var block = new Block
                    {
                        Cells = slicer.GetBlockSlice(r1, c1, r2, c2)
                    };
                    for (var i = 0; i <= block.Cells.GetUpperBound(0); i++)
                    {
                        for (var j = 0; j <= block.Cells.GetUpperBound(1); j++)
                        {
                            block.Cells[i, j].ParentBlock = block;
                        }
                    }
                    blocks[rowIndex, columnIndex] = block;
                }
            }

            return blocks;
        }

        private void RefreshPossibilities(Board board)
        {
            for (var i = 0; i <= board.Cells.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= board.Cells.GetUpperBound(1); j++)
                {
                    var cell = board.Cells[i, j];
                    cell.RemovePossibility(cell.Actual);
                }
            }
        }
    }
}