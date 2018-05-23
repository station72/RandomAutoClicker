using System;

namespace RandomAutoClicker.Infrastructure
{
    public interface IClickerContext : IDisposable
    {
        IIocContainer IocContainer { get; }
    }
}
