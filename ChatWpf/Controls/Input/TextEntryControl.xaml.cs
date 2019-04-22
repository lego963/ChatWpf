using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ChatWpf.Controls.Input
{
    public partial class TextEntryControl : UserControl
    {
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(GridLength), typeof(TextEntryControl), new PropertyMetadata(GridLength.Auto, LabelWidthChangedCallback));

        public TextEntryControl()
        {
            InitializeComponent();
        }

        public static void LabelWidthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                ((TextEntryControl) d).LabelColumnDefinition.Width = (GridLength)e.NewValue;
            }

            catch (Exception)
            {
                Debugger.Break();

                ((TextEntryControl) d).LabelColumnDefinition.Width = GridLength.Auto;
            }
        }
    }
}
