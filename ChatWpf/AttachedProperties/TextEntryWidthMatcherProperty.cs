using System;
using System.Windows;
using System.Windows.Controls;
using ChatWpf.Controls.Input;

namespace ChatWpf.AttachedProperties
{
    public class TextEntryWidthMatcherProperty : BaseAttachedProperty<TextEntryWidthMatcherProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var panel = (sender as Panel);

            SetWidths(panel);

            RoutedEventHandler onLoaded = null;

            onLoaded = (s, ee) =>
            {
                panel.Loaded -= onLoaded;

                SetWidths(panel);

                foreach (var child in panel.Children)
                {
                    if (!(child is TextEntryControl) && !(child is PasswordEntryControl))
                        continue;

                    var label = child is TextEntryControl ? (child as TextEntryControl).Label : (child as PasswordEntryControl).Label;

                    label.SizeChanged += (ss, eee) =>
                    {
                        SetWidths(panel);
                    };
                }
            };
            panel.Loaded += onLoaded;
        }

        private void SetWidths(Panel panel)
        {
            var maxSize = 0d;

            foreach (var child in panel.Children)
            {
                if (!(child is TextEntryControl) && !(child is PasswordEntryControl))
                    continue;
                var label = child is TextEntryControl ? (child as TextEntryControl).Label : (child as PasswordEntryControl).Label;

                maxSize = Math.Max(maxSize, label.RenderSize.Width + label.Margin.Left + label.Margin.Right);
            }

            var gridLength = (GridLength)new GridLengthConverter().ConvertFromString(maxSize.ToString());

            foreach (var child in panel.Children)
            {
                if (child is TextEntryControl text)
                    text.LabelWidth = gridLength;
                else if (child is PasswordEntryControl pass)
                    pass.LabelWidth = gridLength;
            }

        }
    }
}