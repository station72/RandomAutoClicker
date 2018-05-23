using Castle.Windsor;

namespace RandomAutoClicker.Infrastructure
{
    public class IocContainer : IIocContainer
    {
        private readonly IWindsorContainer _innerContainer;

        public IocContainer(IWindsorContainer innerContainer)
        {
            _innerContainer = innerContainer;
        }

        public void Dispose()
        {
            _innerContainer.Dispose();
        }

        public void Release(object instance)
        {
            _innerContainer.Release(instance);
        }

        public T Resolve<T>()
        {
            return _innerContainer.Resolve<T>();
        }
    }
}
