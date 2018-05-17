using RandomAutoClicker.Infrastructure.FSM;
using RandomAutoClicker.Infrastructure.Events;
using RandomAutoClicker.Infrastructure.Keyboard;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;

namespace RandomAutoClicker
{
    public partial class App : Application
    {
        private GCHandle _hnd;
        private MainWindow _window;
        private KeyboardListener _kListener;
        private RawKeyEventHandler _handler;
        private IFsm<ClickerEventArgs> _fsm;
        private IEventBroker<ClickerEventArgs> _eventBroker;

        public App()
        {
            _eventBroker = EventBroker<ClickerEventArgs>.Instance;
            _fsm = new Fsm(_eventBroker, new SubscribesContainer<ClickerEventArgs>(_eventBroker));

            _window = new MainWindow();
            _window.Show();
            _kListener = new KeyboardListener();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _handler = new RawKeyEventHandler(OnKeyDown);
            _kListener.KeyDown += _handler;
            _hnd = GCHandle.Alloc(_kListener);
        }

        void OnKeyDown(object sender, RawKeyEventArgs args)
        {
            Debug.Print("------------");
            Debug.Print("IsSysKey = " + args.IsSysKey);
            Debug.Print("Key = " + args.Key);
            Debug.Print("VkCode = " + args.VKCode);
            _eventBroker.Raise(EventNames.KeyPressed, new ClickerEventArgs
            {
                Data = args.Key,
                Sender = this
            });
            //_window.KeyDownEventHandler(args.Key);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _hnd.Free();
            _kListener.Dispose();
        }
    }

    public delegate void RawKeyEventHandler(object sender, RawKeyEventArgs args);
}
