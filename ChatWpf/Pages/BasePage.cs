using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChatWpf.Animation;

namespace ChatWpf.Pages
{
    public class BasePage : Page
    {
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        public float SlideSeconds { get; set; } = 0.8f;

        public BasePage()
        {
            if (PageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            Loaded += BasePage_Loaded;
        }

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:
                    await this.SlideAndFadeInFromRight(SlideSeconds);
                    break;
            }
        }

        public async Task AnimateOut()
        {
            if (PageUnloadAnimation == PageAnimation.None)
                return;

            switch (PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:
                    await this.SlideAndFadeOutToLeft(SlideSeconds);
                    break;
            }
        }
    }
}