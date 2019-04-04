using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Pages;
using ChatWpf.ValueConverter;

namespace ChatWpf.Controls
{
    /// <summary>
    /// Логика взаимодействия для PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        public ApplicationPage CurrentPage
        {
            get => (ApplicationPage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public PageHost()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this))
                NewPage.Content = Core.IoC.Base.IoC.Application.CurrentPage.ToBasePage();
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage), 
                typeof(ApplicationPage), 
                typeof(PageHost), 
                new UIPropertyMetadata(
                    default(ApplicationPage), 
                    null, 
                    CurrentPagePropertyChanged));

        public BaseViewModel CurrentPageViewModel
        {
            get => (BaseViewModel)GetValue(CurrentPageViewModelProperty);
            set => SetValue(CurrentPageViewModelProperty, value);
        }

        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel),
                typeof(BaseViewModel),
                typeof(PageHost),
                new UIPropertyMetadata());

        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            var currentPage = (ApplicationPage)d.GetValue(CurrentPageProperty);
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);

            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;

            if (newPageFrame.Content is BasePage page &&
                page.ToApplicationPage() == currentPage)
            {
                // Just update the view model
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
                    // Remove old page
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }

            newPageFrame.Content = currentPage.ToBasePage(currentPageViewModel);

            return value;
        }
    }
}
