using System;
using System.Windows.Input;

namespace RandomAutoClicker.Infrastructure.Keyboard
{
    public class RawKeyEventArgs : EventArgs
    {
        public int VKCode;
        public Key Key;
        public bool IsSysKey;

        public RawKeyEventArgs(int VKCode, bool isSysKey)
        {
            this.VKCode = VKCode;
            IsSysKey = isSysKey;
            Key = KeyInterop.KeyFromVirtualKey(VKCode);
        }
    }
}
