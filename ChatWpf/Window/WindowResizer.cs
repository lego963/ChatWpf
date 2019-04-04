using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace ChatWpf.Window
{
    public class WindowResizer
    {
        private System.Windows.Window mWindow;
        private Rect mScreenSize = new Rect();
        private int mEdgeTolerance = 1;
        private DpiScale? mMonitorDpi;
        private IntPtr mLastScreen;
        private WindowDockPosition mLastDock = WindowDockPosition.Undocked;

        public Rect CurrentScreenSize => mScreenSize;

        public Thickness CurrentMonitorMargin { get; private set; } = new Thickness();

        public Point GetCursorPosition()
        {
            // Get mouse position
            GetCursorPos(out POINT lMousePosition);

            // Apply DPI scaling
            return new Point(lMousePosition.X / mMonitorDpi.Value.DpiScaleX, lMousePosition.Y / mMonitorDpi.Value.DpiScaleY);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        public event Action<WindowDockPosition> WindowDockChanged = (dock) => { };

        public event Action WindowFinishedMove = () => { };

        public Rectangle CurrentMonitorSize { get; set; } = new Rectangle();

        public WindowResizer(System.Windows.Window window)
        {
            mWindow = window;
            mWindow.SourceInitialized += Window_SourceInitialized;
            mWindow.SizeChanged += Window_SizeChanged;
            mWindow.LocationChanged += Window_LocationChanged;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var handle = (new WindowInteropHelper(mWindow)).Handle;
            var handleSource = HwndSource.FromHwnd(handle);

            if (handleSource == null)
                return;

            handleSource.AddHook(WindowProc);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WmGetMinMaxInfo(IntPtr.Zero, IntPtr.Zero);

            mMonitorDpi = VisualTreeHelper.GetDpi(mWindow);

            if (mMonitorDpi == null)
                return;

            var top = mWindow.Top;
            var left = mWindow.Left;
            var bottom = top + mWindow.Height;
            var right = left + mWindow.Width;

            var windowTopLeft = new Point(left * mMonitorDpi.Value.DpiScaleX, top * mMonitorDpi.Value.DpiScaleX);
            var windowBottomRight = new Point(right * mMonitorDpi.Value.DpiScaleX, bottom * mMonitorDpi.Value.DpiScaleX);

            var edgedTop = windowTopLeft.Y <= (mScreenSize.Top + mEdgeTolerance) && windowTopLeft.Y >= (mScreenSize.Top - mEdgeTolerance);
            var edgedLeft = windowTopLeft.X <= (mScreenSize.Left + mEdgeTolerance) && windowTopLeft.X >= (mScreenSize.Left - mEdgeTolerance);
            var edgedBottom = windowBottomRight.Y >= (mScreenSize.Bottom - mEdgeTolerance) && windowBottomRight.Y <= (mScreenSize.Bottom + mEdgeTolerance);
            var edgedRight = windowBottomRight.X >= (mScreenSize.Right - mEdgeTolerance) && windowBottomRight.X <= (mScreenSize.Right + mEdgeTolerance);

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

            if (dock != mLastDock)
                WindowDockChanged(dock);

            // Save last dock position
            mLastDock = dock;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Window_SizeChanged(null, null);
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:/* WM_GETMINMAXINFO */
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
                case 0x0232: // WM_EXITSIZEMOVE
                    WindowFinishedMove();
                    break;
            }

            return (IntPtr)0;
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            GetCursorPos(out POINT lMousePosition);

            var lCurrentScreen = MonitorFromPoint(lMousePosition, MonitorOptions.MONITOR_DEFAULTTONEAREST);
            var lPrimaryScreen = MonitorFromPoint(new POINT(0, 0), MonitorOptions.MONITOR_DEFAULTTOPRIMARY);

            var lCurrentScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lCurrentScreen, lCurrentScreenInfo) == false)
                return;

            var lPrimaryScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
                return;

            //if (lCurrentScreen != mLastScreen || mMonitorDpi == null)
            mMonitorDpi = VisualTreeHelper.GetDpi(mWindow);

            mLastScreen = lCurrentScreen;

            var currentX = lCurrentScreenInfo.RCWork.Left - lCurrentScreenInfo.RCMonitor.Left;
            var currentY = lCurrentScreenInfo.RCWork.Top - lCurrentScreenInfo.RCMonitor.Top;
            var currentWidth = (lCurrentScreenInfo.RCWork.Right - lCurrentScreenInfo.RCWork.Left);
            var currentHeight = (lCurrentScreenInfo.RCWork.Bottom - lCurrentScreenInfo.RCWork.Top);
            var currentRatio = (float)currentWidth / (float)currentHeight;

            var primaryX = lPrimaryScreenInfo.RCWork.Left - lPrimaryScreenInfo.RCMonitor.Left;
            var primaryY = lPrimaryScreenInfo.RCWork.Top - lPrimaryScreenInfo.RCMonitor.Top;
            var primaryWidth = (lPrimaryScreenInfo.RCWork.Right - lPrimaryScreenInfo.RCWork.Left);
            var primaryHeight = (lPrimaryScreenInfo.RCWork.Bottom - lPrimaryScreenInfo.RCWork.Top);
            var primaryRatio = (float)primaryWidth / (float)primaryHeight;

            if (lParam != IntPtr.Zero)
            {
                var lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));


                // Set to primary monitor size
                lMmi.PointMaxPosition.X = lPrimaryScreenInfo.RCMonitor.Left;
                lMmi.PointMaxPosition.Y = lPrimaryScreenInfo.RCMonitor.Top;
                lMmi.PointMaxSize.X = lPrimaryScreenInfo.RCMonitor.Right;
                lMmi.PointMaxSize.Y = lPrimaryScreenInfo.RCMonitor.Bottom;

                // Set min size
                var minSize = new Point(mWindow.MinWidth * mMonitorDpi.Value.DpiScaleX, mWindow.MinHeight * mMonitorDpi.Value.DpiScaleX);
                lMmi.PointMinTrackSize.X = (int)minSize.X;
                lMmi.PointMinTrackSize.Y = (int)minSize.Y;

                // Now we have the max size, allow the host to tweak as needed
                Marshal.StructureToPtr(lMmi, lParam, true);
            }

            CurrentMonitorSize = new Rectangle(currentX, currentY, currentWidth + currentX, currentHeight + currentY);

            CurrentMonitorMargin = new Thickness(
                (lCurrentScreenInfo.RCMonitor.Left - lCurrentScreenInfo.RCWork.Left) / mMonitorDpi.Value.DpiScaleX,
                (lCurrentScreenInfo.RCMonitor.Top - lCurrentScreenInfo.RCWork.Top) / mMonitorDpi.Value.DpiScaleY,
                (lCurrentScreenInfo.RCMonitor.Right - lCurrentScreenInfo.RCWork.Right) / mMonitorDpi.Value.DpiScaleX,
                (lCurrentScreenInfo.RCMonitor.Bottom - lCurrentScreenInfo.RCWork.Bottom) / mMonitorDpi.Value.DpiScaleY
            );

            mScreenSize = new Rect(lCurrentScreenInfo.RCWork.Left, lCurrentScreenInfo.RCWork.Top, currentWidth, currentHeight);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MONITORINFO
    {
        public int CBSize = Marshal.SizeOf(typeof(MONITORINFO));
        public Rectangle RCMonitor = new Rectangle();
        public Rectangle RCWork = new Rectangle();
        public int DWFlags = 0;
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
    public struct MINMAXINFO
    {
        public POINT PointReserved;
        public POINT PointMaxSize;
        public POINT PointMaxPosition;
        public POINT PointMinTrackSize;
        public POINT PointMaxTrackSize;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
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