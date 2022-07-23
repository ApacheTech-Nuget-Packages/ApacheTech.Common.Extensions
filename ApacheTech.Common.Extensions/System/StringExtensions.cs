using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.System
{
    /// <summary>
    ///     Provides extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        #region Strings

        /// <summary>
        ///     Returns a default string, if a specified string is <see langword="null" />,empty, or consists only of white-space characters.
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

        /// <summary>
        ///     Determines whether a string contains any letters.
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns><c>true</c> is the string contains letters [Aa..Zz]; otherwise <c>false</c>.</returns>
        public static bool ContainsLetters(this string input)
        {
            return input?.Any(char.IsLetter) ?? false;
        }

        /// <summary>
        ///     Determines whether a string contains any letters.
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns><c>true</c> is the string contains letters [Aa..Zz]; otherwise <c>false</c>.</returns>
        public static bool OnlyContainsLetters(this string input)
        {
            return input?.All(char.IsLetter) ?? false;
        }

        /// <summary>
        ///     Determines whether a string contains any numbers.
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns><c>true</c> is the string contains numbers [0..9]; otherwise <c>false</c>.</returns>
        public static bool ContainsNumbers(this string input)
        {
            return input?.Any(char.IsNumber) ?? false;
        }

        /// <summary>
        ///     Determines whether a string contains any numbers.
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns><c>true</c> is the string contains numbers [0..9]; otherwise <c>false</c>.</returns>
        public static bool OnlyContainsNumbers(this string input)
        {
            return input?.All(char.IsNumber) ?? false;
        }

        /// <summary>
        ///    Strips out non-numeric characters in string, returning only digits
        ///    ref.: https://stackoverflow.com/questions/3977497/stripping-out-non-numeric-characters-in-string
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>the input string numeric part: for example, if input is "XYZ1234A5U6" it will return "123456"</returns>
        public static string GetNumbers(this string input)
        {
            return new string(input?.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        ///     Strips out numeric and special characters in string, returning only letters
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>the letters contained within the input string: for example, if input is "XYZ1234A5U6~()" it will return "XYZAU"</returns>
        public static string GetLetters(this string input)
        {
            return new string(input?.Where(char.IsLetter).ToArray());
        }

        /// <summary>
        ///     Strips out any non-numeric/non-digit character in string, returning only letters and numbers
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>the letters contained within the input string: for example, if input is "XYZ1234A5U6~()" it will return "XYZ1234A5U6"</returns>
        public static string GetLettersAndNumbers(this string input)
        {
            return new string(input?.Where(char.IsLetterOrDigit).ToArray());
        }

        /// <summary>
        ///     Splits a string into sentence form, from a pascal case word.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Returns a <see cref="string"/>, where each word of the input string is separated by a space: for example, if input is "SplitPascalCase", it will return "Split Pascal Case".</returns>
        public static string SplitPascalCase(this string input)
        {
            var words = Regex
                .Matches(input, @"[A-Z]+(?=[A-Z][a-z]+)|\d|[A-Z][a-z]+")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();
            return string.Join(" ", words);
        }

        /// <summary>
        ///     Appends a suffix to a string, if the suffix does not already appear at the end of the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns>A new <see cref="string"/> with the assured suffix.</returns>
        public static string EnsureEndsWith(this string text, string suffix)
        {
            return text.EndsWith(suffix) ? text : $"{text}{suffix}";
        }

        /// <summary>
        ///     Prepends a prefix to a string, if the prefix does not already appear at the start of the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>A new <see cref="string"/> with the assured prefix.</returns>
        public static string EnsureStartsWith(this string text, string prefix)
        {
            return text.StartsWith(prefix) ? text : $"{prefix}{text}";
        }

        /// <summary>
        ///     Pluralises the specified string, based on a input value.
        /// </summary>
        /// <param name="singular">The singular.</param>
        /// <param name="value">The value.</param>
        /// <param name="plural">The plural.</param>
        /// <returns>Either the <paramref name="singular"/> value, or <paramref name="plural"/> value, based on whether <paramref name="value"/> is 1, or otherwise.</returns>
        public static string Pluralise(this string singular, int value, string plural)
        {
            return Math.Abs(value) == 1 ? singular : plural;
        }

        /// <summary>
        ///     Determines whether a string starts with any strings within a given set.
        /// </summary>
        /// <param name="this">The string to test.</param>
        /// <param name="subStrings">The set of sub-strings to search for.</param>
        /// <returns><c>true</c>, if the string does start with any of the sub-strings; otherwise, <c>false</c></returns>
        public static bool StartsWithAnyOf(this string @this, IEnumerable<string> subStrings) => subStrings.Any(@this.StartsWith);

        /// <summary>
        ///     Determines whether a string starts with any strings within a given set.
        /// </summary>
        /// <param name="this">The string to test.</param>
        /// <param name="subStrings">The set of sub-strings to search for.</param>
        /// <returns><c>false</c>, if the string does start with any of the sub-strings; otherwise, <c>true</c></returns>
        public static bool StartsWithNoneOf(this string @this, IEnumerable<string> subStrings) => !@this.StartsWithAnyOf(subStrings);

        /// <summary>
        ///     Determines whether a string contains any strings within a given set.
        /// </summary>
        /// <param name="this">The string to test.</param>
        /// <param name="subStrings">The set of sub-strings to search for.</param>
        /// <returns><c>true</c>, if the string contains any of the sub-strings; otherwise, <c>false</c></returns>
        public static bool ContainsAnyOf(this string @this, IEnumerable<string> subStrings) => subStrings.Any(@this.Contains);

        /// <summary>
        ///     Determines whether a string contains any strings within a given set.
        /// </summary>
        /// <param name="this">The string to test.</param>
        /// <param name="subStrings">The set of sub-strings to search for.</param>
        /// <returns><c>false</c>, if the string contains any of the sub-strings; otherwise, <c>true</c></returns>
        public static bool ContainsNoneOf(this string @this, IEnumerable<string> subStrings) => !@this.ContainsAnyOf(subStrings);

        /// <summary>
        ///     Determines whether a string ends with any strings within a given set.
        /// </summary>
        /// <param name="this">The string to test.</param>
        /// <param name="subStrings">The set of sub-strings to search for.</param>
        /// <returns><c>true</c>, if the string does end with any of the sub-strings; otherwise, <c>false</c></returns>
        public static bool EndsWithAnyOf(this string @this, IEnumerable<string> subStrings) => subStrings.Any(@this.EndsWith);

        /// <summary>
        ///     Determines whether a string ends with any strings within a given set.
        /// </summary>
        /// <param name="this">The string to test.</param>
        /// <param name="subStrings">The set of sub-strings to search for.</param>
        /// <returns><c>false</c>, if the string does end with any of the sub-strings; otherwise, <c>true</c></returns>
        public static bool EndsWithNoneOf(this string @this, IEnumerable<string> subStrings) => !@this.EndsWithAnyOf(subStrings);
    }
}