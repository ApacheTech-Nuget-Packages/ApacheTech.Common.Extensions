using System;
using System.Linq;
using System.Reflection;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.Reflection
{
    public static class MemberReflectionExtensions
    {
        #region Members

        public static bool HasCustomAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttributes().OfType<T>().Any();
        }

        #endregion
    }
}