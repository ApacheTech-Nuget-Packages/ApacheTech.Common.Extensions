using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.Reflection
{
    public static class AssemblyReflectionExtensions
    {
        #region Assemblies

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

        /// <summary>
        ///     Gets the derived types of a specified Attribute, within the assembly.
        /// </summary>
        /// <typeparam name="T">The type of class level attribute to scan for.</typeparam>
        /// <param name="_">The attribute to scan for.</param>
        /// <param name="assembly">The assembly to scan.</param>
        /// <returns>Returns an array of Types that are decorated with the specified class level attribute.</returns>
        public static IEnumerable<(Type Type, T Attribute)> GetDerivedTypes<T>(this T _, Assembly assembly) where T : Attribute
        {
            return assembly.GetTypesWithAttribute<T>();
        }

        /// <summary>   
        ///     Gets the derived types of a specified Attribute, within the assembly.
        /// </summary>
        /// <typeparam name="T">The type of class level attribute to scan for.</typeparam>
        /// <param name="assembly">The assembly to scan.</param>
        /// <returns>Returns an array of Types that are decorated with the specified class level attribute.</returns>
        public static IEnumerable<(Type Type, T Attribute)> GetTypesWithAttribute<T>(this Assembly assembly)
            where T : Attribute
        {
            var types = assembly.GetTypes()
                .Where(type => type.GetCustomAttributes(typeof(T), false).Length > 0);
            return types.Select(p => (p, (T)p.GetCustomAttribute(typeof(T), false)));
        }

        #endregion
    }
}