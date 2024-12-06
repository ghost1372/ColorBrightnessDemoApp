using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;
using System;

namespace ReflectionIT.WinUI.Helpers;

public static class BrushExtensions {


    #region ColorCorrectionFactor Attached Property

    /// <summary> 
    /// Identifies the ColorCorrectionFactor attachted property. This enables animation, styling, binding, etc...
    /// </summary>
    public static readonly DependencyProperty ColorCorrectionFactorProperty =
        DependencyProperty.RegisterAttached("ColorCorrectionFactor",
                                            typeof(double),
                                            typeof(BrushExtensions),
                                            new PropertyMetadata(default(double), OnColorCorrectionFactorChanged));

    /// <summary>
    /// ColorCorrectionFactor changed handler. 
    /// </summary>
    /// <param name="d">SolidColorBrush that changed its ColorCorrectionFactor attached property.</param>
    /// <param name="e">DependencyPropertyChangedEventArgs with the new and old value.</param> 
    private static void OnColorCorrectionFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is SolidColorBrush source) {
            source.DispatcherQueue.TryEnqueue(() => {
                if (e.NewValue is double value) {
                    source.Color = value > 1 ? source.Color.Lighten((float)value) : source.Color.Darken(-(float)value);
                }
            });
        }
    }

    /// <summary>
    /// Gets the value of the ColorCorrectionFactor attached property from the specified SolidColorBrush.
    /// </summary>
    public static double GetColorCorrectionFactor(DependencyObject obj) {
        return (double)obj.GetValue(ColorCorrectionFactorProperty);
    }


    /// <summary>
    /// Sets the value of the ColorCorrectionFactor attached property to the specified SolidColorBrush.
    /// </summary>
    /// <param name="obj">The object on which to set the ColorCorrectionFactor attached property.</param>
    /// <param name="value">The property value to set.</param>
    public static void SetColorCorrectionFactor(DependencyObject obj, double value) {
        obj.SetValue(ColorCorrectionFactorProperty, value);
    }

    #endregion ColorCorrectionFactor Attached Property



}