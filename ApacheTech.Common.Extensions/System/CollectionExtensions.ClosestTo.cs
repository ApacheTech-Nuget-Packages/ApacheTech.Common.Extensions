#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApacheTech.Common.Extensions.System
{
    /// <summary>
    ///     Provides extension methods for collections.
    /// </summary>
    public static partial class CollectionExtensions
    {
        /// <summary>
        ///     Gets the closest value to a given number, within a given set of numbers.
        /// </summary>
        /// <param name="list">The set of numbers to clamp to.</param>
        /// <param name="number">The number to find.</param>
        /// <returns>System.Int32.</returns>
        public static int ClosestTo(this IEnumerable<int> list, int number)
        {
            return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
        }

        /// <summary>
        ///     Gets the closest value to a given number, within a given set of numbers.
        /// </summary>
        /// <param name="list">The set of numbers to clamp to.</param>
        /// <param name="number">The number to find.</param>
        /// <returns>System.Double.</returns>
        public static byte ClosestTo(this IEnumerable<byte> list, byte number)
        {
            return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
        }

        /// <summary>
        ///     Gets the closest value to a given number, within a given set of numbers.
        /// </summary>
        /// <param name="list">The set of numbers to clamp to.</param>
        /// <param name="number">The number to find.</param>
        /// <returns>System.Double.</returns>
        public static short ClosestTo(this IEnumerable<short> list, short number)
        {
            return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
        }

        /// <summary>
        ///     Gets the closest value to a given number, within a given set of numbers.
        /// </summary>
        /// <param name="list">The set of numbers to clamp to.</param>
        /// <param name="number">The number to find.</param>
        /// <returns>System.Double.</returns>
        public static long ClosestTo(this IEnumerable<long> list, long number)
        {
            return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
        }

        /// <summary>
        ///     Gets the closest value to a given number, within a given set of numbers.
        /// </summary>
        /// <param name="list">The set of numbers to clamp to.</param>
        /// <param name="number">The number to find.</param>
        /// <returns>System.Single.</returns>
        public static float ClosestTo(this IEnumerable<float> list, float number)
        {
            return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
        }

        /// <summary>
        ///     Gets the closest value to a given number, within a given set of numbers.
        /// </summary>
        /// <param name="list">The set of numbers to clamp to.</param>
        /// <param name="number">The number to find.</param>
        /// <returns>System.Double.</returns>
        public static double ClosestTo(this IEnumerable<double> list, double number)
        {
            return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
        }

        /// <summary>
        ///     Gets the closest value to a given number, within a given set of numbers.
        /// </summary>
        /// <param name="list">The set of numbers to clamp to.</param>
        /// <param name="number">The number to find.</param>
        /// <returns>System.Double.</returns>
        public static decimal ClosestTo(this IEnumerable<decimal> list, decimal number)
        {
            return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
        }
    }
}