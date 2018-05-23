using System;
using System.Windows.Threading;

namespace RandomAutoClicker.Infrastructure
{
    public class ViewDispatcher : IViewDispatcher
    {
        private readonly Dispatcher _targetDispatcher;
        public ViewDispatcher(Dispatcher targetDispatcher)
        {
            _targetDispatcher = targetDispatcher;
        }

        public void Invoke(Action callback)
        {
            _targetDispatcher.Invoke(callback);
        }
    }
}
