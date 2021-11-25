using System;
using System.Reflection;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.Reflection
{
    public static class PropertyReflectionExtensions
    {
        #region Properties

        /// <summary>
        ///     Tries the get default value of a property, via its <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><c>true</c> if the default value has been set, <c>false</c> otherwise.</returns>
        public static bool TryGetDefaultValue(this ParameterInfo parameter, out object defaultValue)
        {
            bool hasDefaultValue;
            var tryToGetDefaultValue = true;
            defaultValue = null;

            try
            {
                hasDefaultValue = parameter.HasDefaultValue;
            }
            catch (FormatException) when (parameter.ParameterType == typeof(DateTime))
            {
                // Workaround for https://github.com/dotnet/corefx/issues/12338
                // If HasDefaultValue throws FormatException for DateTime
                // we expect it to have default value
                hasDefaultValue = true;
                tryToGetDefaultValue = false;
            }

            if (!hasDefaultValue) return false;

            if (tryToGetDefaultValue)
            {
                defaultValue = parameter.DefaultValue;
            }

            // Workaround for https://github.com/dotnet/corefx/issues/11797
            if (defaultValue == null && parameter.ParameterType.IsValueType)
            {
                defaultValue = Activator.CreateInstance(parameter.ParameterType);
            }
            return true;
        }

        #endregion
    }
}