using System.Windows;
using System.Windows.Input;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Window;

namespace ChatWpf.ViewModels
{
    public class WindowViewModel : BaseViewModel
    {
        private System.Windows.Window _mWindow;

        private WindowResizer mWindowResizer;

        private Thickness _mOuterMarginSize = new Thickness(5);

        private int _mWindowRadius = 10;

        private WindowDockPosition _mDockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth { get; set; } = 800;

        public double WindowMinimumHeight { get; set; } = 500;

        public bool Borderless => (_mWindow.WindowState == WindowState.Maximized || _mDockPosition != WindowDockPosition.Undocked);

        public int ResizeBorder => _mWindow.WindowState == WindowState.Maximized ? 0 : 4;

        public int FlatBorderThickness => Borderless && _mWindow.WindowState != WindowState.Maximized ? 1 : 0;

        public Thickness ResizeBorderThickness => new Thickness(
            OuterMarginSize.Left + ResizeBorder,
            OuterMarginSize.Top + ResizeBorder,
            OuterMarginSize.Right + ResizeBorder,
            OuterMarginSize.Bottom + ResizeBorder);

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public Thickness OuterMarginSize
        {
            get => _mWindow.WindowState == WindowState.Maximized ? mWindowResizer.CurrentMonitorMargin : (Borderless ? new Thickness(0) : _mOuterMarginSize);

            set => _mOuterMarginSize = value;
        }

        public int WindowRadius
        {
            get => Borderless ? 0 : _mWindowRadius;
            set => _mWindowRadius = value;
        }

        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        public bool DimmableOverlayVisible { get; set; }

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        public WindowViewModel(System.Windows.Window window)
        {
            _mWindow = window;

            _mWindow.StateChanged += (sender, e) =>
            {
                WindowResized();
            };

            MinimizeCommand = new RelayCommand(() => _mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_mWindow, GetMousePosition()));

            mWindowResizer = new WindowResizer(_mWindow);

            mWindowResizer.WindowDockChanged += (dock) =>
            {
                _mDockPosition = dock;

                WindowResized();
            };

            mWindowResizer.WindowFinishedMove += () =>
            {
                // Check for moved to top of window and not at an edge
                if (_mDockPosition == WindowDockPosition.Undocked && _mWindow.Top == mWindowResizer.CurrentScreenSize.Top)
                    // If so, move it to the true top (the border size)
                    _mWindow.Top = -OuterMarginSize.Top;
            };
        }

        private Point GetMousePosition()
        {
            return mWindowResizer.GetCursorPosition();
        }

        private void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(FlatBorderThickness));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }

    }
}