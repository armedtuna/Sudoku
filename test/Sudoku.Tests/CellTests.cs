using FluentAssertions;
using Xunit;

namespace Sudoku.Tests
{
    public class CellTests
    {
        [Fact]
        public void ActualString_ShouldReturnSpace()
        {
            var cell = BuildCell(0);
            var result = cell.ToActualString();

            result.Should().Be(" ");
        }

        [Fact]
        public void ActualIsRemoved_FromPossibilities()
        {
            var cell = BuildCell(0);
            cell.SetActual(2);

            cell.Possibilities.Should().NotContain(2);
        }

        [Fact]
        public void PossibilityIsRemoved_FromAllRelatedCells()
        {
            var cell = BuildCell(0);
            var relatedCell1 = BuildCell(4);
            var relatedCell2 = BuildCell(4);
            var relatedCell3 = BuildCell(4);
            cell.ParentRow = new Row { Cells = new[] { relatedCell1 } };
            cell.ParentColumn = new Column { Cells = new[] { relatedCell2 } };
            cell.ParentBlock = new Block { Cells = new[,] { { relatedCell3 } } };

            cell.SetActual(4);

            cell.Actual.Should().Be(4);
            cell.ParentRow.Cells[0].Possibilities.Should().NotContain(4);
            cell.ParentColumn.Cells[0].Possibilities.Should().NotContain(4);
            cell.ParentBlock.Cells[0,0].Possibilities.Should().NotContain(4);
        }

        private Cell BuildCell(byte actual)
        {
            var cell = new Cell();
            cell.SetActual(actual);
            return cell;
        }
    }
}
