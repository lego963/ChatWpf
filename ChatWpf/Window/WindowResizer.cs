using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace ChatWpf.Window
{
    public class WindowResizer
    {
        private System.Windows.Window _window;

        private Rect _screenSize;

        private int _edgeTolerance = 1;

        private DpiScale? _monitorDpi;

        private IntPtr _lastScreen;

        private WindowDockPosition _lastDock = WindowDockPosition.Undocked;

        private bool _beingMoved;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, Monitorinfo lpmi);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(Point pt, MonitorOptions dwFlags);

        [DllImport("user32.dll")]
        static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorOptions dwFlags);

        public event Action<WindowDockPosition> WindowDockChanged = (dock) => { };

        public event Action WindowFinishedMove = () => { };

        public event Action WindowStartedMove = () => { };

        public Rectangle CurrentMonitorSize { get; set; }

        public Thickness CurrentMonitorMargin { get; private set; }

        public Rect CurrentScreenSize => _screenSize;

        public WindowResizer(System.Windows.Window window)
        {
            _window = window;
            _window.SourceInitialized += Window_SourceInitialized;
            _window.SizeChanged += Window_SizeChanged;
            _window.LocationChanged += Window_LocationChanged;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var handle = new WindowInteropHelper(_window).Handle;
            var handleSource = HwndSource.FromHwnd(handle);

            if (handleSource == null)
                return;

            handleSource.AddHook(WindowProc);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Window_SizeChanged(null, null);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WmGetMinMaxInfo(IntPtr.Zero, IntPtr.Zero);
            _monitorDpi = VisualTreeHelper.GetDpi(_window);
            if (_monitorDpi == null)
                return;

            var top = _window.Top;
            var left = _window.Left;
            var bottom = top + _window.Height;
            var right = left + _window.Width;

            var windowTopLeft = new System.Windows.Point(left * _monitorDpi.Value.DpiScaleX, top * _monitorDpi.Value.DpiScaleX);
            var windowBottomRight = new System.Windows.Point(right * _monitorDpi.Value.DpiScaleX, bottom * _monitorDpi.Value.DpiScaleX);

            var edgedTop = windowTopLeft.Y <= (_screenSize.Top + _edgeTolerance) && windowTopLeft.Y >= (_screenSize.Top - _edgeTolerance);
            var edgedLeft = windowTopLeft.X <= (_screenSize.Left + _edgeTolerance) && windowTopLeft.X >= (_screenSize.Left - _edgeTolerance);
            var edgedBottom = windowBottomRight.Y >= (_screenSize.Bottom - _edgeTolerance) && windowBottomRight.Y <= (_screenSize.Bottom + _edgeTolerance);
            var edgedRight = windowBottomRight.X >= (_screenSize.Right - _edgeTolerance) && windowBottomRight.X <= (_screenSize.Right + _edgeTolerance);

            var dock = WindowDockPosition.Undocked;

            if (edgedTop && edgedBottom && edgedLeft)
                dock = WindowDockPosition.Left;
            else if (edgedTop && edgedBottom && edgedRight)
                dock = WindowDockPosition.Right;
            else if (edgedTop && edgedBottom)
                dock = WindowDockPosition.TopBottom;
            else if (edgedTop && edgedLeft)
                dock = WindowDockPosition.TopLeft;
            else if (edgedTop && edgedRight)
                dock = WindowDockPosition.TopRight;
            else if (edgedBottom && edgedLeft)
                dock = WindowDockPosition.BottomLeft;
            else if (edgedBottom && edgedRight)
                dock = WindowDockPosition.BottomRight;
            else
                dock = WindowDockPosition.Undocked;

            if (dock != _lastDock)
                WindowDockChanged(dock);

            _lastDock = dock;
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024: // WM_GETMINMAXINFO
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;

                case 0x0231: // WM_ENTERSIZEMOVE
                    _beingMoved = true;
                    WindowStartedMove();
                    break;

                case 0x0232: // WM_EXITSIZEMOVE
                    _beingMoved = false;
                    WindowFinishedMove();
                    break;
            }

            return (IntPtr)0;
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            GetCursorPos(out var lMousePosition);

            var lCurrentScreen = _beingMoved ?
                MonitorFromPoint(lMousePosition, MonitorOptions.MonitorDefaulttonull) :
                MonitorFromWindow(hwnd, MonitorOptions.MonitorDefaulttonull);

            var lPrimaryScreen = MonitorFromPoint(new Point(0, 0), MonitorOptions.MonitorDefaulttoprimary);

            var lCurrentScreenInfo = new Monitorinfo();
            if (GetMonitorInfo(lCurrentScreen, lCurrentScreenInfo) == false)
                return;

            var lPrimaryScreenInfo = new Monitorinfo();
            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
                return;

            _monitorDpi = VisualTreeHelper.GetDpi(_window);

            _lastScreen = lCurrentScreen;

            var currentX = lCurrentScreenInfo.RCWork.Left - lCurrentScreenInfo.RCMonitor.Left;
            var currentY = lCurrentScreenInfo.RCWork.Top - lCurrentScreenInfo.RCMonitor.Top;
            var currentWidth = (lCurrentScreenInfo.RCWork.Right - lCurrentScreenInfo.RCWork.Left);
            var currentHeight = (lCurrentScreenInfo.RCWork.Bottom - lCurrentScreenInfo.RCWork.Top);
            var currentRatio = (float)currentWidth / currentHeight;

            var primaryX = lPrimaryScreenInfo.RCWork.Left - lPrimaryScreenInfo.RCMonitor.Left;
            var primaryY = lPrimaryScreenInfo.RCWork.Top - lPrimaryScreenInfo.RCMonitor.Top;
            var primaryWidth = (lPrimaryScreenInfo.RCWork.Right - lPrimaryScreenInfo.RCWork.Left);
            var primaryHeight = (lPrimaryScreenInfo.RCWork.Bottom - lPrimaryScreenInfo.RCWork.Top);
            var primaryRatio = (float)primaryWidth / primaryHeight;

            if (lParam != IntPtr.Zero)
            {
                var lMmi = (Minmaxinfo)Marshal.PtrToStructure(lParam, typeof(Minmaxinfo));

                lMmi.PointMaxPosition.X = lPrimaryScreenInfo.RCMonitor.Left;
                lMmi.PointMaxPosition.Y = lPrimaryScreenInfo.RCMonitor.Top;
                lMmi.PointMaxSize.X = lPrimaryScreenInfo.RCMonitor.Right;
                lMmi.PointMaxSize.Y = lPrimaryScreenInfo.RCMonitor.Bottom;

                var minSize = new System.Windows.Point(_window.MinWidth * _monitorDpi.Value.DpiScaleX, _window.MinHeight * _monitorDpi.Value.DpiScaleX);
                lMmi.PointMinTrackSize.X = (int)minSize.X;
                lMmi.PointMinTrackSize.Y = (int)minSize.Y;

                Marshal.StructureToPtr(lMmi, lParam, true);
            }

            CurrentMonitorSize = new Rectangle(currentX, currentY, currentWidth + currentX, currentHeight + currentY);

            CurrentMonitorMargin = new Thickness(
                (lCurrentScreenInfo.RCWork.Left - lCurrentScreenInfo.RCMonitor.Left) / _monitorDpi.Value.DpiScaleX,
                (lCurrentScreenInfo.RCWork.Top - lCurrentScreenInfo.RCMonitor.Top) / _monitorDpi.Value.DpiScaleY,
                (lCurrentScreenInfo.RCMonitor.Right - lCurrentScreenInfo.RCWork.Right) / _monitorDpi.Value.DpiScaleX,
                (lCurrentScreenInfo.RCMonitor.Bottom - lCurrentScreenInfo.RCWork.Bottom) / _monitorDpi.Value.DpiScaleY
                );

            _screenSize = new Rect(lCurrentScreenInfo.RCWork.Left, lCurrentScreenInfo.RCWork.Top, currentWidth, currentHeight);
        }

        public System.Windows.Point GetCursorPosition()
        {
            GetCursorPos(out var lMousePosition);

            return new System.Windows.Point(lMousePosition.X / _monitorDpi.Value.DpiScaleX, lMousePosition.Y / _monitorDpi.Value.DpiScaleY);
        }
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class Monitorinfo
    {
        public int CBSize = Marshal.SizeOf(typeof(Monitorinfo));
        public Rectangle RCMonitor = new Rectangle();
        public Rectangle RCWork = new Rectangle();
        public int DWFlags = 0;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
#pragma warning disable IDE1006 // Naming Styles
        public int Left, Top, Right, Bottom;
#pragma warning restore IDE1006 // Naming Styles

        public Rectangle(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Minmaxinfo
    {
        public Point PointReserved;
        public Point PointMaxSize;
        public Point PointMaxPosition;
        public Point PointMinTrackSize;
        public Point PointMaxTrackSize;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }
}