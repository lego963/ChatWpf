using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace ChatWpf.Window
{
    public class WindowResizer
    {
        private System.Windows.Window _mWindow;

        private Rect _mScreenSize = new Rect();

        private int _mEdgeTolerance = 8;

        private Matrix _mTransformToDevice;

        private IntPtr _mLastScreen;


        private WindowDockPosition _mLastDock = WindowDockPosition.Undocked;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, Monitorinfo lpmi);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(Point pt, MonitorOptions dwFlags);

        public event Action<WindowDockPosition> WindowDockChanged = (dock) => { };

        public Rectangle CurrentMonitorSize { get; set; } = new Rectangle();

        public WindowResizer(System.Windows.Window window)
        {
            _mWindow = window;

            GetTransform();

            _mWindow.SourceInitialized += Window_SourceInitialized;

            _mWindow.SizeChanged += Window_SizeChanged;
        }

        private void GetTransform()
        {
            var source = PresentationSource.FromVisual(_mWindow);

            _mTransformToDevice = default(Matrix);

            if (source == null)
                return;

            _mTransformToDevice = source.CompositionTarget.TransformToDevice;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var handle = (new WindowInteropHelper(_mWindow)).Handle;
            var handleSource = HwndSource.FromHwnd(handle);

            if (handleSource == null)
                return;

            handleSource.AddHook(WindowProc);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_mTransformToDevice == default(Matrix))
                return;

            var size = e.NewSize;

            var top = _mWindow.Top;
            var left = _mWindow.Left;
            var bottom = top + size.Height;
            var right = left + _mWindow.Width;

            var windowTopLeft = _mTransformToDevice.Transform(new System.Windows.Point(left, top));
            var windowBottomRight = _mTransformToDevice.Transform(new System.Windows.Point(right, bottom));

            var edgedTop = windowTopLeft.Y <= (_mScreenSize.Top + _mEdgeTolerance);
            var edgedLeft = windowTopLeft.X <= (_mScreenSize.Left + _mEdgeTolerance);
            var edgedBottom = windowBottomRight.Y >= (_mScreenSize.Bottom - _mEdgeTolerance);
            var edgedRight = windowBottomRight.X >= (_mScreenSize.Right - _mEdgeTolerance);

            var dock = WindowDockPosition.Undocked;

            if (edgedTop && edgedBottom && edgedLeft)
                dock = WindowDockPosition.Left;
            else if (edgedTop && edgedBottom && edgedRight)
                dock = WindowDockPosition.Right;
            else
                dock = WindowDockPosition.Undocked;

            if (dock != _mLastDock)
                WindowDockChanged(dock);

            _mLastDock = dock;
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:/* WM_GETMINMAXINFO */
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            GetCursorPos(out var lMousePosition);

            var lCurrentScreen = MonitorFromPoint(lMousePosition, MonitorOptions.MonitorDefaulttonearest);

            var lCurrentScreenInfo = new Monitorinfo();
            if (GetMonitorInfo(lCurrentScreen, lCurrentScreenInfo) == false)
                return;

            if (lCurrentScreen != _mLastScreen || _mTransformToDevice == default(Matrix))
                GetTransform();

            _mLastScreen = lCurrentScreen;

            var lMmi = (Minmaxinfo)Marshal.PtrToStructure(lParam, typeof(Minmaxinfo));

            lMmi.ptMaxPosition.X = 0;
            lMmi.ptMaxPosition.Y = 0;
            lMmi.ptMaxSize.X = lCurrentScreenInfo.rcWork.Right - lCurrentScreenInfo.rcWork.Left;
            lMmi.ptMaxSize.Y = lCurrentScreenInfo.rcWork.Bottom - lCurrentScreenInfo.rcWork.Top;

            CurrentMonitorSize = new Rectangle(lMmi.ptMaxPosition.X, lMmi.ptMaxPosition.Y, lMmi.ptMaxSize.X + lMmi.ptMaxPosition.X, lMmi.ptMaxSize.Y + lMmi.ptMaxPosition.Y);

            var minSize = _mTransformToDevice.Transform(new System.Windows.Point(_mWindow.MinWidth, _mWindow.MinHeight));

            lMmi.ptMinTrackSize.X = (int)minSize.X;
            lMmi.ptMinTrackSize.Y = (int)minSize.Y;

            _mScreenSize = new Rect(lMmi.ptMaxPosition.X, lMmi.ptMaxPosition.Y, lMmi.ptMaxSize.X, lMmi.ptMaxSize.Y);

            Marshal.StructureToPtr(lMmi, lParam, true);
        }
    }

    enum MonitorOptions : uint
    {
        MonitorDefaulttonull = 0x00000000,
        MonitorDefaulttoprimary = 0x00000001,
        MonitorDefaulttonearest = 0x00000002
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class Monitorinfo
    {
        public int cbSize = Marshal.SizeOf(typeof(Monitorinfo));
        public Rectangle rcMonitor = new Rectangle();
        public Rectangle rcWork = new Rectangle();
        public int dwFlags = 0;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Left, Top, Right, Bottom;

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
        public Point ptReserved;
        public Point ptMaxSize;
        public Point ptMaxPosition;
        public Point ptMinTrackSize;
        public Point ptMaxTrackSize;
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
    }
}