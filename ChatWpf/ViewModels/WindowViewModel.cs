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

        private int _mOuterMarginSize = 10;

        private int _mWindowRadius = 10;

        private WindowDockPosition _mDockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth { get; set; } = 600;

        public double WindowMinimumHeight { get; set; } = 600;

        public bool Borderless => (_mWindow.WindowState == WindowState.Maximized || _mDockPosition != WindowDockPosition.Undocked);

        public int ResizeBorder => 10;

        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public int OuterMarginSize
        {
            get => Borderless ? 0 : _mOuterMarginSize;
            set => _mOuterMarginSize = value;
        }

        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        public int WindowRadius
        {
            get
            {
                return Borderless ? 0 : _mWindowRadius;
            }
            set
            {
                _mWindowRadius = value;
            }
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
        }

        private Point GetMousePosition()
        {
            return mWindowResizer.GetCursorPosition();
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