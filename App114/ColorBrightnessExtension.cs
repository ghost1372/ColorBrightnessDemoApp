﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using Windows.UI;

namespace ReflectionIT.WinUI.Helpers;

/// <summary>
/// MarkupExtension to that can be used to brighten (positive) or darken (negative) a Color.
/// <code>
/// <Rectangle Width="40" Height="40">
///     <Rectangle.Fill>
///         <SolidColorBrush Color = "{helpers:ColorBrightness Color=Red, CorrectionFactor=-0.3}" />
///     </ Rectangle.Fill >
/// </ Rectangle >
/// </code>
/// </summary>
[MarkupExtensionReturnType(ReturnType = typeof(Color))]
public partial class ColorBrightnessExtension : MarkupExtension {

    /// <summary>
    /// Gets or sets the correction factor for the brightness adjustment.
    /// Positive values brighten the color, negative values darken it.
    /// Must be between -1 and 1.
    /// </summary>
    public double CorrectionFactor { get; set; }

    /// <summary>
    /// Gets or sets the color to be adjusted.
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBrightnessExtension"/> class.
    /// </summary>
    public ColorBrightnessExtension() {
    }

    /// <summary>
    /// Provides the value of the adjusted color.
    /// </summary>
    /// <returns>The adjusted <see cref="Color"/>.</returns>
    protected override object ProvideValue() {
        return CorrectionFactor > 1 
            ? Color.Lighten((float)CorrectionFactor) 
            : Color.Darken(-(float)CorrectionFactor);
    }
}
