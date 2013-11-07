// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectPoolTest.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleDesignPrinciples.Tests
{
    using NUnit.Framework;

    using StyleDesignPrinciples.Pools;

    public class ObjectPoolTest
    {
        #region Public Methods and Operators

        [Test]
        public void TestAllocAndFree()
        {
            var pool = new ObjectPool<PoolableObject>(10);
            var poolableObject = pool.Alloc();

            poolableObject.Value = 22;

            pool.Free(poolableObject);

            Assert.AreEqual(0, poolableObject.Value);
        }

        #endregion

        private class PoolableObject : IPoolable
        {
            #region Public Properties

            public int Value { get; set; }

            #endregion

            #region Public Methods and Operators

            public void Reset()
            {
                this.Value = 0;
            }

            #endregion
        }
    }
}