#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ApacheTech.Common.Extensions.DotNet;

/// <summary>
///     Provides extension methods for collections.
/// </summary>
public static partial class CollectionExtensions
{
    /// <summary>
    ///     Returns the first element of a sequence, or null if the sequence contains no elements.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
    /// <returns><see langword="null" /> if <paramref name="source" /> is empty; otherwise, the first element in
    /// <paramref name="source" />.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
    public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source) where TSource : class
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
    public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource?, bool> predicate)
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
    ///     Adds an item to the <see cref="ConcurrentBag{TItem}" />, if it not already present in the collection.
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
    
    /// <summary>
    ///     Chooses a value, at random, from a list of values.
    /// </summary>
    /// <typeparam name="T">The type of values within the collection.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <returns>A value, of type <typeparamref name="T"/>, selected at random from the <paramref name="collection"/>.</returns>
    public static T Random<T>(this IList<T> collection)
    {
        var index = System.Random.Shared.Next(0, collection.Count);
        return collection[index];
    }

    /// <summary>
    ///     Adds a range of entries into a sorted dictionary.
    ///     Includes a work around the fact that keys within Sorted Dictionaries cannot normally be overwritten.
    /// </summary>
    /// <param name="dict">The dictionary to save the syntax list to.</param>
    /// <param name="list">The list of syntax options to add.</param>
    /// <param name="predicate">The data member to search by.</param>
    public static void AddOrUpdateRange<TKey, TValue>(this SortedDictionary<TKey, TValue> dict, IEnumerable<TValue> list, Func<TValue, IEnumerable<TKey>> predicate)
        where TKey : notnull
    {
        var validRecords = list.SelectMany(predicate, (v, k) => new KeyValuePair<TKey, TValue>(k, v));
        foreach (var record in validRecords)
        {
            dict.AddOrUpdate(record);
        }
    }

    /// <summary>
    ///     Adds a range of entries into a sorted dictionary.
    ///     Includes a work around the fact that keys within Sorted Dictionaries cannot normally be overwritten.
    /// </summary>
    /// <param name="dict">The dictionary to save the syntax list to.</param>
    /// <param name="list">The list of syntax options to add.</param>
    /// <param name="predicate">The data member to search by.</param>
    public static void AddOrUpdateRange<TKey, TValue>(this SortedDictionary<TKey, TValue> dict, IEnumerable<TValue> list, Func<TValue, TKey> predicate)
         where TKey : notnull
    {
        foreach (var record in list)
        {
            dict.AddOrUpdate(predicate(record), record);
        }
    }

    /// <summary>
    ///     Adds an entry into a sorted dictionary.
    ///     Includes a work around the fact that keys within Sorted Dictionaries cannot normally be overwritten.
    /// </summary>
    /// <param name="dict">The dictionary to save the syntax list to.</param>
    /// <param name="record">The key-value pair to add.</param>
    public static void AddOrUpdate<TKey, TValue>(this SortedDictionary<TKey, TValue> dict, KeyValuePair<TKey, TValue> record)
        where TKey : notnull
    {
        dict.AddOrUpdate(record.Key, record.Value);
    }

    /// <summary>
    ///     Adds an entry into a sorted dictionary.
    ///     Includes a work around the fact that keys within Sorted Dictionaries cannot normally be overwritten.
    /// </summary>
    /// <param name="dict">The dictionary to save the syntax list to.</param>
    /// <param name="key">The key of the value to add.</param>
    /// <param name="value">The value to add.</param>
    public static void AddOrUpdate<TKey, TValue>(this SortedDictionary<TKey, TValue> dict, TKey key, TValue value)
        where TKey : notnull
    {
        try
        {
            dict.Add(key, value);
        }
        catch (ArgumentException)
        {
            dict.Remove(key);
            dict.Add(key, value);
        }
    }

    /// <summary>
    ///     Adds an item to the <see cref="IDictionary{TKey,TValue}" />, if it not already present in the collection.
    /// </summary>
    /// <typeparam name="TKey">The type of the elements of <paramref name="key" />.</typeparam>
    /// <typeparam name="TValue">The type of the elements of <paramref name="value" />.</typeparam>
    /// <param name="collection">The collection to add the item to.</param>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value to add.</param>
    /// <returns><c>true</c> if the item was added to collection, <c>false</c> otherwise.</returns>
    public static bool AddIfNotPresent<TKey, TValue>(this IDictionary<TKey, TValue> collection, TKey key, TValue value)
    {
        var contains = collection.ContainsKey(key);
        if (!contains) collection.Add(key, value);
        return !contains;
    }

    /// <summary>
    ///     Adds an item to the <see cref="IDictionary{TKey,TValue}" />, if it not already present in the collection.
    /// </summary>
    /// <typeparam name="TKey">The type of the elements of <paramref name="key" />.</typeparam>
    /// <typeparam name="TValue">The type of the elements of <paramref name="value" />.</typeparam>
    /// <param name="collection">The collection to add the item to.</param>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value to add.</param>
    /// <returns><c>true</c> if the item was added to collection, <c>false</c> otherwise.</returns>
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> collection, TKey key, TValue value)
    {
        if (!collection.ContainsKey(key))
        {
            collection.Add(key, value);
            return;
        }
        collection[key] = value;
    }

    /// <summary>
    ///     Adds an item to the <see cref="IEnumerable{TValue}" />, if it should exist within the collection, and doesn't already.
    ///     If the value shouldn't exist, and it does, it will be removed from the collection.
    /// </summary>
    /// <typeparam name="TValue">The type of the elements of <paramref name="value" />.</typeparam>
    /// <param name="collection">The collection to add the item to.</param>
    /// <param name="value">The value to add.</param>
    /// <param name="shouldExist">Whether or not the value should exist within the collection.</param>
    /// <returns><c>true</c> if the item was added to collection, <c>false</c> otherwise.</returns>
    public static void EnsureExistence<TValue>(this List<TValue> collection, TValue value, bool shouldExist) where TValue : IEquatable<TValue>
    {
        collection.RemoveAll(p => p.Equals(value));
        if (shouldExist) collection.Add(value);
    }

    /// <summary>
    ///     Returns the first element of the sequence that satisfies a condition or null if no such element is found.
    /// </summary>
    /// <typeparam name="TValue">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">An <see cref="T:System.Collections.Generic.IDictionary`2" /> to return an element from.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns><see langword="null" /> if <paramref name="source" /> is empty or if no element passes the test specified by
    /// <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test
    /// specified by <paramref name="predicate" />.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
    public static TValue? FirstOrNull<TValue>(this IDictionary<string, TValue> source, Func<KeyValuePair<string, TValue>, bool> predicate)
        where TValue : class
    {
        try
        {
            return source.First(predicate).Value;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    ///     Determines whether the given collection contains any value within a given list of values.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="keys">The list of values.</param>
    /// <returns>
    ///     <c>true</c> if the specified collection contains any of the range of values; otherwise, <c>false</c>.
    /// </returns>
    public static bool ContainsAny<TValue>(this IEnumerable<TValue> source, IEnumerable<TValue> keys)
        where TValue : class
    {
        return keys.Any(source.Contains);
    }

    /// <summary>
    ///     Determines whether the given dictionary contains any key within a given list of keys.
    /// </summary>
    /// <typeparam name="TKey">The type of the key held in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values held in the dictionary.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="keys">The list of keys.</param>
    /// <returns>
    ///     <c>true</c> if the specified dictionary contains any of the range of keys; otherwise, <c>false</c>.
    /// </returns>
    public static bool ContainsAnyKey<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TKey> keys)
        where TValue : class
    {
        return keys.Any(source.Keys.Contains);
    }

    /// <summary>
    ///     Purges the specified dictionary. Disposes all instances held as values, and then clears the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the key held in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values held in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to purge.</param>
    public static void PurgeValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) 
        where TValue : IDisposable
    {
        dictionary.Values.DisposeAll();
        dictionary.Clear();
    }

    /// <summary>
    ///     Purges the specified dictionary. Disposes all instances held as keys, and then clears the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the key held in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values held in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to purge.</param>
    public static void PurgeKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) 
        where TKey : IDisposable
    {
        dictionary.Keys.DisposeAll();
        dictionary.Clear();
    }

    /// <summary>
    ///     Purges the specified dictionary. Disposes all instances held as keys, and then clears the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the key held in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values held in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to purge.</param>
    public static void PurgeKeysAndValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) 
        where TKey : IDisposable 
        where TValue : IDisposable
    {
        dictionary.Keys.DisposeAll();
        dictionary.Clear();
    }

    /// <summary>
    ///     Purges the specified list, by disposing all elements, then clearing the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    public static void Purge<T>(this ICollection<T> list) where T : IDisposable
    {
        list.DisposeAll();
        list.Clear();
    }

    /// <summary>
    ///     Disposes all elements of a list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    public static void DisposeAll<T>(this ICollection<T> list) where T : IDisposable
    {
        foreach (var item in list)
        {
            item?.Dispose();
        }
    }

    /// <summary>
    ///     Removes an item to the <see cref="ICollection{TItem}" />, if it is already present in the collection.
    /// </summary>
    /// <returns><c>true</c> if the item was removed from the collection, <c>false</c> otherwise.</returns>
    public static bool RemoveIfPresent<TKey, TValue>(this IDictionary<TKey, TValue> collection, TKey key)
    {
        var contains = collection.ContainsKey(key);
        if (contains) collection.Remove(key);
        return contains;
    }

    /// <summary>
    ///     Removes an item to the <see cref="ICollection{TItem}" />, if it is already present in the collection.
    /// </summary>
    /// <typeparam name="TItem">The type of the elements of <paramref name="item" />.</typeparam>
    /// <param name="collection">The collection to remove the item from.</param>
    /// <param name="item">The item to remove.</param>
    /// <returns><c>true</c> if the item was removed from the collection, <c>false</c> otherwise.</returns>
    public static bool RemoveIfPresent<TItem>(this ICollection<TItem> collection, TItem item)
    {
        var contains = collection.Contains(item);
        if (contains) collection.Remove(item);
        return contains;
    }
}