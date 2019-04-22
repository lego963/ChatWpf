using System.Windows;
using System.Windows.Controls;

namespace ChatWpf.AttachedProperties
{
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Frame frame)) return;
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            frame.Navigated += (ss, ee) => ((Frame) ss).NavigationService.RemoveBackEntry();
        }
    }
}
