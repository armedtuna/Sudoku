using Xunit;
using DataParsers.General;

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

            // todo-at: switch to fluent validation
            Assert.Equal(5, slice.Length);
            Assert.Equal(new byte[] { 11, 12, 13, 14, 15 }, slice);
        }

        [Fact]
        public void Column_ShouldSlice()
        {
            var data = BuildData();

            var slicer = new ArraySlicer<byte>(data);

            var slice = slicer.GetColumnSlice(1);

            Assert.Equal(4, slice.Length);
            Assert.Equal(new byte[] { 2, 7, 12, 17 }, slice);
        }

        [Fact]
        public void Block_ShouldSlice()
        {
            var data = BuildData();

            var slicer = new ArraySlicer<byte>(data);

            var slice = slicer.GetBlockSlice(1, 1, 2, 3);

            // Assert.Equal(1, slice.GetUpperBound(0));
            // Assert.Equal(2, slice.GetUpperBound(1));
            Assert.Equal(new byte[,] { { 7, 8, 9 }, { 12, 13, 14 } }, slice);
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