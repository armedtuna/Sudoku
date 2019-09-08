using Xunit;
using DataParsers.General;
using FluentAssertions;

namespace SolverUnitTests.DataParsers.TextFile
{
    public class ArraySlicerTests
    {
        [Fact]
        public void Row_ShouldSlice()
        {
            var data = BuildData();

            var slicer = new ArraySlicer<byte>(data);

            var slice = slicer.GetRowSlice(2);

            slice.Length.Should().Be(5);
            slice.Should().Contain(new byte[] { 11, 12, 13, 14, 15 });
        }

        [Fact]
        public void Column_ShouldSlice()
        {
            var data = BuildData();

            var slicer = new ArraySlicer<byte>(data);

            var slice = slicer.GetColumnSlice(1);

            slice.Length.Should().Be(4);
            slice.Should().Contain(new byte[] { 2, 7, 12, 17 });
        }

        [Fact]
        public void Block_ShouldSlice()
        {
            var data = BuildData();

            var slicer = new ArraySlicer<byte>(data);

            var slice = slicer.GetBlockSlice(1, 1, 2, 3);

            slice.GetUpperBound(0).Should().Be(1);
            slice.GetUpperBound(1).Should().Be(2);
            slice.Should().Contain(new byte[,] { { 7, 8, 9 }, { 12, 13, 14 } });
        }

        private byte[,] BuildData()
        {
            return new byte[,]
            {
                {
                    1, 2, 3, 4, 5
                },
                {
                    6, 7, 8, 9, 10
                },
                {
                    11, 12, 13, 14, 15
                },
                {
                    16, 17, 18, 19, 20
                }
            };
        }
    }
}