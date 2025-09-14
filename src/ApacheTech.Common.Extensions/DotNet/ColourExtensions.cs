using System;
using System.Drawing;
using System.Globalization;
using ApacheTech.Common.Extensions.Enum;

// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global

namespace ApacheTech.Common.Extensions.DotNet;

/// <summary>
///     Extension Methods for when working with Colours.
/// </summary>
public static class ColourExtensions
{
    /// <summary>
    ///     Updates a single colour channel within a <see cref="Color"/>
    /// </summary>
    /// <param name="colour">The colour to change the channel of.</param>
    /// <param name="channel">The channel to change the value of.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>A new instance of <see cref="Color"/>, with the updated values.</returns>
    public static Color UpdateColourChannel(this Color colour, ColourChannel channel, byte value)
    {
        return channel switch
        {
            ColourChannel.A => Color.FromArgb(value, colour.R, colour.G, colour.B),
            ColourChannel.R => Color.FromArgb(colour.A, value, colour.G, colour.B),
            ColourChannel.G => Color.FromArgb(colour.A, colour.R, value, colour.B),
            ColourChannel.B => Color.FromArgb(colour.A, colour.R, colour.G, value),
            _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
        };
    }

    /// <summary>
    ///     Normalises the specified colour. That being, it converts each channel value into a <see cref="double"/>, between 0.0 and 1.0, by dividing each value by 255.0.
    /// </summary>
    /// <param name="colour">The colour to normalise.</param>
    /// <returns>A double array, with normalised values set in RGBA order.</returns>
    public static double[] ToNormalisedRgba(this Color colour)
    {
        return new[]
        {
            colour.R / 255d,
            colour.G / 255d,
            colour.B / 255d,
            colour.A / 255d
        };
    }

    /// <summary>
    ///     Normalises the specified colour. That being, it converts each channel value into a <see cref="double"/>, between 0.0 and 1.0, by dividing each value by 255.0.
    /// </summary>
    /// <param name="colour">The colour to normalise.</param>
    /// <returns>A double array, with normalised values set in ARGB order.</returns>
    public static double[] ToNormalisedArgb(this Color colour)
    {
        return new[]
        {
            colour.A / 255d,
            colour.R / 255d,
            colour.G / 255d,
            colour.B / 255d
        };
    }

    /// <summary>
    ///     Normalises the specified colour. That being, it converts each channel value into a <see cref="double"/>, between 0.0 and 1.0, by dividing each value by 255.0.
    /// </summary>
    /// <param name="colour">The colour to normalise.</param>
    /// <returns>A double array, with normalised values set in RGBA order.</returns>
    public static double[] ToNormalisedRgba(this double[] colour)
    {
        return new[]
        {
            colour[0] / 255d,
            colour[1] / 255d,
            colour[2] / 255d,
            colour[3] / 255d
        };
    }

    /// <summary>
    ///     Normalises the specified colour. That being, it converts each channel value into a <see cref="double"/>, between 0.0 and 1.0, by dividing each value by 255.0.
    /// </summary>
    /// <param name="colour">The colour to normalise.</param>
    /// <returns>A double array, with normalised values set in ARGB order.</returns>
    public static double[] ToNormalisedArgb(this double[] colour)
    {
        return new[]
        {
            colour[0] / 255d,
            colour[1] / 255d,
            colour[2] / 255d,
            colour[3] / 255d
        };
    }

    /// <summary>
    ///     Converts a <see cref="Color"/> to an RGB Hexadecimal string.
    /// </summary>
    /// <param name="c">The colour to convert.</param>
    /// <returns>A hex string, in the format #RRGGBB.</returns>
    public static string ToRgbHexString(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

    /// <summary>
    ///     Converts a <see cref="Color"/> to an ARGB Hexadecimal string.
    /// </summary>
    /// <param name="c">The colour to convert.</param>
    /// <returns>A hex string, in the format #AARRGGBB.</returns>
    public static string ToArgbHexString(this Color c) => $"#{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}";

    /// <summary>
    ///     Converts a <see cref="Color"/> to an RGBA Hexadecimal string.
    /// </summary>
    /// <param name="c">The colour to convert.</param>
    /// <returns>A hex string, in the format #RRGGBBAA.</returns>
    public static string ToRgbaHexString(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}";

    /// <summary>
    ///     Converts a <see cref="Color"/> to an RGB() string.
    /// </summary>
    /// <param name="c">The colour to convert.</param>
    /// <returns>A string, in the format RGB(0-255, 0-255, 0-255).</returns>
    public static string ToRgbString(this Color c) => $"RGB({c.R}, {c.G}, {c.B})";


    /// <summary>
    ///     Converts a string representation of a colour, to an ARGB integer value.
    /// </summary>
    /// <param name="colourString">The colour string.</param>
    /// <returns>An integer representation of the ARGB values of a colour.</returns>
    public static int ToArgb(this string colourString)
    {
        var num = Color.Transparent.ToArgb();
        var num2 = Color.Black.ToArgb();

        if (colourString.StartsWith("#"))
        {
            if (colourString.Length != 7)
            {
                return num;
            }
            var s = colourString.Substring(1);
            try
            {
                return int.Parse(s, NumberStyles.HexNumber) | num2;
            }
            catch (Exception)
            {
                return num;
            }
        }
        var color = Color.FromName(colourString);
        return color.A == 255 ? color.ToArgb() : num;
    }

    /// <summary>
    ///     Converts a string representation of a colour, to a <see cref="Color"/>.
    /// </summary>
    /// <param name="colour">The colour to convert.</param>
    /// <returns>A <see cref="Color"/>, converted from the string input.</returns>
    public static Color ToColour(this string colour)
    {
        try
        {
            return colour.StartsWith("#")
                ? Color.FromArgb(int.Parse(colour.Replace("#", ""), NumberStyles.HexNumber))
                : Color.FromName(colour);
        }
        catch (Exception)
        {
            return Color.Black;
        }
    }

    /// <summary>
    ///     Converts a string representation of a colour, to an integer value.
    /// </summary>
    /// <param name="colour">The colour to convert.</param>
    /// <returns>An integer representation of the input colour.</returns>
    public static int ColourValue(this string colour)
    {
        return colour.ToArgb() | -16777216;
    }

}