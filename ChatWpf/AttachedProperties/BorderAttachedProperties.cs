using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatWpf.AttachedProperties
{
    public class ClipFromBorderProperty : BaseAttachedProperty<ClipFromBorderProperty, bool>
    {
        private RoutedEventHandler _borderLoaded;

        private SizeChangedEventHandler _borderSizeChanged;

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var self = (sender as FrameworkElement);

            if (!(self.Parent is Border border))
            {
                Debugger.Break();
                return;
            }

            _borderLoaded = (s1, e1) => Border_OnChange(s1, e1, self);

            _borderSizeChanged = (s1, e1) => Border_OnChange(s1, e1, self);

            if ((bool)e.NewValue)
            {
                border.Loaded += _borderLoaded;
                border.SizeChanged += _borderSizeChanged;
            }
            else
            {
                border.Loaded -= _borderLoaded;
                border.SizeChanged -= _borderSizeChanged;
            }
        }

        private void Border_OnChange(object sender, RoutedEventArgs e, FrameworkElement child)
        {
            var border = (Border)sender;

            if (border.ActualWidth == 0 && border.ActualHeight == 0)
                return;

            var rect = new RectangleGeometry();

            rect.RadiusX = rect.RadiusY = Math.Max(0, border.CornerRadius.TopLeft - (border.BorderThickness.Left * 0.5));

            rect.Rect = new Rect(child.RenderSize);

            child.Clip = rect;
        }
    }
}
