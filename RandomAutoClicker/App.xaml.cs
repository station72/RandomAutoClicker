using RandomAutoClicker.Infrastructure;
using RandomAutoClicker.Infrastructure.Keyboard;
using System.Windows;

namespace RandomAutoClicker
{
    public partial class App : Application
    {
        private readonly IClickerContext _context;
        private readonly IKeyboardManager _keyboardManager;
        private readonly MainWindow _window;

        public App()
        {
            _context = new ClickerContext();
            var ioc = _context.IocContainer;

            _keyboardManager = ioc.Resolve<IKeyboardManager>();
            _window = ioc.Resolve<MainWindow>();

            _window.Show();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _keyboardManager.Dispose();

            var ioc = _context.IocContainer;
            ioc.Release(_keyboardManager);
            ioc.Release(_window);

            _context.Dispose();
        }
    }
}
