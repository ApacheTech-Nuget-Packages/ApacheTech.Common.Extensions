﻿using System;

namespace ApacheTech.Common.Extensions.Functional.Monadic.Validation
{
    /// <summary>
    ///     A monadic wrapper for a type that has failed, during the execution of its function tree.
    /// </summary>
    /// <typeparam name="T">The type of value to wrap.</typeparam>
    /// <seealso cref="Either{T}" />
    public class BadPath<T> : Either<T>
    {
        /// <summary>
        ///     Initialises a new instance of the <see cref="BadPath{T}"/> class.
        /// </summary>
        /// <param name="exception">The exception to throw when this stage of the function tree fails.</param>
        public BadPath(Exception exception) => Exception = exception;

        /// <summary>
        ///     Gets the exception to throw when this stage of the function tree fails.
        /// </summary>
        /// <value>
        ///     The exception to throw when this stage of the function tree fails.
        /// </value>
        public Exception Exception { get; }

        /// <summary>
        ///     Gets the default value of the wrapped type.
        /// </summary>
        /// <value>
        ///     The default value of the type wrapped within this monad.
        /// </value>
        public override T Value => default;
    }
}