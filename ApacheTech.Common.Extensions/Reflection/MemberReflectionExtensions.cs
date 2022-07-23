using System;
using System.Linq;
using System.Reflection;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.Reflection
{
    /// <summary>
    ///     Extension methods for use with class object members.
    /// </summary>
    public static class MemberReflectionExtensions
    {
        #region Members

        /// <summary>
        ///     Determines whether the specified member is decorated with the given custom attribute.
        /// </summary>
        /// <typeparam name="T">The type of custom Attribute to check for.</typeparam>
        /// <param name="member">The member to check.</param>
        /// <returns><c>true</c> if the specified member is decorated with the given custom attribute; otherwise, <c>false</c>.</returns>
        public static bool HasCustomAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttributes().OfType<T>().Any();
        }

        #endregion
    }
}