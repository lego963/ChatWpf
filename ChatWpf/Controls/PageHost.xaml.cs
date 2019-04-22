using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChatWpf.Core.DataModels;
using ChatWpf.Pages;
using ChatWpf.ValueConverter;
using ChatWpf.ViewModel.Application;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.Controls
{
    public partial class PageHost : UserControl
    {
        public ApplicationPage CurrentPage
        {
            get => (ApplicationPage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(ApplicationPage), typeof(PageHost), new UIPropertyMetadata(default(ApplicationPage), null, CurrentPagePropertyChanged));


        public BaseViewModel CurrentPageViewModel
        {
            get => (BaseViewModel)GetValue(CurrentPageViewModelProperty);
            set => SetValue(CurrentPageViewModelProperty, value);
        }

        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel),
                typeof(BaseViewModel), typeof(PageHost),
                new UIPropertyMetadata());

        public PageHost()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                NewPage.Content = new ApplicationViewModel().CurrentPage.ToBasePage();
        }


        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            var currentPage = (ApplicationPage)value;
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);

            var newPageFrame = ((PageHost) d).NewPage;
            var oldPageFrame = ((PageHost) d).OldPage;

            if (newPageFrame.Content is BasePage page &&
                page.ToApplicationPage() == currentPage)
            {
                page.ViewModelObject = currentPageViewModel;

                return value;
            }

            var oldPageContent = newPageFrame.Content;

            newPageFrame.Content = null;

            oldPageFrame.Content = oldPageContent;

            if (oldPageContent is BasePage oldPage)
            {
                oldPage.ShouldAnimateOut = true;

                Task.Delay((int)(oldPage.SlideSeconds * 1000)).ContinueWith((t) =>
                {
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }

            newPageFrame.Content = currentPage.ToBasePage(currentPageViewModel);

            return value;
        }
    }
}
