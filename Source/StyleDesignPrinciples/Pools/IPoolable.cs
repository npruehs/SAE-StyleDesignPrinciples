// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPoolable.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleDesignPrinciples.Pools
{
    /// <summary>
    /// Object that can be pooled to save allocation time.
    /// </summary>
    /// <remarks>
    /// Implementers should use the <see cref="Reset"/> method for resetting
    /// the object to its initial state, i.e. as if it has just been
    /// constructed.
    /// </remarks>
    public interface IPoolable
    {
        #region Public Methods and Operators

        /// <summary>
        /// Resets this object to its initial state.
        /// </summary>
        void Reset();

        #endregion
    }
}