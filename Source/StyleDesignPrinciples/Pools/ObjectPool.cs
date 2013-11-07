// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectPool.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleDesignPrinciples.Pools
{
    using System.Collections.Generic;

    /// <summary>
    /// Pools objects in order to save allocation time.
    /// </summary>
    /// <typeparam name="T">Type of the pooled objects.</typeparam>
    public class ObjectPool<T>
        where T : IPoolable, new()
    {
        #region Fields

        /// <summary>
        /// Pooled objects.
        /// </summary>
        private readonly Stack<T> pool;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Creates a new pool for the specified number of objects.
        /// </summary>
        /// <param name="capacity">Number of pooled objects.</param>
        public ObjectPool(int capacity)
        {
            // Create new pool.
            this.pool = new Stack<T>(capacity);

            this.Capacity = capacity;

            // Create pooled objects.
            for (var i = 0; i < this.Capacity; i++)
            {
                T poolableObject = new T();
                this.pool.Push(poolableObject);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Maximum number of pooled objects.
        /// </summary>
        public int Capacity { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets a pooled object, if there is any, or constructs a new one.
        /// </summary>
        /// <returns>Pooled object.</returns>
        public T Alloc()
        {
            // Get from pool if there is one.
            return this.pool.Count > 0 ? this.pool.Pop() : new T();
        }

        /// <summary>
        /// Returns the specified object to the pool and resets it, if there
        /// is any capacity left.
        /// </summary>
        /// <param name="poolableObject">Object to return.</param>
        public void Free(T poolableObject)
        {
            // Put into pool if hasn't reached maximum capacity yet.
            if (this.pool.Count >= this.Capacity)
            {
                return;
            }

            poolableObject.Reset();
            this.pool.Push(poolableObject);
        }

        #endregion
    }
}