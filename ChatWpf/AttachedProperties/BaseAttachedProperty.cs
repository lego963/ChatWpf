using System;
using System.Windows;

namespace ChatWpf.AttachedProperties
{
    public abstract class BaseAttachedProperty<TParent, TProperty> where TParent : BaseAttachedProperty<TParent, TProperty>, new()
    {
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        public static TParent Instance { get; private set; } = new TParent();

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(TProperty), typeof(BaseAttachedProperty<TParent, TProperty>), new UIPropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Instance.OnValueChanged(d, e);

            Instance.ValueChanged(d, e);
        }

        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            Instance.OnValueUpdated(d, value);
            Instance.ValueUpdated(d, value);
            return value;
        }

        public static TProperty GetValue(DependencyObject d) => (TProperty)d.GetValue(ValueProperty);

        public static void SetValue(DependencyObject d, TProperty value) => d.SetValue(ValueProperty, value);

        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        public virtual void OnValueUpdated(DependencyObject sender, object value) { }

    }
}