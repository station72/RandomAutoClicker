using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RandomAutoClicker.Infrastructure.Keyboard
{
    public class KeyboardListener : IKeyboardListener
    {
        private static IntPtr hookId = IntPtr.Zero;
        public event RawKeyEventHandler KeyDown;

        public KeyboardListener()
        {
            hookId = InterceptKeys.SetHook(HookCallback);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                return HookCallbackInner(nCode, wParam, lParam);
            }
            catch
            {
                Debug.Print("There was some error somewhere...");
            }
            return InterceptKeys.CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private IntPtr HookCallbackInner(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)InterceptKeys.WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);

                    KeyDown?.Invoke(this, new RawKeyEventArgs(vkCode, false));
                }
            }
            return InterceptKeys.CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        ~KeyboardListener()
        {
            ClenUp(false);
        }

        #region IDisposable Members

        private bool _disposed = false;
        public void Dispose()
        {
            ClenUp(true);
            GC.SuppressFinalize(this);
        }

        private void ClenUp(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    InterceptKeys.UnhookWindowsHookEx(hookId);
                }
            }

            _disposed = true;
        }

        #endregion
    }
}
