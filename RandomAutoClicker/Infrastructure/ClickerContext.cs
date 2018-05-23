using Castle.Windsor;
using Castle.Windsor.Installer;
using System;

namespace RandomAutoClicker.Infrastructure
{
    public sealed class ClickerContext : IClickerContext
    {
        private readonly static Object _lockObject = new Object();
        private static IIocContainer _iocContainer;

        static ClickerContext()
        {
            InitIocContainer();
        }

        private static void InitIocContainer()
        {
            var ioc = new WindsorContainer();
            ioc.Install(FromAssembly.This());

            _iocContainer = new IocContainer(ioc);
        }

        public IIocContainer IocContainer
        {
            get { return _iocContainer; }
        }


        public void Dispose()
        {
            _iocContainer.Dispose();
            _iocContainer = null;
        }
    }
}
