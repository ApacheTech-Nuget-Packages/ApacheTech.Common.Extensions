using System;
using System.ComponentModel;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.System
{
    public static class EnumExtensions
    {
        #region Enums

        /// <summary>
        ///     Gets the description for the enum member, decorated with a DescriptionAttribute.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string representation of the description of the enum member, decorated with a DescriptionAttribute.</returns>
        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        ///     Gets the description for the enum member, decorated with a DescriptionAttribute.
        /// </summary>
        /// <typeparam name="T">The type of enum to evaluate.</typeparam>
        /// <param name="en">The enum to evaluate.</param>
        /// <returns>The number of entries within this enumeration.</returns>
        public static int Count<T>(this T en) where T : Enum
        {
            return en.GetType().GetEnumNames().Length;
        }

        #endregion
    }
}