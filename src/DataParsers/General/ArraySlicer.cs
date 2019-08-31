using System;
using System.Collections.Generic;

namespace DataParsers.General
{
    public class ArraySlicer<T>
    {
        private T[,] _data;
        public int RowLowerBound { get; set; }
        public int RowUpperBound { get; set; }
        public int ColumnLowerBound { get; set; }
        public int ColumnUpperBound { get; set; }

        public ArraySlicer(T[,] data)
        {
            _data = data;
            RowLowerBound = _data.GetLowerBound(0);
            RowUpperBound = _data.GetUpperBound(0);
            ColumnLowerBound = _data.GetLowerBound(1);
            ColumnUpperBound = _data.GetUpperBound(1);
        }

        public T[] GetRowSlice(int rowIndex)
        {
            ValidateRowIndex(rowIndex);

            var slice = new List<T>();
            for (var columnIndex = ColumnLowerBound; columnIndex <= ColumnUpperBound; columnIndex++)
            {
                slice.Add(_data[rowIndex, columnIndex]);
            }

            return slice.ToArray();
        }

        public T[] GetColumnSlice(int columnIndex)
        {
            ValidateColumnIndex(columnIndex);

            var slice = new List<T>();
            for (var rowIndex = RowLowerBound; rowIndex <= RowUpperBound; rowIndex++)
            {
                slice.Add(_data[rowIndex, columnIndex]);
            }

            return slice.ToArray();
        }

        public T[,] GetBlockSlice(int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            ValidateBlockIndexes(rowStart, columnStart, rowEnd, columnEnd);

            // var rowMax = rowEnd - rowStart;
            // var columnMax = columnEnd - columnStart;
            var blocks = new T[rowEnd - rowStart + 1, columnEnd - columnStart + 1];
            for (var rowIndex = rowStart; rowIndex <= rowEnd; rowIndex++)
            {
                for (var columnIndex = columnStart; columnIndex <= columnEnd; columnIndex++)
                {
                    blocks[rowIndex - rowStart, columnIndex - columnStart] = _data[rowIndex, columnIndex];
                }
            }

            return blocks;
        }

        private void ValidateRowIndex(int rowIndex)
        {
            if (rowIndex < RowLowerBound || rowIndex > RowUpperBound)
            {
                throw new IndexOutOfRangeException($"Row index {rowIndex} is out of bounds.");
            }
        }

        private void ValidateColumnIndex(int columnIndex)
        {
            if (columnIndex < ColumnLowerBound || columnIndex > ColumnUpperBound)
            {
                throw new IndexOutOfRangeException($"Column index {columnIndex} is out of bounds.");
            }
        }

        private void ValidateBlockIndexes(int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            if (rowStart < RowLowerBound || rowStart > RowUpperBound)
            {
                throw new IndexOutOfRangeException($"Row start {rowStart} is out of bounds.");
            }

            if (rowEnd < RowLowerBound || rowEnd > RowUpperBound)
            {
                throw new IndexOutOfRangeException($"Row end {rowEnd} is out of bounds.");
            }

            if (columnStart < ColumnLowerBound || columnStart > ColumnUpperBound)
            {
                throw new IndexOutOfRangeException($"Column start {columnStart} is out of bounds.");
            }

            if (columnEnd < ColumnLowerBound || columnEnd > ColumnUpperBound)
            {
                throw new IndexOutOfRangeException($"Column end {columnEnd} is out of bounds.");
            }

            if (rowStart > rowEnd)
            {
                throw new IndexOutOfRangeException("Row start must be less than or equal to row end.");
            }

            if (columnStart > columnEnd)
            {
                throw new IndexOutOfRangeException("Column start must be less than or equal to column end.");
            }
        }
    }
}