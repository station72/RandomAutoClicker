using System;

namespace RandomAutoClicker.Infrastructure
{
    public interface IViewDispatcher
    {
        void Invoke(Action callback);
    }
}
