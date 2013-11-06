namespace StyleDesignPrinciples.Tests
{
    using System;

    using NUnit.Framework;

    public class GridTest
    {
        #region Fields

        private Grid<int> grid;

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void SetUp()
        {
            this.grid = new Grid<int>(10, 10);
        }

        [Test]
        public void TestCopyConstructor()
        {
            this.grid[5, 7] = 2;
            var copy = new Grid<int>(this.grid);
            Assert.AreEqual(this.grid, copy);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGridInvalidWidth()
        {
            this.grid = new Grid<int>(-1, 2);
        }

        [Test]
        public void TestIndexer()
        {
            this.grid[5, 7] = 2;
            Assert.AreEqual(2, this.grid[5, 7]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidIndex()
        {
            this.grid[-1, 2] = 3;
        }

        [Test]
        public void TestWidthHeight()
        {
            this.grid = new Grid<int>(3, 4);
            Assert.AreEqual(3, this.grid.Width);
            Assert.AreEqual(4, this.grid.Height);
        }

        #endregion
    }
}