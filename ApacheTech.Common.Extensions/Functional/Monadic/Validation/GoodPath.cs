namespace ApacheTech.Common.Extensions.Functional.Monadic.Validation
{
    /// <summary>
    ///     A monadic wrapper for a function type that has succeeded thus far, during the execution of its function tree.
    /// </summary>
    /// <typeparam name="T">The type of value to wrap.</typeparam>
    /// <seealso cref="Either{T}" />
    public sealed class GoodPath<T> : Either<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GoodPath{T}"/> class.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public GoodPath(T value) => Value = value;

        /// <summary>
        ///     Gets the raw wrapped value.
        /// </summary>
        /// <value>
        ///     The value wrapped within this monad.
        /// </value>
        public override T Value { get; }
    }
}