using System;

namespace RandomAutoClicker.Infrastructure.Keyboard
{
    public interface IKeyboardListener : IDisposable
    {
        event RawKeyEventHandler KeyDown;
    }
}
