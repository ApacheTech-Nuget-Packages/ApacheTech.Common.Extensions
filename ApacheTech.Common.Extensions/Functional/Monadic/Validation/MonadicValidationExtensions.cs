using System;
using ApacheTech.Common.Extensions.System;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

namespace ApacheTech.Common.Extensions.Functional.Monadic.Validation
{
    // For reference: https://www.youtube.com/watch?v=v7WLC5As6g4

    /// <summary>
    ///     Extension methods for monadic functions.
    /// </summary>
    public static class MonadicValidationExtensions
    {
        /// <summary>
        ///     Performs an explicit conversion from <typeparamref name="T"/> to <see cref="Either{T}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static Either<T> ToEither<T>(this T value)
        {
            return new GoodPath<T>(value);
        }

        /// <summary>
        ///     Binds a function to a monad of type <see cref="Either{TFromType}"/>, mutating the return type dependent on the result.
        /// </summary>
        /// <typeparam name="TFromType">The type of the value wrapped within the input monad.</typeparam>
        /// <typeparam name="TToType">The type of the value to wrap within the output monad.</typeparam>
        /// <param name="this">The monad to bind the function to.</param>
        /// <param name="f">The function to bind.</param>
        /// <returns>Returns a <see cref="GoodPath{TToType}"/>, if the execution of the function succeeds; otherwise returns a <see cref="BadPath{TToType}"/>.</returns>
        public static Either<TToType> Bind<TFromType, TToType>(this Either<TFromType> @this,
            Func<TFromType, TToType> f)
        {
            switch (@this)
            {
                case GoodPath<TFromType> goodPath when !goodPath.Value.IsDefaultValue():
                    try
                    {
                        return f(goodPath).ToEither();
                    }
                    catch (Exception e)
                    {
                        return new BadPath<TToType>(e);
                    }
                case BadPath<TToType> badPath:
                    return new BadPath<TToType>(badPath.Exception);
                default:
                    return new BadPath<TToType>(new ArgumentException("Default value."));
            }
        }
    }
}