using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.System
{
    public static class ObjectExtensions
    {
        #region Objects

        /// <summary>
        /// The extended data
        /// </summary>
        private static readonly ConditionalWeakTable<object, object> ExtendedData = new();

        /// <summary>
        ///     Gets a dynamic collection of properties associated with an object instance,
        ///     with a lifetime scoped to the lifetime of the object
        /// </summary>
        /// <param name="obj">The object the properties are associated with.</param>
        /// <returns>A dynamic collection of properties associated with an object instance.</returns>
        public static dynamic DynamicProperties(this object obj)
        {
            return ExtendedData.GetValue(obj, _ => new ExpandoObject());
        }

        /// <summary>
        ///     Dynamically casts the object instance to a specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to cast to.</typeparam>
        /// <param name="obj">The instance to cast.</param>
        /// <returns>An instance of Type <typeparamref name="T" />.</returns>
        public static T As<T>(this object obj)
        {
            return (Type.GetTypeCode(typeof(T)) is TypeCode.DateTime or TypeCode.DBNull or TypeCode.Empty)
                ? throw new ArgumentOutOfRangeException(nameof(T),
                    "Objects of this TypeCode cannot be cast to, dynamically.")
                : (T)obj;
        }

        /// <summary>
        ///     A close approximation to Visual Basic's "With" keyword, that allows
        ///     batch setting of Properties, without needing to initialise the object.
        /// </summary>
        /// <typeparam name="T">The type of object to work with.</typeparam>
        /// <param name="item">The item to work with.</param>
        /// <param name="work">The work to be done.</param>
        public static T With<T>(this T item, Action<T> work)
        {
            work(item);
            return item;
        }

        #endregion
    }
}