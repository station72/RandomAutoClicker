using System;

namespace RandomAutoClicker.Infrastructure.Keyboard
{
    public interface IKeyboardManager : IDisposable
    {
        void StartHandleKeys();
    }
}
