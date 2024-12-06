using System;
using Windows.UI;

namespace ReflectionIT.WinUI.Helpers;

/// <summary>
/// Source: https://gist.github.com/JoshClose/1367327
/// </summary>
public static class ColorHelper {

    /// <summary>
    /// Tints the color by the given percent.
    /// </summary>
    /// <param name="color">The color being tinted.</param>
    /// <param name="percent">The percent to tint. Ex: 0.1 will make the color 10% lighter.</param>
    /// <returns>The new tinted color.</returns>
    public static Color Lighten(this Color color, float percent) {
        var lighting = GetBrightness(color);
        lighting = lighting + lighting * percent;
        if (lighting > 1.0) {
            lighting = 1;
        } else if (lighting <= 0) {
            lighting = 0.1f;
        }
        var tintedColor = ColorHelper.FromHsl(color.A, GetHue(color), GetSaturation(color), lighting);

        return tintedColor;
    }

    /// <summary>
    /// Tints the color by the given percent.
    /// </summary>
    /// <param name="color">The color being tinted.</param>
    /// <param name="percent">The percent to tint. Ex: 0.1 will make the color 10% darker.</param>
    /// <returns>The new tinted color.</returns>
    public static Color Darken(this Color color, float percent) {
        var lighting = GetBrightness(color);
        lighting = lighting - lighting * percent;
        if (lighting > 1.0) {
            lighting = 1;
        } else if (lighting <= 0) {
            lighting = 0;
        }
        var tintedColor = ColorHelper.FromHsl(color.A, GetHue(color), GetSaturation(color), lighting);

        return tintedColor;
    }

    /// <summary>
    /// Converts the HSL values to a Color.
    /// </summary>
    /// <param name="alpha">The alpha.</param>
    /// <param name="hue">The hue.</param>
    /// <param name="saturation">The saturation.</param>
    /// <param name="lighting">The lighting.</param>
    /// <returns></returns>
    public static Color FromHsl(byte alpha, float hue, float saturation, float lighting) {
        //if (alpha is < 0 or > 255) {
        //    throw new ArgumentOutOfRangeException("alpha");
        //}
        if (hue is < 0f or > 360f) {
            throw new ArgumentOutOfRangeException("hue");
        }
        if (saturation is < 0f or > 1f) {
            throw new ArgumentOutOfRangeException("saturation");
        }
        if (lighting is < 0f or > 1f) {
            throw new ArgumentOutOfRangeException("lighting");
        }

        if (0 == saturation) {
            return Color.FromArgb(alpha, Convert.ToByte(lighting * 255), Convert.ToByte(lighting * 255), Convert.ToByte(lighting * 255));
        }

        float fMax, fMid, fMin;
        int iSextant;

        if (0.5 < lighting) {
            fMax = lighting - (lighting * saturation) + saturation;
            fMin = lighting + (lighting * saturation) - saturation;
        } else {
            fMax = lighting + (lighting * saturation);
            fMin = lighting - (lighting * saturation);
        }

        iSextant = (int)Math.Floor(hue / 60f);
        if (300f <= hue) {
            hue -= 360f;
        }
        hue /= 60f;
        hue -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
        fMid = 0 == iSextant % 2 
            ? hue * (fMax - fMin) + fMin 
            : fMin - hue * (fMax - fMin);

        byte bMax = Convert.ToByte(fMax * 255);
        byte bMid = Convert.ToByte(fMid * 255);
        byte bMin = Convert.ToByte(fMin * 255);

        switch (iSextant) {
            case 1:
                return Color.FromArgb(alpha, bMid, bMax, bMin);
            case 2:
                return Color.FromArgb(alpha, bMin, bMax, bMid);
            case 3:
                return Color.FromArgb(alpha, bMin, bMid, bMax);
            case 4:
                return Color.FromArgb(alpha, bMid, bMin, bMax);
            case 5:
                return Color.FromArgb(alpha, bMax, bMin, bMid);
            default:
                return Color.FromArgb(alpha, bMax, bMid, bMin);
        }
    }

    /// <summary>
    /// Gets the brightness of the color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>The brightness value.</returns>
    public static float GetBrightness(this Color color) {
        return (color.R * 0.299f + color.G * 0.587f + color.B * 0.114f) / 255f;
    }

    /// <summary>
    /// Gets the hue of the color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>The hue value.</returns>
    public static float GetHue(this Color color) {
        float r = color.R / 255f;
        float g = color.G / 255f;
        float b = color.B / 255f;

        float max = Math.Max(r, Math.Max(g, b));
        float min = Math.Min(r, Math.Min(g, b));

        float hue = 0f;

        if (max == min) {
            hue = 0f;
        } else if (max == r) {
            hue = (60f * ((g - b) / (max - min)) + 360f) % 360f;
        } else if (max == g) {
            hue = (60f * ((b - r) / (max - min)) + 120f) % 360f;
        } else if (max == b) {
            hue = (60f * ((r - g) / (max - min)) + 240f) % 360f;
        }

        return hue;
    }

    /// <summary>
    /// Gets the saturation of the color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>The saturation value.</returns>
    public static float GetSaturation(this Color color) {
        float r = color.R / 255f;
        float g = color.G / 255f;
        float b = color.B / 255f;

        float max = Math.Max(r, Math.Max(g, b));
        float min = Math.Min(r, Math.Min(g, b));

        float saturation = 0f;

        if (max != 0) {
            saturation = (max - min) / max;
        }

        return saturation;
    }
}
