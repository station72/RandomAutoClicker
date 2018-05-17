using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RandomAutoClicker.Model.Clicker.ClickBehaviour
{
    public abstract class ClickBehaviourBase
    {
        protected const int MOUSEEVENTF_LEFTDOWN = 0x02;
        protected const int MOUSEEVENTF_LEFTUP = 0x04;
        protected const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        protected const int MOUSEEVENTF_RIGHTTUP = 0x10;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Win32Point pt);

        public Point GetCursorPosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(Int32 X, Int32 Y);

        public bool SetCursorPosition(int x, int y)
        {
            return SetCursorPos(x, y);
        }

        [DllImport("user32.dll")]
        protected static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public abstract Task ClickAsync(int xPos, int yPos);
    }
}
