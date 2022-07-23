using System.Globalization;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace ApacheTech.Common.Extensions.System
{
    /// <summary>
    ///     Provides extension methods for numeric types.
    /// </summary>
    public static class NumericalExtensions
    {
        /// <summary>
        ///     Determines whether a value with within a given range.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="min">The inclusive minimum value.</param>
        /// <param name="max">The inclusive maximum value.</param>
        /// <returns>
        ///   <c>true</c> if the value is within range of the minimum and maximum values; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWithinRange(this int val, int min, int max)
        {
            return val >= min && val <= max;
        }

        /// <summary>
        ///     Formats the number in text form, with numerical group separators.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="locale">The locale to use to determine separator amounts, and deliminators.</param>
        /// <returns>Returns a string representation of the number, in human readable form.</returns>
        public static string FormatLargeNumber(this int number, string locale = "en")
        {
            return string.Format(CultureInfo.GetCultureInfo(locale), "{0:#,##0.##}", number);
        }

        /// <summary>
        ///     Formats the number in text form, with numerical group separators.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="locale">The locale to use to determine separator amounts, and deliminators.</param>
        /// <returns>Returns a string representation of the number, in human readable form.</returns>
        public static string FormatLargeNumber(this long number, string locale = "en")
        {
            return string.Format(CultureInfo.GetCultureInfo(locale), "{0:#,##0.##}", number);
        }

        /// <summary>
        ///     Formats the number in text form, with numerical group separators.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="locale">The locale to use to determine separator amounts, and deliminators.</param>
        /// <returns>Returns a string representation of the number, in human readable form.</returns>
        public static string FormatLargeNumber(this float number, string locale = "en")
        {
            return string.Format(CultureInfo.GetCultureInfo(locale), "{0:#,##0.##}", number);
        }

        /// <summary>
        ///     Formats the number in text form, with numerical group separators.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="locale">The locale to use to determine separator amounts, and deliminators.</param>
        /// <returns>Returns a string representation of the number, in human readable form.</returns>
        public static string FormatLargeNumber(this double number, string locale = "en")
        {
            return string.Format(CultureInfo.GetCultureInfo(locale), "{0:#,##0.##}", number);
        }
    }
}
