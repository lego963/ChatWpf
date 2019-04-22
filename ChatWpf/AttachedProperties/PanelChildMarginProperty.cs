using System.Windows;
using System.Windows.Controls;

namespace ChatWpf.AttachedProperties
{
    public class PanelChildMarginProperty : BaseAttachedProperty<PanelChildMarginProperty, string>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Panel panel)
                panel.Loaded += (s, ee) =>
                {
                    foreach (var child in panel.Children)
                        ((FrameworkElement) child).Margin =
                            (Thickness) new ThicknessConverter().ConvertFromString(e.NewValue as string);
                };
        }
    }
}