using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.System
{
    public static class StringExtensions
    {
        #region Strings

        /// <summary>
        ///     Returns a default string, if a specified string is <see langword="null" />,
        ///     empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="defaultString">The default string.</param>
        /// <returns>If the input string,<paramref name="str"/>, is null, empty, or whitespace, returns <paramref name="defaultString"/>, otherwise returns <paramref name="str"/>.</returns>
        public static string IfNullOrWhitespace(this string str, string defaultString)
        {
            return string.IsNullOrWhiteSpace(str) ? defaultString : str;
        }

        /// <summary>
        ///     Returns a default string, if a specified string is <see langword="null" />, or empty.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="defaultString">The default string.</param>
        /// <returns>If the input string,<paramref name="str"/>, is null or empty, returns <paramref name="defaultString"/>, otherwise returns <paramref name="str"/>.</returns>
        public static string IfNullOrEmpty(this string str, string defaultString)
        {
            return string.IsNullOrEmpty(str) ? defaultString : str;
        }

        /// <summary>
        ///     Determines whether the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="values">The list of strings to compare.</param>
        /// <returns>true if <paramref name="values">value</paramref> matches the beginning of this string; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="values">value</paramref> is null.</exception>
        public static bool StartsWithAny(this string str, IEnumerable<string> values)
        {
            return values.Any(str.StartsWith);
        }

        /// <summary>
        ///     Determines whether the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="values">The list of strings to compare.</param>
        /// <returns>true if <paramref name="values">value</paramref> matches the beginning of this string; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="values">value</paramref> is null.</exception>
        public static bool StartsWithAny(this string str, params string[] values)
        {
            return values.Any(str.StartsWith);
        }

        /// <summary>
        ///     Determines whether the string instance contains any of the specified strings.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="values">The list of strings to compare.</param>
        /// <returns>true if <paramref name="values">value</paramref> matches the beginning of this string; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="values">value</paramref> is null.</exception>
        public static bool ContainsAny(this string str, IEnumerable<string> values)
        {
            return values.Any(str.Contains);
        }

        /// <summary>
        ///     Determines whether the string instance contains any of the specified strings.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="values">The list of strings to compare.</param>
        /// <returns>true if <paramref name="values">value</paramref> matches the beginning of this string; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="values">value</paramref> is null.</exception>
        public static bool ContainsAny(this string str, params string[] values)
        {
            return values.Any(str.Contains);
        }

        /// <summary>
        ///     Converts a number to its ordinal string representation.
        /// </summary>
        /// <param name="num">The number to convert.</param>
        /// <returns>A string that represents the number, in ordinal form.</returns>
        public static string ToOrdinal(this int num)
        {
            return (num % 100) switch
            {
                11 => num.ToString("#,###0") + "th",
                12 => num.ToString("#,###0") + "th",
                13 => num.ToString("#,###0") + "th",
                _ => (num % 10) switch
                {
                    1 => num.ToString("#,###0") + "st",
                    2 => num.ToString("#,###0") + "nd",
                    3 => num.ToString("#,###0") + "rd",
                    _ => num.ToString("#,###0") + "th"
                }
            };
        }

        #endregion
    }
}