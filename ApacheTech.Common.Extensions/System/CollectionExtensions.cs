using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.System
{
    public static class CollectionExtensions
    {
        #region Collections

        /// <summary>
        ///     Returns the first element of a sequence, or null if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
        /// <returns><see langword="null" /> if <paramref name="source" /> is empty; otherwise, the first element in
        /// <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
        public static TSource FirstOrNull<TSource>(this IEnumerable<TSource> source) where TSource : class
        {
            return source.DefaultIfEmpty(null).FirstOrDefault();
        }

        /// <summary>
        ///     Returns the first element of the sequence that satisfies a condition or null if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns><see langword="null" /> if <paramref name="source" /> is empty or if no element passes the test specified by
        /// <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test
        /// specified by <paramref name="predicate" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
        public static TSource FirstOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
            where TSource : class
        {
            return source.DefaultIfEmpty(null).FirstOrDefault(predicate);
        }

        /// <summary>
        ///     Adds an item to the <see cref="ICollection{TItem}" />, if it not already present in the collection.
        /// </summary>
        /// <typeparam name="TItem">The type of the elements of <paramref name="item" />.</typeparam>
        /// <param name="collection">The collection to add the item to.</param>
        /// <param name="item">The item to add.</param>
        /// <returns><c>true</c> if the item was added to collection, <c>false</c> otherwise.</returns>
        public static bool AddIfNotPresent<TItem>(this ICollection<TItem> collection, TItem item)
        {
            var contains = collection.Contains(item);
            if (!contains) collection.Add(item);
            return !contains;
        }

        /// <summary>
        ///     Adds an item to the <see cref="ICollection{TItem}" />, if it not already present in the collection.
        /// </summary>
        /// <typeparam name="TItem">The type of the elements of <paramref name="item" />.</typeparam>
        /// <param name="collection">The collection to add the item to.</param>
        /// <param name="item">The item to add.</param>
        /// <returns><c>true</c> if the item was added to collection, <c>false</c> otherwise.</returns>
        public static bool AddIfNotPresent<TItem>(this ConcurrentBag<TItem> collection, TItem item)
        {
            var contains = collection.Contains(item);
            if (!contains) collection.Add(item);
            return !contains;
        }

        #endregion
    }
}