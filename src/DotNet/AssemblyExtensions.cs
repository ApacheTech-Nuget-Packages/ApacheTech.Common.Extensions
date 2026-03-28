using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApacheTech.Common.Extensions.DotNet;

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
        => assembly?.GetTypes().InvokeAll(NullifyStaticClassMembers);

    private static void NullifyStaticClassMembers(Type type)
    {
        type.GetProperties(BindingFlags.Static | BindingFlags.SetProperty).InvokeAll(NullifyStaticProperty);
        type.GetFields(BindingFlags.Static | BindingFlags.SetField).InvokeAll(NullifyStaticField);
    }

    private static void NullifyStaticField(FieldInfo fieldInfo)
    {
        if (fieldInfo.Attributes == FieldAttributes.InitOnly) return;
        if (fieldInfo.FieldType is IDisposable)
        {
            var disposable = (IDisposable?)fieldInfo.GetValue(null);
            disposable?.Dispose();
        }
        fieldInfo.SetValue(null, null);
    }

    private static void NullifyStaticProperty(PropertyInfo propertyInfo)
    {
        if (!propertyInfo.CanWrite) return;
        if (propertyInfo.PropertyType is IDisposable)
        {
            var disposable = (IDisposable?)propertyInfo.GetMethod?.Invoke(null, null);
            disposable?.Dispose();
        }
        propertyInfo.SetMethod?.Invoke(null, null);
    }

    /// <summary>
    ///     Gets a collection of all types implementing a specified open generic interface, within a given assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="openGenericType">Type of the open generic.</param>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(this Assembly assembly, Type openGenericType) =>
        assembly.GetTypes()
            .SelectMany(types => types.GetInterfaces(), (types, interfaces) => new { Types = types, Interfaces = interfaces })
            .Select(t => new { TypeWrapper = t, t.Types.BaseType })
            .Where(t =>
                t.BaseType is { IsGenericType: true } &&
                openGenericType.IsAssignableFrom(t.BaseType.GetGenericTypeDefinition()) ||
                t.TypeWrapper.Interfaces.IsGenericType &&
                openGenericType.IsAssignableFrom(t.TypeWrapper.Interfaces.GetGenericTypeDefinition()))
            .Select(t => t.TypeWrapper.Types);
}