// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Grid.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleDesignPrinciples.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Two-dimensional grid.
    /// </summary>
    /// <typeparam name="T">Type of the elements of this grid.</typeparam>
    public class Grid<T> : IEnumerable<T>, IEquatable<Grid<T>>
    {
        #region Fields

        /// <summary>
        /// Actual grid.
        /// </summary>
        private readonly T[,] grid;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Creates a new grid with the specified width and height.
        /// </summary>
        /// <param name="width">Width of the new grid.</param>
        /// <param name="height">Height of the new grid.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is less than or equal to zero.</exception>
        public Grid(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "width", width, string.Format("Width has to be positive."));
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "height", height, string.Format("Height has to be positive."));
            }

            this.grid = new T[width, height];
        }

        /// <summary>
        /// Creates a shallow copy of the specified grid.
        /// </summary>
        /// <param name="other">Grid to copy.</param>
        public Grid(Grid<T> other)
            : this(other.Width, other.Height)
        {
            for (var i = 0; i < this.Width; i++)
            {
                for (var j = 0; j < this.Height; j++)
                {
                    this.grid[i, j] = other.grid[i, j];
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Height of this grid.
        /// </summary>
        public int Height
        {
            get
            {
                return this.grid.GetUpperBound(1) + 1;
            }
        }

        /// <summary>
        /// Width of this grid.
        /// </summary>
        public int Width
        {
            get
            {
                return this.grid.GetUpperBound(0) + 1;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets or sets the element with the specified indices in this grid.
        /// </summary>
        /// <param name="i">First index of the element.</param>
        /// <param name="j">Second index of the element.</param>
        /// <returns>Element with the specified indices in this grid.</returns>
        public T this[int i, int j]
        {
            get
            {
                this.CheckIndices(i, j);
                return this.grid[i, j];
            }

            set
            {
                this.CheckIndices(i, j);
                this.grid[i, j] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Grid<T> other)
        {
            return this.grid.Rank == other.grid.Rank
                   && Enumerable.Range(0, this.grid.Rank)
                                .All(dimension => this.grid.GetLength(dimension) == other.grid.GetLength(dimension))
                   && this.grid.Cast<T>().SequenceEqual(other.grid.Cast<T>());
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((Grid<T>)obj);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Width; i++)
            {
                for (var j = 0; j < this.Height; j++)
                {
                    if (this.grid[i, j] != null)
                    {
                        yield return this.grid[i, j];
                    }
                }
            }
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.grid != null ? this.grid.GetHashCode() : 0;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (var i = 0; i < this.Width; i++)
            {
                stringBuilder.Append("[ ");

                for (var j = 0; j < this.Height; j++)
                {
                    stringBuilder.AppendFormat("{0}, ", this[i, j]);
                }

                stringBuilder.Length = stringBuilder.Length - 2;
                stringBuilder.AppendLine("]");
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks whether the specified indices are within the bounds of this
        /// grid.
        /// </summary>
        /// <param name="i">First index to check.</param>
        /// <param name="j">Second index to check.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="i"/> or <paramref name="j"/> is less than zero or greater than or equal to the size of this grid.
        /// </exception>
        private void CheckIndices(int i, int j)
        {
            if (i < 0 || i >= this.Width)
            {
                throw new ArgumentOutOfRangeException(
                    "i", i, string.Format("i has to be between 1 and {0}, but was {1}.", this.Width - 1, i));
            }

            if (j < 0 || j >= this.Height)
            {
                throw new ArgumentOutOfRangeException(
                    "j", j, string.Format("j has to be between 1 and {0}, but was {1}.", this.Height - 1, j));
            }
        }

        #endregion
    }
}