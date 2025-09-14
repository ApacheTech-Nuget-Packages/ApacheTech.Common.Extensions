# Common Extension Methods

A curated collection of small, focussed extension methods that enhance and simplify common .NET programming tasks. This library collects battle-tested helpers for collections, strings, enums, reflection, tasks, colours, numeric types and more, all packaged as concise extension methods to keep your code expressive and easy to read.

This repository is intended to be used as a lightweight dependency that can be referenced by other projects. The extensions are designed to be dependency-free, idiomatic, and compatible with modern .NET applications.

**Quick Highlights**
- Focussed helpers for Collections, Strings, Objects, Reflection, Tasks, Colours, Numerics and more.
- Small, well-documented methods intended to be drop-in improvements to standard library usage.
- Designed for clarity and maintainability rather than providing large surface-area APIs.

**Getting Started**
- Install the library via your preferred package manager or add the project to your solution.
- Add `using ApacheTech.Common.Extensions.DotNet;` or `using ApacheTech.Common.Extensions.Reflection;` to access extension methods from code.

## Support the Author

If you find this library useful, and you would like to show appreciation for the work I produce; please consider supporting me, and my work, using one of the methods below. Every single expression of support is most appreciated, and makes it easier to produce updates, and new features for my libraries, moving fowards. Thank you.

 - [Join my Patreon!](https://www.patreon.com/ApacheTechSolutions?fan_landing=true)
 - [Donate via PayPal](http://bitly.com/APGDonate)
 - [Buy Me a Coffee](https://www.buymeacoffee.com/Apache)
 - [Subscribe on Twitch.TV](https://twitch.tv/ApacheGamingUK)
 - [Subscribe on YouTube](https://youtube.com/c/ApacheGamingUK)
 - [Purchase from my Amazon Wishlist](http://amzn.eu/7qvKTFu)
 - [Visit my website!](https://apachegaming.net)

## Extension Methods

The following tables list all extension methods provided by this library, grouped by category. Each entry includes the method signature and a brief description of its functionality.

### Collections

Extension methods for `ICollection<T>`, `IList<T>`, `IDictionary<TKey,TValue>`, `ConcurrentBag<T>`, and `SortedDictionary<TKey,TValue>`.

|Command|Description|
|---|---|
| `myCollection.FirstOrNull<T>()` | Returns the first element of a sequence, or null if the sequence contains no elements. |
| `myCollection.FirstOrNull<T>(Func<T, bool> predicate)` | Returns the first element of the sequence that satisfies a condition or null if no such element is found. |
| `myCollection.AddIfNotPresent<T>(T item)` | Adds an item to the `ICollection`, if it not already present in the collection. |
| `myConcurrentBag.AddIfNotPresent<T>(T item)` | Adds an item to the `ConcurrentBag`, if it not already present in the collection. |
| `myList.Random<T>()` | Chooses a value at random from an `IList<T>`. |
| `mySortedDict.AddOrUpdateRange<TKey,TValue>(IEnumerable<TValue>, Func<TValue, IEnumerable<TKey>>)` | Adds or updates a range of entries in a `SortedDictionary` using a predicate that returns one or more keys. |
| `mySortedDict.AddOrUpdateRange<TKey,TValue>(IEnumerable<TValue>, Func<TValue, TKey>)` | Adds or updates a range of entries in a `SortedDictionary` using a single-key selector. |
| `mySortedDict.AddOrUpdate<TKey,TValue>(KeyValuePair<TKey,TValue>)` | Adds or updates a single key/value pair in a `SortedDictionary`. |
| `mySortedDict.AddOrUpdate<TKey,TValue>(TKey key, TValue value)` | Adds or updates a single entry in a `SortedDictionary` (overwrites if key exists). |
| `myDict.AddIfNotPresent<TKey,TValue>(TKey key, TValue value)` | Adds an item to the `IDictionary` if the key is not already present. |
| `myDict.AddOrUpdate<TKey,TValue>(TKey key, TValue value)` | Adds or updates an entry in an `IDictionary` (sets value if key exists). |
| `myList.EnsureExistence<TValue>(TValue value, bool shouldExist)` | Ensures a value exists (or does not) in a `List<T>`; removes duplicates and adds if required. |
| `myDict.FirstOrNull<TValue>(Func<KeyValuePair<string,TValue>, bool> predicate)` | Returns the first matching value from an `IDictionary<string,TValue>` or null if none found. |
| `myCollection.ContainsAny<T>(IEnumerable<T> keys)` | Determines whether the given collection contains any value within a given list of values. |
| `myDict.ContainsAnyKey<TKey,TValue>(IEnumerable<TKey> keys)` | Determines whether the given dictionary contains any of the specified keys. |
| `myDict.PurgeValues<TKey,TValue>()` | Disposes all values in an `IDictionary` and clears it (where values implement `IDisposable`). |
| `myDict.PurgeKeys<TKey,TValue>()` | Disposes all keys in an `IDictionary` and clears it (where keys implement `IDisposable`). |
| `myDict.PurgeKeysAndValues<TKey,TValue>()` | Disposes keys and values in an `IDictionary` and clears it (where both implement `IDisposable`). |
| `myList.Purge<T>()` | Disposes all elements of a collection and clears it (where elements implement `IDisposable`). |
| `myCollection.DisposeAll<T>()` | Disposes all elements of an `ICollection<T>` (where elements implement `IDisposable`). |
| `myDict.RemoveIfPresent<TKey,TValue>(TKey key)` | Removes a key from an `IDictionary` if present and returns whether removal occurred. |
| `myCollection.RemoveIfPresent<TItem>(TItem item)` | Removes an item from an `ICollection<T>` if present and returns whether removal occurred. |
| `myNumbers.ClosestTo(number)` | Gets the closest numeric value to `number` from an `IEnumerable<T>` (overloads for `int`, `byte`, `short`, `long`, `float`, `double`, `decimal`). |

### Enums

Extension methods for `Enum` types.

|Command|Description|
|---|---|
| `myEnum.GetDescription()` | Gets the description for the enum member, decorated with a `DescriptionAttribute`. |
| `myEnum.Count()` | Gets the number of values within this enumeration. |

### Strings

Extension methods for `string`.

|Command|Description|
|---|---|
| `myString.IfNullOrWhitespace(string defaultString)` | Returns a default string, if a specified string is `null`, empty, or consists only of white-space characters. |
| `myString.IfNullOrEmpty(string defaultString)` | Returns a default string, if a specified string is `null`, or empty. |
| `myString.ToOrdinalNumeral()` | Converts a number to its ordinal string representation (e.g. 1 -> "1st"). |
| `myString.ContainsLetters()` | Determines whether a string contains any letters. |
| `myString.OnlyContainsLetters()` | Determines whether a string contains only letters. |
| `myString.ContainsNumbers()` | Determines whether a string contains any numeric characters. |
| `myString.ContainsOnlyNumbers()` | Determines whether a string contains only numeric characters. |
| `myString.GetNumbers()` | Strips out non-numeric characters and returns only digits. |
| `myString.GetLetters()` | Strips out non-letter characters and returns only letters. |
| `myString.GetAlphaNumerics()` | Strips out special characters and returns only letters and numbers. |
| `myString.GetNonAlphaNumerics()` | Returns only the special (non-alphanumeric) characters from the string. |
| `myString.SplitPascalCase()` | Splits a PascalCase string into separate words. |
| `myString.EnsureEndsWith(string suffix)` | Appends a suffix if it does not already appear at the end. |
| `myString.EnsureStartsWith(string prefix)` | Prepends a prefix if it does not already appear at the start. |
| `myString.Pluralise(int value, string plural)` | Returns singular or plural form based on the value. |
| `myString.StartsWithAnyOf(IEnumerable<string> values)` | Determines whether the beginning of this string matches any of the specified strings. |
| `myString.StartsWithNoneOf(IEnumerable<string> values)` | Determines whether the string starts with none of the specified strings. |
| `myString.ContainsAnyOf(IEnumerable<string> values)` | Determines whether the string contains any of the specified strings. |
| `myString.ContainsNoneOf(IEnumerable<string> values)` | Determines whether the string contains none of the specified strings. |
| `myString.EndsWithAnyOf(IEnumerable<string> values)` | Determines whether the string ends with any of the specified strings. |
| `myString.EndsWithNoneOf(IEnumerable<string> values)` | Determines whether the string ends with none of the specified strings. |
| `myString.EmptyIfNull()` | Returns an empty string if the input is null. |
| `myString.ToLowerCaseInitials()` | Extracts initials from a PascalCase string and converts them to lowercase (e.g. "HelloWorld" -> "hw"). |
| `myObject.ToXml()` | Serialises an object to an indented XML string using `XmlSerializer`. |

### Objects

Extension methods for `object`.

|Command|Description|
|---|---|
| `myObject.DynamicProperties()` | Gets a dynamic collection of properties associated with an object instance, with a lifetime scoped to the lifetime of the object. |
| `myObject.To<T>()` | Dynamically casts the object instance to a specified type (throws for certain TypeCodes). |
| `myObject.As<T>()` | Safe-casts the object instance to a specified type, returning default if cast fails. |
| `myObject.With<T>(Action<T> work)` | Allows batch setting of properties via an action and returns the object (VB-like "With"). |
| `target.CopyFrom<TBase,TTo,TFrom>(TFrom source)` | Copies properties from a source object to a target object when both share a common base type (public getter/setter). |
| `target.CopyFrom<TDerived,TBase>(TBase source)` | Copies properties from a base-type source to a derived-type target (public getter/setter). |
| `source.MapTo<TFrom,TTo>()` | Creates a new instance of `TTo` and copies common public properties from the source. |

### Colours

Extension methods for `System.Drawing.Color`, `string`, and `double[]` related to colour manipulation and conversion.

|Command|Description|
|---|---|
| `myColour.UpdateColourChannel(ColourChannel channel, byte value)` | Updates a single colour channel (A/R/G/B) on a `System.Drawing.Color` and returns a new `Color`. |
| `myColour.ToNormalisedRgba()` | Returns RGBA channels normalised to 0.0-1.0 as a `double[]`. |
| `myColour.ToNormalisedArgb()` | Returns ARGB channels normalised to 0.0-1.0 as a `double[]`. |
| `myDoubleArray.ToNormalisedRgba()` | Normalises a `double[]` assumed to be RGBA by dividing each channel by 255. |
| `myDoubleArray.ToNormalisedArgb()` | Normalises a `double[]` assumed to be ARGB by dividing each channel by 255. |
| `myColour.ToRgbHexString()` | Converts a `Color` to an RGB hex string `#RRGGBB`. |
| `myColour.ToArgbHexString()` | Converts a `Color` to an ARGB hex string `#AARRGGBB`. |
| `myColour.ToRgbaHexString()` | Converts a `Color` to an RGBA hex string `#RRGGBBAA`. |
| `myColour.ToRgbString()` | Converts a `Color` to an `RGB(r, g, b)` string. |
| `myString.ToArgb()` | Parses a colour string (named or `#RRGGBB`) to an ARGB `int`. |
| `myString.ToColour()` | Parses a colour string to a `System.Drawing.Color`. |
| `myString.ColourValue()` | Returns an integer ARGB value for the colour string. |

### Reflection

Extension methods for `Assembly`, `Type`, `MemberInfo`, `ParameterInfo`, and related reflection types.

|Command|Description|
|---|---|
| `myAssembly.InstantiateAllTypes<T>(params object[] constructorArgs)` | Scans an assembly for all instantiable classes that derive from `T` and instantiates them. |
| `myAttribute.GetDerivedTypesFromAssembly<T>(Assembly assembly)` | Gets types decorated with a class-level attribute from an assembly (extension on attribute instance). |
| `myAssembly.GetTypesWithAttribute<T>()` | Gets types decorated with a specified attribute and returns the attribute instance with each type. |
| `myMember.HasCustomAttribute<T>()` | Determines whether the specified member is decorated with the given custom attribute. |
| `myAssembly.HasCustomAttribute<T>()` | Determines whether the specified assembly is decorated with the given custom attribute. |
| `myType.HasCustomAttribute<T>()` | Determines whether the specified type is decorated with the given custom attribute. |
| `myMember.TryGetCustomAttribute<T>(out T attribute)` | Tries to get a custom attribute from a member. |
| `myAssembly.TryGetCustomAttribute<T>(out T attribute)` | Tries to get a custom attribute from an assembly. |
| `myType.TryGetCustomAttribute<T>(out T attribute)` | Tries to get a custom attribute from a type. |
| `myAssembly.GetAllTypesImplementingOpenGenericType(Type openGeneric)` | Gets all types in an assembly that implement a specified open generic interface. |
| `myAssembly.NullifyOrphanedStaticMembers()` | Nullifies and disposes orphaned static fields/properties in an assembly (where applicable). |

### Reflection - Parameters / Properties

Extension methods for `ParameterInfo`, `PropertyInfo`, and related types.

|Command|Description|
|---|---|
| `myParameter.TryGetDefaultValue(out object defaultValue)` | Tries to get the default value of a `ParameterInfo` (handles DateTime edge cases). |
| `myParameter.TryGetDefaultValue<T>(out T defaultValue)` | Tries to get the default value of a `ParameterInfo` strongly-typed. |

### Dynamic

Extension methods for `ExpandoObject` and dynamic types.

|Command|Description|
|---|---|
| `myExpando.HasProperty(string propertyName)` | Determines whether an `ExpandoObject` contains the specified property. |

### Functional

Extension methods for functional programming patterns.

|Command|Description|
|---|---|
| `value.IterateUntil(createNext, finishCondition)` | Iteratively applies `createNext` until `finishCondition` is met, returning the final value. |
| `myObject.InvokeIf(action, tests...)` | Invokes an action if all supplied predicates evaluate to true. |
| `sequence.InvokeAll(action)` | Performs an action on each element of an `IEnumerable<T>`. |
| `myObject.InvokeUnless(action, tests...)` | Invokes an action unless any supplied predicate evaluates to true. |

### Boolean Helpers

Extension methods for `bool` to facilitate conditional execution and fluent interfaces.

|Command|Description|
|---|---|
| `condition.If(trueAction)` | Invokes `trueAction` if the boolean is true. |
| `condition.IfNot(falseAction)` | Invokes `falseAction` if the boolean is false. |
| `condition.IfElse(trueAction, falseAction)` | Invokes one of two actions based on the boolean value. |
| `condition.If<T>(trueAction, args)` | Invokes an action with an argument if condition is true. |
| `condition.IfNot<T>(falseAction, args)` | Invokes an action with an argument if condition is false. |
| `condition.IfElse<T>(trueFunc, falseFunc, args)` | Invokes one of two functions with an argument and returns the result. |
| `state.ActIfTrue(trueAction)` | Performs an action if the boolean is true and returns the original state. |
| `state.ActIfFalse(falseAction)` | Performs an action if the boolean is false and returns the original state. |
| `state.ActIf(trueAction, falseAction)` | Performs one of two actions based on the boolean and returns the original state. |
| `state.ActIf<T>(trueAction, falseAction, args)` | Performs one of two actions with an argument based on the boolean and returns the original state. |

### Numerical

Extension methods for numeric types (`int`, `long`, `float`, `double`, `decimal`).

|Command|Description|
|---|---|
| `val.IsWithinRange(min, max)` | Determines whether a value is within an inclusive range. |
| `value.IsGreaterThan(min)` | Determines whether a value is greater than a given minimum. |
| `value.IsLessThan(max)` | Determines whether a value is less than a given maximum. |
| `value.IsGreaterThanOrEqualTo(min)` | Determines whether a value is greater than or equal to a given minimum. |
| `value.IsLessThanOrEqualTo(max)` | Determines whether a value is less than or equal to a given maximum. |
| `number.FormatLargeNumber(locale)` | Formats a whole number with group separators for the given locale (`int` / `long`). |
| `number.FormatLargeNumber(maxDecimals, locale)` | Formats a floating number with group separators and specified maximum decimals (`float` / `double`). |

### Tasks

Extension methods for `Task` and `Task<TResult>` to facilitate continuations and result transformations.

|Command|Description|
|---|---|
| `task.OnCompletedSuccessfully(Action continuation)` | Executes an action when a `Task` completes successfully. |
| `task.OnCompletedSuccessfully(Func<Task> continuation)` | Executes an asynchronous continuation when a `Task` completes successfully. |
| `task.OnCompletedSuccessfully<TResult>(Action<TResult> continuation)` | Executes an action with the result when a `Task<TResult>` completes successfully. |
| `task.OnCompletedSuccessfully<TResult>(Func<TResult, Task> continuation)` | Executes an async continuation with the result when a `Task<TResult>` completes successfully. |
| `task.OnCompletedSuccessfully<TResult,TNewResult>(Func<TResult, TNewResult> continuation)` | Executes a function to transform the result of a completed `Task<TResult>`. |
| `task.OnCompletedSuccessfully<TResult,TNewResult>(Func<TResult, Task<TNewResult>> continuation)` | Executes an async transform of the result of a completed `Task<TResult>`. |

### Embedded Resources

Extension methods for `Assembly` to facilitate working with embedded resources.

|Command|Description|
|---|---|
| `assembly.ResourceExists(string fileName)` | Determines whether an embedded resource exists within an assembly. |
| `assembly.GetResourceStream(string fileName)` | Reads an embedded resource and returns its contents as a `Stream`. |
| `assembly.GetResourceContent(string fileName)` | Reads an embedded resource and returns its contents as a `string` (UTF-8). |
| `assembly.GetResourceContent(string fileName, Encoding encoding)` | Reads an embedded resource with the specified encoding and returns its contents as a `string`. |
| `assembly.DisembedResource(string resourceName, string fileName)` | Extracts an embedded resource to the specified file path on disk. |