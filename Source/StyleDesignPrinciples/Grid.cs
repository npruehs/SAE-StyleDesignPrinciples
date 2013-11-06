// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Grid.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleDesignPrinciples
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Grid<T> : IEnumerable<T>, IEquatable<Grid<T>>
    {
        #region Fields

        private readonly T[,] grid;

        #endregion

        #region Constructors and Destructors

        public Grid(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "width", width, string.Format("width has to be positive."));
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "height", height, string.Format("height has to be positive."));
            }

            this.grid = new T[width, height];
        }

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

        public int Height
        {
            get
            {
                return this.grid.GetUpperBound(1) + 1;
            }
        }

        public int Width
        {
            get
            {
                return this.grid.GetUpperBound(0) + 1;
            }
        }

        #endregion

        #region Public Indexers

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

        public bool Equals(Grid<T> other)
        {
            return this.grid.Rank == other.grid.Rank
                   && Enumerable.Range(0, this.grid.Rank)
                                .All(dimension => this.grid.GetLength(dimension) == other.grid.GetLength(dimension))
                   && this.grid.Cast<T>().SequenceEqual(other.grid.Cast<T>());
        }

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

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Grid<T>)obj);
        }

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

        public override int GetHashCode()
        {
            return this.grid != null ? this.grid.GetHashCode() : 0;
        }

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Methods

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