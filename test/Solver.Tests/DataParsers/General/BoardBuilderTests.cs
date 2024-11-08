using System;
using System.Text;
using DataParsers.General;
using FluentAssertions;
using Sudoku;
using Xunit;

namespace Solver.Tests.DataParsers.General
{
    public class BoardBuilderTests
    {
        [Fact]
        public void Board_ParsesCellData()
        {
            var board = BuildBoard();

            board.Should().NotBeNull();
            board.Cells.Should().NotBeNull();
            board.Rows.Should().NotBeNull();
            board.Columns.Should().NotBeNull();
            board.Blocks.Should().NotBeNull();
            VerifyBlocks(board);
            VerifyRows(board);
            VerifyColumns(board);
        }

        [Fact]
        public void Board_CreatesCellParents()
        {
            var board = BuildBoard();

            VerifyParentBlocks(board);
            VerifyParentRows(board);
            VerifyParentColumns(board);
        }

        private void VerifyParentRows(Board board)
        {
            for (var blockRowIndex = 0; blockRowIndex < board.Rows.Length; blockRowIndex++)
            {
                var row = board.Rows[blockRowIndex];
                for (var index = 0; index < row.Cells.Length; index++)
                {
                    row.Cells[index].ParentRow.Should().Be(row);
                }
            }
        }

        private void VerifyParentColumns(Board board)
        {
            for (var blockColumnIndex = 0; blockColumnIndex < board.Columns.Length; blockColumnIndex++)
            {
                var column = board.Columns[blockColumnIndex];
                for (var index = 0; index < column.Cells.Length; index++)
                {
                    column.Cells[index].ParentColumn.Should().Be(column);
                }
            }
        }

        private void VerifyParentBlocks(Board board)
        {
            for (var blockRowIndex = 0; blockRowIndex < 3; blockRowIndex++)
            {
                for (var blockColumnIndex = 0; blockColumnIndex < 3; blockColumnIndex++)
                {
                    var block = board.Blocks[blockRowIndex, blockColumnIndex];
                    for (var rowIndex = 0; rowIndex <= block.Cells.GetUpperBound(0); rowIndex++)
                    {
                        for (var columnIndex = 0; columnIndex <= block.Cells.GetUpperBound(1); columnIndex++)
                        {
                            block.Cells[rowIndex, columnIndex].ParentBlock.Should().Be(block);
                        }
                    }
                }
            }
        }

        private void VerifyBlocks(Board board)
        {
            var block1 = BuildJoinedString("2  ", "   ", "  5");
            var block2 = BuildJoinedString(" 94", "8 5", "7  ");
            var block3 = BuildJoinedString("7 5", "1  ", " 4 ");
            var block4 = BuildJoinedString("   ", "1 8", "7 4");
            var block5 = BuildJoinedString("42 ", " 7 ", " 89");
            var block6 = BuildJoinedString("8 7", "5 4", "   ");
            var block7 = BuildJoinedString(" 1 ", "  2", "4 3");
            var block8 = BuildJoinedString("  6", "9 8", "15 ");
            var block9 = BuildJoinedString("3  ", "   ", "  2");
            board.Blocks.GetUpperBound(0).Should().Be(2);
            board.Blocks.GetUpperBound(1).Should().Be(2);
            board.Blocks[0, 0].ToActualString().Should().Be(block1);
            board.Blocks[0, 1].ToActualString().Should().Be(block2);
            board.Blocks[0, 2].ToActualString().Should().Be(block3);
            board.Blocks[1, 0].ToActualString().Should().Be(block4);
            board.Blocks[1, 1].ToActualString().Should().Be(block5);
            board.Blocks[1, 2].ToActualString().Should().Be(block6);
            board.Blocks[2, 0].ToActualString().Should().Be(block7);
            board.Blocks[2, 1].ToActualString().Should().Be(block8);
            board.Blocks[2, 2].ToActualString().Should().Be(block9);
        }

        private void VerifyRows(Board board)
        {
            var row1 = BuildJoinedString("2   947 5");
            var row2 = BuildJoinedString("   8 51  ");
            var row3 = BuildJoinedString("  57   4 ");
            var row4 = BuildJoinedString("   42 8 7");
            var row5 = BuildJoinedString("1 8 7 5 4");
            var row6 = BuildJoinedString("7 4 89   ");
            var row7 = BuildJoinedString(" 1   63  ");
            var row8 = BuildJoinedString("  29 8   ");
            var row9 = BuildJoinedString("4 315   2");
            board.Rows.Length.Should().Be(9);
            board.Rows[0].ToActualString().Should().Be(row1);
            board.Rows[1].ToActualString().Should().Be(row2);
            board.Rows[2].ToActualString().Should().Be(row3);
            board.Rows[3].ToActualString().Should().Be(row4);
            board.Rows[4].ToActualString().Should().Be(row5);
            board.Rows[5].ToActualString().Should().Be(row6);
            board.Rows[6].ToActualString().Should().Be(row7);
            board.Rows[7].ToActualString().Should().Be(row8);
            board.Rows[8].ToActualString().Should().Be(row9);
        }

        private void VerifyColumns(Board board)
        {
            var column1 = BuildJoinedString("2", " ", " ", " ", "1", "7", " ", " ", "4");
            var column2 = BuildJoinedString(" ", " ", " ", " ", " ", " ", "1", " ", " ");
            var column3 = BuildJoinedString(" ", " ", "5", " ", "8", "4", " ", "2", "3");
            var column4 = BuildJoinedString(" ", "8", "7", "4", " ", " ", " ", "9", "1");
            var column5 = BuildJoinedString("9", " ", " ", "2", "7", "8", " ", " ", "5");
            var column6 = BuildJoinedString("4", "5", " ", " ", " ", "9", "6", "8", " ");
            var column7 = BuildJoinedString("7", "1", " ", "8", "5", " ", "3", " ", " ");
            var column8 = BuildJoinedString(" ", " ", "4", " ", " ", " ", " ", " ", " ");
            var column9 = BuildJoinedString("5", " ", " ", "7", "4", " ", " ", " ", "2");
            board.Columns.Length.Should().Be(9);
            board.Columns[0].ToActualString().Should().Be(column1);
            board.Columns[1].ToActualString().Should().Be(column2);
            board.Columns[2].ToActualString().Should().Be(column3);
            board.Columns[3].ToActualString().Should().Be(column4);
            board.Columns[4].ToActualString().Should().Be(column5);
            board.Columns[5].ToActualString().Should().Be(column6);
            board.Columns[6].ToActualString().Should().Be(column7);
            board.Columns[7].ToActualString().Should().Be(column8);
            board.Columns[8].ToActualString().Should().Be(column9);
        }

        private static string BuildJoinedString(params string[] rows)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < rows.Length; i++)
            {
                sb.Append(rows[i] + Environment.NewLine);
            }
            return sb.ToString();
            // return String.Join(Environment.NewLine, rows) +
            //     Environment.NewLine;
        }

        private Board BuildBoard()
        {
            var converter = new BoardBuilder();
            var cells = BuildCells();
            return converter.BuildBoard(cells);
        }

        private Cell[,] BuildCells()
        {
            return new Cell[,]
            {
                { BuildCell(2), BuildCell(0), BuildCell(0), BuildCell(0), BuildCell(9), BuildCell(4), BuildCell(7), BuildCell(0), BuildCell(5) },
                { BuildCell(0), BuildCell(0), BuildCell(0), BuildCell(8), BuildCell(0), BuildCell(5), BuildCell(1), BuildCell(0), BuildCell(0) },
                { BuildCell(0), BuildCell(0), BuildCell(5), BuildCell(7), BuildCell(0), BuildCell(0), BuildCell(0), BuildCell(4), BuildCell(0) },
                { BuildCell(0), BuildCell(0), BuildCell(0), BuildCell(4), BuildCell(2), BuildCell(0), BuildCell(8), BuildCell(0), BuildCell(7) },
                { BuildCell(1), BuildCell(0), BuildCell(8), BuildCell(0), BuildCell(7), BuildCell(0), BuildCell(5), BuildCell(0), BuildCell(4) },
                { BuildCell(7), BuildCell(0), BuildCell(4), BuildCell(0), BuildCell(8), BuildCell(9), BuildCell(0), BuildCell(0), BuildCell(0) },
                { BuildCell(0), BuildCell(1), BuildCell(0), BuildCell(0), BuildCell(0), BuildCell(6), BuildCell(3), BuildCell(0), BuildCell(0) },
                { BuildCell(0), BuildCell(0), BuildCell(2), BuildCell(9), BuildCell(0), BuildCell(8), BuildCell(0), BuildCell(0), BuildCell(0) },
                { BuildCell(4), BuildCell(0), BuildCell(3), BuildCell(1), BuildCell(5), BuildCell(0), BuildCell(0), BuildCell(0), BuildCell(2) }
            };
        }

        private Cell BuildCell(byte number)
        {
            var cell = new Cell();
            cell.SetActual(number);
            return cell;
        }
    }
}