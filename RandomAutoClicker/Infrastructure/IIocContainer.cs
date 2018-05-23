using System;

namespace RandomAutoClicker.Infrastructure
{
    public interface IIocContainer : IDisposable
    {
        T Resolve<T>();

        void Release(object instance);
    }
}
