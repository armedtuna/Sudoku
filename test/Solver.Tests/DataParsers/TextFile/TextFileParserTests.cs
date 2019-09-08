using System;
using System.Collections.Generic;
using System.Text;
using DataParsers.TextFile;
using DataParsers.TextFile.Validators;
using FluentAssertions;
using Xunit;

namespace Solver.Tests.DataParsers.TextFile
{
    public class TextFileParserTests
    {
        [Fact]
        public void ConvertStringArray_ProducesByteArray()
        {
            var lines = new[]
            {
                "123",
                "45",
                " 67",
                "",
                "    89"
            };

            var data = TextFileParser.ConvertToArray(lines);

            data.GetUpperBound(0).Should().Be(4);
            data.GetUpperBound(1).Should().Be(5);
            var expectedData = new byte [,]
            {
                { 1, 2, 3, 0, 0, 0 },
                { 4, 5, 0, 0, 0, 0 },
                { 0, 6, 7, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 8, 9 }
            };
            data.Should().Contain(expectedData);
        }

        [Fact]
        public void ParseString_ProducesCellArray()
        {
            var contents = BuildContents();

            var validator = new ContentsArrayValidator();
            var parser = new TextFileParser(validator);

            var cells = parser.Parse(contents);

            cells.Should().NotBeNull();
            cells.GetUpperBound(0).Should().Be(8);
            cells.GetUpperBound(1).Should().Be(8);
            cells[0, 0].Actual.Should().Be(2);
            cells[0, 1].Actual.Should().Be(0);
            cells[0, 2].Actual.Should().Be(0);
            cells[0, 3].Actual.Should().Be(0);
            cells[0, 4].Actual.Should().Be(9);
            cells[0, 5].Actual.Should().Be(4);
            cells[0, 6].Actual.Should().Be(7);
            cells[0, 7].Actual.Should().Be(0);
            cells[0, 8].Actual.Should().Be(5);
            cells[1, 0].Actual.Should().Be(0);
            cells[1, 1].Actual.Should().Be(0);
            cells[1, 2].Actual.Should().Be(0);
            cells[1, 3].Actual.Should().Be(8);
            cells[1, 4].Actual.Should().Be(0);
            cells[1, 5].Actual.Should().Be(5);
            cells[1, 6].Actual.Should().Be(1);
            cells[1, 7].Actual.Should().Be(0);
            cells[1, 8].Actual.Should().Be(0);
            cells[2, 0].Actual.Should().Be(0);
            cells[2, 1].Actual.Should().Be(0);
            cells[2, 2].Actual.Should().Be(5);
            cells[2, 3].Actual.Should().Be(7);
            cells[2, 4].Actual.Should().Be(0);
            cells[2, 5].Actual.Should().Be(0);
            cells[2, 6].Actual.Should().Be(0);
            cells[2, 7].Actual.Should().Be(4);
            cells[2, 8].Actual.Should().Be(0);
            cells[3, 0].Actual.Should().Be(0);
            cells[3, 1].Actual.Should().Be(0);
            cells[3, 2].Actual.Should().Be(0);
            cells[3, 3].Actual.Should().Be(4);
            cells[3, 4].Actual.Should().Be(2);
            cells[3, 5].Actual.Should().Be(0);
            cells[3, 6].Actual.Should().Be(8);
            cells[3, 0].Actual.Should().Be(0);
            cells[3, 8].Actual.Should().Be(7);
            cells[4, 0].Actual.Should().Be(1);
            cells[4, 1].Actual.Should().Be(0);
            cells[4, 2].Actual.Should().Be(8);
            cells[4, 3].Actual.Should().Be(0);
            cells[4, 4].Actual.Should().Be(7);
            cells[4, 5].Actual.Should().Be(0);
            cells[4, 6].Actual.Should().Be(5);
            cells[4, 7].Actual.Should().Be(0);
            cells[4, 8].Actual.Should().Be(4);
            cells[5, 0].Actual.Should().Be(7);
            cells[5, 1].Actual.Should().Be(0);
            cells[5, 2].Actual.Should().Be(4);
            cells[5, 3].Actual.Should().Be(0);
            cells[5, 4].Actual.Should().Be(8);
            cells[5, 5].Actual.Should().Be(9);
            cells[5, 6].Actual.Should().Be(0);
            cells[5, 7].Actual.Should().Be(0);
            cells[5, 8].Actual.Should().Be(0);
            cells[6, 0].Actual.Should().Be(0);
            cells[6, 1].Actual.Should().Be(1);
            cells[6, 2].Actual.Should().Be(0);
            cells[6, 3].Actual.Should().Be(0);
            cells[6, 4].Actual.Should().Be(0);
            cells[6, 5].Actual.Should().Be(6);
            cells[6, 6].Actual.Should().Be(3);
            cells[6, 7].Actual.Should().Be(0);
            cells[6, 8].Actual.Should().Be(0);
            cells[7, 0].Actual.Should().Be(0);
            cells[7, 1].Actual.Should().Be(0);
            cells[7, 2].Actual.Should().Be(2);
            cells[7, 3].Actual.Should().Be(9);
            cells[7, 4].Actual.Should().Be(0);
            cells[7, 5].Actual.Should().Be(8);
            cells[7, 6].Actual.Should().Be(0);
            cells[7, 7].Actual.Should().Be(0);
            cells[7, 8].Actual.Should().Be(0);
            cells[8, 0].Actual.Should().Be(4);
            cells[8, 1].Actual.Should().Be(0);
            cells[8, 2].Actual.Should().Be(3);
            cells[8, 3].Actual.Should().Be(1);
            cells[8, 4].Actual.Should().Be(5);
            cells[8, 5].Actual.Should().Be(0);
            cells[8, 6].Actual.Should().Be(0);
            cells[8, 7].Actual.Should().Be(0);
            cells[8, 8].Actual.Should().Be(2);
        }

        private string BuildContents()
        {
            return
                "2   947 5" + Environment.NewLine +
                "   8 51" + Environment.NewLine +
                "  57   4" + Environment.NewLine +
                "   42 8 7" + Environment.NewLine +
                "1 8 7 5 4" + Environment.NewLine +
                "7 4 89" + Environment.NewLine +
                " 1   63" + Environment.NewLine +
                "  29 8" + Environment.NewLine +
                "4 315   2";
        }
    }
}