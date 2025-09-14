using System;
using System.Reflection;
using ApacheTech.Common.Extensions.DotNet;


// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace ApacheTech.Common.Extensions.Reflection;

/// <summary>
///     Provides extension methods for reflected properties.
/// </summary>
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
        object? value = null;

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

        if (!hasDefaultValue) goto Return;

        if (tryToGetDefaultValue)
        {
            value = parameter.DefaultValue!;
        }

        // Workaround for https://github.com/dotnet/corefx/issues/11797
        if (value is null && parameter.ParameterType.IsValueType)
        {
            value = Activator.CreateInstance(parameter.ParameterType);
        }

        Return:
        defaultValue = value!;
        return value is not null;
    }

    /// <summary>
    ///     Tries the get default value of a property, via its <see cref="PropertyInfo"/>.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns><c>true</c> if the default value has been set, <c>false</c> otherwise.</returns>
    public static bool TryGetDefaultValue<T>(this ParameterInfo parameter, out T defaultValue)
    {
        bool hasDefaultValue;
        var tryToGetDefaultValue = true;
        T? value = default;

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

        if (!hasDefaultValue) goto Return;

        if (tryToGetDefaultValue)
        {
            value = parameter.DefaultValue!.To<T>();
        }

        // Workaround for https://github.com/dotnet/corefx/issues/11797
        if (value is null && parameter.ParameterType.IsValueType)
        {
            var obj = Activator.CreateInstance(parameter.ParameterType);
            if (obj is not null)
                value = obj.To<T>();
        }

        Return:
        defaultValue = value!;
        return value is not null;
    }

    #endregion
}