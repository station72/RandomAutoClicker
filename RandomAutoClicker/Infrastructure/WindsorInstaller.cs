using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RandomAutoClicker.Infrastructure.Events;
using RandomAutoClicker.Infrastructure.FSM;
using RandomAutoClicker.Infrastructure.Keyboard;
using RandomAutoClicker.ViewModel;
using System.Diagnostics;

namespace RandomAutoClicker.Infrastructure
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IEventBroker<ClickerEventArgs>>().ImplementedBy<EventBroker<ClickerEventArgs>>()
                .OnCreate((p) => Debug.Print($"Event broker = {p.GetHashCode().ToString()}" ))
                .LifestyleSingleton());

            container.Register(Component
                .For<IFsm<ClickerEventArgs>>().ImplementedBy<Fsm>()
                .LifestyleTransient());

            container.Register(Component
                .For<IKeyboardListener>().ImplementedBy<KeyboardListener>()
                .LifestyleSingleton());

            container.Register(Component
                .For<IKeyboardManager>().ImplementedBy<KeyboardManager>()
                .LifestyleSingleton());

            container.Register(Component
                .For<MainWindow>().ImplementedBy<MainWindow>()
                .LifeStyle.Transient);

            container.Register(Component
                .For<MainWindowViewModel>().ImplementedBy<MainWindowViewModel>()
                //.DependsOn(Dependency.OnValue(MainWindowViewModel.... , new object()))
                .LifeStyle.Transient);

            container.Register(Component
                .For<ISubscribesContainer<ClickerEventArgs>>().ImplementedBy<SubscribesContainer<ClickerEventArgs>>()
                .LifeStyle.Transient);

            container.Register(Component
                .For<IViewDispatcher>().ImplementedBy<ViewDispatcher>()
                .LifeStyle.Singleton);
        }
    }
}
