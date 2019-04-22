using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ChatWpf.AttachedProperties
{
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control))
                return;

            control.Loaded += (s, se) => control.Focus();
        }
    }

    public class FocusProperty : BaseAttachedProperty<FocusProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control))
                return;

            if ((bool)e.NewValue)
                control.Focus();
        }
    }

    public class FocusAndSelectProperty : BaseAttachedProperty<FocusAndSelectProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBoxBase control)
            {
                if ((bool)e.NewValue)
                {
                    control.Focus();

                    control.SelectAll();
                }
            }
            if (sender is PasswordBox password)
            {
                if ((bool)e.NewValue)
                {
                    password.Focus();

                    password.SelectAll();
                }
            }
        }
    }
}
