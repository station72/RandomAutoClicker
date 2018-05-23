using System;
using System.Windows;
using System.Windows.Threading;

namespace RandomAutoClicker.Infrastructure
{
    public class ViewDispatcher : IViewDispatcher
    {
        private readonly Dispatcher _targetDispatcher;
        public ViewDispatcher()
        {
            _targetDispatcher = Application.Current.Dispatcher;
        }

        public void Invoke(Action callback)
        {
            _targetDispatcher.Invoke(callback);
        }
    }
}
