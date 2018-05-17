using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace RandomAutoClicker.Model
{
    public class WpfScreen
    {
        private readonly Screen _screen;

        //https://stackoverflow.com/questions/1927540/how-to-get-the-size-of-the-current-screen-in-wpf
        public static IEnumerable<WpfScreen> AllScreens()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                yield return new WpfScreen(screen);
            }
        }

        public static WpfScreen GetScreenFrom(Window window)
        {
            var windowInteropHelper = new WindowInteropHelper(window);
            var screen = Screen.FromHandle(windowInteropHelper.Handle);
            var wpfScreen = new WpfScreen(screen);
            return wpfScreen;
        }

        public static WpfScreen GetScreenFrom(System.Windows.Point point)
        {
            var x = (int)Math.Round(point.X);
            var y = (int)Math.Round(point.Y);

            var drawingPoint = new System.Drawing.Point(x, y);
            var screen = Screen.FromPoint(drawingPoint);
            var wpfScreen = new WpfScreen(screen);

            return wpfScreen;
        }

        public static WpfScreen Primary
        {
            get { return new WpfScreen(Screen.PrimaryScreen); }
        }

        internal WpfScreen(Screen screen)
        {
            _screen = screen;
        }

        public Rect DeviceBounds
        {
            get { return this.GetRect(_screen.Bounds); }
        }

        public Rect WorkingArea
        {
            get { return GetRect(_screen.WorkingArea); }
        }

        private Rect GetRect(Rectangle value)
        {
            return new Rect
            {
                X = value.X,
                Y = value.Y,
                Width = value.Width,
                Height = value.Height
            };
        }

        public bool IsPrimary
        {
            get { return _screen.Primary; }
        }

        public string DeviceName
        {
            get { return _screen.DeviceName; }
        }
    }
}
