using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RandomAutoClicker.Infrastructure.Keyboard
{
    public class KeyboardListener : IDisposable
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
            Dispose();
        }

        #region IDisposable Members

        public void Dispose()
        {
            InterceptKeys.UnhookWindowsHookEx(hookId);
        }

        #endregion
    }
}
