using System.Windows;
using System.Windows.Input;
using ChatWpf.DataModels;
using ChatWpf.ViewModel.Base;
using ChatWpf.Window;

namespace ChatWpf.ViewModel
{
    public class WindowViewModel : BaseViewModel
    {
        private System.Windows.Window mWindow;

        private WindowResizer mWindowResizer;

        private int mOuterMarginSize = 10;

        private int mWindowRadius = 10;

        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth { get; set; } = 600;

        public double WindowMinimumHeight { get; set; } = 600;

        public bool Borderless { get { return (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked); } }

        public int ResizeBorder { get { return Borderless ? 0 : 6; } }

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Chat;

        public int OuterMarginSize
        {
            get
            {
                return Borderless ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get
            {
                return Borderless ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        public WindowViewModel(System.Windows.Window window)
        {
            mWindow = window;

            mWindow.StateChanged += (sender, e) =>
            {
                WindowResized();
            };

            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            mWindowResizer = new WindowResizer(mWindow);

            mWindowResizer.WindowDockChanged += (dock) =>
            {
                mDockPosition = dock;

                WindowResized();
            };
        }

        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindow);

            if (mWindow.WindowState == WindowState.Maximized)
                return new Point(position.X + mWindowResizer.CurrentMonitorSize.Left, position.Y + mWindowResizer.CurrentMonitorSize.Top);
            else
                return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        private void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }

    }
}