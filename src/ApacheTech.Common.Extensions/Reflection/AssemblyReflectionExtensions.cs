using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.Reflection;

/// <summary>
///     Provides extension methods for assemblies.
/// </summary>
public static class AssemblyReflectionExtensions
{
    /// <summary>
    ///     Scans an assembly for all instantiable classes of a specified type, and forms an array of instances.
    /// </summary>
    /// <typeparam name="T">The type of class to find.</typeparam>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="constructorArgs">The constructor arguments to pass to each instance.</param>
    /// <returns>An array of instantiated classes of a specified type.</returns>
    public static IEnumerable<T> InstantiateAllTypes<T>(this Assembly assembly, params object[] constructorArgs)
        where T : class, IComparable<T>
    {
        var objects = assembly
            .GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))
            .Select(type => (T)Activator.CreateInstance(type, constructorArgs))
            .ToList();
        objects.Sort();

        return objects;
    }
}