using System;
using ApacheTech.Common.Extensions.System;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

namespace ApacheTech.Common.Extensions.Functional.Monadic.Optional
{
    /// <summary>
    ///     Extension methods for monadic functions.
    /// </summary>
    public static class MonadicOptionalExtensions
    {
        /// <summary>
        ///     Performs an explicit conversion from <typeparamref name="T"/> to <see cref="ToOptional{T}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static Maybe<T> ToOptional<T>(this T value)
        {
            return new Actual<T>(value);
        }

        /// <summary>
        ///     Binds a function to a monad of type <see cref="Maybe{TFromType}"/>, mutating the return type dependent on the result.
        /// </summary>
        /// <typeparam name="TFromType">The type of the value wrapped within the input monad.</typeparam>
        /// <typeparam name="TToType">The type of the value to wrap within the output monad.</typeparam>
        /// <param name="this">The monad to bind the function to.</param>
        /// <param name="f">The function to bind.</param>
        /// <returns>Returns an <see cref="Actual{TToType}"/>, if the execution of the function succeeds; otherwise returns a <see cref="Nothing{TToType}"/>.</returns>

        public static Maybe<TToType> Bind<TFromType, TToType>(this Maybe<TFromType> @this,
            Func<TFromType, TToType> f)
        {
            switch (@this)
            {
                case Actual<TFromType> something when !something.Value.IsDefaultValue():
                    try
                    {
                        return f(something).ToOptional();
                    }
                    catch (Exception)
                    {
                        return new Nothing<TToType>();
                    }
                default:
                    return new Nothing<TToType>();
            }
        }
    }
}