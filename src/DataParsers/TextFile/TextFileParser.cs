using System;
using System.IO;
using System.Linq;
using FluentValidation;
using Sudoku;

namespace DataParsers.TextFile
{
    public class TextFileParser
    {
        private readonly Validators.ContentsArrayValidator _validator;

        public TextFileParser(Validators.ContentsArrayValidator validator)
        {
            _validator = validator;
        }

        public Cell[,] Load(string path)
        {
            var contents = File.ReadAllText(path);
            return Parse(contents);
        }

        public Cell[,] Parse(string contents)
        {
            var data = ConvertToArray(contents);
            return ParseCells(data);
        }

        private byte[,] ConvertToArray(string contents)
        {
            var lines = contents.Split(Environment.NewLine.ToCharArray());
            _validator.ValidateAndThrow(lines);

            return ConvertToArray(lines);
        }

        public static byte[,] ConvertToArray(string[] lines)
        {
            var longestLine = lines.Max(l => l.Length);

            var data = new byte[lines.Length, longestLine];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var c = line[j];
                    var b = c == ' '
                        ? (byte)0
                        : byte.Parse(c.ToString());
                    data[i, j] = b;
                }
            }

            return data;
        }

        private Cell[,] ParseCells(byte[,] data)
        {
            var maxRows = data.GetUpperBound(0) + 1;
            var maxColumns = data.GetUpperBound(1) + 1;
            var cells = new Cell[maxRows, maxColumns];
            for (var rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < maxColumns; columnIndex++)
                {
                    cells[rowIndex, columnIndex] = BuildCell(data[rowIndex, columnIndex]);
                }
            }

            return cells;
        }

        private Cell BuildCell(byte number)
        {
            var cell = new Cell();
            cell.SetActual(number);
            return cell;
        }
    }
}