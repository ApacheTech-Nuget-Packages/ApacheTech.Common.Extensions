﻿namespace ApacheTech.Common.Extensions.Functional.Monadic.Optional
{
    /// <summary>
    ///     A monadic wrapper for a type that may or may not be valid, during the execution of its function tree.
    /// </summary>
    /// <typeparam name="T">The type of value to wrap.</typeparam>
    public abstract class Maybe<T>
    {
        /// <summary>
        ///     Gets the raw wrapped value.
        /// </summary>
        /// <value>
        ///     The value wrapped within this monad.
        /// </value>
        public abstract T Value { get; }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Maybe{T}"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="this">The instance of <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator T(Maybe<T> @this) => @this.Value;
    }
}