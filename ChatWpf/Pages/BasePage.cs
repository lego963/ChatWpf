using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChatWpf.Animation;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Pages
{
    public class BasePage : UserControl
    {
        private object mViewModel;

        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        public float SlideSeconds { get; set; } = 0.4f;

        public bool ShouldAnimateOut { get; set; }

        public object ViewModelObject
        {
            get => mViewModel;
            set
            {
                if (mViewModel == value)
                    return;
                mViewModel = value;
//OnViewModelChanged();
                DataContext = mViewModel;
            }
        }

        public BasePage()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (PageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            Loaded += BasePage_LoadedAsync;
        }

        private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            if (ShouldAnimateOut)
                await AnimateOutAsync();
            else
                await AnimateInAsync();
        }

        public async Task AnimateInAsync()
        {
            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, false, SlideSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
            }
        }

        public async Task AnimateOutAsync()
        {
            if (PageUnloadAnimation == PageAnimation.None)
                return;

            switch (PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, SlideSeconds);
                    break;
            }
        }

        protected virtual void OnViewModelChanged()
        {

        }
    }

    public class BasePage<TVm> : BasePage
        where TVm : BaseViewModel, new()
    {

        public TVm ViewModel
        {
            get => (TVm)ViewModelObject;
            set => ViewModelObject = value;
        }

        public BasePage() : base()
        {
            ViewModel = Core.IoC.Base.IoC.Get<TVm>();
        }

        public BasePage(TVm specificViewModel = null) : base()
        {
            if (specificViewModel != null)
                ViewModel = specificViewModel;
            else
                ViewModel = Core.IoC.Base.IoC.Get<TVm>();
        }

    }
}