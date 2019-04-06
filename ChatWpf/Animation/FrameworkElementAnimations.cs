using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ChatWpf.Animation
{
    public static class FrameworkElementAnimations
    {
        public static async Task SlideAndFadeInAsync(this FrameworkElement element, AnimationSlideInDirection direction, bool firstLoad, float seconds = 0.3f, bool keepMargin = true, int size = 0)
        {
            var sb = new Storyboard();
            switch (direction)
            {
                case AnimationSlideInDirection.Left:
                    sb.AddSlideFromLeft(seconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                case AnimationSlideInDirection.Right:
                    sb.AddSlideFromRight(seconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                case AnimationSlideInDirection.Top:
                    sb.AddSlideFromTop(seconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
                case AnimationSlideInDirection.Bottom:
                    sb.AddSlideFromBottom(seconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
            }
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            if (seconds != 0 || firstLoad)
                element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }

        public static async Task SlideAndFadeOutAsync(this FrameworkElement element, AnimationSlideInDirection direction, float seconds = 0.3f, bool keepMargin = true, int size = 0)
        {
            var sb = new Storyboard();
            switch (direction)
            {
                case AnimationSlideInDirection.Left:
                    sb.AddSlideToLeft(seconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                case AnimationSlideInDirection.Right:
                    sb.AddSlideToRight(seconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                case AnimationSlideInDirection.Top:
                    sb.AddSlideToTop(seconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
                case AnimationSlideInDirection.Bottom:
                    sb.AddSlideToBottom(seconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
            }
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            if (seconds != 0)
                element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
            if (element.Opacity == 0)
                element.Visibility = Visibility.Hidden;
        }

        public static async Task FadeInAsync(this FrameworkElement element, bool firstLoad, float seconds = 0.3f)
        {
            var sb = new Storyboard();
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            if (seconds != 0 || firstLoad)
                element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }

        public static async Task FadeOutAsync(this FrameworkElement element, float seconds = 0.3f)
        {
            var sb = new Storyboard();
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            if (seconds != 0)
                element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
            element.Visibility = Visibility.Collapsed;
        }

        public static void MarqueeAsync(this FrameworkElement element, float seconds = 3f)
        {
            var sb = new Storyboard();
            var unloaded = false;
            element.Unloaded += (s, e) => unloaded = true;
            Core.IoC.Base.IoC.Task.Run(async () =>
            {
                while (element != null && !unloaded)
                {
                    var width = 0d;
                    var innerWidth = 0d;
                    try
                    {
                        if (element == null || unloaded)
                            break;
                        width = element.ActualWidth;
                        innerWidth = ((element as Border).Child as FrameworkElement).ActualWidth;
                    }
                    catch
                    {
                        break;
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        sb.AddMarquee(seconds, width, innerWidth);
                        sb.Begin(element);
                        element.Visibility = Visibility.Visible;
                    });
                    await Task.Delay((int)seconds * 1000);
                    if (seconds == 0)
                        break;
                }
            });
        }
    }
}
