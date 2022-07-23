using System;

namespace ApacheTech.Common.Extensions.Functional.Monadic.Identity
{
    /// <summary>
    ///     Extension methods for monadic functions.
    /// </summary>
    public static class MonadicIdentityExtensions
    {
        /// <summary>
        ///     Wraps the value in a monad, allowing functional style operations to be executed.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        /// <returns>
        ///     An instance of <see cref="Identity{T}"/>, which wraps the value, and provides access to functional style monadic operations.
        /// </returns>
        public static Identity<T> ToIdentity<T>(this T value)
        {
            return new Identity<T>(value);
        }

        /// <summary>
        ///     Binds a function to a monad of type <see cref="Identity{TFromType}"/>, mutating the return type dependent on the result.
        /// </summary>
        /// <typeparam name="TFrom">The type of the value wrapped within the input monad.</typeparam>
        /// <typeparam name="TTo">The type of the value to wrap within the output monad.</typeparam>
        /// <param name="this">The monad to bind the function to.</param>
        /// <param name="f">The function to bind.</param>
        /// <returns>Returns the result of the bound operation, as an instance of <see cref="Identity{TTo}"/>.</returns>
        public static Identity<TTo> Bind<TFrom, TTo>(this Identity<TFrom> @this, Func<TFrom, TTo> f)
        {
            return f(@this).ToIdentity();
        }

        /// <summary>
        ///     Allows a secondary value to be included within an <see cref="Identity{T}"/>. Designed to approximate the `let` operation in Linq code.
        /// </summary>
        /// <remarks>EXPERIMENTAL: Not guaranteed to work fluently with other monadic operations.</remarks>
        /// <typeparam name="TFromType">The type of from type.</typeparam>
        /// <typeparam name="TLetType">The type of the let type.</typeparam>
        /// <param name="this">The monad to bind the function to.</param>
        /// <param name="f">The function to extract the included value, from the input.</param>
        /// <returns>A tuple, wrapped in an <see cref="Identity{T}"/>, which holds the raw input value, as well as the included secondary value.</returns>
        public static Identity<(TFromType Value, Identity<TLetType> Include)> Include<TFromType, TLetType>(this Identity<TFromType> @this, Func<Identity<TFromType>, TLetType> f)
        {
            return (@this, f(@this));
        }
    }
}