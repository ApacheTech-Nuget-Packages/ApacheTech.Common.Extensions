using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.System
{
    /// <summary>
    ///     Provides extension methods for assemblies.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        ///     Nullifies any orphaned static members within a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static void NullifyOrphanedStaticMembers(this Assembly assembly)
        {
            if (assembly is null) return;
            foreach (var type in assembly.GetTypes())
            {
                foreach (var propertyInfo in type.GetProperties(BindingFlags.Static | BindingFlags.SetProperty))
                {
                    if (!propertyInfo.CanWrite) continue;
                    propertyInfo.SetMethod.Invoke(null, null);
                }

                foreach (var fieldInfo in type.GetFields(BindingFlags.Static | BindingFlags.SetField))
                {
                    if (fieldInfo.Attributes == FieldAttributes.InitOnly) continue;
                    fieldInfo.SetValue(null, null);
                }
            }
        }

        /// <summary>
        ///     Gets a collection of all types implementing a specified open generic interface, within a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan.</param>
        /// <param name="openGenericType">Type of the open generic.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(this Assembly assembly, Type openGenericType)
        {
            return assembly.GetTypes()
                .SelectMany(types => types.GetInterfaces(), (types, interfaces) => new { Types = types, Interfaces = interfaces })
                .Select(t => new { TypeWrapper = t, t.Types.BaseType })
                .Where(t =>
                    t.BaseType is { IsGenericType: true } &&
                    openGenericType.IsAssignableFrom(t.BaseType.GetGenericTypeDefinition()) ||
                    t.TypeWrapper.Interfaces.IsGenericType &&
                    openGenericType.IsAssignableFrom(t.TypeWrapper.Interfaces.GetGenericTypeDefinition()))
                .Select(t => t.TypeWrapper.Types);
        }
    }
}