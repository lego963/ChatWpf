using System.Windows;
using System.Windows.Input;
using ChatWpf.ViewModel.Base;
using ChatWpf.Window;
using Point = System.Windows.Point;

namespace ChatWpf.WPFViewModels
{
    public class WindowViewModel : BaseViewModel
    {
        private readonly System.Windows.Window _window;

        private readonly WindowResizer _windowResizer;

        private Thickness _outerMarginSize = new Thickness(5);

        private int _windowRadius = 10;

        private WindowDockPosition _dockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth { get; set; } = 800;

        public double WindowMinimumHeight { get; set; } = 500;

        public bool BeingMoved { get; set; }


        public bool Borderless => (_window.WindowState == WindowState.Maximized || _dockPosition != WindowDockPosition.Undocked);

        public int ResizeBorder => _window.WindowState == WindowState.Maximized ? 0 : 4;

        public Thickness ResizeBorderThickness => new Thickness(OuterMarginSize.Left + ResizeBorder,
                                                                OuterMarginSize.Top + ResizeBorder,
                                                                OuterMarginSize.Right + ResizeBorder,
                                                                OuterMarginSize.Bottom + ResizeBorder);

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public Thickness OuterMarginSize
        {
            get => _window.WindowState == WindowState.Maximized ? _windowResizer.CurrentMonitorMargin : (Borderless ? new Thickness(0) : _outerMarginSize);
            set => _outerMarginSize = value;
        }

        public int WindowRadius
        {
            get => Borderless ? 0 : _windowRadius;
            set => _windowRadius = value;
        }

        public int FlatBorderThickness => Borderless && _window.WindowState != WindowState.Maximized ? 1 : 0;

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
            _window = window;

            _window.StateChanged += (sender, e) =>
            {
                WindowResized();
            };

            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));

            _windowResizer = new WindowResizer(_window);

            _windowResizer.WindowDockChanged += (dock) =>
            {
                _dockPosition = dock;

                WindowResized();
            };

            _windowResizer.WindowStartedMove += () =>
            {
                BeingMoved = true;
            };

            _windowResizer.WindowFinishedMove += () =>
            {
                BeingMoved = false;

                if (_dockPosition == WindowDockPosition.Undocked && _window.Top == _windowResizer.CurrentScreenSize.Top)
                    _window.Top = -OuterMarginSize.Top;
            };
        }

        private Point GetMousePosition()
        {
            return _windowResizer.GetCursorPosition();
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
